using System;
using System.Drawing;
using System.Windows.Forms;

namespace store_parts
{
    /// <summary>
    /// Dialog for entering barcode ID and quantity for reprinting
    /// </summary>
    public class ReprintBarcodeDialog : Form
    {
        private TextBox txtId;
        private NumericUpDown numQuantity;
        private Button btnOK;
        private Button btnCancel;

        public int BarcodeId { get; private set; }
        public int Quantity { get; private set; }

        public ReprintBarcodeDialog(int preselectedId = 0, int preselectedQty = 1)
        {
            InitializeDialog();
            
            // Set preselected values if provided
            if (preselectedId > 0)
            {
                txtId.Text = preselectedId.ToString();
            }
            
            if (preselectedQty > 0)
            {
                numQuantity.Value = Math.Min(preselectedQty, numQuantity.Maximum);
            }
        }

        private void InitializeDialog()
        {
            // Form properties
            this.Text = "Reprint Barcode";
            this.Size = new Size(350, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;

            // Create controls
            Label lblTitle = new Label
            {
                Text = "Enter the ID and quantity to reprint barcode labels:",
                Location = new Point(20, 15),
                Size = new Size(300, 20),
                Font = new Font("Segoe UI", 9)
            };
            this.Controls.Add(lblTitle);

            // ID Label and TextBox
            Label lblId = new Label
            {
                Text = "Barcode ID:",
                Location = new Point(20, 50),
                Size = new Size(80, 20),
                TextAlign = ContentAlignment.MiddleRight
            };
            this.Controls.Add(lblId);

            txtId = new TextBox
            {
                Location = new Point(110, 48),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };
            txtId.KeyPress += TxtId_KeyPress;
            this.Controls.Add(txtId);

            // Quantity Label and NumericUpDown
            Label lblQuantity = new Label
            {
                Text = "Quantity:",
                Location = new Point(20, 85),
                Size = new Size(80, 20),
                TextAlign = ContentAlignment.MiddleRight
            };
            this.Controls.Add(lblQuantity);

            numQuantity = new NumericUpDown
            {
                Location = new Point(110, 83),
                Size = new Size(100, 25),
                Minimum = 1,
                Maximum = 1000,
                Value = 1,
                Font = new Font("Segoe UI", 10)
            };
            this.Controls.Add(numQuantity);

            // OK Button
            btnOK = new Button
            {
                Text = "Print",
                Location = new Point(130, 120),
                Size = new Size(85, 30),
                DialogResult = DialogResult.None,
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOK.Click += BtnOK_Click;
            this.Controls.Add(btnOK);

            // Cancel Button
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(225, 120),
                Size = new Size(85, 30),
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);

            // Set form properties
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private void TxtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow digits and control characters (backspace, etc.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // Validate ID
            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show("Please enter a Barcode ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtId.Focus();
                return;
            }

            int id;
            if (!int.TryParse(txtId.Text.Trim(), out id) || id <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for Barcode ID.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtId.Focus();
                txtId.SelectAll();
                return;
            }

            // Set properties
            BarcodeId = id;
            Quantity = (int)numQuantity.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
