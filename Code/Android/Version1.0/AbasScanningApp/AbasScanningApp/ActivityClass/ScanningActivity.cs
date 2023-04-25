using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using AbasScanningApp;
using AbasScanningApp.Models;
using AbasScanningApp.Adapter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AbasScanningApp
{
    [Activity(Label = "Abas ScanningApp", MainLauncher = true, WindowSoftInputMode = SoftInput.StateHidden, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class ScanningActivity : AppCompatActivity
    {
        clsGlobal clsGLB;
        clsNetwork oNetwork;
        MediaPlayer mediaPlayerSound;
        Vibrator vibrator;
        List<Scanning> _ListScanning = new List<Scanning>();

        EditText editItemCode, txtDate,editTime;
        TextView txtMsg;

        Button btnReset, btnServerConfiguration;

        RecyclerView recycleViewLocation;
        ScanningAdapter scanningAdapter;
        RecyclerView.LayoutManager mLayoutManager;

        public ScanningActivity()
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

        #region Activity Events
        protected override void OnCreate(Bundle savedInstanceState)
        {

            try
            {
                base.OnCreate(savedInstanceState);
                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.activity_ValidateBarcode);

                Button imgBack = FindViewById<Button>(Resource.Id.btnExit);
                imgBack.Click += (e, a) =>
                {
                    this.Finish();
                };
                Button imgServer = FindViewById<Button>(Resource.Id.btnServerConfiguration);
                imgServer.Click += (e, a) =>
                {
                    OpenActivity(typeof(SettingActivity));
                };
               
                TextView txtHeader = FindViewById<TextView>(Resource.Id.txtHeader);
                txtHeader.Text = "SCANNING";

                txtMsg = FindViewById<TextView>(Resource.Id.txtMsg);
                txtMsg.Text = "";

                editItemCode = FindViewById<EditText>(Resource.Id.editBarcode);
                editItemCode.KeyPress += TxtItemCode_KeyPress;

                editTime = FindViewById<EditText>(Resource.Id.editTime);
                editTime.Enabled = false;

                btnReset = FindViewById<Button>(Resource.Id.btnReset);
                btnReset.Click += BtnReset_Click;
                if (ReadSettingFile() == false)
                    OpenActivity(typeof(SettingActivity));

                recycleViewLocation = FindViewById<RecyclerView>(Resource.Id.recycleViewLocation);
                mLayoutManager = new LinearLayoutManager(this);
                recycleViewLocation.SetLayoutManager(mLayoutManager);

                BindRecycleView();

                vibrator = this.GetSystemService(VibratorService) as Vibrator;
                editItemCode.RequestFocus();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        private void TxtItemCode_KeyPress(object sender, View.KeyEventArgs e)
        {
            try
            {
                if (e.Event.Action == KeyEventActions.Down)
                {
                    if (e.KeyCode == Keycode.Enter)
                    {
                      
                        if (editItemCode.Text.Trim().Length >10)
                        {
                            if (GetTimeFromServer())
                            {
                                SaveScanningAsync();
                                GetTimeFromServer();
                                editItemCode.Text = "";
                            }
                            
                        }
                        else
                        {
                            StartPlayingSound();
                            ShowMessageBox("Invalid Item barcode " + editItemCode.Text.Trim(), this);
                            editItemCode.Text = "";
                            editItemCode.RequestFocus();
                        }
                    }
                    else
                        e.Handled = false;
                }
            }
            catch (Exception ex)
            {
                StartPlayingSound();
                ShowMessageBox("Error : " + ex.Message, this);
            }
        }

        #endregion

        #region Button Events

        private void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                editItemCode.Text = "";
                editTime.Text = "";
                Clear();
                scanningAdapter.NotifyDataSetChanged();
                editItemCode.RequestFocus();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        #endregion

        #region Methods
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
        private void BindRecycleView()
        {
            try
            {
                scanningAdapter = new ScanningAdapter(_ListScanning, this);
                recycleViewLocation.SetAdapter(scanningAdapter);
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }
        private bool ReadSettingFile()
        {
            StreamReader sr = null;
            try
            {
                string folderPath = Path.Combine(clsGlobal.FilePath, clsGlobal.FileFolder);
                string filename = Path.Combine(folderPath, clsGlobal.ServerIpFileName);

                if (File.Exists(filename))
                {
                    sr = new StreamReader(filename);
                    clsGlobal.mSockIp = sr.ReadLine();
                    clsGlobal.mSockPort = Convert.ToInt32(sr.ReadLine());
                    clsGlobal.mDeviceNo = sr.ReadLine();
                    clsGlobal.mGroupNo = sr.ReadLine();
                    sr.Close();
                    sr.Dispose();
                    sr = null;

                    return true;
                }
                return false;
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
        //async Task<string> SaveScanningAsync()
        //{
        //    var progressDialog = ProgressDialog.Show(this, "", "Please wait...", true);
        //    try
        //    {
        //        Clear();
        //        string[] _RESPONSE = await Task.Run(() => SaveScanningFromServer(txtItemCode.Text.Trim(),txtTime.Text.Trim()));

        //        progressDialog.Hide();

        //        switch (_RESPONSE[0])
        //        {
        //            case "VALID":
        //                //Get the rows
        //                string[] ArrRow = _RESPONSE[1].Split("#");
        //                for (int i = 0; i < ArrRow.Length; i++)
        //                {
        //                    //get the columns from the row
        //                    string[] ArrCol = ArrRow[i].Split("$");
        //                    //_ListFgFeeding.Add(new FgFeeding
        //                    //{
        //                    //    LocationCode = ArrCol[0],
        //                    //    Capacity = int.Parse(ArrCol[1]),
        //                    //    AvailableQty = int.Parse(ArrCol[2])
        //                    //});
        //                }
        //                txtItemCode.RequestFocus();
        //                break;

        //            case "INVALID":
        //                txtItemCode.Text = "";
        //                txtItemCode.RequestFocus();
        //                StartPlayingSound();
        //                ShowMessageBox(_RESPONSE[1], this);
        //                break;

        //            case "ERROR":
        //                txtItemCode.Text = "";
        //                txtItemCode.RequestFocus();
        //                StartPlayingSound();
        //                ShowMessageBox(_RESPONSE[1], this);
        //                break;

        //            case "NO_CONNECTION":
        //                txtItemCode.Text = "";
        //                txtItemCode.RequestFocus();
        //                StartPlayingSound();
        //                ShowMessageBox("Communication server not connected", this);
        //                break;

        //            default:
        //                txtItemCode.Text = "";
        //                txtItemCode.RequestFocus();
        //                StartPlayingSound();
        //                ShowMessageBox("No option match from comm server", this);
        //                break;
        //        }
        //        //Refresh the list
        //        fgFeedingAdapter.NotifyDataSetChanged();
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
        //        progressDialog.Hide();
        //    }
        //    finally
        //    {
        //        progressDialog.Hide();
        //    }
        //    return "";
        //}
         string SaveScanningAsync()
        {
            try
            {
                Clear();
                string[] _RESPONSE =  SaveScanningFromServer(editItemCode.Text.Trim());


                switch (_RESPONSE[0])
                {
                    case "VALID":
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        editItemCode.RequestFocus();
                        GetViewDataFromServer();
                        GetTimeFromServer();
                        break;

                    case "INVALID":
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        GetViewDataFromServer();
                        break;

                    case "ERROR":
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "NO_CONNECTION":
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox("Communication server not connected", this);
                        break;

                    default:
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox("No option match from comm server", this);
                        break;
                }
                //Refresh the list
                scanningAdapter.NotifyDataSetChanged();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
                
            }
            finally
            {
               
            }
            return "";
        }

        private string[] SaveScanningFromServer(string binbarcode)
        {
            try
            {
                string _MESSAGE = "SCAN~" + binbarcode + "~" + clsGlobal.mDeviceNo + "~" + clsGlobal.mGroupNo + "}";
                string[] _RESPONSE = oNetwork.fnSendReceiveData(_MESSAGE).Split('~');
                return _RESPONSE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetViewDataFromServer()
        {
            try
            {
                string _MESSAGE = "SELECT~}";
                string[] _RESPONSE = oNetwork.fnSendReceiveData(_MESSAGE).Split('~');
                switch (_RESPONSE[0])
                {
                    case "VALID":
                        //Get the rows
                        string[] ArrRow = _RESPONSE[1].Split("#");
                        for (int i = 0; i < ArrRow.Length; i++)
                        {
                            //get the columns from the row
                            string[] ArrCol = ArrRow[i].Split("$");
                            _ListScanning.Add(new Scanning
                            {
                                ItemCode = ArrCol[0],
                                Date = ArrCol[1],
                                Time = ArrCol[2]
                            });
                        }
                        editItemCode.RequestFocus();
                        break;

                    case "INVALID":
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "ERROR":
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "NO_CONNECTION":
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox("Communication server not connected", this);
                        break;

                    default:
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox("No option match from comm server", this);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool  GetTimeFromServer()
        {
            bool bCheck = false;
            try
            {
                string _MESSAGE = "TIME~}";
                string[] _RESPONSE = oNetwork.fnSendReceiveData(_MESSAGE).Split('~');
              
                switch (_RESPONSE[0])
                {
                    case "VALID":
                        editTime.Text = _RESPONSE[1].ToString();
                        editTime.Enabled = false;
                        editItemCode.RequestFocus();
                        bCheck = true;
                        break;

                    case "INVALID":
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "ERROR":
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox(_RESPONSE[1], this);
                        break;

                    case "NO_CONNECTION":
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox("Communication server not connected", this);
                        break;

                    default:
                        editItemCode.Text = "";
                        editItemCode.RequestFocus();
                        StartPlayingSound();
                        ShowMessageBox("No option match from comm server", this);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bCheck;
        }







        public void ShowMessageBox(string msg, Activity activity)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(activity);
            builder.SetTitle("Message");
            builder.SetMessage(msg);
            builder.SetCancelable(false);
            builder.SetPositiveButton("Ok", handleOkMessage);
            builder.Show();
        }

        void handleOkMessage(object sender, DialogClickEventArgs e)
        {
            try
            {
                vibrator.Cancel();
                StopPlayingSound();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }
        private void StartPlayingSound()
        {
            try
            {
                //Start Vibration
                long[] pattern = { 0, 200, 0 }; //0 to start now, 200 to vibrate 200 ms, 0 to sleep for 0 ms.
                vibrator.Vibrate(pattern, 0);//

                StopPlayingSound();
                mediaPlayerSound = MediaPlayer.Create(this, Resource.Raw.Beep);
                mediaPlayerSound.Start();
            }
            catch (Exception ex) { throw ex; }
        }
        private void StopPlayingSound()
        {
            try
            {
                if (mediaPlayerSound != null)
                {
                    mediaPlayerSound.Stop();
                    mediaPlayerSound.Release();
                    mediaPlayerSound = null;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void Clear()
        {
            try
            {
             
                txtMsg.Text = "";
                _ListScanning.Clear();
            }
            catch (Exception ex)
            {
                clsGLB.ShowMessage(ex.Message, this, MessageTitle.ERROR);
            }
        }

        #endregion

        #region EditText Events

        private void editBinBarcode_KeyPress(object sender, View.KeyEventArgs e)
        {
            
        }

       
        public override void OnBackPressed()
        {
        }

        #endregion
    }
}