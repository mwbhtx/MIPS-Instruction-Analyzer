using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

enum reg_Index: int {

    i_regZero = 0,
    i_regAt, 
    i_regV0,
    i_regV1,
    i_regA0,
    i_regA1,
    i_regA2,
    i_regA3,
    i_regT0,
    i_regT1,
    i_regT2,
    i_regT3,
    i_reg_NumberOfRegisters

}

namespace MIPS_Instruction_Analyzer
{



    public partial class Main : Form
    {



        /* Initialize Register Array : Values Initialize To Zero */
        int[] registerArray = new int[(int)reg_Index.i_reg_NumberOfRegisters];


        public Main()
        {
            InitializeComponent();


        }

        private void Main_Load(object sender, EventArgs e)
        {

        }


        /* Get Current Register Value From Register Array */
        private int getRegisterValue(int registerIndex)
        {
            // initialize return value
            int registerValue = 0;

            // verify registerIndex argument is within limits of the actual amount of registers our system supports
            if (registerIndex >= 0 && registerIndex < (int) reg_Index.i_reg_NumberOfRegisters) {

                // if within limits, extract current register value from register array using index argument
                registerValue = registerArray[registerIndex]; 

            }

            // return register value
            return registerValue; 
        }


        /* Get Register Array Index From String Represented Register Name */
        private int getRegisterIndexFromString(string registerAsString)
        {
            // Init Register Index Return Value
            int registerIndex = 0;


            // Compare Register As String Argument
            switch (registerAsString)
            {
                case "$zero":

                    registerIndex = (int)reg_Index.i_regZero;
                    break;
                case "$at":

                    registerIndex = (int)reg_Index.i_regAt;
                    break;
                case "$v0":

                    registerIndex = (int)reg_Index.i_regV0;
                    break;
                case "$v1":

                    registerIndex = (int)reg_Index.i_regV1;
                    break;
                case "$a0":

                    registerIndex = (int)reg_Index.i_regA0;
                    break;
                case "$a1":

                    registerIndex = (int)reg_Index.i_regA1;
                    break;
                case "$a2":

                    registerIndex = (int)reg_Index.i_regA2;
                    break;
                case "$a3":

                    registerIndex = (int)reg_Index.i_regA3;
                    break;
                case "$t0":

                    registerIndex = (int)reg_Index.i_regT0;
                    break;
                case "$t1":

                    registerIndex = (int)reg_Index.i_regT1;
                    break;
                case "$t2":

                    registerIndex = (int)reg_Index.i_regT2;
                    break;
                case "$t3":

                    registerIndex = (int)reg_Index.i_regT3;
                    break;
                default:
                    registerIndex = 0xFF;
                    break;

            }

            // On Return, Check If Values Is 0xFF. A returned value of 0xFF represents no string match. 
            return registerIndex;

        }
    }
}
