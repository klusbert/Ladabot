namespace KonjoBot.Forms.Settings
{
    partial class AddWaypoint
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
            this.FastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.TextBox4 = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.ComboBox2 = new System.Windows.Forms.ComboBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // FastColoredTextBox1
            // 
            this.FastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(109, 15);
            this.FastColoredTextBox1.CommentPrefix = "\'";
            this.FastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FastColoredTextBox1.Language = FastColoredTextBoxNS.Language.VB;
            this.FastColoredTextBox1.LeftBracket = '(';
            this.FastColoredTextBox1.Location = new System.Drawing.Point(132, 15);
            this.FastColoredTextBox1.Name = "FastColoredTextBox1";
            this.FastColoredTextBox1.RightBracket = ')';
            this.FastColoredTextBox1.Size = new System.Drawing.Size(465, 247);
            this.FastColoredTextBox1.TabIndex = 30;
            this.FastColoredTextBox1.Text = "NextWayPoint";
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new System.Drawing.Point(5, 55);
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new System.Drawing.Size(100, 20);
            this.TextBox4.TabIndex = 29;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(2, 39);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(33, 13);
            this.Label7.TabIndex = 28;
            this.Label7.Text = "Label";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(129, -1);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(131, 13);
            this.Label6.TabIndex = 27;
            this.Label6.Text = "When Waypoint Reached";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(2, -1);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(48, 13);
            this.Label5.TabIndex = 26;
            this.Label5.Text = "Location";
            // 
            // ComboBox2
            // 
            this.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox2.FormattingEnabled = true;
            this.ComboBox2.Items.AddRange(new object[] {
            "PlayerLocation",
            "North",
            "South",
            "East",
            "West"});
            this.ComboBox2.Location = new System.Drawing.Point(5, 15);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new System.Drawing.Size(121, 21);
            this.ComboBox2.TabIndex = 25;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(5, 268);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(592, 23);
            this.Button1.TabIndex = 24;
            this.Button1.Text = "Add/Done";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(4, 76);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(31, 13);
            this.Label4.TabIndex = 23;
            this.Label4.Text = "Type";
            // 
            // ComboBox1
            // 
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Items.AddRange(new object[] {
            "Walk",
            "Shovel",
            "Rope",
            "UpUse",
            "DownUse",
            "Door"});
            this.ComboBox1.Location = new System.Drawing.Point(7, 91);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(69, 21);
            this.ComboBox1.TabIndex = 22;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(4, 208);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(55, 13);
            this.Label3.TabIndex = 21;
            this.Label3.Text = "LocationZ";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(4, 167);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(55, 13);
            this.Label2.TabIndex = 20;
            this.Label2.Text = "LocationY";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(2, 122);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(55, 13);
            this.Label1.TabIndex = 19;
            this.Label1.Text = "LocationX";
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new System.Drawing.Point(7, 224);
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new System.Drawing.Size(52, 20);
            this.TextBox3.TabIndex = 18;
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(7, 183);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(100, 20);
            this.TextBox2.TabIndex = 17;
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(5, 138);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(100, 20);
            this.TextBox1.TabIndex = 16;
            // 
            // AddWaypoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 292);
            this.Controls.Add(this.FastColoredTextBox1);
            this.Controls.Add(this.TextBox4);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.ComboBox2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TextBox3);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.TextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddWaypoint";
            this.Text = "AddWaypoint";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddWaypoint_FormClosing);
            this.Load += new System.EventHandler(this.AddWaypoint_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal FastColoredTextBoxNS.FastColoredTextBox FastColoredTextBox1;
        internal System.Windows.Forms.TextBox TextBox4;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox ComboBox2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox ComboBox1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TextBox3;
        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.TextBox TextBox1;
    }
}