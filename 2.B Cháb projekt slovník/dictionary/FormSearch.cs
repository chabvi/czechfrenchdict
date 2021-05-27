using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dictionary
{
    public partial class FormSearch : Form
    {
        public FormSearch()
        {
            InitializeComponent();
        }
        private void Accent_Click(object sender, EventArgs e)
        {
            string accent = (sender as Button).Text;
            tbSearchbar.Text += accent;
        }
    }
}
