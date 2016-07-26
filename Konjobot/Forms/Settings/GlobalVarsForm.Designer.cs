namespace KonjoBot.Forms.Settings
{
    partial class GlobalVarsForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FastColoredTextBox1
            // 
            this.FastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.FastColoredTextBox1.CommentPrefix = "\'";
            this.FastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FastColoredTextBox1.Language = FastColoredTextBoxNS.Language.VB;
            this.FastColoredTextBox1.LeftBracket = '(';
            this.FastColoredTextBox1.Location = new System.Drawing.Point(12, 2);
            this.FastColoredTextBox1.Name = "FastColoredTextBox1";
            this.FastColoredTextBox1.RightBracket = ')';
            this.FastColoredTextBox1.Size = new System.Drawing.Size(449, 316);
            this.FastColoredTextBox1.TabIndex = 32;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 324);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(438, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GlobalVarsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 352);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FastColoredTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GlobalVarsForm";
            this.Text = "GlobalVarsForm";
            this.Load += new System.EventHandler(this.GlobalVarsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal FastColoredTextBoxNS.FastColoredTextBox FastColoredTextBox1;
        private System.Windows.Forms.Button button1;
    }
}