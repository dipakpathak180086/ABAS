using SILCommServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Classes
{
    public enum EnumDbType { SELECT, INSERT, UPDATE, DELETE, SELECTBYID, SEARCH, VALIDATEUSER, UPDATEPASSWORD, KANBAN, PART, REJECT, BIN, REJECT_NONBARCODE, FRACTION, PICK_DETAILS, COMPLETE };
    public enum EnumProductionStatus { FRACTION = 1, COMPLETE = 2, SERVICE_PART = 3, REJECT = 4, FG_FEEDING = 5, PICK = 6, DISPATCH = 7 };
    public enum EnumAppType { DESKTOPAPP, COMMSERVER, ANDROIDAPP };
    public class clsMsgRule
    {
        public string sResponse = string.Empty;
        public static string sValid = "VALID";
        public static string sInValid = "INVALID";
        public static string sError = "ERROR";
        public static int sPort;
    }
}

