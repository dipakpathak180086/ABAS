using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AbasScanningApp;

namespace AbasScanningApp
{
    [Activity(Label = "Setting", WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SettingActivity : Activity
    {
        clsNetwork oNetwork;
        clsGlobal clsGLB;
        EditText editServerIP;
        EditText editPort;
        EditText editGorupNo;
        EditText editDeviceNo;
       
        public SettingActivity()
        {
            try
            {
                clsGLB = new clsGlobal();
                oNetwork = new clsNetwork();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.activity_setting);

                Button btnSave = FindViewById<Button>(Resource.Id.btnSave);
                btnSave.Click += BtnSave_Click;

                editServerIP = FindViewById<EditText>(Resource.Id.txtServerIP);
                editPort = FindViewById<EditText>(Resource.Id.txtPort);

                editDeviceNo = FindViewById<EditText>(Resource.Id.txtDeviceNo);
                editGorupNo = FindViewById<EditText>(Resource.Id.txtGroupNo);


                Button btnBack = FindViewById<Button>(Resource.Id.btnBack);
                btnBack.Click += (e, a) =>
                {
                    this.Finish();
                };

                ReadSettingFile();

                editServerIP.RequestFocus();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        public override void OnBackPressed() { }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateControls())
                {

                    string folderPath = Path.Combine(clsGlobal.FilePath, clsGlobal.FileFolder);
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string filename = Path.Combine(folderPath, clsGlobal.ServerIpFileName);

                    using (var streamWriter = new StreamWriter(filename, false))
                    {
                        streamWriter.WriteLine(editServerIP.Text.Trim());
                        streamWriter.WriteLine(editPort.Text.Trim());
                        streamWriter.WriteLine(editGorupNo.Text.Trim());
                        streamWriter.WriteLine(editDeviceNo.Text.Trim());

                        MediaScannerConnection.ScanFile(this, new string[] { filename }, null, null);

                        Toast.MakeText(this, "Setting saved", ToastLength.Long).Show();

                        clsGlobal.mSockIp = editServerIP.Text.Trim();
                        clsGlobal.mSockPort = Convert.ToInt32(editPort.Text.Trim());
                        clsGlobal.mGroupNo = editGorupNo.Text.Trim();
                        clsGlobal.mDeviceNo = editDeviceNo.Text.Trim();
                        Finish();
                    }
                }
            }
            catch (Exception ex)
            { clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR); }
        }

        private void ReadSettingFile()
        {
            StreamReader sr = null;
            try
            {
                string folderPath = Path.Combine(clsGlobal.FilePath, clsGlobal.FileFolder);
                string filename = Path.Combine(folderPath, clsGlobal.ServerIpFileName);

                if (File.Exists(filename))
                {
                    sr = new StreamReader(filename);
                    editServerIP.Text = sr.ReadLine();
                    editPort.Text = sr.ReadLine();
                    editDeviceNo.Text = sr.ReadLine();
                    editGorupNo.Text = sr.ReadLine();
                    sr.Close();
                    sr.Dispose();
                    sr = null;

                    clsGlobal.mSockIp = editServerIP.Text.Trim();
                    clsGlobal.mSockPort = Convert.ToInt32(editPort.Text.Trim());
                    clsGlobal.mDeviceNo = editDeviceNo.Text.Trim();
                    clsGlobal.mGroupNo = editGorupNo.Text.Trim();
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                    sr = null;
                }
            }
        }

        private bool ValidateControls()
        {
            try
            {
                bool IsValidate = true;

                if (string.IsNullOrEmpty(editServerIP.Text.Trim()))
                {
                    Toast.MakeText(this, "Input server ip", ToastLength.Long).Show();
                    editServerIP.RequestFocus();
                    IsValidate = false;
                }
                if (string.IsNullOrEmpty(editPort.Text.Trim()))
                {
                    Toast.MakeText(this, "Input server port", ToastLength.Long).Show();
                    editPort.RequestFocus();
                    IsValidate = false;
                }
                if (string.IsNullOrEmpty(editDeviceNo.Text.Trim()))
                {
                    Toast.MakeText(this, "Input Device No", ToastLength.Long).Show();
                    editDeviceNo.RequestFocus();
                    IsValidate = false;
                }
                if (string.IsNullOrEmpty(editGorupNo.Text.Trim()))
                {
                    Toast.MakeText(this, "Input Group No", ToastLength.Long).Show();
                    editGorupNo.RequestFocus();
                    IsValidate = false;
                }

                return IsValidate;
            }
            catch (Exception ex) { throw ex; }
        }

        public void OpenActivity(Type t)
        {
            try
            {
                Intent MenuIntent = new Intent(this, t);
                StartActivity(MenuIntent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}