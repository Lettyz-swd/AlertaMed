using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlertaMed
{
    public partial class Form17 : Form
    {
        public Form17()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form19 form19 = new Form19();
            form19.StartPosition = FormStartPosition.Manual;
            form19.Location = this.Location;
            form19.Size = this.Size;
            form19.Show();
            this.Close();
        }
    }
}
