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

        private void button2_Click(object sender, EventArgs e)
        {
            var instructString = instructBox.Text;
            instructString.Trim();

            var instructArray = instructString.Split(' ');


            for (int x = 0; x < instructArray.Length; x++ )
            {
                Console.WriteLine(instructArray[x]);
            }
        }
    }
}
