using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        /* Get Current Register Value From Register Array */
        private void setRegisterValue(Register_Data regStruct)
        {

            // Set Value Into Register Array Using Passed regStruct Index
            registerArray[regStruct.regIndex] = regStruct.regValue;

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

        public void alertSystemError()
        {
            MessageBox.Show("Error processing data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        /* Split String Into Instruction Array */
        public new bool getInstructionsFromString(string instructionString, ref List<string> inputArgArray)
        {
            bool successflag = true;

            List<char> word = new List<char>();

            // Trim Any Starting & Ending White Space
            instructionString = instructionString.Trim();

            // Loop Through String Searching For Words, Store Word If White Space is Found

            bool whiteSpaceFound = false;           // Flags To Indicate A Word Found

            foreach (char c in instructionString)
            {
                if ((c != ' ') && (c != ','))
                {
                    whiteSpaceFound = false;
                    word.Add(c);
                }
                else if (!whiteSpaceFound)
                {
                    whiteSpaceFound = true;
                    inputArgArray.Add(string.Join("", word.ToArray()));
                    word.Clear();
                }
            }

            // Get Last Word That Was Stored 
            if (word.Count > 0)
            {
                inputArgArray.Add(string.Join("", word.ToArray()));
            }

            if (inputArgArray.Count > 4 || inputArgArray.Count < 3)
            {
                // Input Error
                successflag = false;
            }

            // Return Split String
            return successflag;
        }

        /* Validate OpCode, Determine Type */
        public void validateOpCodeAndDetermineType(ref Opcode_Data opObj, string opCodeString)
        {
            // Assign Op Code String To Object
            opObj.opCodeString = opCodeString;

            // Test For RType Match
            foreach (string opCode in opObj.rTypeArr)
            {
                // Console.WriteLine(op.opCodeString);
                if (opCode == opObj.opCodeString)
                {
                    opObj.rType = true;
                    opObj.opCodeValid = true;
                    return;
                }
            }


            // Test For IType Match
            foreach (string opCode in opObj.iTypeArr)
            {
                // Console.WriteLine(op.opCodeString);
                if (opCode == opObj.opCodeString)
                {
                    opObj.iType = true;
                    opObj.opCodeValid = true;
                    return;
                }
            }


        }

        /* Assign register values based on op-type */
        public void assignRegisterIndexesFromInput(List<string> args, ref Register_Data imm, ref Register_Data rd, ref Register_Data rt, ref Register_Data rs, ref Opcode_Data op)
        {
            /*
             * 
            // R-Type: opcode, rs, rt, rd
            // I-Type: opcode, rt, imm(rs)
            // J-Type: opcode, address
            *
            */

            if (op.iType)
            {
                getRegisterIndexFromString(args[1], ref rs);
                getRegisterIndexFromString(args[2], ref rt);
                getRegisterIndexFromString(args[2], ref imm);
            }
            else if (op.rType)
            {
                getRegisterIndexFromString(args[1], ref rt);
                getRegisterIndexFromString(args[3], ref rs);
                getRegisterIndexFromString(args[3], ref rd);
            }

        }

        public void processITtypeOperation(ref Register_Data imm, ref Register_Data rt, ref Register_Data rs, ref Opcode_Data op)
        {
            switch (op.opCodeString)
            {
                case "sw":
                    // Store rt into register pointed to by (rs + imm)

                    break;
                case "lw":
                    // Load data pointed to by (rs + imm) into register rt

                    break;
            }
        }

        public void processRTtypeOperation(ref Register_Data rd, ref Register_Data rt, ref Register_Data rs, ref Opcode_Data op)
        {
            switch (op.opCodeString)
            {
                case "add":
                    // Add (rs + rt) and store into register rd


                    break;
                case "sub":
                    // Sub (rs - rt) and store into register rd

                    break;
                case "sll":
                    // Shift (rt << rs) and store into rd

                    break;
                case "srl":
                    // Shift (rt >> rs) and store into rd

                    break;
            }
        }

    /* 
     * Instruction Send Button Click Callback Function 
     */
    private void sendInstructionButton_Click(object sender, EventArgs e)
        {

            /* Initialize Input Arguments Array */
            List<string> args = new List<string>();

            /* Initialize Opcode Object */
            Opcode_Data op = new Opcode_Data();

            /* Register Structures */
            Register_Data rt = new Register_Data(); 
            Register_Data rd = new Register_Data();
            Register_Data rs = new Register_Data();
            Register_Data imm = new Register_Data();

            // 1. Read Instruction Text Box
            var instructionString = instructBox.Text;

            // 2. Get Array Of Instruction Entries
            bool properCommandCount = getInstructionsFromString(instructionString, ref args); 


            // Validate Instructions Returned From Function
            if (!properCommandCount)
            {
                // Alerts
                alertSystemError();
                return;
            }


            // 3. Read & Validate OpCode
            validateOpCodeAndDetermineType(ref op, args[0]);


            // 4. Assign Register Indexes To Global Array Based On Opcode Type
            if (op.opCodeValid)
            {
                assignRegisterIndexesFromInput(args, ref imm, ref rd, ref rt, ref rs, ref op);

            } else
            {
                // Alerts
                alertSystemError();
                return;
            }


            // 5. Process Operation Code Instruction
            if (op.iType && rs.regIndexSet && rt.regIndexSet && imm.regIndexSet)
            {
                processITtypeOperation(ref imm, ref rt, ref rs, ref op);
            }
            else if (op.rType && rs.regIndexSet && rt.regIndexSet && rd.regIndexSet)
            {
                processRTtypeOperation(ref rd, ref rt, ref rs, ref op); 

            } else
            {
                // Alerts
                alertSystemError();
                return;
            }


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
        public string[] rTypeArr = new string[] { };
        public string[] iTypeArr = new string[] { };
        public bool opCodeValid;
        public bool rType;
        public bool iType;
        public bool jType;

        public Opcode_Data()
        {
            // Initialize class flags to false 
            this.opCodeValid = false;
            // initialze array with R-Type
            rTypeArr = new string[] { "add", "sub", "div" };
            // initialze array with I-Type
            iTypeArr = new string[] { "sw", "lw" };
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
