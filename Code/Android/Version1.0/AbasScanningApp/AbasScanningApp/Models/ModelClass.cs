using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AbasScanningApp.Models
{


    #region Scanning
    public class Scanning
    {
        public string ItemCode { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }

    #endregion

    
}