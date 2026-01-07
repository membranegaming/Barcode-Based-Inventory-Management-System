using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;

namespace store_parts
{
    /// <summary>
    /// Barcode printer class that supports BarTender SDK and direct TSPL commands.
    /// </summary>
    public class BarcodePrinter
    {
        #region Properties

        /// <summary>
        /// Name of the printer to use
        /// </summary>
        public string PrinterName { get; set; }

        /// <summary>
        /// Path to BarTender label template file (.btw)
        /// </summary>
        public string TemplatePath { get; set; }

        /// <summary>
        /// Printing method: BarTender or TSPL
        /// </summary>
        public PrintMethod Method { get; set; } = PrintMethod.TSPL;

        /// <summary>
        /// Label width in mm (for TSPL)
        /// </summary>
        public int LabelWidth { get; set; } = 50;

        /// <summary>
        /// Label height in mm (for TSPL)
        /// </summary>
        public int LabelHeight { get; set; } = 30;

        /// <summary>
        /// Gap between labels in mm (for TSPL)
        /// </summary>
        public int LabelGap { get; set; } = 3;

        /// <summary>
        /// Barcode height in dots (for TSPL)
        /// </summary>
        public int BarcodeHeight { get; set; } = 100;

        /// <summary>
        /// Barcode X position in dots (for TSPL)
        /// </summary>
        public int BarcodeX { get; set; } = 50;

        /// <summary>
        /// Barcode Y position in dots (for TSPL)
        /// </summary>
        public int BarcodeY { get; set; } = 50;

        #endregion

        #region Enums

        public enum PrintMethod
        {
            BarTender,
            TSPL
        }

        #endregion

        #region Constructor

        public BarcodePrinter()
        {
            PrinterName = GetDefaultPrinter();
        }

        public BarcodePrinter(string printerName)
        {
            PrinterName = printerName;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get list of available printers on the system
        /// </summary>
        public static List<string> GetAvailablePrinters()
        {
            List<string> printers = new List<string>();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }
            return printers;
        }

        /// <summary>
        /// Get the default printer name
        /// </summary>
        public static string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            return settings.PrinterName;
        }

        /// <summary>
        /// Find TSC printer if available
        /// </summary>
        public static string FindTSCPrinter()
        {
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (printer.ToUpper().Contains("TSC") || printer.ToUpper().Contains("TTP"))
                {
                    return printer;
                }
            }
            return null;
        }

        /// <summary>
        /// Print barcode labels for a given ID
        /// </summary>
        /// <param name="id">The ID to encode in the barcode</param>
        /// <param name="quantity">Number of labels to print</param>
        /// <returns>True if successful</returns>
        public bool PrintBarcode(string id, int quantity)
        {
            if (string.IsNullOrEmpty(PrinterName))
            {
                throw new InvalidOperationException("No printer selected");
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "ID cannot be empty");
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than 0", nameof(quantity));
            }

            switch (Method)
            {
                case PrintMethod.BarTender:
                    return PrintWithBarTender(id, quantity);
                case PrintMethod.TSPL:
                    return PrintWithTSPL(id, quantity);
                default:
                    return PrintWithTSPL(id, quantity);
            }
        }

        /// <summary>
        /// Print a test label
        /// </summary>
        public bool PrintTestLabel()
        {
            return PrintBarcode("TEST123", 1);
        }

        /// <summary>
        /// Load printer settings from application settings
        /// </summary>
        public static BarcodePrinter LoadFromSettings()
        {
            var printer = new BarcodePrinter();
            try
            {
                var s = Properties.Settings.Default;
                
                if (!string.IsNullOrEmpty(s.BarcodePrinterName))
                    printer.PrinterName = s.BarcodePrinterName;
                
                if (!string.IsNullOrEmpty(s.BarcodeTemplatePath))
                    printer.TemplatePath = s.BarcodeTemplatePath;
                
                printer.Method = (PrintMethod)s.BarcodePrintMethod;
                printer.LabelWidth = s.BarcodeLabelWidth > 0 ? s.BarcodeLabelWidth : 50;
                printer.LabelHeight = s.BarcodeLabelHeight > 0 ? s.BarcodeLabelHeight : 30;
                printer.LabelGap = s.BarcodeLabelGap > 0 ? s.BarcodeLabelGap : 3;
                printer.BarcodeHeight = s.BarcodeBarcodeHeight > 0 ? s.BarcodeBarcodeHeight : 100;
                printer.BarcodeX = s.BarcodePosX > 0 ? s.BarcodePosX : 50;
                printer.BarcodeY = s.BarcodePosY > 0 ? s.BarcodePosY : 50;
            }
            catch
            {
                // Use defaults if settings fail to load
            }
            return printer;
        }

        /// <summary>
        /// Save printer settings to application settings
        /// </summary>
        public void SaveToSettings()
        {
            var s = Properties.Settings.Default;
            s.BarcodePrinterName = PrinterName ?? "";
            s.BarcodeTemplatePath = TemplatePath ?? "";
            s.BarcodePrintMethod = (int)Method;
            s.BarcodeLabelWidth = LabelWidth;
            s.BarcodeLabelHeight = LabelHeight;
            s.BarcodeLabelGap = LabelGap;
            s.BarcodeBarcodeHeight = BarcodeHeight;
            s.BarcodePosX = BarcodeX;
            s.BarcodePosY = BarcodeY;
            s.Save();
        }

        #endregion

        #region Private Methods - BarTender

        /// <summary>
        /// Print using BarTender SDK
        /// </summary>
        private bool PrintWithBarTender(string id, int quantity)
        {
            // Check if template exists
            if (string.IsNullOrEmpty(TemplatePath) || !File.Exists(TemplatePath))
            {
                throw new FileNotFoundException("BarTender template not found. Please configure the template path in Printer Settings.", TemplatePath);
            }

            try
            {
                // Use late binding to avoid hard dependency on BarTender SDK
                // This allows the app to run even if BarTender is not installed
                Type btEngineType = Type.GetTypeFromProgID("BarTender.Application");
                
                if (btEngineType == null)
                {
                    throw new InvalidOperationException("BarTender is not installed or registered on this system.");
                }

                dynamic btApp = Activator.CreateInstance(btEngineType);
                btApp.Visible = false;

                try
                {
                    // Open the template
                    dynamic btFormat = btApp.Formats.Open(TemplatePath, false, "");
                    
                    try
                    {
                        // Set the printer
                        if (!string.IsNullOrEmpty(PrinterName))
                        {
                            btFormat.PrintSetup.Printer = PrinterName;
                        }

                        // Set the barcode data - try common field names
                        try
                        {
                            btFormat.SetNamedSubStringValue("ID", id);
                        }
                        catch
                        {
                            try
                            {
                                btFormat.SetNamedSubStringValue("Barcode", id);
                            }
                            catch
                            {
                                try
                                {
                                    btFormat.SetNamedSubStringValue("Data", id);
                                }
                                catch
                                {
                                    // If no named field found, the template might use embedded data source
                                }
                            }
                        }

                        // Set quantity and print
                        btFormat.PrintSetup.NumberOfSerializedLabels = quantity;
                        btFormat.Print("", false);

                        return true;
                    }
                    finally
                    {
                        btFormat.Close(1); // 1 = DoNotSaveChanges
                    }
                }
                finally
                {
                    btApp.Quit(1); // 1 = DoNotSaveChanges
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("BarTender print error: " + ex.Message);
                throw;
            }
        }

        #endregion

        #region Private Methods - TSPL

        /// <summary>
        /// Print using TSPL commands directly to TSC printer
        /// </summary>
        private bool PrintWithTSPL(string id, int quantity)
        {
            try
            {
                string tsplCommands = GenerateTSPLCommands(id, quantity);
                return SendCommandsToPrinter(tsplCommands);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("TSPL print error: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Generate TSPL commands for barcode printing
        /// </summary>
        private string GenerateTSPLCommands(string id, int quantity)
        {
            // Calculate text position (centered under barcode)
            // Text Y position = Barcode Y + Barcode Height + small gap
            int textY = BarcodeY + BarcodeHeight + 10;
            
            // TSPL command format for TSC printers
            string commands = $@"SIZE {LabelWidth} mm, {LabelHeight} mm
GAP {LabelGap} mm, 0 mm
DIRECTION 1
CLS
BARCODE {BarcodeX}, {BarcodeY}, ""128"", {BarcodeHeight}, 0, 0, 2, 2, ""{id}""
TEXT {BarcodeX}, {textY}, ""3"", 0, 1, 1, ""{id}""
PRINT {quantity}, 1
";
            return commands;
        }

        /// <summary>
        /// Send raw commands to printer using Windows API
        /// </summary>
        private bool SendCommandsToPrinter(string commands)
        {
            IntPtr hPrinter = IntPtr.Zero;
            DOCINFOA di = new DOCINFOA();
            bool success = false;

            di.pDocName = "Barcode Label";
            di.pDataType = "RAW";

            if (OpenPrinter(PrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    if (StartPagePrinter(hPrinter))
                    {
                        IntPtr pBytes = System.Runtime.InteropServices.Marshal.StringToCoTaskMemAnsi(commands);
                        int dwWritten = 0;
                        success = WritePrinter(hPrinter, pBytes, commands.Length, out dwWritten);
                        System.Runtime.InteropServices.Marshal.FreeCoTaskMem(pBytes);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }

            return success;
        }

        #endregion

        #region Windows API Declarations

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        private class DOCINFOA
        {
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
            public string pDocName;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
            public string pOutputFile;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
            public string pDataType;
        }

        [System.Runtime.InteropServices.DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, ExactSpelling = true, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern bool OpenPrinter([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [System.Runtime.InteropServices.DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [System.Runtime.InteropServices.DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, ExactSpelling = true, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [System.Runtime.InteropServices.In, System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStruct)] DOCINFOA di);

        [System.Runtime.InteropServices.DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [System.Runtime.InteropServices.DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [System.Runtime.InteropServices.DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [System.Runtime.InteropServices.DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        #endregion
    }
}
