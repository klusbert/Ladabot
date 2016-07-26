using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KonjoBot.Objects;
namespace KonjoBot.Forms
{
    public partial class HudForm : Form
    {
        Client client;
        public HudForm(Client cl)
        {
            InitializeComponent();
            client = cl;
            this.FormClosing += HudForm_FormClosing;
            comboBox1.SelectedIndex = 0;
        }

        void HudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client.Forms.CloseForms)
            {
                e.Cancel = false;

            }
            else
            {
                e.Cancel = true;
                this.Hide();
            }
          
        }

        private void Hud_Load(object sender, EventArgs e)
        {
            hScrollBar1.ValueChanged += hScrollBar1_ValueChanged;
            hScrollBar2.ValueChanged += hScrollBar2_ValueChanged;
            hScrollBar3.ValueChanged += hScrollBar3_ValueChanged;

        }
        private void hScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = GetColor((byte)hScrollBar1.Value, (byte)hScrollBar2.Value, (byte)hScrollBar3.Value);
        }

        private void hScrollBar3_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = GetColor((byte)hScrollBar1.Value, (byte)hScrollBar2.Value, (byte)hScrollBar3.Value);
        }

        void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = GetColor((byte)hScrollBar1.Value, (byte)hScrollBar2.Value, (byte)hScrollBar3.Value);

        }
        private Image GetColor(byte red, byte green, byte blue)
        {
            Color clr = ProcessColors(red, green, blue);
            Bitmap b = new Bitmap(200, 25);

            for (int x = 0; x < 200; x++)
            {
                for (int y = 0; y < 25; y++)
                {
                    b.SetPixel(x, y, clr);

                }
            }
            return b;


        }
        private Color ProcessColors(byte red, byte green, byte blue)
        {
            return Color.FromArgb(red, green, blue);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ushort font = 1;
            switch (comboBox1.Text)
            {
                case "Normal":
                    font = 1;
                break;
                case "NormalWithBorder":
                      font = 2;
                break;
                case "Small":
                font = 3;
                break;
                case "Wierd":
                    font = 4;
                break;
           }
            if (textBox1.Text != "" && textBox2.Text != "")
            {
             

            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
