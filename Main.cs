using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MIPS_Instruction_Analyzer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            // Store the number found in the numBox to new variable
            decimal num = numBox.Value;

            // Convert num variable into string and store into new variable
            string numToString = num.ToString();

            // Show string variable inside textBox box
            textBox.Text = numToString; 

        }
    }
}
