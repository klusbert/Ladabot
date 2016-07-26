namespace KonjoBot.Forms
{
    partial class ScriptForm
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
            this.Label3 = new System.Windows.Forms.Label();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClearToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ListBox2 = new System.Windows.Forms.ListBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.ViewCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.AutoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CurePosionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HealingLowHighToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ManaTrainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HealingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExamplesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExecuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.ContextMenuStrip2.SuspendLayout();
            this.ContextMenuStrip1.SuspendLayout();
            this.MenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(468, 382);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(82, 13);
            this.Label3.TabIndex = 17;
            this.Label3.Text = "Running Scripts";
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.CopyToolStripMenuItem.Text = "Copy";
            // 
            // ContextMenuStrip2
            // 
            this.ContextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyToolStripMenuItem,
            this.ClearToolStripMenuItem1});
            this.ContextMenuStrip2.Name = "ContextMenuStrip2";
            this.ContextMenuStrip2.Size = new System.Drawing.Size(103, 48);
            // 
            // ClearToolStripMenuItem1
            // 
            this.ClearToolStripMenuItem1.Name = "ClearToolStripMenuItem1";
            this.ClearToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.ClearToolStripMenuItem1.Text = "Clear";
            // 
            // ListBox2
            // 
            this.ListBox2.ContextMenuStrip = this.ContextMenuStrip2;
            this.ListBox2.FormattingEnabled = true;
            this.ListBox2.Location = new System.Drawing.Point(12, 401);
            this.ListBox2.Name = "ListBox2";
            this.ListBox2.Size = new System.Drawing.Size(441, 82);
            this.ListBox2.TabIndex = 16;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(9, 385);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(62, 13);
            this.Label2.TabIndex = 15;
            this.Label2.Text = "Error check";
            // 
            // ViewCodeToolStripMenuItem
            // 
            this.ViewCodeToolStripMenuItem.Name = "ViewCodeToolStripMenuItem";
            this.ViewCodeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.ViewCodeToolStripMenuItem.Text = "View Code";
            this.ViewCodeToolStripMenuItem.Click += new System.EventHandler(this.ViewCodeToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.DeleteToolStripMenuItem.Text = "Delete";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // StartToolStripMenuItem
            // 
            this.StartToolStripMenuItem.Name = "StartToolStripMenuItem";
            this.StartToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.StartToolStripMenuItem.Text = "Start/Stop";
            this.StartToolStripMenuItem.Click += new System.EventHandler(this.StartToolStripMenuItem_Click);
            // 
            // ContextMenuStrip1
            // 
            this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartToolStripMenuItem,
            this.DeleteToolStripMenuItem,
            this.ViewCodeToolStripMenuItem});
            this.ContextMenuStrip1.Name = "ContextMenuStrip1";
            this.ContextMenuStrip1.Size = new System.Drawing.Size(131, 70);
            // 
            // ListBox1
            // 
            this.ListBox1.ContextMenuStrip = this.ContextMenuStrip1;
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.Location = new System.Drawing.Point(471, 398);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(287, 82);
            this.ListBox1.TabIndex = 14;
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(12, 362);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(114, 20);
            this.TextBox1.TabIndex = 13;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 346);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(35, 13);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "Name";
            // 
            // AutoToolStripMenuItem
            // 
            this.AutoToolStripMenuItem.Name = "AutoToolStripMenuItem";
            this.AutoToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.AutoToolStripMenuItem.Text = "Auto";
            // 
            // CurePosionToolStripMenuItem
            // 
            this.CurePosionToolStripMenuItem.Name = "CurePosionToolStripMenuItem";
            this.CurePosionToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.CurePosionToolStripMenuItem.Text = "Cure posion";
            // 
            // CureToolStripMenuItem
            // 
            this.CureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurePosionToolStripMenuItem});
            this.CureToolStripMenuItem.Name = "CureToolStripMenuItem";
            this.CureToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.CureToolStripMenuItem.Text = "Cure";
            // 
            // HealingLowHighToolStripMenuItem
            // 
            this.HealingLowHighToolStripMenuItem.Name = "HealingLowHighToolStripMenuItem";
            this.HealingLowHighToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.HealingLowHighToolStripMenuItem.Text = "Healing Low / high";
            // 
            // ManaTrainToolStripMenuItem
            // 
            this.ManaTrainToolStripMenuItem.Name = "ManaTrainToolStripMenuItem";
            this.ManaTrainToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.ManaTrainToolStripMenuItem.Text = "Mana Train";
            // 
            // HealingToolStripMenuItem
            // 
            this.HealingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ManaTrainToolStripMenuItem,
            this.HealingLowHighToolStripMenuItem});
            this.HealingToolStripMenuItem.Name = "HealingToolStripMenuItem";
            this.HealingToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.HealingToolStripMenuItem.Text = "Healing";
            // 
            // ExamplesToolStripMenuItem
            // 
            this.ExamplesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HealingToolStripMenuItem,
            this.CureToolStripMenuItem,
            this.AutoToolStripMenuItem});
            this.ExamplesToolStripMenuItem.Name = "ExamplesToolStripMenuItem";
            this.ExamplesToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.ExamplesToolStripMenuItem.Text = "Examples";
            // 
            // ExecuteToolStripMenuItem
            // 
            this.ExecuteToolStripMenuItem.Name = "ExecuteToolStripMenuItem";
            this.ExecuteToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.ExecuteToolStripMenuItem.Text = "Execute";
            this.ExecuteToolStripMenuItem.Click += new System.EventHandler(this.ExecuteToolStripMenuItem_Click);
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.NewToolStripMenuItem.Text = "New";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.SaveToolStripMenuItem.Text = "Save";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.OpenToolStripMenuItem.Text = "Open";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.NewToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.ExecuteToolStripMenuItem,
            this.ExamplesToolStripMenuItem});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(768, 24);
            this.MenuStrip1.TabIndex = 10;
            this.MenuStrip1.Text = "MenuStrip1";
            // 
            // FastColoredTextBox1
            // 
            this.FastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.FastColoredTextBox1.CommentPrefix = "\'";
            this.FastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FastColoredTextBox1.Language = FastColoredTextBoxNS.Language.VB;
            this.FastColoredTextBox1.LeftBracket = '(';
            this.FastColoredTextBox1.Location = new System.Drawing.Point(12, 27);
            this.FastColoredTextBox1.Name = "FastColoredTextBox1";
            this.FastColoredTextBox1.RightBracket = ')';
            this.FastColoredTextBox1.Size = new System.Drawing.Size(756, 316);
            this.FastColoredTextBox1.TabIndex = 31;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(132, 360);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(213, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "Global Variables";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // ScriptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 489);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FastColoredTextBox1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.ListBox2);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.ListBox1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.MenuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ScriptForm";
            this.Text = "ScriptForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptForm_FormClosing);
            this.Load += new System.EventHandler(this.ScriptForm_Load);
            this.ContextMenuStrip2.ResumeLayout(false);
            this.ContextMenuStrip1.ResumeLayout(false);
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip2;
        internal System.Windows.Forms.ToolStripMenuItem ClearToolStripMenuItem1;
        internal System.Windows.Forms.ListBox ListBox2;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ToolStripMenuItem ViewCodeToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem StartToolStripMenuItem;
        internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
        internal System.Windows.Forms.ListBox ListBox1;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ToolStripMenuItem AutoToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem CurePosionToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem CureToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem HealingLowHighToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ManaTrainToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem HealingToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ExamplesToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ExecuteToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        internal System.Windows.Forms.MenuStrip MenuStrip1;
        internal FastColoredTextBoxNS.FastColoredTextBox FastColoredTextBox1;
        private System.Windows.Forms.Button button1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}