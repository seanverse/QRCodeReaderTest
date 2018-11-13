using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Test of qrcode generation and recognition
    /// author: seanrush@163.com
    /// </summary>
    public partial class FormQRReadTest : Form
    {
        public FormQRReadTest()
        {
            InitializeComponent();
        }             

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (this.imgOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = this.imgOpenFileDialog.FileName;
                Bitmap bmp = new Bitmap(fileName);
                this.picBoxQRCode.Image = bmp;
                Application.DoEvents();
                BarcodeReader reader = new BarcodeReader();
                reader.Options.CharacterSet = "UTF-8";                               
                Result result = reader.Decode(bmp);
                this.txtCodeText.Text = result.Text;               
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string codeText = this.txtCodeText.Text.Trim();
            if (string.IsNullOrWhiteSpace(codeText))
            {
                MessageBox.Show("Please input the generated text first.","Hint", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.CharacterSet = "UTF-8";
            options.DisableECI = true; // Extended Channel Interpretation (ECI) 主要用于特殊的字符集。并不是所有的扫描器都支持这种编码。 
            options.ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H; // 纠错级别 
            options.Width = 300; options.Height = 300; options.Margin = 1; // options.Hints，更多属性，也可以在这里添加。
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE; writer.Options = options;

            Image temp = this.picBoxQRCode.Image;
            this.picBoxQRCode.Image = null;
            if (temp != null) { temp.Dispose(); temp = null; }
            Bitmap bmp = writer.Write(this.txtCodeText.Text);  // Write 具备生成、写入两个功能 
            this.picBoxQRCode.Image = bmp;            
        }

        private void FormQRReadTest_Load(object sender, EventArgs e)
        {
            //if (File.Exists)
        }
    }
}
