using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KonjoBot.Forms.Settings
{
    public partial class GetHotkey : Form
    {
        private static GetHotkey _form;
        private static Keys _keys;
        private static bool _iskeySelected = false;

        public GetHotkey()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void GetHotkey_Load(object sender, EventArgs e)
        {
          
        }


        public static Keys GetKey()
        {
            _form = new GetHotkey();
            _iskeySelected = false;
            _form.KeyDown += _form_KeyDown;
           
            _form.ShowDialog();
            if(_iskeySelected)
            {
                return _keys;                
            }
            else
            {
               return Keys.None;
            }
        }

        static void _form_KeyDown(object sender, KeyEventArgs e)
        {
            _form.Label1.ForeColor = Color.Green;
            _form.Label1.Text = e.KeyCode.ToString();
            _keys = e.KeyCode;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _iskeySelected = true;
            _form.Dispose();
        }
    }
}
