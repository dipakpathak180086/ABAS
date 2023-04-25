using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using System.Net.Http;
using System.Text;
using AbasScanningApp.Models;
using System.Collections.Generic;
using System.IO;
using Android.Content.PM;
using Android.Views;
using Android.Net;
using System.Net.Sockets;

namespace AbasScanningApp
{
    public enum MessageTitle
    {
        ERROR = 1,
        CONFIRM = 2,
        INFORMATION = 3,
    }

    public class clsGlobal : Activity
    {
        public static string FileFolder = "SatoFiles";
        public static string ServerIpFileName = "ServerIP.txt";
        public static string FilePath = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        internal static string mSockIp = "";
        internal static Int32 mSockPort = 0;
        internal static string mDeviceNo = "";
        internal static string mGroupNo = "";
        public static string UserId { get; set; }
        public static string UserName { get; set; }
        public static string UserGroup { get; set; }
        public static string SelectedBackNo { get; set; } = "";

        //***************************************
        public void ShowMessage(string msg, Activity activity, MessageTitle MsgTitle)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(activity);
            builder.SetTitle(MsgTitle.ToString());
            builder.SetMessage(msg);
            builder.SetCancelable(false);
            builder.SetPositiveButton("OK", delegate { Finish(); });
            builder.Show();
        }
    }
}
