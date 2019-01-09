using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tunnel.Word.Test;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UnitTest4 test = new UnitTest4();
            //UnitTest2 test = new UnitTest2();
            //test.TestTunnelMonth();
            test.test();
            
        }
    }
}
