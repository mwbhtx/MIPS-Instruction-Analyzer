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

        /* Initialize Register Array : Values Initialize To Zero */
        int[] registerArray = new int[(int)reg_Index.i_reg_NumberOfRegisters];



        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private bool validateRegisterIndex(int registerIndex)
        {
            // Initialize status flag
            bool processSuccessful = false;


            // verify registerIndex argument is within limits of the actual amount of registers our system supports
            if (registerIndex >= 0 && registerIndex < (int)reg_Index.i_reg_NumberOfRegisters)
            {
                // Set flag that index is valid
                processSuccessful = true;

            }

            return processSuccessful; 

        }


        /* Set New Value Into Register Array */
        private bool setRegisterValue(int registerIndex, int registerValue)
        {
            bool processSuccessful = false;



            // verify registerIndex argument is within limits of the actual amount of registers our system supports
            if (registerIndex >= 0 && registerIndex < (int)reg_Index.i_reg_NumberOfRegisters)
            {

                // if within limits, extract current register value from register array using index argument
                registerValue = registerArray[registerIndex];

            }


            // Return Flag
            return processSuccessful; 
        }

        /* Get Current Register Value From Register Array */
        private Register_Data getRegisterValue(Register_Data regStruct)
        {

            // Get Value From Register Array Using Passed regStruct Index
            regStruct.regValue = registerArray[regStruct.regIndex];

            // Set Flag That Data Is Set
            regStruct.regValueSet = true; 

            // return register value
            return regStruct; 
        }


        /* Get Register Array Index From String Represented Register Name */
        private Register_Data getRegisterIndexFromString(string registerAsString, ref Register_Data regStruct)
        {
            // Set Initial Value That Index Is Valid
            regStruct.regIndexSet = true; 

            // Compare Register As String Argument, if match found assign the associated index to return value
            switch (registerAsString)
            {
                case "$zero":

                    regStruct.regIndex = (int)reg_Index.i_regZero;
                    break;
                case "$at":

                    regStruct.regIndex = (int)reg_Index.i_regAt;
                    break;
                case "$v0":

                    regStruct.regIndex = (int)reg_Index.i_regV0;
                    break;
                case "$v1":

                    regStruct.regIndex = (int)reg_Index.i_regV1;
                    break;
                case "$a0":

                    regStruct.regIndex = (int)reg_Index.i_regA0;
                    break;
                case "$a1":

                    regStruct.regIndex = (int)reg_Index.i_regA1;
                    break;
                case "$a2":

                    regStruct.regIndex = (int)reg_Index.i_regA2;
                    break;
                case "$a3":

                    regStruct.regIndex = (int)reg_Index.i_regA3;
                    break;
                case "$t0":

                    regStruct.regIndex = (int)reg_Index.i_regT0;
                    break;
                case "$t1":

                    regStruct.regIndex = (int)reg_Index.i_regT1;
                    break;
                case "$t2":

                    regStruct.regIndex = (int)reg_Index.i_regT2;
                    break;
                case "$t3":

                    regStruct.regIndex = (int)reg_Index.i_regT3;
                    break;
                default:

                    // If No Match, Indicate Register Index Not Valid
                    regStruct.regIndexSet = false;
                    break;

            }

            // On Return, Check If Values Is 0xFF. A returned value of 0xFF represents no string match. 
            return regStruct;

        }

        
        /* Split String Into Instruction Array */
        public new string[] getInstructionsFromString(string instructionString)
        {
            // Initialize Returned Array
            var newArray = new string[4];

            // Split String

            /* Debugging */

            /* Acceptable Strings 
             * var testString1 = "add $v0, $v1";
             * var testString2 = "add $v0 $v1";
             * 
             */

            // Return Split String
            return newArray;
        }

        /* 
         * 
         * Instruction Send Button Click Callback Function 
         * 
         */
            private void sendInstructionButton_Click(object sender, EventArgs e)
        {

            /* Initialize Opcode Object */
            Opcode_Data op = new Opcode_Data();

            /* Register Structures */
            Register_Data rt = new Register_Data(); 
            Register_Data rd = new Register_Data();
            Register_Data rs = new Register_Data();
            

            // R-Type: opcode, rs, rt, rd
            // I-Type: opcode, rs, rt, imm
            // J-Type: opcode, address


            // 1. Read Instruction Text Box
            var instructionString = instructBox.Text;

            // 2. Get Array Of Instruction Entries
            var instructionArray = getInstructionsFromString(instructionString);

            // [ add, $s0, $s1, $s2 ] 

            // 3. Validate OpCode ( Read Add From Split String ), Also Make Sure It Matches Supported Types

            // 4. Determine Instruction Type

            // 5.a : In case of R-Type : Get Index Of RS
            // 5.b : In case of R-Type : Get Index Of RS
            // 5.c : In case of R-Type : Get Index Of RS

            // 6.a :  Get Index To Global Array
            // 6.b :  Get Index To Global Array
            // 6.c :  Get Index To Global Array

        }
    }


    // Class To Manage Register Data Passed Around
    public class Register_Data
    {
        public int regValue;
        public int regIndex;
        public bool regValueSet;
        public bool regIndexSet;

        public Register_Data()
        {
            // Initialize class flags to false 
            this.regValueSet = false;
            this.regIndexSet = false;
        }
    }


    // Class To Manage Register Data Passed Around
    public class Opcode_Data
    {
        public string opCodeString;
        public bool opCodeValid;
        public bool rType;
        public bool iType;
        public bool jType;

        public Opcode_Data()
        {
            // Initialize class flags to false 
            this.opCodeValid = false;
        }

    }


    // Enum To Track Valid Register Array Indices
    enum reg_Index : int
    {

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

}
