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

            Form15 form15 = new Form15();
            form15.StartPosition = FormStartPosition.Manual;
            form15.Location = this.Location;
            form15.Size = this.Size;
            form15.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form16 form16 = new Form16();
            form16.StartPosition = FormStartPosition.Manual;
            form16.Location = this.Location;
            form16.Size = this.Size;
            form16.Show();
            this.Close();
        }
    }
}
