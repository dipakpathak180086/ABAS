using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace HELLMANN_COM_SERVER.Classes
{
    public class clsCycleCountNew
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
        internal string fnFindCCNo()
        {
            IDBManager oManager = oMain.DBProvider();
            try
            {
                oManager.Open();
                DataTable dtCCNo = oManager.ExecuteDataSet(System.Data.CommandType.Text, "select DISTINCT CntNo FROM pCycleCount WHERE Status NOT IN (9,11,12)").Tables[0];//New Code 12-march-2014
                if (dtCCNo.Rows.Count != 0)
                {
                    DataTable dtOrderType = oManager.ExecuteDataSet(System.Data.CommandType.Text, "SELECT DISTINCT GI.OrderNo FROM  pGateIn GI INNER JOIN pPurchaseOrderHeader POH   ON GI.OrderNo=POH.OrderNo  WHERE GI.GateInNo='" + dtCCNo.Rows[0]["CntNo"].ToString() + "' AND POH.DocumentType ='ZSIL'").Tables[0];//New Code 12-march-2014
                    if (dtOrderType.Rows.Count > 0)
                    {
                        oRuls.sResponse = clsMsgRule.sInValid;
                    }
                    else
                    {
                        oRuls.sResponse = clsMsgRule.sValid + "~" + oMain.DtToString(dtCCNo);
                    }
                }
                else
                {
                    oRuls.sResponse = clsMsgRule.sInValid;
                }
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
        internal string fnFindCCDtls(string CCNo)
        {
            IDBManager oManager = oMain.DBProvider();
            try
            {
                oManager.Open();
                DataTable dtLoc = oManager.ExecuteDataSet(CommandType.Text, "SELECT Distinct Code+'|'+Description FROM mStorageLocation WHERE CCNo='" + CCNo + "'").Tables[0];
                if (dtLoc.Rows.Count != 0)
                { oRuls.sResponse = clsMsgRule.sValid + "~" + oMain.DtToString(dtLoc); }
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
        /// <param name="sCCNo"></param>
        /// <param name="sLocation"></param>
        /// <returns></returns>
        internal string fnFindLocDtls(string sCCNo, string sLocation)
        {
            IDBManager oManager = oMain.DBProvider();
            try
            {
                sLocation = sLocation.Split('|')[0].ToString();
                oManager.Open();
                string lPONumber = string.Empty;

                if (clsSetting.gABColorPlant == "ABColor")
                {
                    //DataTable dtPlant = oManager.ExecuteDataSet(CommandType.Text, "SELECT plantcode FROM pCycleCount WHERE  CntNo='" + sCCNo + "' ").Tables[0];
                    DataTable dtCycleCountNo = oManager.ExecuteDataSet(CommandType.Text, "SELECT DISTINCT GI.OrderNo FROM pCycleCount CC  INNER JOIN pGateIn GI ON GI.GateInNo=CC.CntNo AND GI.PlantCode =CC.PlantCode where ISNULL(GI.SubPlant,'') = 'ABColor' and CC.CntNo='" + sCCNo + "'").Tables[0];//New Code 12-march-2014
                    if (dtCycleCountNo.Rows.Count > 0)
                    {
                        lPONumber = dtCycleCountNo.Rows[0]["OrderNo"].ToString();//New Code added by Santosh 12-march-2014
                        DataTable dtCCTQty = oManager.ExecuteDataSet(CommandType.Text, "SELECT SUM(CCTQty)CCTQty,SUM(CCScanedQty)CCSQty FROM mStorageLocation WHERE  Code='" + sLocation + "' AND CCNo='" + sCCNo + "'").Tables[0];
                        DataTable dtSubLoc = oManager.ExecuteDataSet(CommandType.Text, "SELECT Description FROM mSubStorageLocation where LocCode= '" + sLocation + "'").Tables[0];
                        DataTable dtCamera = oManager.ExecuteDataSet(CommandType.Text, "SELECT DISTINCT G.MatCode FROM pGateIn G INNER JOIN mMaterial M ON G.MatCode=M.MatCode INNER JOIN mStorageLocation S ON G.StorageLocation=S.Code AND S.CCNo='" + sCCNo + "' AND OrderNo='" + lPONumber + "' AND M.Division='80' AND G.StorageLocation='" + sLocation + "' AND G.Status NOT IN(3,12,26,27) AND M.SerialProfiler<>'' ORDER BY G.MatCode").Tables[0];
                        DataTable dtCCDQty = oManager.ExecuteDataSet(CommandType.Text, "EXECUTE CC_DETAILS_NEW_SP '" + sCCNo + "','" + sLocation + "','" + lPONumber + "'").Tables[0];
                        if (dtCCDQty.Rows.Count != 0)
                        { oRuls.sResponse = clsMsgRule.sValid + "~" + dtCCTQty.Rows[0][0].ToString() + "~" + dtCCTQty.Rows[0][1].ToString() + "~" + oMain.DtToString(dtCCDQty) + "~" + oMain.DtToString(dtCamera) + "~" + oMain.DtToString(dtSubLoc); }
                        else
                        { oRuls.sResponse = clsMsgRule.sInValid; }
                    }

                }
                else
                {
                    //DataTable dtPlant = oManager.ExecuteDataSet(CommandType.Text, "SELECT plantcode FROM pCycleCount WHERE  CntNo='" + sCCNo + "' ").Tables[0];
                    DataTable dtCycleCountNo = oManager.ExecuteDataSet(CommandType.Text, "SELECT DISTINCT GI.OrderNo FROM pCycleCount CC  INNER JOIN pGateIn GI ON GI.GateInNo=CC.CntNo AND GI.PlantCode =CC.PlantCode where ISNULL(GI.SubPlant,'') !='ABColor' and CC.CntNo='" + sCCNo + "'").Tables[0];//New Code 12-march-2014
                    //lPONumber = clsSetting.sPlant + "OLDSTK";
                    if (dtCycleCountNo.Rows.Count > 0)
                    {
                        lPONumber = dtCycleCountNo.Rows[0]["OrderNo"].ToString();//New Code added by Santosh 12-march-2014
                        DataTable dtCCTQty = oManager.ExecuteDataSet(CommandType.Text, "SELECT SUM(CCTQty)CCTQty,SUM(CCScanedQty)CCSQty FROM mStorageLocation WHERE  Code='" + sLocation + "' AND CCNo='" + sCCNo + "'").Tables[0];
                        DataTable dtSubLoc = oManager.ExecuteDataSet(CommandType.Text, "SELECT Description FROM mSubStorageLocation where LocCode= '" + sLocation + "'").Tables[0];
                        DataTable dtCamera = oManager.ExecuteDataSet(CommandType.Text, "SELECT DISTINCT G.MatCode FROM pGateIn G INNER JOIN mMaterial M ON G.MatCode=M.MatCode INNER JOIN mStorageLocation S ON G.StorageLocation=S.Code AND S.CCNo='" + sCCNo + "' AND OrderNo='" + lPONumber + "' AND M.Division='80' AND G.StorageLocation='" + sLocation + "' AND G.Status NOT IN(3,12,26,27) AND M.SerialProfiler<>'' ORDER BY G.MatCode").Tables[0];
                        DataTable dtCCDQty = oManager.ExecuteDataSet(CommandType.Text, "EXECUTE CC_DETAILS_NEW_SP '" + sCCNo + "','" + sLocation + "','" + lPONumber + "'").Tables[0];
                        if (dtCCDQty.Rows.Count != 0)
                        { oRuls.sResponse = clsMsgRule.sValid + "~" + dtCCTQty.Rows[0][0].ToString() + "~" + dtCCTQty.Rows[0][1].ToString() + "~" + oMain.DtToString(dtCCDQty) + "~" + oMain.DtToString(dtCamera) + "~" + oMain.DtToString(dtSubLoc); }
                        else
                        { oRuls.sResponse = clsMsgRule.sInValid; }
                    }

                }

            }
            catch (Exception ex)
            { oRuls.sResponse = clsMsgRule.sError + "~" + ex.Message.ToString(); }
            finally
            { oManager.Close(); }
            return oRuls.sResponse;
        }
        /// <summary>
        /// Do cycle count
        /// </summary>
        /// <param name="sCCNo"></param>
        /// <param name="sLocation"></param>
        /// <param name="sBarcode"></param>
        /// <param name="sUser"></param>
        /// <returns></returns>
        internal string fnDoCC(string sCCNo, string sLocation, string sBarcode, string sUser, string sCameraMatCode, string sSubLoc)
        {
            IDBManager oManager = oMain.DBProvider();
            oRuls.sResponse = string.Empty;
            string sReturn = string.Empty;
            string sLineItem = string.Empty;
            string sGateInNo = string.Empty;
            string sEmulsion = string.Empty;
            int iTQty = 0;
            int iSQty = 0;
            try
            {
                sLocation = sLocation.Split('|')[0].ToString();
                oManager.Open();

                 string sOrderNo = string.Empty;
                 string IsOldNewStock = string.Empty;
                //string IsOldNewStock = OldNew(sCCNo, oManager);
                if (sCCNo.Substring(1,4).Contains("O"))
                { IsOldNewStock = "O"; }
                if (IsOldNewStock == "O")
                {
                    WriteLog(IsOldNewStock);
                    string sInvoiceNo = string.Empty;
                    sGateInNo = sCCNo;
                    DataTable dtPrintDtls = PrintStatus(sBarcode, sGateInNo, oManager);
                    if (dtPrintDtls.Rows.Count != 0)//if data not in printing
                    {
                        sGateInNo = dtPrintDtls.Rows[0]["GateInNo"].ToString();//New Code 12-march-2014 By Santosh
                        sLineItem = dtPrintDtls.Rows[0]["Item"].ToString();//New Code 12-march-2014 By Santosh
                        sEmulsion = dtPrintDtls.Rows[0]["Emulsion"].ToString();//New Code 04-April-2014 By Brajesh
                        oRuls.sResponse = TranStatus(sBarcode, sCCNo, oManager);
                        if (oRuls.sResponse == string.Empty)
                        {
                            bool sLocExist = ValidateLocation(sLocation, oManager, sGateInNo);
                            if (sLocExist == false)
                            { oRuls.sResponse = "LOCATION MISMATCHD"; return oRuls.sResponse; }
                            bool sMatExist = ValidateMaterial(dtPrintDtls.Rows[0][3].ToString(), sLocation, oManager,sGateInNo);
                            if (sMatExist == false)
                            { oRuls.sResponse = "MATERIAL MISMATCHD"; return oRuls.sResponse; }
                            bool IsScanComplete = ScanComplete(sCCNo, sLocation, dtPrintDtls.Rows[0][3].ToString(), oManager,sGateInNo);
                            if (IsScanComplete == true)
                            {
                                oManager.BeginTransaction(oManager.Connection);//                                                                                                                                                                                       GateInNo                                   ,PlantCode                               ,MatCode                                    ,OrderNo                                        ,InvoiceNo,                                                                                            
                                oManager.ExecuteNonQuery(CommandType.Text, "INSERT INTO tMovement(BarCode,GateIn,PlantCode,MatCode,OrderNo,InvoiceNo,Status,StrLoc,QCOn,QCBy,PutawayOn,PutawayBy,CCNo,CCOn,CCBy,Item,CCStatus,SubLoc,Emulsion)VALUES('" + sBarcode + "','" + dtPrintDtls.Rows[0][1].ToString() + "','" + dtPrintDtls.Rows[0][4].ToString() + "','" + dtPrintDtls.Rows[0][3].ToString() + "','" + dtPrintDtls.Rows[0][0].ToString() + "','" + dtPrintDtls.Rows[0][2].ToString() + "',4,'" + sLocation + "',GetDate(),'" + sUser + "',GetDate(),'" + sUser + "','" + sCCNo + "',GetDate(),'" + sUser + "','" + sLineItem + "','I','" + sSubLoc + "','" + sEmulsion + "')");
                                oManager.ExecuteNonQuery(CommandType.Text, "UPDATE pGateIn SET ScannedQty=ScannedQty+1,Status=2 WHERE GateInNo='" + sGateInNo + "' AND MatCode='" + dtPrintDtls.Rows[0][3].ToString() + "' AND StorageLocation='" + sLocation + "' AND Item='" + sLineItem + "' AND MatQty>ScannedQty");
                                IDataReader QtyReader = oManager.ExecuteReader(CommandType.Text, "SELECT MatQty,ScannedQty FROM pGateIn WHERE GateInNo='" + sGateInNo + "' AND MatCode='" + dtPrintDtls.Rows[0][3].ToString() + "' AND StorageLocation='" + sLocation + "' AND Item='" + sLineItem + "'");
                                if (QtyReader.Read())
                                { iTQty = Convert.ToInt32(QtyReader[0].ToString()); iSQty = Convert.ToInt32(QtyReader[0].ToString()); }
                                QtyReader.Close();
                                if (iTQty == iSQty)
                                { oManager.ExecuteNonQuery(CommandType.Text, "UPDATE pGateIn SET Status=3 WHERE GateInNo='" + sGateInNo + "' AND MatCode='" + dtPrintDtls.Rows[0][3].ToString() + "' AND StorageLocation='" + sLocation + "' AND Item='" + sLineItem + "' AND MatQty>ScannedQty"); }

                                oManager.ExecuteNonQuery(CommandType.Text, "UPDATE mStorageLocation SET CCScanedQty=CCScanedQty+1 WHERE CCNo='" + sCCNo + "' AND Code='" + sLocation + "'");
                                oManager.ExecuteNonQuery(CommandType.Text, "UPDATE pCycleCount SET Status = 2,CCSQty = CCSQty + 1,CCCompletedBy='" + sUser + "',CCCompletedOn=GETDATE() WHERE CntNo='" + sCCNo + "'");
                                oRuls.sResponse = clsMsgRule.sValid;
                                oManager.CommitTransaction();
                                string sQty = Qty(sLocation, sCCNo, dtPrintDtls.Rows[0][3].ToString(), oManager, sOrderNo);
                                oRuls.sResponse = clsMsgRule.sValid + "~" + sQty;
                            }
                            else
                            { oRuls.sResponse = clsMsgRule.sIComplete; }
                        }
                        else
                        { return oRuls.sResponse; }
                    }
                    else if (dtPrintDtls.Rows.Count == 0 && sCameraMatCode != "")//if data not in printing
                    {
                        //WriteLog("1");
                        oRuls.sResponse = TranStatus(sBarcode, sCCNo, oManager);
                        //WriteLog("2");
                        if (oRuls.sResponse == string.Empty)
                        {
                            DataTable dtStockIn = ValidateStockInTable(sBarcode, oManager);
                            //WriteLog("3");
                            if (dtStockIn.Rows.Count == 0)
                            { oRuls.sResponse = "BARCODE NOT IN PSTOCKIN"; return oRuls.sResponse; }
                            else if (dtStockIn.Rows[0][0].ToString() != sCameraMatCode)
                            { oRuls.sResponse = "MATERIAL MISMATCHD"; return oRuls.sResponse; }
                            else if (dtStockIn.Rows[0][1].ToString() != sLocation)
                            { oRuls.sResponse = "LOCATION MISMATCHD"; return oRuls.sResponse; }
                            bool IsScanComplete = ScanComplete(sCCNo, sLocation, sCameraMatCode, oManager,sGateInNo);
                            //WriteLog("4");
                            if (IsScanComplete == true)
                            {
                                string sGateInNo1 = string.Empty;
                                IDataReader reader = oManager.ExecuteReader(CommandType.Text, "SELECT Item,GateInNo, OrderNo, InvoiceNo FROM pGateIn WHERE     (TransportNo = '" + sBarcode + "')");
                                if (reader.Read())
                                { sLineItem = reader[0].ToString(); sGateInNo1 = reader[1].ToString(); sOrderNo = reader[2].ToString(); sInvoiceNo = reader[3].ToString(); }
                                reader.Close();
                                //WriteLog(sLineItem + " " + sGateInNo1 + " " + sOrderNo + " " + sInvoiceNo);
                                oManager.BeginTransaction(oManager.Connection);
                                oManager.ExecuteNonQuery(CommandType.Text, "INSERT INTO tPrinting(BarCode,OrderNo,GateInNo,InvoiceNo,MatCode,PlantCode,Item)VALUES('" + sBarcode + "','" + sOrderNo + "','" + sGateInNo1 + "','" + sInvoiceNo + "','" + sCameraMatCode + "','" + clsSetting.sPlant + "'," + Convert.ToInt32(sLineItem) + ")");
                                oManager.ExecuteNonQuery(CommandType.Text, "INSERT INTO tMovement(BarCode,GateIn,PlantCode,MatCode,OrderNo,InvoiceNo,Status,StrLoc,QCOn,QCBy,PutawayOn,PutawayBy,CCNo,CCOn,CCBy,Item,CCStatus,SubLoc)VALUES('" + sBarcode + "','" + sGateInNo1 + "','" + clsSetting.sPlant + "','" + sCameraMatCode + "','" + sOrderNo + "','" + sInvoiceNo + "',4,'" + sLocation + "',GetDate(),'" + sUser + "',GetDate(),'" + sUser + "','" + sCCNo + "',GetDate(),'" + sUser + "'," + Convert.ToInt32(sLineItem) + ",'I','" + sSubLoc + "')");
                                oManager.ExecuteNonQuery(CommandType.Text, "UPDATE pGateIn SET ScannedQty=ScannedQty+1,Status=3 WHERE TransportNo='" + sBarcode + "' AND GateInNo='" + sGateInNo1 + "'");
                                oManager.ExecuteNonQuery(CommandType.Text, "UPDATE mStorageLocation SET CCScanedQty=CCScanedQty+1 WHERE CCNo='" + sCCNo + "' AND Code='" + sLocation + "'");
                                oManager.ExecuteNonQuery(CommandType.Text, "UPDATE pCycleCount SET Status = 2,CCSQty = CCSQty + 1,CCCompletedBy='" + sUser + "',CCCompletedOn=GETDATE() WHERE CntNo='" + sCCNo + "'");
                                oManager.CommitTransaction();
                                string sQty = Qty(sLocation, sCCNo, sCameraMatCode, oManager, sOrderNo);
                                oRuls.sResponse = clsMsgRule.sValid + "~" + sQty;
                            }
                            else
                            { oRuls.sResponse = clsMsgRule.sIComplete; }
                        }
                        else
                        { return oRuls.sResponse; }
                    }
                    else
                    { oRuls.sResponse = "BARCODE NOT PRINTED"; }
                }
                else
                {
                    //WriteLog("New");
                    DataTable dtPrintDtls = PrintStatus(sBarcode, oManager);
                    if (dtPrintDtls.Rows.Count != 0)//if data not in printing
                    {

                        oRuls.sResponse = TranStatus(sBarcode, sCCNo, oManager);
                        DataTable dtOrderNo = oManager.ExecuteDataSet(CommandType.Text, "SELECT DISTINCT GI.OrderNo FROM pCycleCount CC  INNER JOIN pGateIn GI ON GI.GateInNo=CC.CntNo AND GI.PlantCode =CC.PlantCode where CC.CntNo='" + sCCNo + "'").Tables[0];//New Code 12-march-2014
                        if (oRuls.sResponse == string.Empty)
                        {
                            sOrderNo = dtOrderNo.Rows[0]["OrderNo"].ToString();
                            oManager.BeginTransaction(oManager.Connection);//                                                                                                                                                                                       GateInNo                                   ,PlantCode                               ,MatCode                                    ,OrderNo                                        ,InvoiceNo,                                                                                            
                            oManager.ExecuteNonQuery(CommandType.Text, "INSERT INTO tMovement(BarCode,GateIn,PlantCode,MatCode,OrderNo,InvoiceNo,Status,StrLoc,QCOn,QCBy,PutawayOn,PutawayBy,CCNo,CCOn,CCBy,Item,CCStatus,SubLoc)VALUES('" + sBarcode + "','" + dtPrintDtls.Rows[0][1].ToString() + "','" + dtPrintDtls.Rows[0][4].ToString() + "','" + dtPrintDtls.Rows[0][3].ToString() + "','" + dtPrintDtls.Rows[0][0].ToString() + "','" + dtPrintDtls.Rows[0][2].ToString() + "',4,'" + sLocation + "',GetDate(),'" + sUser + "',GetDate(),'" + sUser + "','" + sCCNo + "',GetDate(),'" + sUser + "','" + dtPrintDtls.Rows[0][5].ToString() + "','I','" + sSubLoc + "','" + dtPrintDtls.Rows[0]["Emulsion"].ToString() + "')");
                            oManager.ExecuteNonQuery(CommandType.Text, "UPDATE mStorageLocation SET CCScanedQty=CCScanedQty+1 WHERE CCNo='" + sCCNo + "' AND Code='" + sLocation + "'");
                            oManager.ExecuteNonQuery(CommandType.Text, "UPDATE pCycleCount SET Status = 2,CCSQty = CCSQty + 1,CCCompletedBy='" + sUser + "',CCCompletedOn=GETDATE() WHERE CntNo='" + sCCNo + "'");
                            oRuls.sResponse = clsMsgRule.sValid;
                            oManager.CommitTransaction();
                            string sQty = Qty(sLocation, sCCNo, dtPrintDtls.Rows[0][3].ToString(), oManager,sOrderNo);
                            oRuls.sResponse = clsMsgRule.sValid + "~" + sQty;
                        }
                        else if (oRuls.sResponse == clsMsgRule.sBarcodeIntMovement)
                        {
                            #region To check at the time of cycle count if box location and selected location not same
                            string sBarcodeLoc = BarcodeLoc(sBarcode, oManager);
                            if (sBarcodeLoc != sLocation && oRuls.sResponse != string.Empty)
                            { oRuls.sResponse = clsMsgRule.sLocationMismatch; return oRuls.sResponse; }
                            #endregion
                            oManager.BeginTransaction(oManager.Connection);//                                                                                                                                                                                       GateInNo                                   ,PlantCode                               ,MatCode                                    ,OrderNo                                        ,InvoiceNo,                                                                                            
                            oManager.ExecuteNonQuery(CommandType.Text, "UPDATE tMovement SET CCNo='" + sCCNo + "',CCOn=GETDATE(),CCBy='" + sUser + "',CCStatus='U' WHERE BarCode='" + sBarcode + "'");
                            oManager.ExecuteNonQuery(CommandType.Text, "UPDATE mStorageLocation SET CCScanedQty=CCScanedQty+1 WHERE CCNo='" + sCCNo + "' AND Code='" + sLocation + "'");
                            oManager.ExecuteNonQuery(CommandType.Text, "UPDATE pCycleCount SET Status = 2,CCSQty = CCSQty + 1,CCCompletedBy='" + sUser + "',CCCompletedOn=GETDATE() WHERE CntNo='" + sCCNo + "'");
                            oRuls.sResponse = clsMsgRule.sValid;
                            oManager.CommitTransaction();
                            string sQty = Qty(sLocation, sCCNo, dtPrintDtls.Rows[0][3].ToString(), oManager, sOrderNo);
                            oRuls.sResponse = clsMsgRule.sValid + "~" + sQty;
                        }
                    }
                    else
                    { oRuls.sResponse = clsMsgRule.sBarcodeNotInPrinting; }
                }
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
        /// <param name="sLocation"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private bool ValidateLocation(string sLocation, IDBManager oManager, string sGateInNo)
        {
            bool IsLocExist = false;
            try
            {
                oManager.Open();
                IDataReader reader = null;
                if (clsSetting.gABColorPlant == "ABColor")
                {
                    reader = oManager.ExecuteReader(System.Data.CommandType.Text, "SELECT MatCode FROM pGateIn WHERE GateInNo='" + sGateInNo + "' AND StorageLocation='" + sLocation + "' AND ISNULL(SubPlant,'') = 'ABColor' ");

                }
                else
                {
                    reader = oManager.ExecuteReader(System.Data.CommandType.Text, "SELECT MatCode FROM pGateIn WHERE GateInNo='" + sGateInNo + "' AND StorageLocation='" + sLocation + "' AND ISNULL(SubPlant,'') != 'ABColor' ");

                }
                while (reader.Read())
                { IsLocExist = true; }
                reader.Close();
            }
            catch (Exception ex)
            { throw ex; }
            return IsLocExist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sMatcode"></param>
        /// <param name="sLocation"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private bool ValidateMaterial(string sMatcode, string sLocation, IDBManager oManager,string sGateInNo)
        {
            bool IsMatExist = false;
            try
            {
                oManager.Open();

                IDataReader reader = null;
                if (clsSetting.gABColorPlant == "ABColor")
                {
                    reader = oManager.ExecuteReader(System.Data.CommandType.Text, "SELECT MatCode FROM pGateIn WHERE GateInNo='" + sGateInNo + "' AND StorageLocation='" + sLocation + "' AND MatCode='" + sMatcode + "' AND ISNULL(SubPlant,'') = 'ABColor'  ");
                }
                else
                {
                    reader = oManager.ExecuteReader(System.Data.CommandType.Text, "SELECT MatCode FROM pGateIn WHERE GateInNo='" + sGateInNo + "' AND StorageLocation='" + sLocation + "' AND MatCode='" + sMatcode + "' AND ISNULL(SubPlant,'') != 'ABColor' ");
                }
                while (reader.Read())
                { IsMatExist = true; }
                reader.Close();
            }
            catch (Exception ex)
            { throw ex; }
            return IsMatExist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sBarcode"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private DataTable ValidateStockInTable(string sBarcode, IDBManager oManager)
        {
            try
            {
                oManager.Open();
                DataTable dtStock = oManager.ExecuteDataSet(System.Data.CommandType.Text, "SELECT MatCode, StrLoc, MatQty FROM pStockIn WHERE (SerialNo = '" + sBarcode + "')").Tables[0];
                return dtStock;
            }
            catch (Exception ex)
            { throw ex; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sBarcode"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private string BarcodeLoc(string sBarcode, IDBManager oManager)
        {
            string sBarcodeLoc = string.Empty;
            try
            {
                IDataReader rdr = oManager.ExecuteReader(CommandType.Text, "SELECT StrLoc FROM tMovement WHERE Barcode='" + sBarcode + "'");
                while (rdr.Read())
                { sBarcodeLoc = rdr[0].ToString(); }
                rdr.Close();
            }
            catch (Exception ex)
            { throw ex; }
            return sBarcodeLoc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sCCNo"></param>
        /// <param name="sLocation"></param>
        /// <param name="sMatCode"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private bool ScanComplete(string sCCNo, string sLocation, string sMatCode, IDBManager oManager,string sGateInNo)
        {
            bool IsScanComplete = false;
            try
            {
                StringBuilder sb = new StringBuilder();
                string sOrderNo = string.Empty;//New Code added by Santosh 12-march-2014
                if (clsSetting.gABColorPlant == "ABColor")
                {
                    DataTable dtCCNoA = oManager.ExecuteDataSet(CommandType.Text, "SELECT DISTINCT GI.OrderNo FROM pCycleCount CC  INNER JOIN pGateIn GI ON GI.GateInNo=CC.CntNo AND GI.PlantCode =CC.PlantCode where ISNULL(GI.SubPlant,'') = 'ABColor' and CC.CntNo='" + sCCNo + "' and GI.GateInNo='" + sGateInNo + "'").Tables[0];//New Code 12-march-2014
                    sOrderNo = dtCCNoA.Rows[0]["OrderNo"].ToString();//New Code added by Santosh 12-march-2014

                    sb.AppendLine("SELECT SUM(T.TQty),SUM(T.SQty) FROM");
                    sb.AppendLine("(");
                    sb.AppendLine("	SELECT MatQty TQty,0 SQty FROM pGateIn WHERE OrderNo='" + sOrderNo + "' AND StorageLocation='" + sLocation + "' AND MatCode='" + sMatCode + "'");
                    sb.AppendLine("		UNION ALL");
                    sb.AppendLine("	SELECT 0 TQty,COUNT(*)SQty FROM tMovement WHERE StrLoc='" + sLocation + "' AND MatCode='" + sMatCode + "' AND CCNo='" + sCCNo + "'");
                    sb.AppendLine(")t HAVING SUM(T.TQty)>SUM(T.SQty)");
                }
                else
                {
                    DataTable dtCCNo = oManager.ExecuteDataSet(CommandType.Text, "SELECT DISTINCT GI.OrderNo FROM pCycleCount CC  INNER JOIN pGateIn GI ON GI.GateInNo=CC.CntNo AND GI.PlantCode =CC.PlantCode where ISNULL(GI.SubPlant,'') != 'ABColor' and CC.CntNo='" + sCCNo + "'").Tables[0];//New Code 12-march-2014
                    sOrderNo = dtCCNo.Rows[0]["OrderNo"].ToString();//New Code  added by Santosh 12-march-2014

                    sb.AppendLine("SELECT SUM(T.TQty),SUM(T.SQty) FROM");
                    sb.AppendLine("(");
                    sb.AppendLine("	SELECT MatQty TQty,0 SQty FROM pGateIn WHERE OrderNo='" + sOrderNo + "' AND StorageLocation='" + sLocation + "' AND MatCode='" + sMatCode + "'");
                    sb.AppendLine("		UNION ALL");
                    sb.AppendLine("	SELECT 0 TQty,COUNT(*)SQty FROM tMovement WHERE StrLoc='" + sLocation + "' AND MatCode='" + sMatCode + "' AND CCNo='" + sCCNo + "'");
                    sb.AppendLine(")t HAVING SUM(T.TQty)>SUM(T.SQty)");
                }

                IDataReader rdr = oManager.ExecuteReader(CommandType.Text, sb.ToString());
                while (rdr.Read())
                { IsScanComplete = true; }
                rdr.Close();
            }
            catch (Exception ex)
            { throw ex; }
            return IsScanComplete;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sLocation"></param>
        /// <param name="sCCNo"></param>
        /// <param name="sMatCode"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private string Qty(string sLocation, string sCCNo, string sMatCode, IDBManager oManager,string sOrderNo)
        {
            string sQty = string.Empty;
            try
            {
                DataTable dtCCTQty = oManager.ExecuteDataSet(System.Data.CommandType.Text, "SELECT SUM(CCTQty)CCTQty,SUM(CCScanedQty)CCSQty FROM mStorageLocation WHERE  Code='" + sLocation + "' AND CCNo='" + sCCNo + "'").Tables[0];
                DataTable dtCCDQty = oManager.ExecuteDataSet(System.Data.CommandType.Text, "EXECUTE CC_DETAILS_NEW_MAT_SP '" + sCCNo + "','" + sLocation + "','" + sMatCode + "','" + sOrderNo + "'").Tables[0];
                sQty = dtCCTQty.Rows[0][0].ToString() + "~" + dtCCTQty.Rows[0][1].ToString() + "~" + oMain.DtToString(dtCCDQty);
            }
            catch (Exception ex)
            { throw ex; }
            return sQty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sCCNo"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private string OldNew(string sCCNo, IDBManager oManager)//not used after 12-march-2014
        {
            string IsLocal = string.Empty;
            try
            {
                int rowcount = Convert.ToInt32(oManager.ExecuteScalar(CommandType.Text, "SELECT COUNT(*)ROWCNT FROM pCycleCount"));
                if (rowcount == 1) { IsLocal = "O"; } else { IsLocal = "N"; }
            }
            catch (Exception ex)
            { throw ex; }
            return IsLocal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sBarcode"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private DataTable PrintStatus(string sBarcode,string sGateInNo, IDBManager oManager)
        {
            DataTable dtPrintDtls = null;
            try
            { dtPrintDtls = oManager.ExecuteDataSet(System.Data.CommandType.Text, "SELECT OrderNo,GateInNo,InvoiceNo,MatCode,PlantCode,Item,Emulsion FROM tPrinting WHERE BarCode='" + sBarcode + "' AND GateInNo='" + sGateInNo + "'").Tables[0]; }
            catch (Exception)
            { throw; }
            return dtPrintDtls;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sBarcode"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private DataTable PrintStatus(string sBarcode, IDBManager oManager)
        {
            DataTable dtPrintDtls = null;
            try
            { dtPrintDtls = oManager.ExecuteDataSet(System.Data.CommandType.Text, "SELECT OrderNo,GateInNo,InvoiceNo,MatCode,PlantCode,Item FROM tPrinting WHERE BarCode='" + sBarcode + "'").Tables[0]; }
            catch (Exception)
            { throw; }
            return dtPrintDtls;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sBarcode"></param>
        /// <param name="sCCNo"></param>
        /// <param name="oManager"></param>
        /// <returns></returns>
        private string TranStatus(string sBarcode, string sCCNo, IDBManager oManager)
        {
            string sLocal = string.Empty;
            //IDataReader rdr = oManager.ExecuteReader(CommandType.Text, "SELECT CCNo FROM tMovement WHERE BarCode='" + sBarcode + "'");
            IDataReader rdr = oManager.ExecuteReader(CommandType.Text, "SELECT CCNo FROM tMovement WHERE BarCode='" + sBarcode + "' and status <> 5 ");
            while (rdr.Read())
            {
                //if (rdr[0].ToString() != "")
                //{
                if (rdr[0].ToString() == string.Empty)
                { sLocal = clsMsgRule.sBarcodeIntMovement; }
                else if (rdr[0].ToString() == sCCNo)
                { sLocal = clsMsgRule.sCCScanned; }
                else
                { sLocal = clsMsgRule.sCCScanned + "~" + rdr[0].ToString(); }
                //}
            }
            rdr.Close();
            return sLocal;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sCCNo"></param>
        ///// <param name="sLocation"></param>
        ///// <param name="sCameraMatCode"></param>
        ///// <param name="oManager"></param>
        ///// <returns></returns>
        //private string CCMATLOCCAMERA(string sCCNo, string sLocation, string sCameraMatCode, IDBManager oManager)
        //{
        //    string sReturn = string.Empty;
        //    try
        //    {
        //        DataTable dtCCLOC = oManager.ExecuteDataSet(CommandType.Text, "SELECT * FROM MSTORAGELOCATION WHERE CCNo='" + sCCNo + "' AND CODE='" + sLocation + "'").Tables[0];
        //        if (dtCCLOC.Rows.Count > 0)
        //        {
        //            DataTable dtmatccloc = oManager.ExecuteDataSet(CommandType.Text, "SELECT * FROM pGateIn WHERE OrderNo='OLDSTOCK01' AND StorageLocation='" + sLocation + "' AND MATCODE='" + sCameraMatCode + "'").Tables[0];
        //            if (dtmatccloc.Rows.Count > 0)
        //            { sReturn = "MAT EXIST"; }
        //            else
        //            { sReturn = "MAT NOT EXIST WITH LOC"; }
        //        }
        //        else
        //        { sReturn = "LOC NOT EXIST"; }
        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //    return sReturn;
        //}
        ///// <summary>
        ///// To check material associated with particula location
        ///// </summary>
        ///// <param name="sCCNo"></param>
        ///// <param name="sLocation"></param>
        ///// <param name="sBarcode"></param>
        ///// <param name="oManager"></param>
        ///// <returns></returns>
        //private string CCMATLOC(string sCCNo, string sLocation, string sBarcode, IDBManager oManager)
        //{
        //    string sReturn = string.Empty;
        //    try
        //    {
        //        DataTable dtCCLOC = oManager.ExecuteDataSet(CommandType.Text, "SELECT * FROM MSTORAGELOCATION WHERE CCNo='" + sCCNo + "' AND CODE='" + sLocation + "'").Tables[0];
        //        if (dtCCLOC.Rows.Count > 0)
        //        {
        //            DataTable dtmatccloc = oManager.ExecuteDataSet(CommandType.Text, "SELECT * FROM pGateIn G INNER JOIN tPrinting P ON G.GateInNo=P.GateInNo AND G.MatCode=P.MatCode AND G.OrderNo='OLDSTOCK01' AND G.StorageLocation='" + sLocation + "' AND P.BarCode='" + sBarcode + "'").Tables[0];
        //            if (dtmatccloc.Rows.Count > 0)
        //            { sReturn = "MAT EXIST"; }
        //            else
        //            { sReturn = "MAT NOT EXIST WITH LOC"; }
        //        }
        //        else
        //        { sReturn = "LOC NOT EXIST"; }
        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //    return sReturn;
        //}
        /// <summary>
        /// This method is not usefull for this application,To check jcb sorting
        /// </summary>
        /// <returns></returns>
        internal string DEFECT()
        {
            IDBManager oManager = oMain.DBProvider();
            try
            {
                oManager.Open();
                DataTable dtCCDQty = oManager.ExecuteDataSet(System.Data.CommandType.Text, "SELECT     Code, [Short text for code] FROM         Sheet123$").Tables[0];
                if (dtCCDQty.Rows.Count != 0)
                { oRuls.sResponse = clsMsgRule.sValid + "~" + oMain.DtToString(dtCCDQty); }
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
        /// <param name="sFile"></param>
        private void WriteLog(string sFile)
        {
            try
            {
            StreamWriter sw = new StreamWriter("D:\\Log.txt", true);
            sw.WriteLine(sFile);
            sw.Close();
            }
            catch (Exception)
            { throw; }
        }
    }
}
