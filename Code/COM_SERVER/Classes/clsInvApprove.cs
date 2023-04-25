using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HELLMANN_COM_SERVER.Classes
{
    class clsInvApprove
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
                DataTable dtInvoice = oManager.ExecuteDataSet(System.Data.CommandType.Text, "SELECT DISTINCT InvoiceNo FROM pPicklist WHERE (CancelStatus <> 13 OR CancelStatus IS NULL) AND InvoiceNo IS NOT NULL GROUP BY InvoiceNo HAVING SUM(ToPickQty)=SUM(PickedQty)").Tables[0];
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

                sb.AppendLine("SELECT p.OrderNo,d.Description,p.PklNo,StorageLocation AS Loc, MatCode, ToPickQty, PickedQty, ISNULL(AprvQty,0)AprvQty FROM pPicklist p inner join mDestination d on p.Destination=d.Code WHERE (InvoiceNo = '" + sInvoice + "')");
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
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        /// <param name="p_3"></param>
        /// <returns></returns>
        internal string fnDoCRInvoice(string sInvoice, string sPicklistNo, string sBarcode, string sUser)
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
                else if (oRuls.sResponse.Split('~')[1].ToString() == "")
                {
                    oRuls.sResponse = PickListStatus(sInvoice, oManager);//to check picklist is complete or not
                    if (oRuls.sResponse == "")
                    { oRuls.sResponse = clsMsgRule.sIComplete; return oRuls.sResponse; }
                    string sMatCode = GetMatCode(sBarcode, sPicklistNo, sInvoice, oManager);//to get item code and check mat code is exist in this picklist or not
                    if (sMatCode == "")
                    { oRuls.sResponse = clsMsgRule.sInValidMatPick; return oRuls.sResponse; }
                    else
                    {
                        oManager.Open();
                        oManager.BeginTransaction(oManager.Connection);
                        IDataReader rdPickList = oManager.ExecuteReader(CommandType.Text, "select top 1 Item,StorageLocation from pPicklist where InvoiceNo='" + sInvoice + "' and MatCode='" + sMatCode + "' and PickedQty> ISNULL(AprvQty, 0)");
                        while (rdPickList.Read())//To find top 1 Line item code and storage location from Picking to update picking table
                        { sLineItem = rdPickList[0].ToString(); sStrjLoc = rdPickList[1].ToString(); }
                        rdPickList.Close();

                        //commented by Santosh on 22-04-2014 for this DeliveryNo =(isnull(DeliveryNo,'') + ',' + )
                        //oManager.ExecuteNonQuery(CommandType.Text, "Update tMovement set Slot='" + sInvoice + "',DeliveryNo = isnull(DeliveryNo,'') + ',' + '" + sInvoice + "',RequestedBy='" + sUser + "',RequestedOn=GetDate() where Barcode='" + sBarcode + "' AND Status=5" + ";" + "Update pPicklist set AprvQty=isnull( AprvQty,0)+1,CancelStatus=2 where PklNo='" + sPicklistNo + "' and MatCode='" + sMatCode + "' and Item='" + sLineItem + "' and StorageLocation='" + sStrjLoc + "' and PickedQty>=ISNULL(AprvQty,0)");
                        oManager.ExecuteNonQuery(CommandType.Text, "Update tMovement set Slot='" + sInvoice + "',DeliveryNo = '" + sInvoice + "',RequestedBy='" + sUser + "',RequestedOn=GetDate() where Barcode='" + sBarcode + "' AND Status=5" + ";" + "Update pPicklist set AprvQty=isnull( AprvQty,0)+1,CancelStatus=2 where PklNo='" + sPicklistNo + "' and MatCode='" + sMatCode + "' and Item='" + sLineItem + "' and StorageLocation='" + sStrjLoc + "' and PickedQty>=ISNULL(AprvQty,0)");
                        DataTable dtQty = oManager.ExecuteDataSet(CommandType.Text, "SELECT SUM(ToPickQty)ToPickQty, SUM(PickedQty)PickedQty, ISNULL(SUM(AprvQty),0)AprvQty FROM pPicklist WHERE InvoiceNo = '" + sInvoice + "' AND MatCode='" + sMatCode + "'").Tables[0];
                        if (dtQty.Rows.Count != 0)
                        {
                            if (dtQty.Rows[0][1].ToString() == dtQty.Rows[0][2].ToString())
                            {
                                //oManager.ExecuteNonQuery(CommandType.Text, "Update pPicklist set CancelStatus=13 where InvoiceNo='" + sInvoice + "' AND MatCode='" + sMatCode + "'");
                                oRuls.sResponse = clsMsgRule.sIPikComplete + "~" + dtQty.Rows[0][0].ToString() + "~" + dtQty.Rows[0][1].ToString() + "~" + dtQty.Rows[0][2].ToString() + "~" + sMatCode + "~" + sStrjLoc;
                                DataTable dtTQty = oManager.ExecuteDataSet(CommandType.Text, "SELECT SUM(ToPickQty)ToPickQty, SUM(PickedQty)PickedQty, ISNULL(SUM(AprvQty),0)AprvQty FROM pPicklist WHERE InvoiceNo = '" + sInvoice + "'").Tables[0];
                                if (dtTQty.Rows[0][1].ToString() == dtTQty.Rows[0][2].ToString())
                                {
                                    //oManager.ExecuteNonQuery(CommandType.Text, "Update pPicklist set CancelStatus=13 where InvoiceNo='" + sInvoice + "'");
                                    oRuls.sResponse = clsMsgRule.sIComplete; 
                                }
                            }
                            else
                            {
                                DataTable dtScanned = oManager.ExecuteDataSet(CommandType.Text, "SELECT SUM(ToPickQty)ToPickQty, SUM(PickedQty)PickedQty, ISNULL(SUM(AprvQty),0)AprvQty FROM pPicklist WHERE MatCode='" + sMatCode + "' and InvoiceNo = '" + sInvoice + "'").Tables[0];
                                oRuls.sResponse = clsMsgRule.sValid + "~" + dtScanned.Rows[0][0].ToString() + "~" + dtScanned.Rows[0][1].ToString() + "~" + dtScanned.Rows[0][2].ToString() + "~" + sMatCode + "~" + sStrjLoc;
                            }
                        }
                        oManager.CommitTransaction();
                    }
                }
                else
                { oRuls.sResponse = clsMsgRule.sCCScanned; }
            }
            catch (Exception ex)
            { oManager.RollBackTransaction(); oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString(); }
            finally
            { oManager.Close(); }
            return oRuls.sResponse;
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
                reader = oManager.ExecuteReader(System.Data.CommandType.Text, "SELECT Distinct PklNo FROM pPicklist WHERE InvoiceNo='" + sInvoice + "' GROUP BY PklNo HAVING SUM(PickedQty)> ISNULL(SUM(AprvQty),0)");
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
                reader = oManager.ExecuteReader(System.Data.CommandType.Text, "SELECT COUNT(*)TRow,Slot FROM tMovement WHERE BarCode='" + sBarcode + "' AND PicklistNo='" + sPicklistNo + "'  GROUP BY Slot");
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
                reader = oManager.ExecuteReader(System.Data.CommandType.Text, "Select distinct * from(select MatCode from pPicklist where InvoiceNo ='" + sInvoice + "')P inner join (select MatCode from tMovement where  BarCode='" + sBarcode + "' or OuterBox='" + sBarcode + "' AND PicklistNo='" + sPicklistNo + "')M ON P.MatCode=M.MatCode");
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
