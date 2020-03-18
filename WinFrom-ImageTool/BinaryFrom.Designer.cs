namespace WinFrom
{
    partial class BinaryFrom
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Restore = new System.Windows.Forms.Button();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.trackBar_Th = new System.Windows.Forms.TrackBar();
            this.btn_NVE = new System.Windows.Forms.Button();
            this.Neighbor = new System.Windows.Forms.NumericUpDown();
            this.btn_WOV = new System.Windows.Forms.Button();
            this.btn_VE = new System.Windows.Forms.Button();
            this.btn_Threshold = new System.Windows.Forms.Button();
            this.ThresholdValue = new System.Windows.Forms.NumericUpDown();
            this.check_DrawingColor = new System.Windows.Forms.CheckBox();
            this.check_Negative = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOtsu = new System.Windows.Forms.Button();
            this.comBox_AdaptType = new System.Windows.Forms.ComboBox();
            this.label_C = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_Size = new System.Windows.Forms.Label();
            this.UpDown_AdaptC = new System.Windows.Forms.NumericUpDown();
            this.UpDown_AdaptSize = new System.Windows.Forms.NumericUpDown();
            this.btn_Adaptive = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label_time = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_Th = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comBox_MorphType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Morphology = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.UpDown_Morphology_x = new System.Windows.Forms.NumericUpDown();
            this.UpDown_Morphology_y = new System.Windows.Forms.NumericUpDown();
            this.btn_Replace = new System.Windows.Forms.Button();
            this.btn_Recovery = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Th)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Neighbor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdValue)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_AdaptC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_AdaptSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_Morphology_x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_Morphology_y)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(13, 19);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFile.TabIndex = 111;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 14F);
            this.label3.Location = new System.Drawing.Point(17, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 19);
            this.label3.TabIndex = 115;
            this.label3.Text = "Source Image";
            // 
            // btn_Restore
            // 
            this.btn_Restore.Location = new System.Drawing.Point(175, 19);
            this.btn_Restore.Name = "btn_Restore";
            this.btn_Restore.Size = new System.Drawing.Size(75, 23);
            this.btn_Restore.TabIndex = 122;
            this.btn_Restore.Text = "Restore";
            this.btn_Restore.UseVisualStyleBackColor = true;
            this.btn_Restore.Click += new System.EventHandler(this.btn_Restore_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Location = new System.Drawing.Point(94, 19);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(75, 23);
            this.btnSaveFile.TabIndex = 112;
            this.btnSaveFile.Text = "Save File";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // trackBar_Th
            // 
            this.trackBar_Th.Location = new System.Drawing.Point(136, 124);
            this.trackBar_Th.Maximum = 255;
            this.trackBar_Th.Name = "trackBar_Th";
            this.trackBar_Th.Size = new System.Drawing.Size(96, 45);
            this.trackBar_Th.TabIndex = 137;
            this.trackBar_Th.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar_Th.Scroll += new System.EventHandler(this.trackBarScrollTh);
            // 
            // btn_NVE
            // 
            this.btn_NVE.BackColor = System.Drawing.SystemColors.Control;
            this.btn_NVE.Location = new System.Drawing.Point(13, 85);
            this.btn_NVE.Name = "btn_NVE";
            this.btn_NVE.Size = new System.Drawing.Size(69, 27);
            this.btn_NVE.TabIndex = 128;
            this.btn_NVE.Text = "NVE";
            this.btn_NVE.UseVisualStyleBackColor = true;
            this.btn_NVE.Click += new System.EventHandler(this.btn_NVE_Click);
            // 
            // Neighbor
            // 
            this.Neighbor.Location = new System.Drawing.Point(88, 90);
            this.Neighbor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Neighbor.Name = "Neighbor";
            this.Neighbor.Size = new System.Drawing.Size(42, 22);
            this.Neighbor.TabIndex = 22;
            this.Neighbor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Neighbor.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Neighbor.ValueChanged += new System.EventHandler(this.NeighborChange);
            this.Neighbor.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Neighbor_MouseWheel);
            // 
            // btn_WOV
            // 
            this.btn_WOV.Location = new System.Drawing.Point(163, 51);
            this.btn_WOV.Name = "btn_WOV";
            this.btn_WOV.Size = new System.Drawing.Size(69, 27);
            this.btn_WOV.TabIndex = 130;
            this.btn_WOV.Text = "WOV";
            this.btn_WOV.UseVisualStyleBackColor = true;
            this.btn_WOV.Click += new System.EventHandler(this.btn_WOV_Click);
            // 
            // btn_VE
            // 
            this.btn_VE.Location = new System.Drawing.Point(88, 51);
            this.btn_VE.Name = "btn_VE";
            this.btn_VE.Size = new System.Drawing.Size(69, 27);
            this.btn_VE.TabIndex = 127;
            this.btn_VE.Text = "VE";
            this.btn_VE.UseVisualStyleBackColor = true;
            this.btn_VE.Click += new System.EventHandler(this.btn_VE_Click);
            // 
            // btn_Threshold
            // 
            this.btn_Threshold.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Threshold.Location = new System.Drawing.Point(13, 118);
            this.btn_Threshold.Name = "btn_Threshold";
            this.btn_Threshold.Size = new System.Drawing.Size(69, 27);
            this.btn_Threshold.TabIndex = 132;
            this.btn_Threshold.Text = "Threshold";
            this.btn_Threshold.UseVisualStyleBackColor = true;
            this.btn_Threshold.Click += new System.EventHandler(this.btn_Threshold_Click);
            // 
            // ThresholdValue
            // 
            this.ThresholdValue.Location = new System.Drawing.Point(88, 123);
            this.ThresholdValue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ThresholdValue.Name = "ThresholdValue";
            this.ThresholdValue.Size = new System.Drawing.Size(42, 22);
            this.ThresholdValue.TabIndex = 131;
            this.ThresholdValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ThresholdValue.ValueChanged += new System.EventHandler(this.ThresholdChange);
            this.ThresholdValue.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ThresholdValue_MouseWheel);
            // 
            // check_DrawingColor
            // 
            this.check_DrawingColor.AutoSize = true;
            this.check_DrawingColor.Location = new System.Drawing.Point(136, 28);
            this.check_DrawingColor.Name = "check_DrawingColor";
            this.check_DrawingColor.Size = new System.Drawing.Size(91, 16);
            this.check_DrawingColor.TabIndex = 135;
            this.check_DrawingColor.Text = "DrawingColor";
            this.check_DrawingColor.UseVisualStyleBackColor = true;
            // 
            // check_Negative
            // 
            this.check_Negative.AutoSize = true;
            this.check_Negative.Location = new System.Drawing.Point(136, 8);
            this.check_Negative.Name = "check_Negative";
            this.check_Negative.Size = new System.Drawing.Size(94, 16);
            this.check_Negative.TabIndex = 136;
            this.check_Negative.Text = "NegativeImage";
            this.check_Negative.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.btnOtsu);
            this.panel1.Controls.Add(this.comBox_AdaptType);
            this.panel1.Controls.Add(this.label_C);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label_Size);
            this.panel1.Controls.Add(this.UpDown_AdaptC);
            this.panel1.Controls.Add(this.UpDown_AdaptSize);
            this.panel1.Controls.Add(this.btn_Adaptive);
            this.panel1.Controls.Add(this.check_Negative);
            this.panel1.Controls.Add(this.check_DrawingColor);
            this.panel1.Controls.Add(this.ThresholdValue);
            this.panel1.Controls.Add(this.btn_Threshold);
            this.panel1.Controls.Add(this.btn_VE);
            this.panel1.Controls.Add(this.btn_WOV);
            this.panel1.Controls.Add(this.Neighbor);
            this.panel1.Controls.Add(this.btn_NVE);
            this.panel1.Controls.Add(this.trackBar_Th);
            this.panel1.Location = new System.Drawing.Point(863, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 234);
            this.panel1.TabIndex = 22;
            // 
            // btnOtsu
            // 
            this.btnOtsu.Location = new System.Drawing.Point(13, 50);
            this.btnOtsu.Name = "btnOtsu";
            this.btnOtsu.Size = new System.Drawing.Size(69, 28);
            this.btnOtsu.TabIndex = 126;
            this.btnOtsu.Text = "Otsu";
            this.btnOtsu.UseVisualStyleBackColor = true;
            this.btnOtsu.Click += new System.EventHandler(this.btnOtsu_Click);
            // 
            // comBox_AdaptType
            // 
            this.comBox_AdaptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comBox_AdaptType.FormattingEnabled = true;
            this.comBox_AdaptType.Location = new System.Drawing.Point(142, 204);
            this.comBox_AdaptType.Name = "comBox_AdaptType";
            this.comBox_AdaptType.Size = new System.Drawing.Size(90, 20);
            this.comBox_AdaptType.Sorted = true;
            this.comBox_AdaptType.TabIndex = 142;
            this.comBox_AdaptType.SelectionChangeCommitted += new System.EventHandler(this.comboBox_AdaptType_SelectionChangeCommitted);
            // 
            // label_C
            // 
            this.label_C.AutoSize = true;
            this.label_C.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_C.Font = new System.Drawing.Font("新細明體", 14F);
            this.label_C.Location = new System.Drawing.Point(200, 154);
            this.label_C.Name = "label_C";
            this.label_C.Size = new System.Drawing.Size(21, 19);
            this.label_C.TabIndex = 141;
            this.label_C.Text = "C";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Font = new System.Drawing.Font("新細明體", 14F);
            this.label1.Location = new System.Drawing.Point(25, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 19);
            this.label1.TabIndex = 123;
            this.label1.Text = "Otsu";
            // 
            // label_Size
            // 
            this.label_Size.AutoSize = true;
            this.label_Size.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label_Size.Font = new System.Drawing.Font("新細明體", 14F);
            this.label_Size.Location = new System.Drawing.Point(142, 154);
            this.label_Size.Name = "label_Size";
            this.label_Size.Size = new System.Drawing.Size(40, 19);
            this.label_Size.TabIndex = 127;
            this.label_Size.Text = "Size";
            // 
            // UpDown_AdaptC
            // 
            this.UpDown_AdaptC.Location = new System.Drawing.Point(190, 176);
            this.UpDown_AdaptC.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDown_AdaptC.Name = "UpDown_AdaptC";
            this.UpDown_AdaptC.Size = new System.Drawing.Size(42, 22);
            this.UpDown_AdaptC.TabIndex = 140;
            this.UpDown_AdaptC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UpDown_AdaptC.ValueChanged += new System.EventHandler(this.AdaptC_Change);
            this.UpDown_AdaptC.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.UpDown_AdaptC_MouseWheel);
            // 
            // UpDown_AdaptSize
            // 
            this.UpDown_AdaptSize.Location = new System.Drawing.Point(141, 176);
            this.UpDown_AdaptSize.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDown_AdaptSize.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.UpDown_AdaptSize.Name = "UpDown_AdaptSize";
            this.UpDown_AdaptSize.Size = new System.Drawing.Size(42, 22);
            this.UpDown_AdaptSize.TabIndex = 139;
            this.UpDown_AdaptSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UpDown_AdaptSize.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.UpDown_AdaptSize.ValueChanged += new System.EventHandler(this.AdaptSizeChange);
            this.UpDown_AdaptSize.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.UpDown_AdaptSize_MouseWheel);
            // 
            // btn_Adaptive
            // 
            this.btn_Adaptive.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Adaptive.Location = new System.Drawing.Point(13, 176);
            this.btn_Adaptive.Name = "btn_Adaptive";
            this.btn_Adaptive.Size = new System.Drawing.Size(117, 48);
            this.btn_Adaptive.TabIndex = 138;
            this.btn_Adaptive.Text = "AdaptiveThreshold";
            this.btn_Adaptive.UseVisualStyleBackColor = true;
            this.btn_Adaptive.Click += new System.EventHandler(this.btn_Adaptive_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBox2.Location = new System.Drawing.Point(443, 81);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(400, 400);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 114;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBox1.Location = new System.Drawing.Point(17, 81);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 113;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 14F);
            this.label4.Location = new System.Drawing.Point(439, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 19);
            this.label4.TabIndex = 118;
            this.label4.Text = "Result Image";
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.Font = new System.Drawing.Font("新細明體", 14F);
            this.label_time.Location = new System.Drawing.Point(761, 59);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(0, 19);
            this.label_time.TabIndex = 121;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 14F);
            this.label7.Location = new System.Drawing.Point(761, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 19);
            this.label7.TabIndex = 121;
            // 
            // label_Th
            // 
            this.label_Th.AutoSize = true;
            this.label_Th.Font = new System.Drawing.Font("新細明體", 16F);
            this.label_Th.Location = new System.Drawing.Point(439, 496);
            this.label_Th.Name = "label_Th";
            this.label_Th.Size = new System.Drawing.Size(105, 22);
            this.label_Th.TabIndex = 22;
            this.label_Th.Text = "Threshold: ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel2.Controls.Add(this.comBox_MorphType);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btn_Morphology);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.UpDown_Morphology_x);
            this.panel2.Controls.Add(this.UpDown_Morphology_y);
            this.panel2.Location = new System.Drawing.Point(863, 307);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(241, 99);
            this.panel2.TabIndex = 143;
            // 
            // comBox_MorphType
            // 
            this.comBox_MorphType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comBox_MorphType.FormattingEnabled = true;
            this.comBox_MorphType.Location = new System.Drawing.Point(128, 64);
            this.comBox_MorphType.Name = "comBox_MorphType";
            this.comBox_MorphType.Size = new System.Drawing.Size(102, 20);
            this.comBox_MorphType.TabIndex = 148;
            this.comBox_MorphType.SelectionChangeCommitted += new System.EventHandler(this.comBox_MorphType_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Font = new System.Drawing.Font("新細明體", 14F);
            this.label2.Location = new System.Drawing.Point(16, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 19);
            this.label2.TabIndex = 143;
            this.label2.Text = "Morphology";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label5.Font = new System.Drawing.Font("新細明體", 14F);
            this.label5.Location = new System.Drawing.Point(145, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 19);
            this.label5.TabIndex = 147;
            this.label5.Text = "y";
            // 
            // btn_Morphology
            // 
            this.btn_Morphology.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Morphology.Location = new System.Drawing.Point(13, 63);
            this.btn_Morphology.Name = "btn_Morphology";
            this.btn_Morphology.Size = new System.Drawing.Size(102, 20);
            this.btn_Morphology.TabIndex = 144;
            this.btn_Morphology.Text = "Morphology";
            this.btn_Morphology.UseVisualStyleBackColor = true;
            this.btn_Morphology.Click += new System.EventHandler(this.btn_Morphology_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label6.Font = new System.Drawing.Font("新細明體", 14F);
            this.label6.Location = new System.Drawing.Point(144, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 19);
            this.label6.TabIndex = 143;
            this.label6.Text = "x";
            // 
            // UpDown_Morphology_x
            // 
            this.UpDown_Morphology_x.Location = new System.Drawing.Point(162, 6);
            this.UpDown_Morphology_x.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDown_Morphology_x.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDown_Morphology_x.Name = "UpDown_Morphology_x";
            this.UpDown_Morphology_x.Size = new System.Drawing.Size(42, 22);
            this.UpDown_Morphology_x.TabIndex = 145;
            this.UpDown_Morphology_x.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UpDown_Morphology_x.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.UpDown_Morphology_x.ValueChanged += new System.EventHandler(this.UpDown_Morphology_x_Change);
            this.UpDown_Morphology_x.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Morphology_x_MouseWheel);

            // 
            // UpDown_Morphology_y
            // 
            this.UpDown_Morphology_y.Location = new System.Drawing.Point(163, 30);
            this.UpDown_Morphology_y.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.UpDown_Morphology_y.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDown_Morphology_y.Name = "UpDown_Morphology_y";
            this.UpDown_Morphology_y.Size = new System.Drawing.Size(42, 22);
            this.UpDown_Morphology_y.TabIndex = 146;
            this.UpDown_Morphology_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UpDown_Morphology_y.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpDown_Morphology_y.ValueChanged += new System.EventHandler(this.UpDown_Morphology_y_Change);
            this.UpDown_Morphology_y.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Morphology_y_MouseWheel);
            // 
            // btn_Replace
            // 
            this.btn_Replace.Location = new System.Drawing.Point(256, 19);
            this.btn_Replace.Name = "btn_Replace";
            this.btn_Replace.Size = new System.Drawing.Size(75, 23);
            this.btn_Replace.TabIndex = 144;
            this.btn_Replace.Text = "Replace";
            this.btn_Replace.UseVisualStyleBackColor = true;
            this.btn_Replace.Click += new System.EventHandler(this.btn_Replace_Click);
            // 
            // btn_Recovery
            // 
            this.btn_Recovery.Location = new System.Drawing.Point(342, 19);
            this.btn_Recovery.Name = "btn_Recovery";
            this.btn_Recovery.Size = new System.Drawing.Size(75, 23);
            this.btn_Recovery.TabIndex = 145;
            this.btn_Recovery.Text = "Recovery";
            this.btn_Recovery.UseVisualStyleBackColor = true;
            this.btn_Recovery.Click += new System.EventHandler(this.btn_Recovery_Click);
            // 
            // BinaryFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1165, 619);
            this.Controls.Add(this.btn_Recovery);
            this.Controls.Add(this.btn_Replace);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label_Th);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_Restore);
            this.Controls.Add(this.label_time);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Name = "BinaryFrom";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Th)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Neighbor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdValue)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_AdaptC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_AdaptSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_Morphology_x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDown_Morphology_y)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Restore;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.TrackBar trackBar_Th;
        private System.Windows.Forms.Button btn_NVE;
        private System.Windows.Forms.NumericUpDown Neighbor;
        private System.Windows.Forms.Button btn_WOV;
        private System.Windows.Forms.Button btn_VE;
        private System.Windows.Forms.Button btn_Threshold;
        private System.Windows.Forms.NumericUpDown ThresholdValue;
        private System.Windows.Forms.CheckBox check_DrawingColor;
        private System.Windows.Forms.CheckBox check_Negative;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOtsu;
        private System.Windows.Forms.Label label_Th;
        private System.Windows.Forms.Button btn_Adaptive;
        private System.Windows.Forms.ComboBox comBox_AdaptType;
        private System.Windows.Forms.Label label_C;
        private System.Windows.Forms.Label label_Size;
        private System.Windows.Forms.NumericUpDown UpDown_AdaptC;
        private System.Windows.Forms.NumericUpDown UpDown_AdaptSize;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_Morphology;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown UpDown_Morphology_x;
        private System.Windows.Forms.NumericUpDown UpDown_Morphology_y;
        private System.Windows.Forms.ComboBox comBox_MorphType;
        private System.Windows.Forms.Button btn_Replace;
        private System.Windows.Forms.Button btn_Recovery;
        //private System.Windows.Forms.Button btn_PerspectiveCorrectionAll;
        //private System.Windows.Forms.TextBox txt_RefBlock;
    }
}

