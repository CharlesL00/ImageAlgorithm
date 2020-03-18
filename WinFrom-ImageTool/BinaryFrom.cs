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

        /// <summary>
        /// _InputImage  : 原始輸入影像
        /// _SourceImage : Algo執行影像
        /// </summary>
        private Mat _InputImage;
        private Mat _SourceImage;
        //private Mat _TargetImage;
        private List<Mat> _RecoveryImage=new List<Mat>();

        private string _FileImagePath = "";
        private int _ImageMaxWidth = 0;
        private bool _Freeze = false;         //避免 trackBarScrollTh() 資料更動而多執行 ThresholdChange()
        private int _MouseWheel_TriggerCount = 0;
        private decimal _MouseWheel_ErrorCompensation = 0; //updowm 物件於滑鼠滾動時會產生細微誤差而變數有誤
        

        protected class AdaptTypeCoboxList
        {
            private string _AdaptType;
            private AdaptiveThresholdTypes _AdaptMethodNumber;

            public string AdaptType { get { return _AdaptType; } set { _AdaptType = value; } }
            public AdaptiveThresholdTypes AdaptMethodNumber { get { return _AdaptMethodNumber; } set { _AdaptMethodNumber = value; } }
        }

        protected class MorphTypesCoboxList
        {
            private string _MorphologyType;
            private MorphTypes _MorphologyMethod;

            public string MorphologyType { get { return _MorphologyType; } set { _MorphologyType = value; } }
            public MorphTypes MorphologyMethod { get { return _MorphologyMethod; } set { _MorphologyMethod = value; } }
        }

        public BinaryFrom()
        {
            InitializeComponent();
            InitializeComboBox();

            //初始化 comboBox
            binary.AdaptiveTypes = (AdaptiveThresholdTypes)comBox_AdaptType.SelectedValue;
            binary.MorphTypes = (MorphTypes)comBox_MorphType.SelectedValue;
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
                    _InputImage = new Mat();
                    Src.CopyTo(_InputImage);
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
            try
            {
                return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
            }
            catch (Exception e) 
            { 
                return null; 
            }
        }

        public Mat BitmapToMat(Bitmap image)
        {
            try
            {
                return OpenCvSharp.Extensions.BitmapConverter.ToMat(image);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void btn_Restore_Click(object sender, EventArgs e)
        {
            if (_InputImage != null)
            {
                _SourceImage = _InputImage;
                pictureBox2.Image = MatToBitmap(_SourceImage);
            }
            else
            {
                MessageBox.Show("Please Open New Image");
            }
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

                    //避免 ThresholdValue值更動而觸發
                    _MouseWheel_TriggerCount = 0;
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

                    //binary.AdaptiveTypes = (AdaptiveThresholdTypes)comboBox_AdaptType.SelectedValue;
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

        private void btn_Morphology_Click(object sender, EventArgs e)
        {
            if (_SourceImage != null)
            {
                pictureBox2.Image = MatToBitmap(Process_Morphology());
            }
        }

        // -------------------------- 輸入資料更動事件 --------------------------

        private void trackBarScrollTh(object sender, EventArgs e)
        {
            if (_SourceImage != null && !_Freeze)
            {
                label_Th.Text = "Threshold: ";
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

        private void NeighborChange(object sender, EventArgs e)
        {
            _MouseWheel_TriggerCount++;

            if (_SourceImage != null && !_Freeze && _MouseWheel_TriggerCount >= 3)
            {
                label_Th.Text = "Threshold: ";
                _MouseWheel_TriggerCount = 0;
                Mat Dst = new Mat();
                using (Mat Src = new Mat())
                using (Mat Gray = new Mat())
                {
                    _SourceImage.CopyTo(Src);

                    Cv2.CvtColor(Src, Gray, ColorConversionCodes.BGR2GRAY);

                    if (check_Negative.Checked)
                        Tool.Negative(Gray, Gray);

                    binary.Neighbor = (int)(Neighbor.Value + _MouseWheel_ErrorCompensation);
                    int Threshold = binary.Otsu(Gray, Dst, Binary.OtsuType.NVE);
                    Cv2.Threshold(Gray, Dst, Threshold, 255, ThresholdTypes.Binary);

                    //Drawing Color
                    Tool.SetColor(255, 0, 0);
                    if (check_DrawingColor.Checked)
                        Tool.OutColor(Gray, Dst, Dst, true);

                    label_Th.Text += Threshold.ToString();
                    ThresholdValue.Value = Threshold;
                    trackBar_Th.Value = Threshold;

                    //避免 ThresholdValue值更動而觸發
                    _MouseWheel_TriggerCount = 0;
                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
            else
            {
                trackBar_Th.Value = (int)(ThresholdValue.Value + _MouseWheel_ErrorCompensation);
            }
        }

        private void ThresholdChange(object sender, EventArgs e)
        {
            _MouseWheel_TriggerCount++;

            if (_SourceImage != null && !_Freeze && _MouseWheel_TriggerCount>=3)
            {
                label_Th.Text = "Threshold: ";
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

                    int Threshold = (int)(ThresholdValue.Value  + _MouseWheel_ErrorCompensation);
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

            if (_SourceImage != null && _MouseWheel_TriggerCount>=3)
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

                    int AdaptSize = (int)(UpDown_AdaptSize.Value + _MouseWheel_ErrorCompensation);
                    if (AdaptSize < 3)
                        binary.AdaptBlockSize = 3;
                    else if (AdaptSize % 2 == 0)
                        binary.AdaptBlockSize = AdaptSize - 1;
                    else
                        binary.AdaptBlockSize = AdaptSize;

                    binary.Adapt_c = (int)(UpDown_AdaptC.Value  + _MouseWheel_ErrorCompensation);

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

                    int AdaptSize = (int)(UpDown_AdaptSize.Value + _MouseWheel_ErrorCompensation);
                    if (AdaptSize % 2 == 0)
                        binary.AdaptBlockSize = AdaptSize - 1;
                    else
                        binary.AdaptBlockSize = AdaptSize;

                    binary.Adapt_c = (int)(UpDown_AdaptC.Value + _MouseWheel_ErrorCompensation);
                    binary.Otsu(Gray, Dst, Binary.OtsuType.Adaptive);

                    //Drawing Color
                    Tool.SetColor(255, 0, 0);
                    if (check_DrawingColor.Checked)
                        Tool.OutColor(Gray, Dst, Dst, true);

                }
                pictureBox2.Image = MatToBitmap(Dst);
            }
        }

        void UpDown_Morphology_x_Change(object sender, EventArgs e)
        {
            _MouseWheel_TriggerCount++;

            if (_SourceImage != null && _MouseWheel_TriggerCount >= 3)
            {
                pictureBox2.Image = MatToBitmap(Process_Morphology());
            }
        }

        void UpDown_Morphology_y_Change(object sender, EventArgs e)
        {
            _MouseWheel_TriggerCount++;

            if (pictureBox2.Image != null && _MouseWheel_TriggerCount >= 3)
            {
                pictureBox2.Image = MatToBitmap(Process_Morphology());
            }

        }

        private Mat Process_Morphology()
        {
            Mat Dst = new Mat();
            using (Mat MorphMat = new Mat())
            {
                _SourceImage.CopyTo(MorphMat);

                if (check_Negative.Checked)
                    Tool.Negative(MorphMat, MorphMat);

                if (binary.MorphTypes != MorphTypes.HitMiss)
                {
                    int x = (int)(UpDown_Morphology_x.Value + _MouseWheel_ErrorCompensation);
                    int y = (int)(UpDown_Morphology_y.Value + _MouseWheel_ErrorCompensation);
                    binary.Morphology(MorphMat, Dst, x, y);

                    //Drawing Color
                    Tool.SetColor(255, 0, 0);
                    if (check_DrawingColor.Checked)
                    {
                        if (Dst.Channels() == 1)
                        {
                            using (Mat Src = new Mat())
                            {
                                _SourceImage.CopyTo(Src);
                                Tool.OutColor(Src, Dst, Dst, true);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Don't Drawing Color,Image Not One Channel");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Image Must One Channel In 'HitMiss' Type");
                }
            }
            return Dst;
        }
        // -------------------------- 上下按鈕控制事件 --------------------------

        void Neighbor_MouseWheel(object sender, MouseEventArgs e)
        {
            // (1) numberOfTextLinesToMove :+120=向上滑，-120=向下滑
            // (2) 原先 combox Increment = SystemInformation.MouseWheelScrollLines
            //     且 SystemInformation.MouseWheelScrollLines 值為3

            int numberOfTextLinesToMove = e.Delta;
            Neighbor.Increment = 1m / SystemInformation.MouseWheelScrollLines;
            _MouseWheel_ErrorCompensation = Neighbor.Increment;

            if (numberOfTextLinesToMove > 0 && ThresholdValue.Value < 0)
                ThresholdValue.Value = 0;
        }

        void ThresholdValue_MouseWheel(object sender, MouseEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta;
            ThresholdValue.Increment = 1m / SystemInformation.MouseWheelScrollLines;
            _MouseWheel_ErrorCompensation = ThresholdValue.Increment;

            if (ThresholdValue.Value < 0)
                ThresholdValue.Value = 0;            
        }

        void trackBar_Th_MouseWheel(object sender, MouseEventArgs e)
        {
            //int numberOfTextLinesToMove = e.Delta;
            //trackBar_Th.Scroll = 1m / SystemInformation.MouseWheelScrollLines;
            //if (numberOfTextLinesToMove > 0 && ThresholdValue.Value < 0)
            //    ThresholdValue.Value = 0;
        }

        void UpDown_AdaptSize_MouseWheel(object sender, MouseEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta;
            UpDown_AdaptSize.Increment = 2m / SystemInformation.MouseWheelScrollLines;
            _MouseWheel_ErrorCompensation = UpDown_AdaptSize.Increment;

            if (UpDown_AdaptSize.Value < 3)
                UpDown_AdaptSize.Value = 3;   
        }

        void UpDown_AdaptC_MouseWheel(object sender, MouseEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta;
            UpDown_AdaptC.Increment = 1m / SystemInformation.MouseWheelScrollLines;
            _MouseWheel_ErrorCompensation = UpDown_AdaptC.Increment;

            if (UpDown_AdaptC.Value < 0)
                UpDown_AdaptC.Value = 0;
        }

        void Morphology_x_MouseWheel(object sender, MouseEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta;
            UpDown_Morphology_x.Increment = 1m / SystemInformation.MouseWheelScrollLines;
            _MouseWheel_ErrorCompensation = UpDown_Morphology_x.Increment;

            if (UpDown_Morphology_x.Value < 0)
                UpDown_Morphology_x.Value = 0;

            _MouseWheel_TriggerCount = 0;
        }

        void Morphology_y_MouseWheel(object sender, MouseEventArgs e)
        {
            int numberOfTextLinesToMove = e.Delta;
            UpDown_Morphology_y.Increment = 1m / SystemInformation.MouseWheelScrollLines;
            _MouseWheel_ErrorCompensation = UpDown_Morphology_y.Increment;

            if (UpDown_Morphology_y.Value < 0)
                UpDown_Morphology_y.Value = 0;

            _MouseWheel_TriggerCount = 0;
        }


        // -------------------------- ComboBox選擇事件 --------------------------

        private void comboBox_AdaptType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            binary.AdaptiveTypes = (AdaptiveThresholdTypes)comBox_AdaptType.SelectedValue;
        }

        private void comBox_MorphType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            binary.MorphTypes = (MorphTypes)comBox_MorphType.SelectedValue;
        }



        //-------------------------
        private void InitializeComboBox()
        {
            List<AdaptTypeCoboxList> AdaptTypeSet = new List<AdaptTypeCoboxList>()
            {
                new AdaptTypeCoboxList()
                {
                    AdaptType = "GaussianC",
                    AdaptMethodNumber = AdaptiveThresholdTypes.GaussianC
                },
                new AdaptTypeCoboxList()
                {
                    AdaptType = "MeanC",
                    AdaptMethodNumber = AdaptiveThresholdTypes.MeanC
                }
            };
            comBox_AdaptType.DataSource = AdaptTypeSet;
            comBox_AdaptType.DisplayMember = "AdaptType";
            comBox_AdaptType.ValueMember = "AdaptMethodNumber";

            List<MorphTypesCoboxList> MorphologyTypeSet = new List<MorphTypesCoboxList>()
            {
                new MorphTypesCoboxList()
                {
                    MorphologyType = "Dilate",
                    MorphologyMethod = MorphTypes.Dilate
                },
                new MorphTypesCoboxList()
                {
                    MorphologyType = "Erode",
                    MorphologyMethod = MorphTypes.Erode
                },
                new MorphTypesCoboxList()
                {
                    MorphologyType = "Close",
                    MorphologyMethod = MorphTypes.Close
                },
                new MorphTypesCoboxList()
                {
                    MorphologyType = "Open",
                    MorphologyMethod = MorphTypes.Open
                },
                new MorphTypesCoboxList()
                {
                    MorphologyType = "TopHat",
                    MorphologyMethod = MorphTypes.TopHat
                },
                new MorphTypesCoboxList()
                {
                    MorphologyType = "BlackHat",
                    MorphologyMethod = MorphTypes.BlackHat
                },
                new MorphTypesCoboxList()
                {
                    MorphologyType = "Gradient",
                    MorphologyMethod = MorphTypes.Gradient
                },
                new MorphTypesCoboxList()
                {
                    MorphologyType = "HitMiss",
                    MorphologyMethod = MorphTypes.HitMiss
                }
            };
            comBox_MorphType.DataSource = MorphologyTypeSet;
            comBox_MorphType.DisplayMember = "MorphologyType";
            comBox_MorphType.ValueMember = "MorphologyMethod";
        }

        private void btn_Replace_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null)
            {
                _RecoveryImage.Add(new Mat());
                int LastIndex = _RecoveryImage.Count()-1;

                BitmapToMat((Bitmap)pictureBox1.Image).CopyTo(_RecoveryImage[LastIndex]);
                pictureBox1.Image = pictureBox2.Image;
                _SourceImage = BitmapToMat((Bitmap)pictureBox2.Image);
            }
        }

        private void btn_Recovery_Click(object sender, EventArgs e)
        {
            int ImgCount=_RecoveryImage.Count();
            if (_RecoveryImage.Count > 0)
            {
                pictureBox1.Image = MatToBitmap(_RecoveryImage[ImgCount-1]);
                _RecoveryImage[ImgCount - 1].Dispose();
                _RecoveryImage.RemoveAt(ImgCount - 1);
            }
        }
    }
}
