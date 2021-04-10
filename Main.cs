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
                registerArray[registerIndex] = registerValue;

            }


            // Return Flag
            return processSuccessful;
        }

        /* Get Current Register Value From Register Array */
        private void getRegisterValue(Register_Data regStruct)
        {

            // Get Value From Register Array Using Passed regStruct Index
            regStruct.regValue = registerArray[regStruct.regIndex];

            // Set Flag That Data Is Set
            regStruct.regValueSet = true;

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

        public void alertSystemError(string errorString)
        {
            MessageBox.Show(errorString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        /* Split String Into Instruction Array */
        public bool getInstructionsFromString(string instructionString, ref List<string> inputArgArray)
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

        /* Assing Immediate Constant Value From String */
        public void setImmediateValueFromString(string valueString, ref Register_Data imm)
        {
            // Convert String To Integer
            int intValue = int.Parse(valueString);

            // Set Value Into Immediate Object
            imm.regValue = intValue;

            // Set Flag That Value Is Ready
            imm.regValueSet = true; 
            
        }

        // Execute I-Type Operations (Consider Using Function Callbacks If Have The Time) 
        public void processITtypeOperation(ref List<string> args, ref Register_Data imm, ref Register_Data rt, ref Register_Data rs, ref Opcode_Data op)
        {
            switch (op.opCodeString)
            {
                case "sw":

                    // Perform: [ sw $rt, imm($rs) ]        :: Mem4B(R[$rs] + SignExt16b(imm)) ← R[$rt]


                    break;
                case "lw":

                    // Perform: [ lw $rt, imm($rs) ]        :: R[$rt] ← Mem4B(R[$rs] + SignExt16b(imm))

                    break;

                case "addi":

                    // Perform: [ addi $rt, $rs, imm ]      :: R[$rt] ← R[$rs] + SignExt16b(imm)

                    // 1. Set Register Indexes
                    getRegisterIndexFromString(args[1], ref rt);
                    getRegisterIndexFromString(args[2], ref rs);

                    // 2. Get Required Values
                    getRegisterValue(rs);
                    setImmediateValueFromString(args[3], ref imm);

                    // 3. Perform Any Math
                    int valueResult = rs.regValue + imm.regValue;

                    // 4. Store Result Into Return Register Object

                    // 4. Store Result Into Return Register
                    setRegisterValue(rt.regIndex, valueResult);

                    // 5. Update Any GUI Changes
                    setGuiHexStringFromInt(valueResult, rt.regIndex);

                    break;
            }
        }

        public void processRTtypeOperation(ref List<string> args, ref Register_Data rd, ref Register_Data rt, ref Register_Data rs, ref Opcode_Data op)
        {
            switch (op.opCodeString)
            {
                case "add":
                    {

                        // [ add $rd, $rs, $rt] ::  R[$rd] ← R[$rs] + R[$rt]

                        // 1. Set Register Indexes
                        getRegisterIndexFromString(args[3], ref rt);
                        getRegisterIndexFromString(args[2], ref rs);
                        getRegisterIndexFromString(args[1], ref rd);

                        // 2. Get Required Values
                        getRegisterValue(rs);
                        getRegisterValue(rt);

                        // 3. Perform Any Math
                        int valueResult = rs.regValue + rt.regValue;

                        // 4. Store Result Into Return Register
                        setRegisterValue(rd.regIndex, valueResult);

                        // 5. Update Any GUI Changes
                        setGuiHexStringFromInt(valueResult, rd.regIndex);


                        break;
                    }
                case "sub":
                    {
                        // [ sub $rd, $rs, $rt ] ::    R[$rd] ← R[$rs] - R[$rt]


                        // 1. Set Register Indexes
                        getRegisterIndexFromString(args[1], ref rd);
                        getRegisterIndexFromString(args[2], ref rs);
                        getRegisterIndexFromString(args[3], ref rt);

                        // 2. Get Required Values
                        getRegisterValue(rs);
                        getRegisterValue(rt);

                        // 3. Perform Any Math
                        int valueResult = rs.regValue - rt.regValue;

                        // 4. Store Result Into Return Register
                        setRegisterValue(rd.regIndex, valueResult);

                        // 5. Update Any GUI Changes
                        setGuiHexStringFromInt(valueResult, rd.regIndex);

                        break;

                    }
                case "sll":
                    {
                        // [ sll $rd, $rt, shamt ]	R[$rd] ← R[$rt] << shamt

                        break;
                    }
                case "srl":
                    {
                        // [ srl $rd, $rt, shamt ] ::	R[$rd] ← R[$rt] >> shamt

                        break;
                    }
            }
        }

        private void convertHexStringToFloat(string hexString) // Expects String Argument In Form ("0x00") // no hex length requirement
        {
            hexString = hexString.Remove(0, 2);

            uint num = uint.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);

            byte[] floatVals = BitConverter.GetBytes(num);
            float f = BitConverter.ToSingle(floatVals, 0);
            Console.WriteLine("float convert = {0}", f);
        }
        private void convertHexStringToInt(string hexString)    // Expects String Argument In Form ("0x00") // no hex length requirement
        {
            hexString = hexString.Remove(0, 2); 

            uint num = uint.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);

            byte[] intVals = BitConverter.GetBytes(num);
            int i = BitConverter.ToInt32(intVals, 0); 
            Console.WriteLine("int convert = {0}", i);
        }

        /* 
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
                alertSystemError("Invalid number of commands");
                return;
            }


            // 3. Read & Validate OpCode
            validateOpCodeAndDetermineType(ref op, args[0]);


            // 4. Return early if op-code is invalid
            if (!op.opCodeValid)
            {
                // Alerts
                alertSystemError("Invalid op-code found");
                return;
            }

            // 5. Process Operation Code Instruction
            if (op.iType)
            {
                processITtypeOperation(ref args, ref imm, ref rt, ref rs, ref op);
            }
            else if (op.rType)
            {
                processRTtypeOperation(ref args, ref rd, ref rt, ref rs, ref op); 

            } else
            {
                // Alerts
                alertSystemError("Op-code type not set correctly");
                return;
            }

            // 6. Update GUI Register Values

        }



        // set GUI representation of new data
        public void setGuiHexStringFromInt(int value, int regIndex) {

            // Convert integer 182 as a hex in a string variable
            string hexString = String.Format("0x{0}", value.ToString("X8"));

            // Store Into Correct GUI Register Text Box
            switch(regIndex)
            {
                case (int)reg_Index.i_regZero:
                    regZero.Text = hexString; 
                    break;
                case (int)reg_Index.i_regAt:
                    regAt.Text = hexString;
                    break;
                case (int)reg_Index.i_regV0:
                    regAt.Text = hexString;
                    break;
                case (int)reg_Index.i_regV1:
                    regV1.Text = hexString;
                    break;
                case (int)reg_Index.i_regA0:
                    regA0.Text = hexString;
                    break;
                case (int)reg_Index.i_regA1:
                    regA1.Text = hexString;
                    break;
                case (int)reg_Index.i_regA2:
                    regA2.Text = hexString;
                    break;
                case (int)reg_Index.i_regA3:
                    regA3.Text = hexString;
                    break;
                case (int)reg_Index.i_regT0:
                    regT0.Text = hexString;
                    break;
                case (int)reg_Index.i_regT1:
                    regT1.Text = hexString;
                    break;
                case (int)reg_Index.i_regT2:
                    regT2.Text = hexString;
                    break;
                case (int)reg_Index.i_regT3:
                    regT3.Text = hexString;
                    break;
                default:
                    // If No Match, Set Alert Box

                    break;

            }
        }

        private void initValuesButton_Click(object sender, EventArgs e)
        {
            // Init default reg object for function arguments
            Register_Data tempObj = new Register_Data();

            // Set Value To Zero
            tempObj.regValue = 0;
            tempObj.regValueSet = true;


            // For Each Register Index Supported, Set Value To Zero
            for (int x = 0; x < (int)reg_Index.i_reg_NumberOfRegisters; x++)
            {
                // Set New Index For Register 
                tempObj.regIndex = x;
                tempObj.regIndexSet = true;

                // Set Global Array Values To Zero Using Temp Reg Object
                setRegisterValue(tempObj);

                // Set GUI Register Values to Zero using Temp Reg Object
                setGuiHexStringFromInt(tempObj.regValue, tempObj.regIndex);

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
            iTypeArr = new string[] { "sw", "lw", "addi" };
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
