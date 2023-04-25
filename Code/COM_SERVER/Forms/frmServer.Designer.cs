namespace COM_SERVER
{
    partial class frmServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServer));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.picDisconnect = new System.Windows.Forms.PictureBox();
            this.pnlDBSeting = new System.Windows.Forms.Panel();
            this.lblString = new System.Windows.Forms.Label();
            this.txtConString = new System.Windows.Forms.TextBox();
            this.cmdTestCon = new System.Windows.Forms.Button();
            this.cmbSchema = new System.Windows.Forms.ComboBox();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdBack = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.lblPwd = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.pnlLog = new System.Windows.Forms.Panel();
            this.lvLog = new System.Windows.Forms.ListView();
            this.colClient = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdLogBack = new System.Windows.Forms.Button();
            this.picConnect = new System.Windows.Forms.PictureBox();
            this.pnlClients = new System.Windows.Forms.Panel();
            this.lvClient = new System.Windows.Forms.ListView();
            this.colClientIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cloTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdClientBack = new System.Windows.Forms.Button();
            this.lblConnect = new System.Windows.Forms.Label();
            this.pnlServer = new System.Windows.Forms.Panel();
            this.cmdHide = new System.Windows.Forms.Button();
            this.cmdDbServer = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.cmdClients = new System.Windows.Forms.Button();
            this.cmdLog = new System.Windows.Forms.Button();
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdDisconnect = new System.Windows.Forms.Button();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDisconnect)).BeginInit();
            this.pnlDBSeting.SuspendLayout();
            this.pnlLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picConnect)).BeginInit();
            this.pnlClients.SuspendLayout();
            this.pnlServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlHeader.Controls.Add(this.label1);
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(531, 66);
            this.pnlHeader.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(531, 23);
            this.label1.TabIndex = 1;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(531, 66);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Communication Server";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlImage
            // 
            this.pnlImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImage.Controls.Add(this.picDisconnect);
            this.pnlImage.Controls.Add(this.pnlDBSeting);
            this.pnlImage.Controls.Add(this.pnlLog);
            this.pnlImage.Controls.Add(this.picConnect);
            this.pnlImage.Controls.Add(this.pnlClients);
            this.pnlImage.Location = new System.Drawing.Point(0, 66);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(382, 341);
            this.pnlImage.TabIndex = 34;
            // 
            // picDisconnect
            // 
            this.picDisconnect.BackgroundImage = global::COM_SERVER.Properties.Resources.con1;
            this.picDisconnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picDisconnect.InitialImage = null;
            this.picDisconnect.Location = new System.Drawing.Point(3, 0);
            this.picDisconnect.Name = "picDisconnect";
            this.picDisconnect.Size = new System.Drawing.Size(376, 341);
            this.picDisconnect.TabIndex = 30;
            this.picDisconnect.TabStop = false;
            // 
            // pnlDBSeting
            // 
            this.pnlDBSeting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDBSeting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDBSeting.Controls.Add(this.lblString);
            this.pnlDBSeting.Controls.Add(this.txtConString);
            this.pnlDBSeting.Controls.Add(this.cmdTestCon);
            this.pnlDBSeting.Controls.Add(this.cmbSchema);
            this.pnlDBSeting.Controls.Add(this.cmbServer);
            this.pnlDBSeting.Controls.Add(this.cmdSave);
            this.pnlDBSeting.Controls.Add(this.cmdBack);
            this.pnlDBSeting.Controls.Add(this.txtPwd);
            this.pnlDBSeting.Controls.Add(this.lblPwd);
            this.pnlDBSeting.Controls.Add(this.txtUserID);
            this.pnlDBSeting.Controls.Add(this.lblUserID);
            this.pnlDBSeting.Controls.Add(this.lblDatabase);
            this.pnlDBSeting.Controls.Add(this.lblServer);
            this.pnlDBSeting.Location = new System.Drawing.Point(23, 313);
            this.pnlDBSeting.Name = "pnlDBSeting";
            this.pnlDBSeting.Size = new System.Drawing.Size(10, 23);
            this.pnlDBSeting.TabIndex = 32;
            // 
            // lblString
            // 
            this.lblString.AutoSize = true;
            this.lblString.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblString.Location = new System.Drawing.Point(40, 225);
            this.lblString.Name = "lblString";
            this.lblString.Size = new System.Drawing.Size(124, 14);
            this.lblString.TabIndex = 45;
            this.lblString.Text = "Connection String";
            // 
            // txtConString
            // 
            this.txtConString.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConString.Location = new System.Drawing.Point(41, 242);
            this.txtConString.Name = "txtConString";
            this.txtConString.ReadOnly = true;
            this.txtConString.Size = new System.Drawing.Size(288, 22);
            this.txtConString.TabIndex = 44;
            // 
            // cmdTestCon
            // 
            this.cmdTestCon.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdTestCon.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTestCon.Image = global::COM_SERVER.Properties.Resources.Close_icon;
            this.cmdTestCon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdTestCon.Location = new System.Drawing.Point(165, 178);
            this.cmdTestCon.Name = "cmdTestCon";
            this.cmdTestCon.Size = new System.Drawing.Size(164, 40);
            this.cmdTestCon.TabIndex = 43;
            this.cmdTestCon.Text = "Test Connection";
            this.cmdTestCon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdTestCon.UseVisualStyleBackColor = true;
            this.cmdTestCon.Click += new System.EventHandler(this.cmdTestCon_Click);
            // 
            // cmbSchema
            // 
            this.cmbSchema.FormattingEnabled = true;
            this.cmbSchema.Location = new System.Drawing.Point(114, 151);
            this.cmbSchema.Name = "cmbSchema";
            this.cmbSchema.Size = new System.Drawing.Size(215, 21);
            this.cmbSchema.TabIndex = 42;
            this.cmbSchema.Enter += new System.EventHandler(this.cmbSchema_Enter);
            // 
            // cmbServer
            // 
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Location = new System.Drawing.Point(114, 52);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(215, 21);
            this.cmbServer.TabIndex = 41;
            this.cmbServer.Enter += new System.EventHandler(this.cmbServer_Enter);
            // 
            // cmdSave
            // 
            this.cmdSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdSave.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSave.Image = global::COM_SERVER.Properties.Resources.Save_icon__1_;
            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSave.Location = new System.Drawing.Point(145, 270);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(87, 40);
            this.cmdSave.TabIndex = 39;
            this.cmdSave.Text = "&Save";
            this.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdBack
            // 
            this.cmdBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBack.Image = global::COM_SERVER.Properties.Resources.back_icon__2_;
            this.cmdBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdBack.Location = new System.Drawing.Point(242, 270);
            this.cmdBack.Name = "cmdBack";
            this.cmdBack.Size = new System.Drawing.Size(87, 40);
            this.cmdBack.TabIndex = 38;
            this.cmdBack.Text = "Bac&k";
            this.cmdBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdBack.UseVisualStyleBackColor = true;
            this.cmdBack.Click += new System.EventHandler(this.cmdBack_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPwd.Location = new System.Drawing.Point(114, 115);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(215, 22);
            this.txtPwd.TabIndex = 7;
            // 
            // lblPwd
            // 
            this.lblPwd.AutoSize = true;
            this.lblPwd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPwd.Location = new System.Drawing.Point(38, 118);
            this.lblPwd.Name = "lblPwd";
            this.lblPwd.Size = new System.Drawing.Size(72, 14);
            this.lblPwd.TabIndex = 6;
            this.lblPwd.Text = "Password";
            // 
            // txtUserID
            // 
            this.txtUserID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserID.Location = new System.Drawing.Point(114, 85);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(215, 22);
            this.txtUserID.TabIndex = 5;
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserID.Location = new System.Drawing.Point(38, 88);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(52, 14);
            this.lblUserID.TabIndex = 4;
            this.lblUserID.Text = "UserId";
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabase.Location = new System.Drawing.Point(38, 153);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(70, 14);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "DataBase";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServer.Location = new System.Drawing.Point(38, 52);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(52, 14);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server";
            // 
            // pnlLog
            // 
            this.pnlLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLog.Controls.Add(this.lvLog);
            this.pnlLog.Controls.Add(this.cmdLogBack);
            this.pnlLog.Location = new System.Drawing.Point(10, 313);
            this.pnlLog.Name = "pnlLog";
            this.pnlLog.Size = new System.Drawing.Size(10, 23);
            this.pnlLog.TabIndex = 40;
            // 
            // lvLog
            // 
            this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colClient,
            this.colData,
            this.colDate});
            this.lvLog.GridLines = true;
            this.lvLog.Location = new System.Drawing.Point(12, 19);
            this.lvLog.Name = "lvLog";
            this.lvLog.Size = new System.Drawing.Size(364, 271);
            this.lvLog.TabIndex = 39;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            // 
            // colClient
            // 
            this.colClient.Text = "ClientIP";
            this.colClient.Width = 100;
            // 
            // colData
            // 
            this.colData.Text = "Req./Resp.";
            this.colData.Width = 227;
            // 
            // colDate
            // 
            this.colDate.Text = "ConnectAt";
            this.colDate.Width = 100;
            // 
            // cmdLogBack
            // 
            this.cmdLogBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdLogBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLogBack.Image = global::COM_SERVER.Properties.Resources.back_icon__2_;
            this.cmdLogBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdLogBack.Location = new System.Drawing.Point(289, 296);
            this.cmdLogBack.Name = "cmdLogBack";
            this.cmdLogBack.Size = new System.Drawing.Size(87, 40);
            this.cmdLogBack.TabIndex = 38;
            this.cmdLogBack.Text = "Bac&k";
            this.cmdLogBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdLogBack.UseVisualStyleBackColor = true;
            this.cmdLogBack.Click += new System.EventHandler(this.cmdLogBack_Click);
            // 
            // picConnect
            // 
            this.picConnect.BackgroundImage = global::COM_SERVER.Properties.Resources.network;
            this.picConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picConnect.InitialImage = null;
            this.picConnect.Location = new System.Drawing.Point(5, 2);
            this.picConnect.Name = "picConnect";
            this.picConnect.Size = new System.Drawing.Size(371, 340);
            this.picConnect.TabIndex = 30;
            this.picConnect.TabStop = false;
            // 
            // pnlClients
            // 
            this.pnlClients.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlClients.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClients.Controls.Add(this.lvClient);
            this.pnlClients.Controls.Add(this.cmdClientBack);
            this.pnlClients.Location = new System.Drawing.Point(39, 316);
            this.pnlClients.Name = "pnlClients";
            this.pnlClients.Size = new System.Drawing.Size(10, 20);
            this.pnlClients.TabIndex = 33;
            // 
            // lvClient
            // 
            this.lvClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colClientIP,
            this.cloTime});
            this.lvClient.GridLines = true;
            this.lvClient.Location = new System.Drawing.Point(12, 19);
            this.lvClient.Name = "lvClient";
            this.lvClient.Size = new System.Drawing.Size(364, 271);
            this.lvClient.TabIndex = 39;
            this.lvClient.UseCompatibleStateImageBehavior = false;
            this.lvClient.View = System.Windows.Forms.View.Details;
            // 
            // colClientIP
            // 
            this.colClientIP.Text = "ClientIP";
            this.colClientIP.Width = 100;
            // 
            // cloTime
            // 
            this.cloTime.Text = "ConnectAt";
            this.cloTime.Width = 100;
            // 
            // cmdClientBack
            // 
            this.cmdClientBack.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClientBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClientBack.Image = global::COM_SERVER.Properties.Resources.back_icon__2_;
            this.cmdClientBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClientBack.Location = new System.Drawing.Point(289, 296);
            this.cmdClientBack.Name = "cmdClientBack";
            this.cmdClientBack.Size = new System.Drawing.Size(87, 40);
            this.cmdClientBack.TabIndex = 38;
            this.cmdClientBack.Text = "Bac&k";
            this.cmdClientBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClientBack.UseVisualStyleBackColor = true;
            this.cmdClientBack.Click += new System.EventHandler(this.cmdClientBack_Click);
            // 
            // lblConnect
            // 
            this.lblConnect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblConnect.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnect.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblConnect.Location = new System.Drawing.Point(0, 407);
            this.lblConnect.Name = "lblConnect";
            this.lblConnect.Size = new System.Drawing.Size(382, 36);
            this.lblConnect.TabIndex = 31;
            this.lblConnect.Text = "Server Status";
            this.lblConnect.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlServer
            // 
            this.pnlServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlServer.Controls.Add(this.cmdHide);
            this.pnlServer.Controls.Add(this.cmdDbServer);
            this.pnlServer.Controls.Add(this.lblVersion);
            this.pnlServer.Controls.Add(this.cmdClients);
            this.pnlServer.Controls.Add(this.cmdLog);
            this.pnlServer.Controls.Add(this.cmdExit);
            this.pnlServer.Controls.Add(this.cmdDisconnect);
            this.pnlServer.Controls.Add(this.cmdConnect);
            this.pnlServer.Location = new System.Drawing.Point(382, 66);
            this.pnlServer.Name = "pnlServer";
            this.pnlServer.Size = new System.Drawing.Size(144, 377);
            this.pnlServer.TabIndex = 35;
            // 
            // cmdHide
            // 
            this.cmdHide.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdHide.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdHide.Image = global::COM_SERVER.Properties.Resources._49015;
            this.cmdHide.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdHide.Location = new System.Drawing.Point(3, 235);
            this.cmdHide.Name = "cmdHide";
            this.cmdHide.Size = new System.Drawing.Size(136, 40);
            this.cmdHide.TabIndex = 5;
            this.cmdHide.Text = "&Hide Server";
            this.cmdHide.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdHide.UseVisualStyleBackColor = true;
            this.cmdHide.Click += new System.EventHandler(this.cmdHide_Click);
            // 
            // cmdDbServer
            // 
            this.cmdDbServer.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdDbServer.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDbServer.Image = global::COM_SERVER.Properties.Resources.search_database;
            this.cmdDbServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDbServer.Location = new System.Drawing.Point(3, 189);
            this.cmdDbServer.Name = "cmdDbServer";
            this.cmdDbServer.Size = new System.Drawing.Size(136, 40);
            this.cmdDbServer.TabIndex = 4;
            this.cmdDbServer.Text = "&DBSetting";
            this.cmdDbServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDbServer.UseVisualStyleBackColor = true;
            this.cmdDbServer.Click += new System.EventHandler(this.cmdDbServer_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblVersion.Location = new System.Drawing.Point(3, 352);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(140, 22);
            this.lblVersion.TabIndex = 36;
            this.lblVersion.Text = "Version:";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdClients
            // 
            this.cmdClients.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClients.Image = global::COM_SERVER.Properties.Resources.view_icon;
            this.cmdClients.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClients.Location = new System.Drawing.Point(3, 143);
            this.cmdClients.Name = "cmdClients";
            this.cmdClients.Size = new System.Drawing.Size(136, 40);
            this.cmdClients.TabIndex = 3;
            this.cmdClients.Text = "&View Clients";
            this.cmdClients.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdClients.UseVisualStyleBackColor = true;
            this.cmdClients.Click += new System.EventHandler(this.cmdClients_Click);
            // 
            // cmdLog
            // 
            this.cmdLog.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLog.Image = global::COM_SERVER.Properties.Resources.Actions_view_calendar_month_icon;
            this.cmdLog.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdLog.Location = new System.Drawing.Point(3, 97);
            this.cmdLog.Name = "cmdLog";
            this.cmdLog.Size = new System.Drawing.Size(136, 40);
            this.cmdLog.TabIndex = 2;
            this.cmdLog.Text = "&View &Logs";
            this.cmdLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdLog.UseVisualStyleBackColor = true;
            this.cmdLog.Click += new System.EventHandler(this.cmdLog_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdExit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = global::COM_SERVER.Properties.Resources.Close_icon__1_;
            this.cmdExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExit.Location = new System.Drawing.Point(3, 281);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(136, 40);
            this.cmdExit.TabIndex = 6;
            this.cmdExit.Text = "E&xit Server";
            this.cmdExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdDisconnect
            // 
            this.cmdDisconnect.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDisconnect.Image = global::COM_SERVER.Properties.Resources.disconnect_icon;
            this.cmdDisconnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDisconnect.Location = new System.Drawing.Point(3, 51);
            this.cmdDisconnect.Name = "cmdDisconnect";
            this.cmdDisconnect.Size = new System.Drawing.Size(136, 40);
            this.cmdDisconnect.TabIndex = 1;
            this.cmdDisconnect.Text = "&Disconnect";
            this.cmdDisconnect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdDisconnect.UseVisualStyleBackColor = true;
            this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
            // 
            // cmdConnect
            // 
            this.cmdConnect.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConnect.Image = global::COM_SERVER.Properties.Resources.connect_icon;
            this.cmdConnect.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cmdConnect.Location = new System.Drawing.Point(3, 5);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(136, 40);
            this.cmdConnect.TabIndex = 0;
            this.cmdConnect.Text = "&Connect";
            this.cmdConnect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(531, 443);
            this.ControlBox = false;
            this.Controls.Add(this.lblConnect);
            this.Controls.Add(this.pnlServer);
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Communication Server";
            this.Load += new System.EventHandler(this.frmServer_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDisconnect)).EndInit();
            this.pnlDBSeting.ResumeLayout(false);
            this.pnlDBSeting.PerformLayout();
            this.pnlLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picConnect)).EndInit();
            this.pnlClients.ResumeLayout(false);
            this.pnlServer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox picConnect;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Button cmdDisconnect;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.PictureBox picDisconnect;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.Panel pnlServer;
        private System.Windows.Forms.Button cmdClients;
        private System.Windows.Forms.Button cmdLog;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button cmdDbServer;
        private System.Windows.Forms.Panel pnlDBSeting;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lblPwd;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdBack;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.ComboBox cmbSchema;
        private System.Windows.Forms.Button cmdTestCon;
        private System.Windows.Forms.Label lblString;
        private System.Windows.Forms.TextBox txtConString;
        private System.Windows.Forms.Panel pnlClients;
        private System.Windows.Forms.ListView lvClient;
        private System.Windows.Forms.Button cmdClientBack;
        private System.Windows.Forms.ColumnHeader colClientIP;
        private System.Windows.Forms.ColumnHeader cloTime;
        private System.Windows.Forms.Panel pnlLog;
        private System.Windows.Forms.ListView lvLog;
        private System.Windows.Forms.ColumnHeader colClient;
        private System.Windows.Forms.ColumnHeader colData;
        private System.Windows.Forms.Button cmdLogBack;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.Button cmdHide;
        private System.Windows.Forms.Label label1;
    }
}

