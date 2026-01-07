using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace store_parts
{
    /// <summary>
    /// Form for previewing barcode labels before printing
    /// </summary>
    public partial class BarcodePrintPreviewForm : Form
    {
        private string _barcodeData;
        private int _quantity;
        private BarcodePrinter _printer;
        private int _currentPage = 1;

        public bool PrintConfirmed { get; private set; } = false;

        public BarcodePrintPreviewForm(string barcodeData, int quantity)
        {
            _barcodeData = barcodeData;
            _quantity = quantity;
            _printer = BarcodePrinter.LoadFromSettings();

            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // Form properties
            this.Text = "Barcode Print Preview";
            this.Size = new Size(500, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Main layout panel
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4,
                Padding = new Padding(10)
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Info
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // Preview
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40)); // Navigation
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50)); // Buttons

            // Info panel
            Panel infoPanel = new Panel { Dock = DockStyle.Fill };
            Label lblInfo = new Label
            {
                Text = $"ID: {_barcodeData}  |  Quantity: {_quantity} label(s)  |  Printer: {_printer?.PrinterName ?? "Not configured"}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };
            infoPanel.Controls.Add(lblInfo);
            mainLayout.Controls.Add(infoPanel, 0, 0);

            // Preview panel with border
            Panel previewContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.LightGray
            };

            PictureBox previewBox = new PictureBox
            {
                Name = "previewBox",
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.CenterImage,
                BorderStyle = BorderStyle.FixedSingle
            };
            previewBox.Paint += PreviewBox_Paint;
            previewContainer.Controls.Add(previewBox);
            mainLayout.Controls.Add(previewContainer, 0, 1);

            // Navigation panel
            Panel navPanel = new Panel { Dock = DockStyle.Fill };
            FlowLayoutPanel navFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = false
            };
            navFlow.Padding = new Padding((this.Width - 200) / 2, 5, 0, 0);

            Button btnPrev = new Button
            {
                Name = "btnPrev",
                Text = "<< Previous",
                Width = 80,
                Enabled = false
            };
            btnPrev.Click += BtnPrev_Click;

            Label lblPage = new Label
            {
                Name = "lblPage",
                Text = $"Label 1 of {_quantity}",
                Width = 100,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 5, 0, 0)
            };

            Button btnNext = new Button
            {
                Name = "btnNext",
                Text = "Next >>",
                Width = 80,
                Enabled = _quantity > 1
            };
            btnNext.Click += BtnNext_Click;

            navFlow.Controls.AddRange(new Control[] { btnPrev, lblPage, btnNext });
            navPanel.Controls.Add(navFlow);
            mainLayout.Controls.Add(navPanel, 0, 2);

            // Button panel
            Panel buttonPanel = new Panel { Dock = DockStyle.Fill };
            FlowLayoutPanel buttonFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 10, 10, 0)
            };

            Button btnCancel = new Button
            {
                Text = "Cancel",
                Width = 100,
                Height = 35,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.Click += (s, e) => { PrintConfirmed = false; this.Close(); };

            Button btnPrint = new Button
            {
                Text = "Print",
                Width = 120,
                Height = 35,
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnPrint.Click += BtnPrint_Click;

            Button btnPrinterSettings = new Button
            {
                Text = "Settings",
                Width = 100,
                Height = 35
            };
            btnPrinterSettings.Click += BtnPrinterSettings_Click;

            buttonFlow.Controls.AddRange(new Control[] { btnCancel, btnPrint, btnPrinterSettings });
            buttonPanel.Controls.Add(buttonFlow);
            mainLayout.Controls.Add(buttonPanel, 0, 3);

            this.Controls.Add(mainLayout);
            this.AcceptButton = btnPrint;
            this.CancelButton = btnCancel;
        }

        private void PreviewBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb == null) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Calculate scale to fit preview
            int labelWidth = _printer?.LabelWidth ?? 50;
            int labelHeight = _printer?.LabelHeight ?? 30;

            // Convert mm to pixels (approximately 3.78 pixels per mm at 96 DPI)
            float scale = 3.78f;
            int pixelWidth = (int)(labelWidth * scale);
            int pixelHeight = (int)(labelHeight * scale);

            // Scale to fit in preview box
            float scaleX = (float)(pb.Width - 40) / pixelWidth;
            float scaleY = (float)(pb.Height - 40) / pixelHeight;
            float finalScale = Math.Min(scaleX, scaleY);

            int drawWidth = (int)(pixelWidth * finalScale);
            int drawHeight = (int)(pixelHeight * finalScale);
            int startX = (pb.Width - drawWidth) / 2;
            int startY = (pb.Height - drawHeight) / 2;

            // Draw label background with shadow
            g.FillRectangle(new SolidBrush(Color.FromArgb(50, 0, 0, 0)), 
                startX + 3, startY + 3, drawWidth, drawHeight);
            g.FillRectangle(Brushes.White, startX, startY, drawWidth, drawHeight);
            g.DrawRectangle(Pens.Black, startX, startY, drawWidth, drawHeight);

            // Draw barcode representation
            int barcodeX = startX + (int)((_printer?.BarcodeX ?? 50) * finalScale * scale / 8);
            int barcodeY = startY + (int)((_printer?.BarcodeY ?? 50) * finalScale * scale / 8);
            int barcodeHeight = (int)((_printer?.BarcodeHeight ?? 100) * finalScale / 3);
            int barcodeWidth = (int)(drawWidth * 0.7);

            // Ensure barcode fits within label
            if (barcodeX + barcodeWidth > startX + drawWidth - 10)
                barcodeWidth = startX + drawWidth - barcodeX - 10;
            if (barcodeY + barcodeHeight > startY + drawHeight - 30)
                barcodeHeight = startY + drawHeight - barcodeY - 30;

            // Draw barcode bars (Code 128 simulation)
            DrawBarcodeSimulation(g, barcodeX, barcodeY, barcodeWidth, barcodeHeight);

            // Draw barcode text
            using (Font font = new Font("Consolas", 10 * finalScale, FontStyle.Bold))
            {
                SizeF textSize = g.MeasureString(_barcodeData, font);
                float textX = barcodeX + (barcodeWidth - textSize.Width) / 2;
                float textY = barcodeY + barcodeHeight + 5;
                g.DrawString(_barcodeData, font, Brushes.Black, textX, textY);
            }

            // Draw label number
            using (Font font = new Font("Segoe UI", 8 * finalScale))
            {
                string labelText = $"Label {_currentPage} of {_quantity}";
                SizeF textSize = g.MeasureString(labelText, font);
                g.DrawString(labelText, font, Brushes.Gray, 
                    startX + drawWidth - textSize.Width - 5, 
                    startY + 5);
            }

            // Draw label dimensions
            using (Font font = new Font("Segoe UI", 7))
            {
                string dimText = $"{labelWidth}mm x {labelHeight}mm";
                g.DrawString(dimText, font, Brushes.DarkGray, startX + 5, startY + drawHeight + 5);
            }
        }

        private void DrawBarcodeSimulation(Graphics g, int x, int y, int width, int height)
        {
            // Generate pseudo-random bars based on barcode data
            Random rand = new Random(_barcodeData.GetHashCode());
            int barX = x;
            int minBarWidth = Math.Max(1, width / 60);
            int maxBarWidth = Math.Max(2, width / 30);

            while (barX < x + width - maxBarWidth)
            {
                int barWidth = rand.Next(minBarWidth, maxBarWidth + 1);
                bool isBlack = rand.Next(2) == 0;

                if (isBlack)
                {
                    g.FillRectangle(Brushes.Black, barX, y, barWidth, height);
                }

                barX += barWidth;
            }

            // Draw start and stop patterns (thick bars)
            g.FillRectangle(Brushes.Black, x, y, minBarWidth * 2, height);
            g.FillRectangle(Brushes.Black, x + width - minBarWidth * 2, y, minBarWidth * 2, height);
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateNavigation();
                RefreshPreview();
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage < _quantity)
            {
                _currentPage++;
                UpdateNavigation();
                RefreshPreview();
            }
        }

        private void UpdateNavigation()
        {
            var lblPage = this.Controls.Find("lblPage", true);
            if (lblPage.Length > 0)
                ((Label)lblPage[0]).Text = $"Label {_currentPage} of {_quantity}";

            var btnPrev = this.Controls.Find("btnPrev", true);
            if (btnPrev.Length > 0)
                ((Button)btnPrev[0]).Enabled = _currentPage > 1;

            var btnNext = this.Controls.Find("btnNext", true);
            if (btnNext.Length > 0)
                ((Button)btnNext[0]).Enabled = _currentPage < _quantity;
        }

        private void RefreshPreview()
        {
            var previewBox = this.Controls.Find("previewBox", true);
            if (previewBox.Length > 0)
                previewBox[0].Invalidate();
        }

        private void BtnPrinterSettings_Click(object sender, EventArgs e)
        {
            using (PrinterSettingsForm settingsForm = new PrinterSettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    _printer = BarcodePrinter.LoadFromSettings();
                    RefreshPreview();

                    // Update info label
                    foreach (Control c in this.Controls)
                    {
                        if (c is TableLayoutPanel tlp)
                        {
                            foreach (Control c2 in tlp.Controls)
                            {
                                if (c2 is Panel p)
                                {
                                    foreach (Control c3 in p.Controls)
                                    {
                                        if (c3 is Label lbl && lbl.ForeColor == Color.DarkBlue)
                                        {
                                            lbl.Text = $"ID: {_barcodeData}  |  Quantity: {_quantity} label(s)  |  Printer: {_printer?.PrinterName ?? "Not configured"}";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (_printer == null || string.IsNullOrEmpty(_printer.PrinterName))
            {
                MessageBox.Show("Please configure a printer first.", "Printer Not Configured",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintConfirmed = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
