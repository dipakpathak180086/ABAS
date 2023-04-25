using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace COM_SERVER
{
    static class Program
    {
        static string sPath = Application.StartupPath + "\\IsRunning.txt";
        static string ProductName = string.Empty;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static bool ReadFile()
        {
            bool IsRun = false;
            try
            {
                StreamReader reader = new StreamReader(sPath);
                while (!reader.EndOfStream)
                { ProductName = reader.ReadLine(); IsRun = true; }
                reader.Close();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message.ToString()); }
            return IsRun;
        }
        [STAThread]
        static void Main()
        {
            if (!ReadFile())
                return;
            bool createdNew;
            System.Threading.Mutex m = new System.Threading.Mutex(true, ProductName, out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("Application already running", "COM Server", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmServer());
        }
    }
}
