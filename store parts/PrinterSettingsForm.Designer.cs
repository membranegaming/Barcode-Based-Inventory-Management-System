namespace store_parts
{
    partial class PrinterSettingsForm
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
            this.grpPrinter = new System.Windows.Forms.GroupBox();
            this.btnRefreshPrinters = new System.Windows.Forms.Button();
            this.lblPrinterStatus = new System.Windows.Forms.Label();
            this.cmbPrinters = new System.Windows.Forms.ComboBox();
            this.lblPrinter = new System.Windows.Forms.Label();
            this.grpMethod = new System.Windows.Forms.GroupBox();
            this.btnBrowseTemplate = new System.Windows.Forms.Button();
            this.txtTemplatePath = new System.Windows.Forms.TextBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.rbTSPL = new System.Windows.Forms.RadioButton();
            this.rbBarTender = new System.Windows.Forms.RadioButton();
            this.grpTSPL = new System.Windows.Forms.GroupBox();
            this.numBarcodeY = new System.Windows.Forms.NumericUpDown();
            this.lblBarcodeY = new System.Windows.Forms.Label();
            this.numBarcodeX = new System.Windows.Forms.NumericUpDown();
            this.lblBarcodeX = new System.Windows.Forms.Label();
            this.numBarcodeHeight = new System.Windows.Forms.NumericUpDown();
            this.lblBarcodeHeight = new System.Windows.Forms.Label();
            this.numLabelGap = new System.Windows.Forms.NumericUpDown();
            this.lblGap = new System.Windows.Forms.Label();
            this.numLabelHeight = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.numLabelWidth = new System.Windows.Forms.NumericUpDown();
            this.lblWidth = new System.Windows.Forms.Label();
            this.btnTestPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.grpPrinter.SuspendLayout();
            this.grpMethod.SuspendLayout();
            this.grpTSPL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBarcodeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBarcodeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBarcodeHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLabelGap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLabelHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLabelWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPrinter
            // 
            this.grpPrinter.Controls.Add(this.btnRefreshPrinters);
            this.grpPrinter.Controls.Add(this.lblPrinterStatus);
            this.grpPrinter.Controls.Add(this.cmbPrinters);
            this.grpPrinter.Controls.Add(this.lblPrinter);
            this.grpPrinter.Location = new System.Drawing.Point(12, 12);
            this.grpPrinter.Name = "grpPrinter";
            this.grpPrinter.Size = new System.Drawing.Size(410, 80);
            this.grpPrinter.TabIndex = 0;
            this.grpPrinter.TabStop = false;
            this.grpPrinter.Text = "Printer Selection";
            // 
            // btnRefreshPrinters
            // 
            this.btnRefreshPrinters.Location = new System.Drawing.Point(329, 23);
            this.btnRefreshPrinters.Name = "btnRefreshPrinters";
            this.btnRefreshPrinters.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshPrinters.TabIndex = 3;
            this.btnRefreshPrinters.Text = "Refresh";
            this.btnRefreshPrinters.UseVisualStyleBackColor = true;
            this.btnRefreshPrinters.Click += new System.EventHandler(this.btnRefreshPrinters_Click);
            // 
            // lblPrinterStatus
            // 
            this.lblPrinterStatus.AutoSize = true;
            this.lblPrinterStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblPrinterStatus.Location = new System.Drawing.Point(83, 52);
            this.lblPrinterStatus.Name = "lblPrinterStatus";
            this.lblPrinterStatus.Size = new System.Drawing.Size(83, 13);
            this.lblPrinterStatus.TabIndex = 2;
            this.lblPrinterStatus.Text = "No printer found";
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinters.FormattingEnabled = true;
            this.cmbPrinters.Location = new System.Drawing.Point(86, 24);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Size = new System.Drawing.Size(237, 21);
            this.cmbPrinters.TabIndex = 1;
            this.cmbPrinters.SelectedIndexChanged += new System.EventHandler(this.cmbPrinters_SelectedIndexChanged);
            // 
            // lblPrinter
            // 
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new System.Drawing.Point(15, 27);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(40, 13);
            this.lblPrinter.TabIndex = 0;
            this.lblPrinter.Text = "Printer:";
            // 
            // grpMethod
            // 
            this.grpMethod.Controls.Add(this.btnBrowseTemplate);
            this.grpMethod.Controls.Add(this.txtTemplatePath);
            this.grpMethod.Controls.Add(this.lblTemplate);
            this.grpMethod.Controls.Add(this.rbTSPL);
            this.grpMethod.Controls.Add(this.rbBarTender);
            this.grpMethod.Location = new System.Drawing.Point(12, 98);
            this.grpMethod.Name = "grpMethod";
            this.grpMethod.Size = new System.Drawing.Size(410, 100);
            this.grpMethod.TabIndex = 1;
            this.grpMethod.TabStop = false;
            this.grpMethod.Text = "Print Method";
            // 
            // btnBrowseTemplate
            // 
            this.btnBrowseTemplate.Location = new System.Drawing.Point(329, 65);
            this.btnBrowseTemplate.Name = "btnBrowseTemplate";
            this.btnBrowseTemplate.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseTemplate.TabIndex = 4;
            this.btnBrowseTemplate.Text = "Browse...";
            this.btnBrowseTemplate.UseVisualStyleBackColor = true;
            this.btnBrowseTemplate.Click += new System.EventHandler(this.btnBrowseTemplate_Click);
            // 
            // txtTemplatePath
            // 
            this.txtTemplatePath.Location = new System.Drawing.Point(86, 67);
            this.txtTemplatePath.Name = "txtTemplatePath";
            this.txtTemplatePath.Size = new System.Drawing.Size(237, 20);
            this.txtTemplatePath.TabIndex = 3;
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(15, 70);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(54, 13);
            this.lblTemplate.TabIndex = 2;
            this.lblTemplate.Text = "Template:";
            // 
            // rbTSPL
            // 
            this.rbTSPL.AutoSize = true;
            this.rbTSPL.Checked = true;
            this.rbTSPL.Location = new System.Drawing.Point(18, 42);
            this.rbTSPL.Name = "rbTSPL";
            this.rbTSPL.Size = new System.Drawing.Size(196, 17);
            this.rbTSPL.TabIndex = 1;
            this.rbTSPL.TabStop = true;
            this.rbTSPL.Text = "TSPL (Direct TSC Printer Commands)";
            this.rbTSPL.UseVisualStyleBackColor = true;
            this.rbTSPL.CheckedChanged += new System.EventHandler(this.rbTSPL_CheckedChanged);
            // 
            // rbBarTender
            // 
            this.rbBarTender.AutoSize = true;
            this.rbBarTender.Location = new System.Drawing.Point(18, 19);
            this.rbBarTender.Name = "rbBarTender";
            this.rbBarTender.Size = new System.Drawing.Size(156, 17);
            this.rbBarTender.TabIndex = 0;
            this.rbBarTender.Text = "BarTender SDK (Template)";
            this.rbBarTender.UseVisualStyleBackColor = true;
            this.rbBarTender.CheckedChanged += new System.EventHandler(this.rbBarTender_CheckedChanged);
            // 
            // grpTSPL
            // 
            this.grpTSPL.Controls.Add(this.numBarcodeY);
            this.grpTSPL.Controls.Add(this.lblBarcodeY);
            this.grpTSPL.Controls.Add(this.numBarcodeX);
            this.grpTSPL.Controls.Add(this.lblBarcodeX);
            this.grpTSPL.Controls.Add(this.numBarcodeHeight);
            this.grpTSPL.Controls.Add(this.lblBarcodeHeight);
            this.grpTSPL.Controls.Add(this.numLabelGap);
            this.grpTSPL.Controls.Add(this.lblGap);
            this.grpTSPL.Controls.Add(this.numLabelHeight);
            this.grpTSPL.Controls.Add(this.lblHeight);
            this.grpTSPL.Controls.Add(this.numLabelWidth);
            this.grpTSPL.Controls.Add(this.lblWidth);
            this.grpTSPL.Location = new System.Drawing.Point(12, 204);
            this.grpTSPL.Name = "grpTSPL";
            this.grpTSPL.Size = new System.Drawing.Size(410, 130);
            this.grpTSPL.TabIndex = 2;
            this.grpTSPL.TabStop = false;
            this.grpTSPL.Text = "TSPL Settings (Label Size && Position)";
            // 
            // numBarcodeY
            // 
            this.numBarcodeY.Location = new System.Drawing.Point(307, 97);
            this.numBarcodeY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBarcodeY.Name = "numBarcodeY";
            this.numBarcodeY.Size = new System.Drawing.Size(80, 20);
            this.numBarcodeY.TabIndex = 11;
            this.numBarcodeY.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblBarcodeY
            // 
            this.lblBarcodeY.AutoSize = true;
            this.lblBarcodeY.Location = new System.Drawing.Point(220, 99);
            this.lblBarcodeY.Name = "lblBarcodeY";
            this.lblBarcodeY.Size = new System.Drawing.Size(81, 13);
            this.lblBarcodeY.TabIndex = 10;
            this.lblBarcodeY.Text = "Barcode Y (dot):";
            // 
            // numBarcodeX
            // 
            this.numBarcodeX.Location = new System.Drawing.Point(115, 97);
            this.numBarcodeX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBarcodeX.Name = "numBarcodeX";
            this.numBarcodeX.Size = new System.Drawing.Size(80, 20);
            this.numBarcodeX.TabIndex = 9;
            this.numBarcodeX.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblBarcodeX
            // 
            this.lblBarcodeX.AutoSize = true;
            this.lblBarcodeX.Location = new System.Drawing.Point(15, 99);
            this.lblBarcodeX.Name = "lblBarcodeX";
            this.lblBarcodeX.Size = new System.Drawing.Size(81, 13);
            this.lblBarcodeX.TabIndex = 8;
            this.lblBarcodeX.Text = "Barcode X (dot):";
            // 
            // numBarcodeHeight
            // 
            this.numBarcodeHeight.Location = new System.Drawing.Point(307, 58);
            this.numBarcodeHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numBarcodeHeight.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numBarcodeHeight.Name = "numBarcodeHeight";
            this.numBarcodeHeight.Size = new System.Drawing.Size(80, 20);
            this.numBarcodeHeight.TabIndex = 7;
            this.numBarcodeHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblBarcodeHeight
            // 
            this.lblBarcodeHeight.AutoSize = true;
            this.lblBarcodeHeight.Location = new System.Drawing.Point(220, 60);
            this.lblBarcodeHeight.Name = "lblBarcodeHeight";
            this.lblBarcodeHeight.Size = new System.Drawing.Size(82, 13);
            this.lblBarcodeHeight.TabIndex = 6;
            this.lblBarcodeHeight.Text = "Barcode Height:";
            // 
            // numLabelGap
            // 
            this.numLabelGap.Location = new System.Drawing.Point(115, 58);
            this.numLabelGap.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numLabelGap.Name = "numLabelGap";
            this.numLabelGap.Size = new System.Drawing.Size(80, 20);
            this.numLabelGap.TabIndex = 5;
            this.numLabelGap.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblGap
            // 
            this.lblGap.AutoSize = true;
            this.lblGap.Location = new System.Drawing.Point(15, 60);
            this.lblGap.Name = "lblGap";
            this.lblGap.Size = new System.Drawing.Size(54, 13);
            this.lblGap.TabIndex = 4;
            this.lblGap.Text = "Gap (mm):";
            // 
            // numLabelHeight
            // 
            this.numLabelHeight.Location = new System.Drawing.Point(307, 25);
            this.numLabelHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numLabelHeight.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numLabelHeight.Name = "numLabelHeight";
            this.numLabelHeight.Size = new System.Drawing.Size(80, 20);
            this.numLabelHeight.TabIndex = 3;
            this.numLabelHeight.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(220, 27);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(64, 13);
            this.lblHeight.TabIndex = 2;
            this.lblHeight.Text = "Height (mm):";
            // 
            // numLabelWidth
            // 
            this.numLabelWidth.Location = new System.Drawing.Point(115, 25);
            this.numLabelWidth.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numLabelWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numLabelWidth.Name = "numLabelWidth";
            this.numLabelWidth.Size = new System.Drawing.Size(80, 20);
            this.numLabelWidth.TabIndex = 1;
            this.numLabelWidth.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(15, 27);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(61, 13);
            this.lblWidth.TabIndex = 0;
            this.lblWidth.Text = "Width (mm):";
            // 
            // btnTestPrint
            // 
            this.btnTestPrint.Location = new System.Drawing.Point(12, 350);
            this.btnTestPrint.Name = "btnTestPrint";
            this.btnTestPrint.Size = new System.Drawing.Size(100, 30);
            this.btnTestPrint.TabIndex = 3;
            this.btnTestPrint.Text = "Test Print";
            this.btnTestPrint.UseVisualStyleBackColor = true;
            this.btnTestPrint.Click += new System.EventHandler(this.btnTestPrint_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(241, 350);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(337, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "BarTender Templates (*.btw)|*.btw|All Files (*.*)|*.*";
            this.openFileDialog.Title = "Select BarTender Template";
            // 
            // PrinterSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 396);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnTestPrint);
            this.Controls.Add(this.grpTSPL);
            this.Controls.Add(this.grpMethod);
            this.Controls.Add(this.grpPrinter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrinterSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Printer Settings";
            this.Load += new System.EventHandler(this.PrinterSettingsForm_Load);
            this.grpPrinter.ResumeLayout(false);
            this.grpPrinter.PerformLayout();
            this.grpMethod.ResumeLayout(false);
            this.grpMethod.PerformLayout();
            this.grpTSPL.ResumeLayout(false);
            this.grpTSPL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBarcodeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBarcodeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBarcodeHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLabelGap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLabelHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLabelWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPrinter;
        private System.Windows.Forms.Button btnRefreshPrinters;
        private System.Windows.Forms.Label lblPrinterStatus;
        private System.Windows.Forms.ComboBox cmbPrinters;
        private System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.GroupBox grpMethod;
        private System.Windows.Forms.Button btnBrowseTemplate;
        private System.Windows.Forms.TextBox txtTemplatePath;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.RadioButton rbTSPL;
        private System.Windows.Forms.RadioButton rbBarTender;
        private System.Windows.Forms.GroupBox grpTSPL;
        private System.Windows.Forms.NumericUpDown numBarcodeY;
        private System.Windows.Forms.Label lblBarcodeY;
        private System.Windows.Forms.NumericUpDown numBarcodeX;
        private System.Windows.Forms.Label lblBarcodeX;
        private System.Windows.Forms.NumericUpDown numBarcodeHeight;
        private System.Windows.Forms.Label lblBarcodeHeight;
        private System.Windows.Forms.NumericUpDown numLabelGap;
        private System.Windows.Forms.Label lblGap;
        private System.Windows.Forms.NumericUpDown numLabelHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown numLabelWidth;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Button btnTestPrint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
