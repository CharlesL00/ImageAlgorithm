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
    public partial class BinaryFrom : Form, ValueInterface
    {        
        Binary binary = new Binary();
        ImageTool Tool = new ImageTool();

        private Mat _SourceImage;
        private string _FileImagePath = "";
        private int _ImageMaxWidth = 0;
        private bool _Freeze = false;         //避免 trackBarScrollTh() 資料更動而多執行 ThresholdChange()
        private int _MouseWheel_TriggerCount = 0;

        // https://blog.xuite.net/f8789/DCLoveEP/33810131-C%23+-+%E4%BD%BF%E7%94%A8+List%3CT%3E+%E7%82%BA+ComboBox+%E5%8A%A0%E5%85%A5+Item
        protected class AdaptTypeCoboxList
        {
            private string _AdaptType;
            private int _AdaptMethodNumber;
            public string AdaptType { get { return _AdaptType; } set { _AdaptType = value; } }
            public int AdaptMethodNumber { get { return _AdaptMethodNumber; } set { _AdaptMethodNumber = value; } }
            //public string AdaptType { get; set; }
            //public int AdaptMethodNumber { get; set; }
        }

        public BinaryFrom()
        {
            InitializeComponent();

            List<AdaptTypeCoboxList> AdaptTypeSet = new List<AdaptTypeCoboxList>()
            {
                new AdaptTypeCoboxList()
                {
                    AdaptType = "GaussianC",
                    AdaptMethodNumber = 0
                },
                new AdaptTypeCoboxList()
                {
                    AdaptType = "MeanC",
                    AdaptMethodNumber = 1
                }
            };
            comboBox_AdaptType.DataSource = AdaptTypeSet;
            comboBox_AdaptType.DisplayMember = "AdaptType";
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
                using (Mat Src = new Mat(_FileImagePath))
                {
                    _SourceImage = new Mat();
                    Src.CopyTo(_SourceImage);
                    if (Src.Cols > Src.Rows)
                        _ImageMaxWidth = Src.Cols;
                    else
                        _ImageMaxWidth = Src.Rows;

                    UpDown_AdaptSize.Maximum = _ImageMaxWidth;
                }

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
            using (Mat Src = new Mat())
            using (Mat Gray = new Mat())
            {
                _SourceImage.CopyTo(Src);
                Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                int VE_Threshold = binary.Otsu(Gray,Dst,Binary.OtsuType.VE);
                Cv2.Threshold(Gray, Dst, VE_Threshold, 255, ThresholdTypes.Binary);
            }
            pictureBox2.Image = MatToBitmap(Dst);
        }

        private void btnOtsu_Click(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            if (_SourceImage != null)
            {
                Mat Dst = new Mat();
                using (Mat Src = new Mat())
                using (Mat Gray = new Mat())
                {
                    _SourceImage.CopyTo(Src);

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
                    ThresholdValue.Value = Threshold;
                    trackBar_Th.Value = Threshold;
                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
        }

        private void btn_VE_Click(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            if (_SourceImage != null)
            {
                Mat Dst = new Mat();
                using (Mat Src = new Mat())
                using (Mat Gray = new Mat())
                {
                    _SourceImage.CopyTo(Src);

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
                    ThresholdValue.Value = Threshold;
                    trackBar_Th.Value = Threshold;
                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
        }

        private void btn_NVE_Click(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            if (_SourceImage != null)
            {
                Mat Dst = new Mat();
                using (Mat Src = new Mat())
                using (Mat Gray = new Mat())
                {
                    _SourceImage.CopyTo(Src);

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
                    ThresholdValue.Value = Threshold;
                    trackBar_Th.Value = Threshold;
                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
        }

        private void btn_WOV_Click(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            if (_SourceImage != null)
            {
                Mat Dst = new Mat();
                using (Mat Src = new Mat())
                using (Mat Gray = new Mat())
                {
                    _SourceImage.CopyTo(Src);

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
                    ThresholdValue.Value = Threshold;
                    trackBar_Th.Value = Threshold;
                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
        }

        private void btn_Adaptive_Click(object sender, EventArgs e)
        {
            //Cv2.AdaptiveThreshold(Gray, AdaptiveThreshold, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.BinaryInv, AdaptBlockSize, Adapt_c);
            if (_SourceImage != null)
            {
                label_Th.Text = "Threshold: NON";

                Mat Dst = new Mat();
                using (Mat Src = new Mat())
                using (Mat Gray = new Mat())
                {
                    _SourceImage.CopyTo(Src);

                    Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                    if (check_Negative.Checked)
                        Tool.Negative(Gray, Gray);

                    int AdaptSize = (int)UpDown_AdaptSize.Value;
                    if (AdaptSize % 2 == 0)
                        binary.AdaptBlockSize = AdaptSize - 1;
                    else
                        binary.AdaptBlockSize = AdaptSize;

                    binary.Adapt_c = (int)UpDown_AdaptC.Value;
                    binary.Otsu(Gray, Dst, Binary.OtsuType.Adaptive);

                    //Drawing Color
                    Tool.SetColor(255, 0, 0);
                    if (check_DrawingColor.Checked)
                        Tool.OutColor(Gray, Dst, Dst, true);

                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
        }

        private void btn_Threshold_Click(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            if (_SourceImage != null)
            {
                Mat Dst = new Mat();
                using (Mat Src = new Mat())
                using (Mat Gray = new Mat())
                {
                    _SourceImage.CopyTo(Src);

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
        }

        private void trackBarScrollTh(object sender, EventArgs e)
        {
            label_Th.Text = "Threshold: ";

            if (_SourceImage != null && !_Freeze)
            {
                _Freeze = true;

                Mat Dst = new Mat();
                using (Mat Src = new Mat())
                using (Mat Gray = new Mat())
                {
                    _SourceImage.CopyTo(Src);

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
                    _Freeze = false;
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
            _MouseWheel_TriggerCount++;
            label_Th.Text = "Threshold: ";

            if (_SourceImage != null && !_Freeze && _MouseWheel_TriggerCount>=3)
            {
                _MouseWheel_TriggerCount = 0;
                _Freeze = true;

                Mat Dst = new Mat();
                using (Mat Src = new Mat())
                using (Mat Gray = new Mat())
                {
                    _SourceImage.CopyTo(Src);

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
                    _Freeze = false;
                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
            else
            {
                trackBar_Th.Value = (int)ThresholdValue.Value;
            }
        }

        void AdaptSizeChange(object sender, EventArgs e)
        {
            _MouseWheel_TriggerCount++;
            Console.WriteLine("AdaptSizeChange");

            if (_SourceImage != null && _MouseWheel_TriggerCount>=3)
            {
                _MouseWheel_TriggerCount = 0;
                Console.WriteLine("AdaptSizeChange_Into");

                label_Th.Text = "Threshold: NON";
                Mat Dst = new Mat();
                using (Mat Src = new Mat(_FileImagePath))
                using (Mat Gray = new Mat())
                {
                    Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                    if (check_Negative.Checked)
                        Tool.Negative(Gray, Gray);

                    // 2020/03/16 Bug-會錯值
                    int AdaptSize = (int)UpDown_AdaptSize.Value;
                    if (AdaptSize < 3)
                        binary.AdaptBlockSize = 3;
                    else if (AdaptSize % 2 == 0)
                        binary.AdaptBlockSize = AdaptSize - 1;
                    else
                        binary.AdaptBlockSize = AdaptSize;

                    binary.Adapt_c = (int)UpDown_AdaptC.Value;
                    binary.Otsu(Gray, Dst, Binary.OtsuType.Adaptive);

                    //Drawing Color
                    Tool.SetColor(255, 0, 0);
                    if (check_DrawingColor.Checked)
                        Tool.OutColor(Gray, Dst, Dst, true);

                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
        }

        void AdaptC_Change(object sender, EventArgs e)
        {
            _MouseWheel_TriggerCount++;

            if (_SourceImage != null && _MouseWheel_TriggerCount >= 3)
            {
                _MouseWheel_TriggerCount = 0;
                label_Th.Text = "Threshold: NON";

                Mat Dst = new Mat();
                using (Mat Src = new Mat(_FileImagePath))
                using (Mat Gray = new Mat())
                {
                    Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                    if (check_Negative.Checked)
                        Tool.Negative(Gray, Gray);

                    int AdaptSize = (int)UpDown_AdaptSize.Value;
                    if (AdaptSize % 2 == 0)
                        binary.AdaptBlockSize = AdaptSize - 1;
                    else
                        binary.AdaptBlockSize = AdaptSize;

                    binary.Adapt_c = (int)UpDown_AdaptC.Value;
                    binary.Otsu(Gray, Dst, Binary.OtsuType.Adaptive);

                    //Drawing Color
                    Tool.SetColor(255, 0, 0);
                    if (check_DrawingColor.Checked)
                        Tool.OutColor(Gray, Dst, Dst, true);

                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
        }

        // -------------------------- 上下按鈕控制事件 --------------------------

        void Neighbor_MouseWheel(object sender, MouseEventArgs e)
        {
            // (1) numberOfTextLinesToMove :+120=向上滑，-120=向下滑
            // (2) 原先 combox Increment = SystemInformation.MouseWheelScrollLines
            //     且 SystemInformation.MouseWheelScrollLines 值為3

            int numberOfTextLinesToMove = e.Delta;
            Neighbor.Increment = 1m / SystemInformation.MouseWheelScrollLines;
            if (numberOfTextLinesToMove > 0 && ThresholdValue.Value < 0)
                ThresholdValue.Value = 0;
        }

        void ThresholdValue_MouseWheel(object sender, MouseEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta;
            ThresholdValue.Increment = 1m / SystemInformation.MouseWheelScrollLines;

            if (ThresholdValue.Value < 0)
                ThresholdValue.Value = 0;            
        }

        void UpDown_AdaptSize_MouseWheel(object sender, MouseEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta;
            UpDown_AdaptSize.Increment = 2m / SystemInformation.MouseWheelScrollLines;

            if (UpDown_AdaptSize.Value < 3)
                UpDown_AdaptSize.Value = 3;   
        }

        void UpDown_AdaptC_MouseWheel(object sender, MouseEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta;
            UpDown_AdaptC.Increment = 1m / SystemInformation.MouseWheelScrollLines;

            if (UpDown_AdaptC.Value < 0)
                UpDown_AdaptC.Value = 0;
        }
    }
}
