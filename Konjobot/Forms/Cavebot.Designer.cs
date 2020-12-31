namespace KonjoBot.Forms
{
    partial class Cavebot
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.walkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ropeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shovelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.upUseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downUseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.doorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rampHoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.northToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.southToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.westToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.macheteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.northToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.southToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eastToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.westToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.costumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 177);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Waypoints";
            // 
            // listBox1
            // 
            this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(185, 160);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 142);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.walkToolStripMenuItem,
            this.ropeToolStripMenuItem,
            this.shovelToolStripMenuItem,
            this.toolStripSeparator2,
            this.upUseToolStripMenuItem,
            this.downUseToolStripMenuItem,
            this.toolStripSeparator3,
            this.doorToolStripMenuItem,
            this.rampHoleToolStripMenuItem,
            this.macheteToolStripMenuItem,
            this.toolStripSeparator4,
            this.costumToolStripMenuItem});
            this.addToolStripMenuItem.Image = global::KonjoBot.Properties.Resources.Add_v2;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // walkToolStripMenuItem
            // 
            this.walkToolStripMenuItem.Name = "walkToolStripMenuItem";
            this.walkToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.walkToolStripMenuItem.Text = "Walk";
            this.walkToolStripMenuItem.Click += new System.EventHandler(this.walkToolStripMenuItem_Click);
            // 
            // ropeToolStripMenuItem
            // 
            this.ropeToolStripMenuItem.Name = "ropeToolStripMenuItem";
            this.ropeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ropeToolStripMenuItem.Text = "Rope";
            this.ropeToolStripMenuItem.Click += new System.EventHandler(this.ropeToolStripMenuItem_Click);
            // 
            // shovelToolStripMenuItem
            // 
            this.shovelToolStripMenuItem.Name = "shovelToolStripMenuItem";
            this.shovelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.shovelToolStripMenuItem.Text = "Shovel";
            this.shovelToolStripMenuItem.Click += new System.EventHandler(this.shovelToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // upUseToolStripMenuItem
            // 
            this.upUseToolStripMenuItem.Name = "upUseToolStripMenuItem";
            this.upUseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.upUseToolStripMenuItem.Text = "UpUse";
            this.upUseToolStripMenuItem.Click += new System.EventHandler(this.upUseToolStripMenuItem_Click);
            // 
            // downUseToolStripMenuItem
            // 
            this.downUseToolStripMenuItem.Name = "downUseToolStripMenuItem";
            this.downUseToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.downUseToolStripMenuItem.Text = "DownUse";
            this.downUseToolStripMenuItem.Click += new System.EventHandler(this.downUseToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // doorToolStripMenuItem
            // 
            this.doorToolStripMenuItem.Name = "doorToolStripMenuItem";
            this.doorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.doorToolStripMenuItem.Text = "Door";
            this.doorToolStripMenuItem.Click += new System.EventHandler(this.doorToolStripMenuItem_Click);
            // 
            // rampHoleToolStripMenuItem
            // 
            this.rampHoleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.northToolStripMenuItem,
            this.southToolStripMenuItem,
            this.eastToolStripMenuItem,
            this.westToolStripMenuItem});
            this.rampHoleToolStripMenuItem.Name = "rampHoleToolStripMenuItem";
            this.rampHoleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rampHoleToolStripMenuItem.Text = "Ramp/Hole";
            // 
            // northToolStripMenuItem
            // 
            this.northToolStripMenuItem.Name = "northToolStripMenuItem";
            this.northToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.northToolStripMenuItem.Text = "North";
            this.northToolStripMenuItem.Click += new System.EventHandler(this.northToolStripMenuItem_Click);
            // 
            // southToolStripMenuItem
            // 
            this.southToolStripMenuItem.Name = "southToolStripMenuItem";
            this.southToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.southToolStripMenuItem.Text = "South";
            this.southToolStripMenuItem.Click += new System.EventHandler(this.southToolStripMenuItem_Click);
            // 
            // eastToolStripMenuItem
            // 
            this.eastToolStripMenuItem.Name = "eastToolStripMenuItem";
            this.eastToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.eastToolStripMenuItem.Text = "East";
            this.eastToolStripMenuItem.Click += new System.EventHandler(this.eastToolStripMenuItem_Click);
            // 
            // westToolStripMenuItem
            // 
            this.westToolStripMenuItem.Name = "westToolStripMenuItem";
            this.westToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.westToolStripMenuItem.Text = "West";
            this.westToolStripMenuItem.Click += new System.EventHandler(this.westToolStripMenuItem_Click);
            // 
            // macheteToolStripMenuItem
            // 
            this.macheteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.northToolStripMenuItem1,
            this.southToolStripMenuItem1,
            this.eastToolStripMenuItem1,
            this.westToolStripMenuItem1});
            this.macheteToolStripMenuItem.Name = "macheteToolStripMenuItem";
            this.macheteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.macheteToolStripMenuItem.Text = "Machete";
            this.macheteToolStripMenuItem.Click += new System.EventHandler(this.macheteToolStripMenuItem_Click);
            // 
            // northToolStripMenuItem1
            // 
            this.northToolStripMenuItem1.Name = "northToolStripMenuItem1";
            this.northToolStripMenuItem1.Size = new System.Drawing.Size(105, 22);
            this.northToolStripMenuItem1.Text = "North";
            this.northToolStripMenuItem1.Click += new System.EventHandler(this.northToolStripMenuItem1_Click);
            // 
            // southToolStripMenuItem1
            // 
            this.southToolStripMenuItem1.Name = "southToolStripMenuItem1";
            this.southToolStripMenuItem1.Size = new System.Drawing.Size(105, 22);
            this.southToolStripMenuItem1.Text = "South";
            this.southToolStripMenuItem1.Click += new System.EventHandler(this.southToolStripMenuItem1_Click);
            // 
            // eastToolStripMenuItem1
            // 
            this.eastToolStripMenuItem1.Name = "eastToolStripMenuItem1";
            this.eastToolStripMenuItem1.Size = new System.Drawing.Size(105, 22);
            this.eastToolStripMenuItem1.Text = "East";
            this.eastToolStripMenuItem1.Click += new System.EventHandler(this.eastToolStripMenuItem1_Click);
            // 
            // westToolStripMenuItem1
            // 
            this.westToolStripMenuItem1.Name = "westToolStripMenuItem1";
            this.westToolStripMenuItem1.Size = new System.Drawing.Size(105, 22);
            this.westToolStripMenuItem1.Text = "West";
            this.westToolStripMenuItem1.Click += new System.EventHandler(this.westToolStripMenuItem1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // costumToolStripMenuItem
            // 
            this.costumToolStripMenuItem.Name = "costumToolStripMenuItem";
            this.costumToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.costumToolStripMenuItem.Text = "Costum";
            this.costumToolStripMenuItem.Click += new System.EventHandler(this.costumToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Image = global::KonjoBot.Properties.Resources.Clear;
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::KonjoBot.Properties.Resources.Erase;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::KonjoBot.Properties.Resources.Save_file;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Image = global::KonjoBot.Properties.Resources.Open_file;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Location = new System.Drawing.Point(0, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 95);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Looter";
            // 
            // listBox2
            // 
            this.listBox2.ContextMenuStrip = this.contextMenuStrip2;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(6, 17);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(185, 69);
            this.listBox2.TabIndex = 2;
            this.listBox2.DoubleClick += new System.EventHandler(this.listBox2_DoubleClick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.clearToolStripMenuItem1,
            this.deleteToolStripMenuItem1,
            this.toolStripSeparator5,
            this.saveToolStripMenuItem1,
            this.loadToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(108, 120);
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Image = global::KonjoBot.Properties.Resources.Add_v2;
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.addToolStripMenuItem1.Text = "Add";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.addToolStripMenuItem1_Click);
            // 
            // clearToolStripMenuItem1
            // 
            this.clearToolStripMenuItem1.Image = global::KonjoBot.Properties.Resources.Clear;
            this.clearToolStripMenuItem1.Name = "clearToolStripMenuItem1";
            this.clearToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.clearToolStripMenuItem1.Text = "Clear";
            this.clearToolStripMenuItem1.Click += new System.EventHandler(this.clearToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Image = global::KonjoBot.Properties.Resources.Erase;
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(104, 6);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Image = global::KonjoBot.Properties.Resources.Save_file;
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            // 
            // loadToolStripMenuItem1
            // 
            this.loadToolStripMenuItem1.Image = global::KonjoBot.Properties.Resources.Open_file;
            this.loadToolStripMenuItem1.Name = "loadToolStripMenuItem1";
            this.loadToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.loadToolStripMenuItem1.Text = "Load";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBox3);
            this.groupBox3.Location = new System.Drawing.Point(0, 279);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(198, 109);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Attacker";
            // 
            // listBox3
            // 
            this.listBox3.ContextMenuStrip = this.contextMenuStrip3;
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(6, 19);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(185, 82);
            this.listBox3.TabIndex = 0;
            this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            this.listBox3.DoubleClick += new System.EventHandler(this.listBox3_DoubleClick);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem2,
            this.clearToolStripMenuItem2,
            this.deleteToolStripMenuItem2,
            this.toolStripSeparator6,
            this.saveToolStripMenuItem2,
            this.loadToolStripMenuItem2});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(108, 120);
            // 
            // addToolStripMenuItem2
            // 
            this.addToolStripMenuItem2.Image = global::KonjoBot.Properties.Resources.Add_v2;
            this.addToolStripMenuItem2.Name = "addToolStripMenuItem2";
            this.addToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
            this.addToolStripMenuItem2.Text = "Add";
            this.addToolStripMenuItem2.Click += new System.EventHandler(this.addToolStripMenuItem2_Click);
            // 
            // clearToolStripMenuItem2
            // 
            this.clearToolStripMenuItem2.Image = global::KonjoBot.Properties.Resources.Clear;
            this.clearToolStripMenuItem2.Name = "clearToolStripMenuItem2";
            this.clearToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
            this.clearToolStripMenuItem2.Text = "Clear";
            this.clearToolStripMenuItem2.Click += new System.EventHandler(this.clearToolStripMenuItem2_Click);
            // 
            // deleteToolStripMenuItem2
            // 
            this.deleteToolStripMenuItem2.Image = global::KonjoBot.Properties.Resources.Erase;
            this.deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            this.deleteToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem2.Text = "Delete";
            this.deleteToolStripMenuItem2.Click += new System.EventHandler(this.deleteToolStripMenuItem2_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(104, 6);
            // 
            // saveToolStripMenuItem2
            // 
            this.saveToolStripMenuItem2.Image = global::KonjoBot.Properties.Resources.Save_file;
            this.saveToolStripMenuItem2.Name = "saveToolStripMenuItem2";
            this.saveToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
            this.saveToolStripMenuItem2.Text = "Save";
            // 
            // loadToolStripMenuItem2
            // 
            this.loadToolStripMenuItem2.Image = global::KonjoBot.Properties.Resources.Open_file;
            this.loadToolStripMenuItem2.Name = "loadToolStripMenuItem2";
            this.loadToolStripMenuItem2.Size = new System.Drawing.Size(107, 22);
            this.loadToolStripMenuItem2.Text = "Load";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 394);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(56, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Follow";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 417);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(47, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Loot";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(6, 440);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(57, 17);
            this.checkBox3.TabIndex = 5;
            this.checkBox3.Text = "Attack";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(113, 435);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 22);
            this.button1.TabIndex = 6;
            this.button1.Text = "Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Cavebot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 462);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Cavebot";
            this.Text = "Cavebot";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Cavebot_FormClosing);
            this.Load += new System.EventHandler(this.Cavebot_Load);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBox3;
        public System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem walkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ropeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shovelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem upUseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downUseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem doorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rampHoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem northToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem southToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem westToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem macheteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem northToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem southToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eastToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem westToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem costumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem2;
        private System.Windows.Forms.Timer timer1;
    }
}