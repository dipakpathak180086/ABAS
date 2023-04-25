using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using AbasScanningApp.Models;
using Java.Lang;
using Exception = System.Exception;

namespace AbasScanningApp.Adapter
{
    public class ScanningAdapter : RecyclerView.Adapter
    {
        public List<Scanning> lstItem;
        Context context;
        public ScanningAdapter(List<Scanning> itemDetails, Context cont)
        {
            lstItem = itemDetails;
            context = cont;
        }
        public override int ItemCount
        {
            get { return lstItem.Count; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            try
            {
                DispatchHolder vh = holder as DispatchHolder;
                vh.txtItemCode.Text = lstItem[position].ItemCode;
                vh.txtDate.Text = lstItem[position].Date.ToString();
                vh.txtTime.Text = lstItem[position].Time.ToString();
                ////Change Background color
                //if (lstItem[position].ScanQty >= lstItem[position].Qty)
                //{
                //    vh.txtBackNo.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                //    vh.txtQty.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                //    vh.txtScanQty.SetBackgroundColor(Android.Graphics.Color.LightGreen);
                //}
                //else
                //{
                //    vh.txtBackNo.SetBackgroundResource(Resource.Drawable.BorderStyle);
                //    vh.txtQty.SetBackgroundResource(Resource.Drawable.BorderStyle);
                //    vh.txtScanQty.SetBackgroundResource(Resource.Drawable.BorderStyle);
                //}
            }
            catch (System.Exception ex) { Toast.MakeText(context, ex.Message, ToastLength.Long).Show(); }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            DispatchHolder vh = null;
            try
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.view_dispatch, parent, false);
                vh = new DispatchHolder(itemView);

            }
            catch (Exception ex) { Toast.MakeText(context, ex.Message, ToastLength.Long).Show(); }
            return vh;
        }
    }

    public class DispatchHolder : RecyclerView.ViewHolder
    {
        public TextView txtItemCode;
        public TextView txtDate;
        public TextView txtTime;
        public ImageButton imgbtnViewLoc { get; set; }
        public DispatchHolder(View itemview) : base(itemview)
        {
            try
            {
                txtItemCode = itemview.FindViewById<TextView>(Resource.Id.txtItemCode);
                txtDate = itemview.FindViewById<TextView>(Resource.Id.txtDate);
                txtTime = itemview.FindViewById<TextView>(Resource.Id.txtTime);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}