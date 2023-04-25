using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HELLMANN_COM_SERVER.Classes
{
    class clsInvCancel
    {
        /// <summary>
        /// 
        /// </summary>
        clsMsgRule oRuls = new clsMsgRule();
        /// <summary>
        /// 
        /// </summary>
        clsMain oMain = new clsMain();
        /// <summary>
        /// 
        /// </summary>
        StringBuilder sb;
        /// <summary>
        /// Get Picklist No. to do picking
        /// </summary>
        /// <returns></returns>
        internal string fnInvoice()
        {
            IDBManager oManager = oMain.DBProvider();
            try
            {
                oManager.Open();
                DataTable dtInvoice = oManager.ExecuteDataSet(System.Data.CommandType.Text, "SELECT DISTINCT InvoiceNo FROM pPicklist WHERE (ISNULL(CancelStatus,1) = 15) GROUP BY InvoiceNo HAVING SUM(ToPickQty)=SUM(PickedQty) AND SUM(PickedQty)=SUM(AprvQty)").Tables[0];
                if (dtInvoice.Rows.Count != 0)
                { oRuls.sResponse = clsMsgRule.sValid + "~" + oMain.DtToString(dtInvoice); }
                else
                { oRuls.sResponse = clsMsgRule.sInValid; }
            }
            catch (Exception ex)
            { oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString(); }
            finally
            { oManager.Close(); }
            return oRuls.sResponse;
        }
        /// <summary>
        /// To find one picklist details
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal string fnInvoiceDtls(string sInvoice)
        {
            IDBManager oManager = oMain.DBProvider();
            try
            {
                sb = new StringBuilder();
                sb.AppendLine("SELECT p.OrderNo,d.Description,p.PklNo,StorageLocation AS Loc, MatCode, PickedQty, ISNULL(AprvQty,0)AprvQty,ISNULL(CnclQty,0)CnclQty FROM pPicklist p inner join mDestination d on p.Destination=d.Code WHERE (InvoiceNo = '" + sInvoice + "')");
                oManager.Open();
                DataTable dtInvoice = oManager.ExecuteDataSet(System.Data.CommandType.Text, sb.ToString()).Tables[0];
                if (dtInvoice.Rows.Count != 0)
                { oRuls.sResponse = clsMsgRule.sValid + "~" + oMain.DtToString(dtInvoice); }
                else
                { oRuls.sResponse = clsMsgRule.sInValid; }
            }
            catch (Exception ex)
            { oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString(); }
            finally
            { oManager.Close(); }
            return oRuls.sResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sInvoice"></param>
        /// <returns></returns>
        internal string fnLocDtls(string sInvoice,string sLoc)
        {
            IDBManager oManager = oMain.DBProvider();
            try
            {
                sb = new StringBuilder();
                sb.AppendLine("SELECT A.LocCode,B.StorageLocation FROM ");
                sb.AppendLine("(");
                sb.AppendLine("	SELECT LocCode FROM mSubStorageLocation WHERE Description='" + sLoc + "'");
                sb.AppendLine(")A LEFT JOIN ");
                sb.AppendLine("(");
                sb.AppendLine("	SELECT StorageLocation FROM pPicklist WHERE InvoiceNo='" + sInvoice + "'");
                sb.AppendLine(")B ON A.LocCode=B.StorageLocation");
                oManager.Open();
                DataTable dtLoc = oManager.ExecuteDataSet(System.Data.CommandType.Text, sb.ToString()).Tables[0];
                if (dtLoc.Rows.Count != 0)
                { oRuls.sResponse = clsMsgRule.sValid + "~" + dtLoc.Rows[0][0].ToString() + "~" + dtLoc.Rows[0][1].ToString(); }
                else
                { oRuls.sResponse = clsMsgRule.sInValid; }
            }
            catch (Exception ex)
            { oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString(); }
            finally
            { oManager.Close(); }
            return oRuls.sResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <param name="p_3"></param>
        /// <returns></returns>
        internal string fnDoCRInvoice(string sInvoice, string sPicklistNo, string sBarcode, string sUser, string sLocation)
        {
            string sFindBarcode = string.Empty;
            string sBoxPicklistNo = string.Empty;
            string sLineItem = string.Empty;
            string sStrjLoc = string.Empty;
            IDBManager oManager = oMain.DBProvider();
            try
            {
                oRuls.sResponse = BoxStatus(sBarcode, sPicklistNo, oManager);//to check box status
                if (oRuls.sResponse == "")
                { oRuls.sResponse = "INVALID"; return oRuls.sResponse; }
                else if (oRuls.sResponse.Split('~')[1].ToString() != sInvoice)
                { oRuls.sResponse = clsMsgRule.sInValidInv; return oRuls.sResponse; }
                else if (oRuls.sResponse.Split('~')[1].ToString() == sInvoice && oRuls.sResponse.Split('~')[0].ToString() == "PIK")
                {
                    oRuls.sResponse = PickListStatus(sInvoice, oManager);//to check picklist is complete or not
                    if (oRuls.sResponse == "")
                    { oRuls.sResponse = clsMsgRule.sIComplete; return oRuls.sResponse; }
                    sStrjLoc = StrjLoc(sInvoice, sLocation, oManager);//To check scanned location invoice loc same and check qty
                    if (sStrjLoc == "")
                    { oRuls.sResponse = "LOC NOT EXIST"; return oRuls.sResponse; }
                    else if (sStrjLoc.Split('~')[0].ToString() == sStrjLoc.Split('~')[1].ToString())
                    { oRuls.sResponse = "SCAN CMPLT IN LOC"; return oRuls.sResponse; }
                    string sMatCode = GetMatCode(sBarcode, sPicklistNo, sInvoice, oManager);//to get item code and check mat code is exist in this picklist or not
                    if (sMatCode == "")
                    { oRuls.sResponse = clsMsgRule.sInValidMatPick; return oRuls.sResponse; }
                    else
                    {
                        oManager.Open();
                        oManager.BeginTransaction(oManager.Connection);
                        IDataReader rdPickList = oManager.ExecuteReader(CommandType.Text, "select top 1 Item,StorageLocation from pPicklist where InvoiceNo='" + sInvoice + "' and MatCode='" + sMatCode + "' and ISNULL(AprvQty, 0)>ISNULL(CnclQty, 0) ");
                        while (rdPickList.Read())//To find top 1 Line item code and storage location from Picking to update picking table
                        { sLineItem = rdPickList[0].ToString(); sStrjLoc = rdPickList[1].ToString(); }
                        rdPickList.Close();
                        oManager.ExecuteNonQuery(CommandType.Text, "Update tMovement set SubLoc='" + sLocation + "',Status=4,PicklistNo='',CancelledBy='" + sUser + "',CancelleddOn=GetDate() where Barcode='" + sBarcode + "' AND Status=5" + ";" + "Update pPicklist set CnclQty=isnull( CnclQty,0)+1 where PklNo='" + sPicklistNo + "' and MatCode='" + sMatCode + "' and Item='" + sLineItem + "' and StorageLocation='" + sStrjLoc + "' and ISNULL(AprvQty, 0)>ISNULL(CnclQty, 0)");
                        oManager.ExecuteNonQuery(CommandType.Text, "update S set S.PendingQty = isnull(S.PendingQty,0) - 1,S.Status = case when S.PendingQty = 0 then 1 else 2 end from pSalesOrderDetails S inner join pPicklist P on S.OrderNo = P.OrderNo and S.MatCode = P.MatCode and S.Item = P.Item and S.StorageLocation = S.StorageLocation and S.PlantCode = P.PlantCode where  P.PklNo = '" + sPicklistNo + "' and S.MatCode = '" + sMatCode + "' and S.Item = '" + sLineItem + "' and P.StorageLocation='" + sStrjLoc + "'");

                        DataTable dtQty = oManager.ExecuteDataSet(CommandType.Text, "SELECT SUM(ToPickQty)ToPickQty, SUM(PickedQty)PickedQty, ISNULL(SUM(AprvQty),0)AprvQty, ISNULL(SUM(CnclQty),0)CnclQty  FROM pPicklist WHERE InvoiceNo = '" + sInvoice + "' AND MatCode='" + sMatCode + "'").Tables[0];
                        if (dtQty.Rows.Count != 0)
                        {
                            if (dtQty.Rows[0][1].ToString() == dtQty.Rows[0][2].ToString() && dtQty.Rows[0][2].ToString() == dtQty.Rows[0][3].ToString())
                            {
                                oManager.ExecuteNonQuery(CommandType.Text, "Update pPicklist set CancelStatus=16 where InvoiceNo='" + sInvoice + "' AND MatCode='" + sMatCode + "'");
                                oRuls.sResponse = clsMsgRule.sIPikComplete + "~" + dtQty.Rows[0][1].ToString() + "~" + dtQty.Rows[0][2].ToString() + "~" + dtQty.Rows[0][3].ToString() + "~" + sMatCode + "~" + sStrjLoc;
                                DataTable dtTQty = oManager.ExecuteDataSet(CommandType.Text, "SELECT SUM(ToPickQty)ToPickQty, SUM(PickedQty)PickedQty, ISNULL(SUM(AprvQty),0)AprvQty, ISNULL(SUM(CnclQty),0)CnclQty FROM pPicklist WHERE InvoiceNo = '" + sInvoice + "'").Tables[0];
                                if (dtTQty.Rows[0][1].ToString() == dtTQty.Rows[0][2].ToString() && dtTQty.Rows[0][2].ToString() == dtTQty.Rows[0][3].ToString())
                                {
                                    oManager.ExecuteNonQuery(CommandType.Text, "Update pPicklist set CancelStatus=16 where InvoiceNo='" + sInvoice + "'");
                                    oManager.ExecuteNonQuery(CommandType.Text, "update S set S.Status = 1 FROM pSalesOrderDetails S inner join pPicklist P on S.OrderNo = P.OrderNo and S.MatCode = P.MatCode and S.Item = P.Item and S.StorageLocation = S.StorageLocation and S.PlantCode = P.PlantCode where P.PklNo = '" + sPicklistNo + "' and S.PendingQty=0");
                                     
                                    oRuls.sResponse = clsMsgRule.sIComplete; 
                                }
                            }
                            else
                            {
                                DataTable dtScanned = oManager.ExecuteDataSet(CommandType.Text, "SELECT SUM(ToPickQty)ToPickQty, SUM(PickedQty)PickedQty, ISNULL(SUM(AprvQty),0)AprvQty, ISNULL(SUM(CnclQty),0)CnclQty FROM pPicklist WHERE MatCode='" + sMatCode + "' and InvoiceNo = '" + sInvoice + "'").Tables[0];
                                oRuls.sResponse = clsMsgRule.sValid + "~" + dtScanned.Rows[0][1].ToString() + "~" + dtScanned.Rows[0][2].ToString() + "~" + dtScanned.Rows[0][3].ToString() + "~" + sMatCode + "~" + sStrjLoc;
                            }
                        }
                        oManager.CommitTransaction();
                    }
                }
                else
                { oRuls.sResponse = oRuls.sResponse.Split('~')[0].ToString(); }
            }
            catch (Exception ex)
            { oManager.RollBackTransaction(); oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString(); }
            finally
            { oManager.Close(); }
            return oRuls.sResponse;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="sInvoice"></param>
        /// <param name="sLocation"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private string StrjLoc(string sInvoice, string sLocation, IDBManager oManager)
        {
            string sStrjLoc = string.Empty;
            IDataReader reader = null;
            try
            {
                sb = new StringBuilder();
                sb.AppendLine("SELECT A.PickedQty,ISNULL(A.CnclQty,0)CnclQty,A.StorageLocation FROM");
                sb.AppendLine("(");
                sb.AppendLine("	SELECT StorageLocation,Sum(ISNULL(PickedQty,0))PickedQty,Sum(ISNULL(CnclQty,0))CnclQty FROM pPicklist  WHERE InvoiceNo='" + sInvoice + "' group by StorageLocation");
                sb.AppendLine(")A INNER JOIN");
                sb.AppendLine("(");
                sb.AppendLine("	SELECT LocCode FROM mSubStorageLocation WHERE Description='" + sLocation + "'");
                sb.AppendLine(")B ON A.StorageLocation=B.LocCode");
                oManager.Open();
                //reader = oManager.ExecuteReader(System.Data.CommandType.Text, "SELECT Distinct PklNo FROM pPicklist WHERE (Status = 3) and PklNo='" + sPicklistNo + "' and MatCode='" + sMatCode + "'");
                reader = oManager.ExecuteReader(System.Data.CommandType.Text, sb.ToString());
                while (reader.Read())
                { sStrjLoc = reader[0].ToString() + "~" + reader[1].ToString() + "~" + reader[2].ToString(); }
            }
            catch (Exception ex)
            { oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString(); }
            finally
            { reader.Close(); oManager.Close(); }
            return sStrjLoc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sPicklistNo"></param>
        /// <param name="sMatCode"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private string ItemStatus(string sPicklistNo, string sMatCode, IDBManager oManager)
        {
            IDataReader reader=null;
            try
            {
                oRuls.sResponse = string.Empty;
                oManager.Open();
                reader = oManager.ExecuteReader(System.Data.CommandType.Text, "SELECT Distinct PklNo FROM pPicklist WHERE (Status = 3) and PklNo='" + sPicklistNo + "' and MatCode='" + sMatCode + "'");
                while (reader.Read())
                { oRuls.sResponse = reader[0].ToString(); }
            }
            catch (Exception ex)
            { oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString(); }
            finally
            { reader.Close(); oManager.Close(); }
            return oRuls.sResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sPicklistNo"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private string PickListStatus(string sInvoice, IDBManager oManager)
        {
            IDataReader reader=null;
            try
            {
                oRuls.sResponse = string.Empty;
                oManager.Open();
                reader = oManager.ExecuteReader(System.Data.CommandType.Text, "SELECT Distinct PklNo FROM pPicklist WHERE (ISNULL(CancelStatus,1) = 15) and InvoiceNo='" + sInvoice + "'");
                while (reader.Read())
                { oRuls.sResponse = reader[0].ToString(); }
            }
            catch (Exception ex)
            { oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString(); }
            finally
            { reader.Close(); oManager.Close(); }
            return oRuls.sResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sBarcode"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private string BoxStatus(string sBarcode,string sPicklistNo, IDBManager oManager)
        {
            IDataReader reader=null;
            try
            {
                oRuls.sResponse = string.Empty;
                oManager.Open();
                reader = oManager.ExecuteReader(System.Data.CommandType.Text, "SELECT s.Name ,Slot FROM tMovement t INNER JOIN mStatus S ON t.Status=s.Code WHERE (BarCode='" + sBarcode + "' or OuterBox='" + sBarcode + "') AND PicklistNo='" + sPicklistNo + "'");
                while (reader.Read())
                { oRuls.sResponse = reader[0].ToString() + "~" + reader[1].ToString(); }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { reader.Close(); oManager.Close(); }
            return oRuls.sResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sBarcode"></param>
        /// <param name="sPicklistNo"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private string GetMatCode(string sBarcode, string sPicklistNo, string sInvoice, IDBManager oManager)
        {
            IDataReader reader=null;
            try
            {
                oRuls.sResponse = string.Empty;
                oManager.Open();
                reader = oManager.ExecuteReader(System.Data.CommandType.Text, "Select distinct * from(select MatCode from pPicklist where InvoiceNo ='" + sInvoice + "')P inner join (select MatCode from tMovement where Status=5 AND (BarCode='" + sBarcode + "' or OuterBox='" + sBarcode + "') AND PicklistNo='" + sPicklistNo + "')M ON P.MatCode=M.MatCode");
                while (reader.Read())
                { oRuls.sResponse = reader[0].ToString(); }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { reader.Close(); oManager.Close(); }
            return oRuls.sResponse;
        }
    }
}
