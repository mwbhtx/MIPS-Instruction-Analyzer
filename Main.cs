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
using System.Diagnostics;



namespace MIPS_Instruction_Analyzer
{

    public partial class Main : Form
    {

        /* Initialize Register Array : Values Initialize To Zero */
        int[] registerArray = new int[(int)reg_Index.i_reg_NumberOfRegisters];
        
        // initialize base 2 variable for conversions
        int binBase = 2;

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

            // Compute binary representation of register number
            regStruct.Register_Bin();

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

                    // Compute binary representation of opcode
                    opObj.op_Bin();
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

                    // Compute binary representation of opcode
                    opObj.op_Bin();

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

            // set binary string representation:
            string tempImm = imm.regBin; // init empty string
            tempImm = Convert.ToString(intValue, binBase);        // convert integer to string using base 2

            // pad up shamt binary number
            string padBits = padUpBits(tempImm.Length, 16);        // Add Additional Bits After 
            imm.regBin = padBits + tempImm;

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

                    // 4. Store Result Into Return Register
                    setRegisterValue(rt.regIndex, valueResult);

                    // 5. Update Any GUI Changes
                    setGuiHexStringFromInt(valueResult, rt.regIndex);
                    setGuiBinaryRepTextBox(rt, rs, op, null, imm);

                    break;
            }
        }

        // Return Shift Amount As Integer, While Also Setting Shift Amount Binary String
        public int getShamtIntAndSetBinString(ref Opcode_Data opcodeObj, string shamtString)
        {

            // Get shamt from input string as integer
            int shamtVal = int.Parse(shamtString);

            // set binary string representation
            string tempShamt = opcodeObj.shamtBin;
            tempShamt = Convert.ToString(shamtVal, binBase);        // int binBase = 2

            // pad up shamt binary number
            string padBits = padUpBits(tempShamt.Length, 5);
            opcodeObj.shamtBin = padBits + tempShamt;

            return shamtVal;

        }

        // Set GUI Binary Representation Text Box
        public void setGuiBinaryRepTextBox(Register_Data rt, Register_Data rs, Opcode_Data op, Register_Data rd = null, Register_Data imm = null)
        {
            // init string
            var outputString = "empty";

            if ( rd != null && rd.regBin == null)
            {
                rd.regBin = "00000";
            }
            if (imm != null && imm.regBin == null)
            {
                imm.regBin = "00000";
            }

            // Check For Null Values
            if (rs.regBin == null)
            {
                rs.regBin = "00000";
            }

            if (op.shamtBin == null)
            {
                op.shamtBin = "00000";
            }

            // Move Binary Text Box
            fullBingroup.Location = new Point(13, 186);
            fullBingroup.Visible = true;

            // Set GUI String Based On Op-Type
            if (op.rType)
            {
                // Disable iType Representation Group Box
                iTypeBinGroup.Visible = false;
                rTypeBinGroup.Visible = true;

                // Move Correct Box Into Position
                rTypeBinGroup.Location = new Point(12, 103);


                // [ op + rs + rt + rd + shamt + func ]
                outputString = op.opBin + rs.regBin + rt.regBin + rd.regBin + op.shamtBin + op.functBin;

                // Set Individual Representation Text Boxes
                opTxtBoxRtype.Text = op.opBin;
                rsTxtBoxRtype.Text = rs.regBin;
                rtTxtBoxRtype.Text = rt.regBin;
                rdTxtBoxRtype.Text = rd.regBin;
                shamtTxtBoxRtype.Text = op.shamtBin;
                funcTxtBoxRtype.Text = op.functBin;
            }
            else if (op.iType)
            {

                // Disable rType Representation Group Box
                iTypeBinGroup.Visible = true;
                rTypeBinGroup.Visible = false;


                // Move Correct Box Into Position
                iTypeBinGroup.Location = new Point(12, 103);


                // [ op + rs + rt + imm ]
                outputString = op.opBin + rs.regBin + rt.regBin + imm.regBin;

                // Set Individual Representation Text Boxes
                opTxtBoxItype.Text = op.opBin;
                rsTxtBoxItype.Text = rs.regBin;
                rtTxtBoxItype.Text = rt.regBin;
                immTxtBoxItype.Text = imm.regBin;
            }


            // Set GUI String
            binRepTextBox.Text = outputString; 
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
                        setGuiBinaryRepTextBox(rt, rs, op, rd);


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
                        setGuiBinaryRepTextBox(rt, rs, op, rd);

                        break;

                    }
                case "sll":
                    {
                        // [ sll $rd, $rt, shamt ]	R[$rd] ← R[$rt] << shamt

                        // 1. Set Register Indexes
                        getRegisterIndexFromString(args[1], ref rd);
                        getRegisterIndexFromString(args[2], ref rt);

                        // 2. Get Required Values
                        getRegisterValue(rt);
                        var shamtVal = getShamtIntAndSetBinString(ref op, args[3]);

                        // 3. Perform Any Math
                        int valueResult = rt.regValue << shamtVal;

                        // 4. Store Result Into Return Register
                        setRegisterValue(rd.regIndex, valueResult);

                        // 5. Update Any GUI Changes
                        setGuiHexStringFromInt(valueResult, rd.regIndex);
                        setGuiBinaryRepTextBox(rt, rs, op, rd);

                        break;
                    }
                case "srl":
                    {
                        // [ srl $rd, $rt, shamt ] ::	R[$rd] ← R[$rt] >> shamt
                        // [ sll $rd, $rt, shamt ]	R[$rd] ← R[$rt] << shamt

                        // 1. Set Register Indexes
                        getRegisterIndexFromString(args[1], ref rd);
                        getRegisterIndexFromString(args[2], ref rt);

                        // 2. Get Required Values
                        getRegisterValue(rt);
                        int shamtVal = getShamtIntAndSetBinString(ref op, args[3]);
         
                        // 3. Perform Any Math
                        int valueResult = rt.regValue >> shamtVal;

                        // 4. Store Result Into Return Register
                        setRegisterValue(rd.regIndex, valueResult);

                        // 5. Update Any GUI Changes
                        setGuiHexStringFromInt(valueResult, rd.regIndex);
                        setGuiBinaryRepTextBox(rt, rs, op, rd);

                        break;
                    }
                case "div": //using rs as destination for LO
                    {
                        // LO ← R[$rs] / R[$rt]  div $rs, $rt     div $rs, $rt
                        // HI ← R[$rs] % R[$rt]  div $rs, $rt     div $rs, $rt

                        // 1. Set Register Indexes
                        getRegisterIndexFromString(args[1], ref rs);
                        getRegisterIndexFromString(args[2], ref rt);

                        // 2. Get Required Values
                        getRegisterValue(rs);
                        getRegisterValue(rt);

                        // 3. Perform Any Math
                        try
                        {
                            int valueResult = rs.regValue / rt.regValue;

                            // 4. Store Result Into Return Register
                            setRegisterValue(rs.regIndex, valueResult);

                            // 5. Update Any GUI Changes
                            setGuiHexStringFromInt(valueResult, rs.regIndex);
                            setGuiBinaryRepTextBox(rt, rs, op, rd);

                        }
                        catch
                        {
                            alertSystemError("Divide By Zero Error.");
                        }

                        break;
                    }
                        case "mult": //using rs as destination 
                    {
                        // {HI, LO} ← R[$rs] * R[$rt]      mult $rs, $rt

                        // 1. Set Register Indexes
                        getRegisterIndexFromString(args[1], ref rs);
                        getRegisterIndexFromString(args[2], ref rt);

                        // 2. Get Required Values
                        getRegisterValue(rs);
                        getRegisterValue(rt);

                        // 3. Perform Any Math
                        int valueResult = rs.regValue * rt.regValue;

                        // 4. Store Result Into Return Register
                        setRegisterValue(rs.regIndex, valueResult);

                        // 5. Update Any GUI Changes
                        setGuiHexStringFromInt(valueResult, rs.regIndex);
                        setGuiBinaryRepTextBox(rt, rs, op, rd);

                        break;
                    }
            }
        }

        private string padUpBits (int bitLength, int bitMax) // expects length of string with binary presentation and total number of bits desired
        {
            string padString = "";
            if (bitLength <= bitMax)
                {
                    int padNum = bitMax - bitLength;
                    padString = string.Concat(Enumerable.Repeat("0", padNum));
                }
            return padString;
        }

        private void convertHexStringToFloat(string hexString) // Expects String Argument In Form ("0x00") // no hex length requirement
        {
            hexString = hexString.Remove(0, 2);

            uint num = uint.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);

            byte[] floatVals = BitConverter.GetBytes(num);
            float f = BitConverter.ToSingle(floatVals, 0);
            Debug.WriteLine("float convert = {0}", f);
        }
        private void convertHexStringToInt(string hexString)    // Expects String Argument In Form ("0x00") // no hex length requirement
        {
            hexString = hexString.Remove(0, 2); 

            uint num = uint.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);

            byte[] intVals = BitConverter.GetBytes(num);
            int i = BitConverter.ToInt32(intVals, 0); 
            Debug.WriteLine("int convert = {0}", i);
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

            // Start By Setting All Register Text Foreground To Black, We Will Later Highlight Register That Changed
            foreach (TextBox textBox in this.RegistersGroupBox.Controls.OfType<TextBox>())
            {
                textBox.ForeColor = Color.Black; 
            }

            // Set color to be used for highlighting purposes
            var foreColor = Color.Red;
            var backColor = SystemColors.Control;


            // Convert integer 182 as a hex in a string variable
            string hexString = String.Format("0x{0}", value.ToString("X8"));

            // Store Into Correct GUI Register Text Box
            switch(regIndex)
            {
                case (int)reg_Index.i_regZero:
                    regZero.Text = hexString;
                    regZero.ForeColor = foreColor;
                    regZero.BackColor = backColor;
                    break;
                case (int)reg_Index.i_regAt:
                    regAt.Text = hexString;
                    regAt.ForeColor = foreColor;
                    regAt.BackColor = backColor;
                    break;
                case (int)reg_Index.i_regV0:
                    regV0.Text = hexString;
                    regV0.ForeColor = foreColor;
                    regV0.BackColor = backColor;
                    break;
                case (int)reg_Index.i_regV1:
                    regV1.Text = hexString;
                    regV1.ForeColor = foreColor;
                    regV1.BackColor = backColor;
                    break;
                case (int)reg_Index.i_regA0:
                    regA0.Text = hexString;
                    regA0.ForeColor = foreColor;
                    regA0.BackColor = backColor;
                    break;
                case (int)reg_Index.i_regA1:
                    regA1.Text = hexString;
                    regA1.ForeColor = foreColor;
                    regA1.BackColor = backColor;
                    break;
                case (int)reg_Index.i_regA2:
                    regA2.Text = hexString;
                    regA2.ForeColor = foreColor;
                    regA2.BackColor = backColor;
                    break;
                case (int)reg_Index.i_regA3:
                    regA3.Text = hexString;
                    regA3.ForeColor = foreColor;
                    regA3.BackColor = backColor;
                    break;
                case (int)reg_Index.i_regT0:
                    regT0.Text = hexString;
                    regT0.ForeColor = foreColor;
                    regT0.BackColor = backColor;
                    break;
                case (int)reg_Index.i_regT1:
                    regT1.Text = hexString;
                    regT1.ForeColor = foreColor;
                    regT1.BackColor = backColor;
                    break;
                case (int)reg_Index.i_regT2:
                    regT2.Text = hexString;
                    regT2.ForeColor = foreColor;
                    regT2.BackColor = backColor;
                    break;
                case (int)reg_Index.i_regT3:
                    regT3.Text = hexString;
                    regT3.ForeColor = foreColor;
                    regT3.BackColor = backColor;
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

        private void Label17_Click(object sender, EventArgs e)
        {

        }

        private void RegT2_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegT3_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegT1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegV1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegT0_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegA3_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegA2_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegA1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegA0_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegV0_TextChanged(object sender, EventArgs e)
        {

        }

        private void BinRepTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }


    // Class To Manage Register Data Passed Around
    public class Register_Data
    {
        public int regValue;
        public int regIndex;
        public bool regValueSet;
        public bool regIndexSet;
        public string regBin;

        public Register_Data()
        {
            // Initialize class flags to false 
            this.regValueSet = false;
            this.regIndexSet = false;
        }

        public void Register_Bin()
        {
            // convert index number into string holding 5-bit binary representation
            switch(this.regIndex)
            {
                case 1:
                    regBin = "00001";
                    break;
                case 2:
                    regBin = "00010";
                    break; 
                case 3:
                    regBin = "00011";
                    break; 
                case 4:
                    regBin = "00100";
                    break; 
                case 5:
                    regBin = "00101";
                    break; 
                case 6:
                    regBin = "00110";
                    break; 
                case 7:
                    regBin = "00111";
                    break; 
                case 8:
                    regBin = "01000";
                    break; 
                case 9:
                    regBin = "01001";
                    break; 
                case 10:
                    regBin = "01010";
                    break; 
                case 11:
                    regBin = "01011";
                    break; 
                default:
                    regBin = "00000";
                    break;
            }
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
        public string opBin;
        public string functBin;
        public string addressBin;
        public string shamtBin; // set in Process R Type function

        public Opcode_Data()
        {
            // Initialize class flags to false 
            this.opCodeValid = false;
            // initialze array with R-Type
            rTypeArr = new string[] { "add", "sub", "div", "mult", "sll", "srl" };
            // initialze array with I-Type
            iTypeArr = new string[] { "sw", "lw", "addi" };
        }

        public void op_Bin()
        {
            if (this.rType)
            {
                this.opBin = "000000";
                // set funct binary presentation
                switch(this.opCodeString)
                {
                    case "add":
                        this.functBin = "100000";
                        this.shamtBin = "00000";
                        break;
                    case "sub":
                        this.functBin = "100010";
                        this.shamtBin = "00000";
                        break;
                    case "div":
                        this.functBin = "011010";
                        this.shamtBin = "00000";
                        break;
                    case "mult":
                        this.functBin = "011000";
                        this.shamtBin = "00000";
                        break;
                    case "sll":
                        this.functBin = "000000";
                        break;
                    case "srl":
                        this.functBin = "000010";
                        break;
                    default:
                        this.shamtBin = "00000";
                        break;
                }
                
            } else if (this.iType)
                {
                switch(this.opCodeString)
                {
                    case "sw":
                        this.opBin = "101011";
                        break;
                    case "lw":
                        this.opBin = "100011";
                        break;
                    case "addi":
                        this.opBin = "001000";
                        break;
                    default:
                        //Error?
                        break;
                }
            }
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
