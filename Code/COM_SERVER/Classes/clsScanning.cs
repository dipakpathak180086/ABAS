using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SatoLib;
using System.Data.SqlClient;

namespace Classes
{
   
    class clsScanning
    {
        clsMsgRule oRuls = new clsMsgRule();
        StringBuilder sb = null;

        SqlHelper _SqlHelper = new SqlHelper();
        #region MyFuncation
        /// <summary>
        /// Execute Operation 
        /// </summary>
        /// <returns></returns>
        public string  Scanning_ExecuteTask(string itemCode, string groupNo, string deviceNo)
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[5];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = "SCAN";
                param[1] = new SqlParameter("@BARCODE", SqlDbType.VarChar, 100);
                param[1].Value = itemCode;
                param[2] = new SqlParameter("@GROUP", SqlDbType.VarChar, 50);
                param[2].Value = groupNo;
                param[3] = new SqlParameter("@DEVICE", SqlDbType.VarChar, 50);
                param[3].Value = deviceNo;
                param[4] = new SqlParameter("@CREATED_BY", SqlDbType.VarChar, 50);
                param[4].Value = "ADMIN";
                DataTable dt = _SqlHelper.ExecuteDataset(clsSetting.sCon, CommandType.StoredProcedure, "[PRC_SCANNING]", param).Tables[0];
                string[] msg = dt.Rows[0][0].ToString().Split('~');
                if (msg[0] == "VALID")
                { oRuls.sResponse = clsMsgRule.sValid + "~" + msg[1]; }
                else
                { oRuls.sResponse = clsMsgRule.sInValid + "~" + msg[1]; }
            }
            catch (Exception ex)
            {
                oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString();
            }
            finally
            {
                
            }
            return oRuls.sResponse;
        }
        public string DtToString(DataTable dt)
        {
            string sRow = string.Empty;
            string sDTString = string.Empty;
            if (dt.Rows.Count != 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sCol = string.Empty;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sCol = sCol + dt.Rows[i][j].ToString() + "$";
                    }
                    sRow = sRow + sCol.Substring(0, sCol.Length - 1) + "#";
                }
                sDTString = sRow.Substring(0, sRow.Length - 1);
            }
            return sDTString;
        }

        public string Select_ExecuteTask()
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[5];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = "SELECT";
                DataTable dt = _SqlHelper.ExecuteDataset(clsSetting.sCon, CommandType.StoredProcedure, "[PRC_SCANNING]", param).Tables[0];
                if (dt.Rows.Count > 0)
                { oRuls.sResponse = clsMsgRule.sValid + "~" + DtToString(dt); }
                else
                { oRuls.sResponse = clsMsgRule.sInValid + "~" + DtToString(dt); }
            }
            catch (Exception ex)
            {
                oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString();
            }
            finally
            {

            }
            return oRuls.sResponse;
        }
        public string Select_Time_ExecuteTask()
        {
            _SqlHelper = new SqlHelper();
            try
            {
                SqlParameter[] param = new SqlParameter[5];

                param[0] = new SqlParameter("@TYPE", SqlDbType.VarChar, 100);
                param[0].Value = "TIME";
                DataTable dt = _SqlHelper.ExecuteDataset(clsSetting.sCon, CommandType.StoredProcedure, "[PRC_SCANNING]", param).Tables[0];
                string[] msg = dt.Rows[0][0].ToString().Split('~');
                if (msg[0] == "VALID")
                { oRuls.sResponse = clsMsgRule.sValid + "~" + msg[1]; }
                else
                { oRuls.sResponse = clsMsgRule.sInValid + "~" + msg[1]; }
            }
            catch (Exception ex)
            {
                oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString();
            }
            finally
            {

            }
            return oRuls.sResponse;
        }

        #endregion

    }
}
