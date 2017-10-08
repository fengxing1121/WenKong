using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;

namespace Device
{
    public class RelayDevice
    {
        #region Device Update Event
        public class RelayDeviceEventArgs :EventArgs
        {
            public RelayDeviceEventArgs( int para_index, bool sucess)
            {
                index = para_index; suc = sucess;
            }
            private int index;
            private bool suc;

            public int Index { get { return index; } }
            public bool Sucess { get { return suc; } }
        }

        public event EventHandler<RelayDeviceEventArgs> DeviceUpdate;
        #endregion

        public enum Paras_r
        {
            Elect = 0,
            MainHeat,
            MainCoolF,
            SubHeat,
            SubCoolF,
            SubCool,
            WaterIn,
            SubCircle,
            WaterOut
        };

        #region Protocol
        private RelayProtocol ryProtocol = new RelayProtocol();
        #endregion

        #region Device Members
        // 线程安全
        private object locker = new object();

        // Device value
        private DateTime ctrlStartTime;
        public bool[] paraValues = { false, false, false, false, false, false, false, false, false };
        public string[] paraChNames =
            { "总电源", "主槽控温", "辅槽控温", "辅槽制冷  ", "辅槽循环  ", "主槽快冷  ", "辅槽快冷  ", "海水进", "海水出  " };

        // Device Name
        private string deviceName = string.Empty;
        private string portName = string.Empty;
        #endregion

        #region Public Methods
        public bool setParam(Paras_r param, bool status)
        {
            Err_t err = Err_t.NoError;
            lock(locker)
            {
                err = ryProtocol.SetRelay((RelayProtocol.Cmd_r)param, status);
                if (err == Err_t.NoError)
                    paraValues[(int)param] = status;
            }

            // 触发 Device Update Event
            RelayDeviceEventArgs ev = new RelayDeviceEventArgs((int)param, err == Err_t.NoError);
            OnRaiseDeviceUpdateEvent(ev);

            return (err == Err_t.NoError);
        }
        #endregion

        #region Private Method
        private void OnRaiseDeviceUpdateEvent(RelayDeviceEventArgs e)
        {
            EventHandler<RelayDeviceEventArgs> handler = DeviceUpdate;
            if(handler !=null)
            {
                handler(this, e);
            }
        }
        #endregion
    }
}
