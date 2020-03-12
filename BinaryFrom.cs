using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

using OpenCvSharp;
using OpenCvSharp.XFeatures2D;
using Test;
namespace WinFrom
{
    public partial class BinaryFrom : Form
    {
        private string _FileImagePath = "";
        private bool Freeze = false;         //避免 trackBarScrollTh() 資料更動而多執行 ThresholdChange()

        // https://blog.xuite.net/f8789/DCLoveEP/33810131-C%23+-+%E4%BD%BF%E7%94%A8+List%3CT%3E+%E7%82%BA+ComboBox+%E5%8A%A0%E5%85%A5+Item
        public class AdaptCoboxList
        {
            private string typename;
            private int typevalue;
            public string TypeName { set { typename = value; } }
            public int TypeValue { get { return typevalue; } set { typevalue = value; } }
        }

        public BinaryFrom()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpFDialog = new OpenFileDialog();
            OpFDialog.Filter = @"(*.bmp,*.jpg,*png)|*.bmp;*.jpg;*png";
            OpFDialog.FilterIndex = 3;
            OpFDialog.RestoreDirectory = true;
            OpFDialog.InitialDirectory = @"D:\\";
            if (OpFDialog.ShowDialog() == DialogResult.OK)
            {
                _FileImagePath = OpFDialog.FileName;
                pictureBox1.ImageLocation = _FileImagePath;
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            Bitmap SaveBmp = (Bitmap)pictureBox2.Image;

            SaveFileDialog SvFDialog = new SaveFileDialog();
            SvFDialog.Filter = @"(*.bmp,*.jpg,*png)|*.bmp;*.jpg;*png";
            SvFDialog.FilterIndex = 3;
            SvFDialog.RestoreDirectory = true;
            if (SvFDialog.ShowDialog() == DialogResult.OK)
            {
                ImageFormat format = ImageFormat.Jpeg;
                switch (Path.GetExtension(SvFDialog.FileName).ToLower())
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    default:
                        MessageBox.Show(this, "Unsupported image format was specified", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
                try
                {
                    SaveBmp.Save(SvFDialog.FileName, format);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Failed writing image file", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public Bitmap MatToBitmap(Mat image)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
        }

        public Mat BitmapToMat(Bitmap image)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToMat(image);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Binary binary = new Binary();

            Mat Dst = new Mat();
            using (Mat Src = new Mat(_FileImagePath))
            using (Mat Gray = new Mat())
            {
                Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                int VE_Threshold = binary.Otsu(Gray,Dst,Binary.OtsuType.VE);
                Cv2.Threshold(Gray, Dst, VE_Threshold, 255, ThresholdTypes.Binary);
            }
            pictureBox2.Image = MatToBitmap(Dst);
        }

        Binary binary = new Binary();
        ImageTool Tool = new ImageTool();

        private void btnOtsu_Click(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            Mat Dst = new Mat();
            using (Mat Src = new Mat(_FileImagePath))
            using (Mat Gray = new Mat())
            {
                Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                if (check_Negative.Checked)
                    Tool.Negative(Gray, Gray);

                int Threshold = binary.Otsu(Gray, Dst, Binary.OtsuType.Otsu);
                Cv2.Threshold(Gray, Dst, Threshold, 255, ThresholdTypes.Binary);

                //Drawing Color
                Tool.SetColor(255, 0, 0);
                if (check_DrawingColor.Checked)
                    Tool.OutColor(Gray, Dst, Dst, true);

                label_Th.Text += Threshold.ToString();
            }
            pictureBox2.Image = MatToBitmap(Dst);
        }

        private void btn_VE_Click(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            Mat Dst = new Mat();
            using (Mat Src = new Mat(_FileImagePath))
            using (Mat Gray = new Mat())
            {
                Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                if (check_Negative.Checked)
                    Tool.Negative(Gray, Gray);

                int Threshold = binary.Otsu(Gray, Dst, Binary.OtsuType.VE);
                Cv2.Threshold(Gray, Dst, Threshold, 255, ThresholdTypes.Binary);

                //Drawing Color
                Tool.SetColor(255, 0, 0);
                if (check_DrawingColor.Checked)
                    Tool.OutColor(Gray, Dst, Dst, true);

                label_Th.Text += Threshold.ToString();
            }
            pictureBox2.Image = MatToBitmap(Dst);
        }

        private void btn_NVE_Click(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            Mat Dst = new Mat();
            using (Mat Src = new Mat(_FileImagePath))
            using (Mat Gray = new Mat())
            {
                Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                if (check_Negative.Checked)
                    Tool.Negative(Gray, Gray);

                binary.Neighbor = (int)Neighbor.Value;
                int Threshold = binary.Otsu(Gray, Dst, Binary.OtsuType.NVE);
                Cv2.Threshold(Gray, Dst, Threshold, 255, ThresholdTypes.Binary);

                //Drawing Color
                Tool.SetColor(255, 0, 0);
                if (check_DrawingColor.Checked)
                    Tool.OutColor(Gray, Dst, Dst, true);

                label_Th.Text += Threshold.ToString();
            }
            pictureBox2.Image = MatToBitmap(Dst);
        }

        private void btn_WOV_Click(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            Mat Dst = new Mat();
            using (Mat Src = new Mat(_FileImagePath))
            using (Mat Gray = new Mat())
            {
                Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                if (check_Negative.Checked)
                    Tool.Negative(Gray, Gray);

                binary.Neighbor = (int)Neighbor.Value;
                int Threshold = binary.Otsu(Gray, Dst, Binary.OtsuType.WOV);
                Cv2.Threshold(Gray, Dst, Threshold, 255, ThresholdTypes.Binary);

                //Drawing Color
                Tool.SetColor(255, 0, 0);
                if (check_DrawingColor.Checked)
                    Tool.OutColor(Gray, Dst, Dst, true);

                label_Th.Text += Threshold.ToString();
            }
            pictureBox2.Image = MatToBitmap(Dst);
        }

        private void btn_Adaptive_Click(object sender, EventArgs e)
        {
            //Cv2.AdaptiveThreshold(Gray, AdaptiveThreshold, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.BinaryInv, AdaptBlockSize, Adapt_c);
            label_Th.Text = "Threshold: NON";

            Mat Dst = new Mat();
            using (Mat Src = new Mat(_FileImagePath))
            using (Mat Gray = new Mat())
            {
                Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                if (check_Negative.Checked)
                    Tool.Negative(Gray, Gray);

                binary.Neighbor = (int)Neighbor.Value;
                binary.Otsu(Gray, Dst, Binary.OtsuType.Adaptive);

                //Drawing Color
                Tool.SetColor(255, 0, 0);
                if (check_DrawingColor.Checked)
                    Tool.OutColor(Gray, Dst, Dst, true);

            }
            pictureBox2.Image = MatToBitmap(Dst);
        }

        private void btn_Threshold_Click(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            Mat Dst = new Mat();
            using (Mat Src = new Mat(_FileImagePath))
            using (Mat Gray = new Mat())
            {
                Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                if (check_Negative.Checked)
                    Tool.Negative(Gray, Gray);

                int Threshold = (int)ThresholdValue.Value;
                trackBar_Th.Value = Threshold;
                Cv2.Threshold(Gray, Dst, Threshold, 255, ThresholdTypes.Binary);

                //Drawing Color
                Tool.SetColor(255, 0, 0);
                if (check_DrawingColor.Checked)
                    Tool.OutColor(Gray, Dst, Dst, true);

                label_Th.Text += Threshold.ToString();
            }
            pictureBox2.Image = MatToBitmap(Dst);
        }

        private void trackBarScrollTh(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            if (_FileImagePath != "" && !Freeze)
            {
                Freeze = true;

                Mat Dst = new Mat();
                using (Mat Src = new Mat(_FileImagePath))
                using (Mat Gray = new Mat())
                {
                    Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                    if (check_Negative.Checked)
                        Tool.Negative(Gray, Gray);

                    int Threshold = trackBar_Th.Value;
                    ThresholdValue.Value = Threshold;
                    Cv2.Threshold(Gray, Dst, Threshold, 255, ThresholdTypes.Binary);

                    //Drawing Color
                    Tool.SetColor(255, 0, 0);
                    if (check_DrawingColor.Checked)
                        Tool.OutColor(Gray, Dst, Dst, true);

                    label_Th.Text += Threshold.ToString();
                    Freeze = false;
                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
            else
            {
                ThresholdValue.Value = trackBar_Th.Value;
            }

        }

        private void ThresholdChange(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            if (_FileImagePath != "" && !Freeze)
            {
                Freeze = true;

                Mat Dst = new Mat();
                using (Mat Src = new Mat(_FileImagePath))
                using (Mat Gray = new Mat())
                {
                    Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                    if (check_Negative.Checked)
                        Tool.Negative(Gray, Gray);

                    int Threshold = (int)ThresholdValue.Value;
                    trackBar_Th.Value = Threshold;
                    Cv2.Threshold(Gray, Dst, Threshold, 255, ThresholdTypes.Binary);

                    //Drawing Color
                    Tool.SetColor(255, 0, 0);
                    if (check_DrawingColor.Checked)
                        Tool.OutColor(Gray, Dst, Dst, true);

                    label_Th.Text += Threshold.ToString();
                    Freeze = false;
                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
            else
            {
                trackBar_Th.Value = (int)ThresholdValue.Value;
            }
        }




    }
}
