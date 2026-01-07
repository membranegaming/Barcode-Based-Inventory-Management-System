using System;
using System.Windows.Forms;

namespace store_parts
{
    /// <summary>
    /// Form for configuring barcode printer settings.
    /// Supports both BarTender SDK and direct TSPL commands.
    /// </summary>
    public partial class PrinterSettingsForm : Form
    {
        private BarcodePrinter _printer;

        public PrinterSettingsForm()
        {
            InitializeComponent();
            _printer = new BarcodePrinter();
        }

        private void PrinterSettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPrinters();
                LoadCurrentSettings();
                UpdateUIState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading settings: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load available printers into the combo box
        /// </summary>
        private void LoadPrinters()
        {
            cmbPrinters.Items.Clear();
            var printers = BarcodePrinter.GetAvailablePrinters();

            foreach (string printer in printers)
            {
                cmbPrinters.Items.Add(printer);
            }

            // Try to select TSC printer first
            string tscPrinter = BarcodePrinter.FindTSCPrinter();
            if (!string.IsNullOrEmpty(tscPrinter))
            {
                int idx = cmbPrinters.FindStringExact(tscPrinter);
                if (idx >= 0)
                {
                    cmbPrinters.SelectedIndex = idx;
                    lblPrinterStatus.Text = "TSC Printer Detected";
                    lblPrinterStatus.ForeColor = System.Drawing.Color.Green;
                    return;
                }
            }

            // Select first printer if available
            if (cmbPrinters.Items.Count > 0)
            {
                cmbPrinters.SelectedIndex = 0;
                lblPrinterStatus.Text = "Printer available";
                lblPrinterStatus.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                lblPrinterStatus.Text = "No printer found";
                lblPrinterStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// Load current settings from application settings
        /// </summary>
        private void LoadCurrentSettings()
        {
            try
            {
                _printer = BarcodePrinter.LoadFromSettings();

                // Set printer selection
                if (!string.IsNullOrEmpty(_printer.PrinterName))
                {
                    int idx = cmbPrinters.FindStringExact(_printer.PrinterName);
                    if (idx >= 0) cmbPrinters.SelectedIndex = idx;
                }

                // Set print method
                if (_printer.Method == BarcodePrinter.PrintMethod.BarTender)
                {
                    rbBarTender.Checked = true;
                }
                else
                {
                    rbTSPL.Checked = true;
                }

                // Set template path
                txtTemplatePath.Text = _printer.TemplatePath ?? "";

                // Set TSPL settings
                numLabelWidth.Value = _printer.LabelWidth;
                numLabelHeight.Value = _printer.LabelHeight;
                numLabelGap.Value = _printer.LabelGap;
                numBarcodeHeight.Value = _printer.BarcodeHeight;
                numBarcodeX.Value = _printer.BarcodeX;
                numBarcodeY.Value = _printer.BarcodeY;
            }
            catch
            {
                // Use defaults if settings fail to load
            }
        }

        /// <summary>
        /// Update UI state based on selected print method
        /// </summary>
        private void UpdateUIState()
        {
            bool isBarTender = rbBarTender.Checked;

            // Template controls only enabled for BarTender
            lblTemplate.Enabled = isBarTender;
            txtTemplatePath.Enabled = isBarTender;
            btnBrowseTemplate.Enabled = isBarTender;

            // TSPL settings only enabled for TSPL method
            grpTSPL.Enabled = !isBarTender;
        }

        private void cmbPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPrinter = cmbPrinters.SelectedItem?.ToString() ?? "";
            
            if (selectedPrinter.ToUpper().Contains("TSC") || selectedPrinter.ToUpper().Contains("TTP"))
            {
                lblPrinterStatus.Text = "TSC Printer Selected";
                lblPrinterStatus.ForeColor = System.Drawing.Color.Green;
            }
            else if (!string.IsNullOrEmpty(selectedPrinter))
            {
                lblPrinterStatus.Text = "Printer selected";
                lblPrinterStatus.ForeColor = System.Drawing.Color.Blue;
            }
        }

        private void rbBarTender_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUIState();
        }

        private void rbTSPL_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUIState();
        }

        private void btnBrowseTemplate_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtTemplatePath.Text = openFileDialog.FileName;
            }
        }

        private void btnRefreshPrinters_Click(object sender, EventArgs e)
        {
            LoadPrinters();
        }

        private void btnTestPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPrinters.SelectedItem == null)
                {
                    MessageBox.Show("Please select a printer.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Apply current settings to printer
                ApplySettingsToPrinter();

                Cursor = Cursors.WaitCursor;
                bool success = _printer.PrintTestLabel();
                Cursor = Cursors.Default;

                if (success)
                {
                    MessageBox.Show("Test label sent to printer successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to send test label.\n\nPlease check:\n" +
                        "• Printer is turned on\n" +
                        "• Printer is connected\n" +
                        "• Labels are loaded",
                        "Print Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Error: " + ex.Message, "Print Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPrinters.SelectedItem == null)
                {
                    MessageBox.Show("Please select a printer.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ApplySettingsToPrinter();
                _printer.SaveToSettings();

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving settings: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Apply form values to the printer object
        /// </summary>
        private void ApplySettingsToPrinter()
        {
            _printer.PrinterName = cmbPrinters.SelectedItem?.ToString();
            _printer.Method = rbBarTender.Checked ? 
                BarcodePrinter.PrintMethod.BarTender : 
                BarcodePrinter.PrintMethod.TSPL;
            _printer.TemplatePath = txtTemplatePath.Text;
            _printer.LabelWidth = (int)numLabelWidth.Value;
            _printer.LabelHeight = (int)numLabelHeight.Value;
            _printer.LabelGap = (int)numLabelGap.Value;
            _printer.BarcodeHeight = (int)numBarcodeHeight.Value;
            _printer.BarcodeX = (int)numBarcodeX.Value;
            _printer.BarcodeY = (int)numBarcodeY.Value;
        }
    }
}
