using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Diagnostics;
using System.Collections;
using System.Data;

namespace Utils
{
    public class CTempSets:CollectionBase
    {
        public CTempSets() { }
        public float[] this[int index]
        {
            get { return (float[])List[index]; }
            set { List[index] = value; }
        }
        public int Add(float[] value) { return (List.Add(value)); }
        //public int Count { get { return List.Count; } }
    }

    public static class DataBase
    {
        private static string _pathDataBase;
        private static string _fileDB;

        static DataBase()
        {
            // 设置路径
            _pathDataBase = "DataBase";

            _fileDB = _pathDataBase + "/tempSet.mdb";

            if(!File.Exists(_fileDB))
            {
                Debug.WriteLine("数据库不存在！");
            }
        }


        /// <summary>
        /// 记录主槽参数
        /// </summary>
        /// <param name="tempSet"></param>
        /// <returns></returns>
        public static bool writeTempSetM(float[] tempSet)
        {
            // 数据长度必须为 7
            if (tempSet.Length != 7)
                return false;

            // 检查数据库文件是否存在
            if (!File.Exists(_fileDB))
            {
                Debug.WriteLine("数据库不存在！");
                return false;
            }

            // 连接数据库
            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _fileDB);
                conn.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                try { conn.Close(); } catch { }
                return false;
            }

            // SQL 插入语句
            try
            {
                OleDbCommand dele = new OleDbCommand("delete from TempSetM where tpSetM = " + tempSet[0].ToString(), conn);
                string strInsert = "INSERT INTO TempSetM(tpSetM, tpAdjustM, advanceM, fuzzyM, ratioM, integM, powerM ) VALUES(";
                for (int i = 0; i < tempSet.Length; i++)
                {
                    strInsert += tempSet[i].ToString();
                    if (i == tempSet.Length - 1)
                        strInsert += ")";
                    else
                        strInsert += ",";
                }
                OleDbCommand inst = new OleDbCommand(strInsert, conn);
                dele.ExecuteNonQuery();
                inst.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                try { conn.Close(); } catch { }
                return false;
            }


            return true;
        }

        /// <summary>
        /// 记录辅槽参数
        /// </summary>
        /// <param name="tempSet"></param>
        /// <returns></returns>
        public static bool writeTempSetS(float[] tempSet)
        {
            // 数据长度必须为 7
            if (tempSet.Length != 7)
                return false;

            // 检查数据库文件是否存在
            if (!File.Exists(_fileDB))
            {
                Debug.WriteLine("数据库不存在！");
                return false;
            }

            // 连接数据库
            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _fileDB);
                conn.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                try { conn.Close(); } catch { }
                return false;
            }

            // SQL 插入语句
            try
            {
                OleDbCommand dele = new OleDbCommand("delete from TempSetS where tpSetS = " + tempSet[0].ToString(), conn);
                string strInsert = "INSERT INTO TempSetS(tpSetS, tpAdjustS, advanceS, fuzzyS, ratioS, integS, powerS ) VALUES(";
                for (int i = 0; i < tempSet.Length; i++)
                {
                    strInsert += tempSet[i].ToString();
                    if (i == tempSet.Length - 1)
                        strInsert += ")";
                    else
                        strInsert += ",";
                }
                OleDbCommand inst = new OleDbCommand(strInsert, conn);
                dele.ExecuteNonQuery();
                inst.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                try { conn.Close(); } catch { }
                return false;
            }


            return true;
        }


        /// <summary>
        /// 从数据库中查找主槽参数集，返回 List
        /// 出现错误则返回 null
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static CTempSets checkTempSetM(float temp, float tolerance)
        {
            // 检查数据库文件是否存在
            if (!File.Exists(_fileDB))
            {
                Debug.WriteLine("数据库不存在！");
                return null;
            }

            // 连接数据库
            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _fileDB);
                conn.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                try { conn.Close(); } catch { }
                return null;
            }

            // 建立 SQL 查询指令
            OleDbDataAdapter oleDapAdapter = null;
            DataSet ds = null;
            try
            {
                string strCheck = "select * from TempSetM where tpSetM between ";
                strCheck += (temp - tolerance).ToString() + " and " + (temp + tolerance).ToString();

                oleDapAdapter = new OleDbDataAdapter(strCheck, conn);
                ds = new DataSet();
                oleDapAdapter.Fill(ds);
                conn.Close();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                try { conn.Close(); } catch { }
                return null;
            }

            // 从 DataSet 中解析数据
            CTempSets result = new CTempSets();
            try
            {
                foreach (DataRow mDr in ds.Tables[0].Rows)
                {
                    float[] tempset = new float[7];
                    for (int i = 1; i < ds.Tables[0].Columns.Count; i++)
                    {
                        
                        tempset[i-1] = (float)mDr[ds.Tables[0].Columns[i]];
                    }
                    result.Add(tempset);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return result;
        }

        /// <summary>
        /// 从数据库中查找辅槽参数集，返回 List
        /// 出现错误则返回 null
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static CTempSets checkTempSetS(float temp, float tolerance)
        {
            // 检查数据库文件是否存在
            if (!File.Exists(_fileDB))
            {
                Debug.WriteLine("数据库不存在！");
                return null;
            }

            // 连接数据库
            OleDbConnection conn = null;
            try
            {
                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _fileDB);
                conn.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                try { conn.Close(); } catch { }
                return null;
            }

            // 建立 SQL 查询指令
            OleDbDataAdapter oleDapAdapter = null;
            DataSet ds = null;
            try
            {
                string strCheck = "select * from TempSetS where tpSetS between ";
                strCheck += (temp - tolerance).ToString() + " and " + (temp + tolerance).ToString();

                oleDapAdapter = new OleDbDataAdapter(strCheck, conn);
                ds = new DataSet();
                oleDapAdapter.Fill(ds);
                conn.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                try { conn.Close(); } catch { }
                return null;
            }

            // 从 DataSet 中解析数据
            CTempSets result = new CTempSets();
            try
            {
                foreach (DataRow mDr in ds.Tables[0].Rows)
                {
                    float[] tempset = new float[7];
                    for (int i = 1; i < ds.Tables[0].Columns.Count; i++)
                    {

                        tempset[i - 1] = (float)mDr[ds.Tables[0].Columns[i]];
                    }
                    result.Add(tempset);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return result;
        }
    }
}
