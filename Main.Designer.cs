namespace MIPS_Instruction_Analyzer
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.numBox = new System.Windows.Forms.NumericUpDown();
            this.transferButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numBox)).BeginInit();
            this.SuspendLayout();
            // 
            // numBox
            // 
            this.numBox.Location = new System.Drawing.Point(194, 76);
            this.numBox.Name = "numBox";
            this.numBox.Size = new System.Drawing.Size(120, 20);
            this.numBox.TabIndex = 0;
            // 
            // transferButton
            // 
            this.transferButton.Location = new System.Drawing.Point(194, 102);
            this.transferButton.Name = "transferButton";
            this.transferButton.Size = new System.Drawing.Size(120, 52);
            this.transferButton.TabIndex = 1;
            this.transferButton.Text = "TRANSFER";
            this.transferButton.UseVisualStyleBackColor = true;
            this.transferButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(194, 160);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(120, 20);
            this.textBox.TabIndex = 2;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 319);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.transferButton);
            this.Controls.Add(this.numBox);
            this.Name = "Main";
            this.Text = "MIPS Instruction Analyzer";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numBox;
        private System.Windows.Forms.Button transferButton;
        private System.Windows.Forms.TextBox textBox;
    }
}

