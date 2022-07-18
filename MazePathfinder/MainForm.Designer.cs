namespace MazePathfinder
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mazePathfinderLabel1 = new System.Windows.Forms.Label();
            this.calculationStartButton = new System.Windows.Forms.Button();
            this.imageSaveButton = new System.Windows.Forms.Button();
            this.returnStartButton = new System.Windows.Forms.Button();
            this.recalculationButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.palletButton = new System.Windows.Forms.Button();
            this.fileButton = new System.Windows.Forms.Button();
            this.goalLabel = new MazePathfinder.Extensions.OpacityLabel();
            this.startLabel = new MazePathfinder.Extensions.OpacityLabel();
            this.mazePathfinderLabel2 = new MazePathfinder.Extensions.OpacityLabel();
            this.mazePathfinderLabel3 = new System.Windows.Forms.Label();
            this.goalPosCross = new System.Windows.Forms.PictureBox();
            this.startPosCross = new System.Windows.Forms.PictureBox();
            this.startPos = new System.Windows.Forms.PictureBox();
            this.goalPos = new System.Windows.Forms.PictureBox();
            this.mazePathfinderPictureBox = new System.Windows.Forms.PictureBox();
            this.loadingNow = new System.Windows.Forms.PictureBox();
            this.calculationReject = new System.Windows.Forms.Button();
            this.useBfsButton = new System.Windows.Forms.RadioButton();
            this.useDfsButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.goalPosCross)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPosCross)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goalPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mazePathfinderPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingNow)).BeginInit();
            this.SuspendLayout();
            // 
            // mazePathfinderLabel1
            // 
            this.mazePathfinderLabel1.AutoSize = true;
            this.mazePathfinderLabel1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mazePathfinderLabel1.ForeColor = System.Drawing.Color.Gray;
            this.mazePathfinderLabel1.Location = new System.Drawing.Point(150, 185);
            this.mazePathfinderLabel1.Name = "mazePathfinderLabel1";
            this.mazePathfinderLabel1.Size = new System.Drawing.Size(282, 48);
            this.mazePathfinderLabel1.TabIndex = 0;
            this.mazePathfinderLabel1.Text = "ここに迷路の画像ファイルをドロップ\r\nまたは";
            this.mazePathfinderLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // calculationStartButton
            // 
            this.calculationStartButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.calculationStartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calculationStartButton.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.calculationStartButton.Location = new System.Drawing.Point(395, 319);
            this.calculationStartButton.Name = "calculationStartButton";
            this.calculationStartButton.Size = new System.Drawing.Size(100, 30);
            this.calculationStartButton.TabIndex = 6;
            this.calculationStartButton.Text = "解析する";
            this.calculationStartButton.UseVisualStyleBackColor = true;
            this.calculationStartButton.Visible = false;
            this.calculationStartButton.Click += new System.EventHandler(this.CalculationStartButton_Click);
            // 
            // imageSaveButton
            // 
            this.imageSaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.imageSaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.imageSaveButton.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.imageSaveButton.Location = new System.Drawing.Point(256, 319);
            this.imageSaveButton.Name = "imageSaveButton";
            this.imageSaveButton.Size = new System.Drawing.Size(100, 30);
            this.imageSaveButton.TabIndex = 9;
            this.imageSaveButton.Text = "画像保存";
            this.imageSaveButton.UseVisualStyleBackColor = true;
            this.imageSaveButton.Visible = false;
            this.imageSaveButton.Click += new System.EventHandler(this.ImageSaveButton_Click);
            // 
            // returnStartButton
            // 
            this.returnStartButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.returnStartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.returnStartButton.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.returnStartButton.Location = new System.Drawing.Point(72, 319);
            this.returnStartButton.Name = "returnStartButton";
            this.returnStartButton.Size = new System.Drawing.Size(136, 30);
            this.returnStartButton.TabIndex = 10;
            this.returnStartButton.Text = "初期画面に戻る";
            this.returnStartButton.UseVisualStyleBackColor = true;
            this.returnStartButton.Visible = false;
            this.returnStartButton.Click += new System.EventHandler(this.ReturnStartButton_Click);
            // 
            // recalculationButton
            // 
            this.recalculationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.recalculationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.recalculationButton.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.recalculationButton.Location = new System.Drawing.Point(309, 319);
            this.recalculationButton.Name = "recalculationButton";
            this.recalculationButton.Size = new System.Drawing.Size(100, 30);
            this.recalculationButton.TabIndex = 11;
            this.recalculationButton.Text = "再解析";
            this.recalculationButton.UseVisualStyleBackColor = false;
            this.recalculationButton.Visible = false;
            this.recalculationButton.Click += new System.EventHandler(this.RecalculationButton_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveFileDialog_FileOk);
            // 
            // palletButton
            // 
            this.palletButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.palletButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.palletButton.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.palletButton.Location = new System.Drawing.Point(234, 319);
            this.palletButton.Name = "palletButton";
            this.palletButton.Size = new System.Drawing.Size(136, 30);
            this.palletButton.TabIndex = 12;
            this.palletButton.Text = "パレット表示";
            this.palletButton.UseVisualStyleBackColor = false;
            this.palletButton.Visible = false;
            this.palletButton.Click += new System.EventHandler(this.PalletButton_Click);
            // 
            // fileButton
            // 
            this.fileButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.fileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fileButton.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fileButton.Location = new System.Drawing.Point(233, 240);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(115, 30);
            this.fileButton.TabIndex = 14;
            this.fileButton.Text = "ファイルを選択";
            this.fileButton.UseVisualStyleBackColor = false;
            this.fileButton.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // goalLabel
            // 
            this.goalLabel.AutoSize = true;
            this.goalLabel.BackColor = System.Drawing.SystemColors.Window;
            this.goalLabel.Font = new System.Drawing.Font("メイリオ", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.goalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(102)))), ((int)(((byte)(177)))));
            this.goalLabel.Location = new System.Drawing.Point(358, 233);
            this.goalLabel.Name = "goalLabel";
            this.goalLabel.Opacity = 1D;
            this.goalLabel.Size = new System.Drawing.Size(103, 25);
            this.goalLabel.TabIndex = 5;
            this.goalLabel.Text = " ゴール地点";
            this.goalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.goalLabel.Visible = false;
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.BackColor = System.Drawing.SystemColors.Window;
            this.startLabel.Font = new System.Drawing.Font("メイリオ", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.startLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(64)))), ((int)(((byte)(50)))));
            this.startLabel.Location = new System.Drawing.Point(157, 233);
            this.startLabel.Name = "startLabel";
            this.startLabel.Opacity = 1D;
            this.startLabel.Size = new System.Drawing.Size(120, 25);
            this.startLabel.TabIndex = 4;
            this.startLabel.Text = " スタート地点";
            this.startLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.startLabel.Visible = false;
            // 
            // mazePathfinderLabel2
            // 
            this.mazePathfinderLabel2.BackColor = System.Drawing.Color.White;
            this.mazePathfinderLabel2.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mazePathfinderLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(64)))), ((int)(((byte)(50)))));
            this.mazePathfinderLabel2.Location = new System.Drawing.Point(57, 151);
            this.mazePathfinderLabel2.Name = "mazePathfinderLabel2";
            this.mazePathfinderLabel2.Opacity = 1D;
            this.mazePathfinderLabel2.Size = new System.Drawing.Size(470, 46);
            this.mazePathfinderLabel2.TabIndex = 1;
            this.mazePathfinderLabel2.Text = "　 迷路のスタート地点とゴール地点を正確に指定してください\r\nアイコンをドラッグ : 移動　アイコン上でスクロール : 拡大・縮小";
            this.mazePathfinderLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mazePathfinderLabel2.Visible = false;
            this.mazePathfinderLabel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.mazePathfinderLabel2.MouseEnter += new System.EventHandler(this.MazePathfinderLabel2_MouseEnter);
            this.mazePathfinderLabel2.MouseLeave += new System.EventHandler(this.MazePathfinderLabel2_MouseLeave);
            this.mazePathfinderLabel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.mazePathfinderLabel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // mazePathfinderLabel3
            // 
            this.mazePathfinderLabel3.AutoSize = true;
            this.mazePathfinderLabel3.BackColor = System.Drawing.SystemColors.Window;
            this.mazePathfinderLabel3.Font = new System.Drawing.Font("メイリオ", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mazePathfinderLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(50)))), ((int)(((byte)(36)))));
            this.mazePathfinderLabel3.Location = new System.Drawing.Point(218, 155);
            this.mazePathfinderLabel3.Margin = new System.Windows.Forms.Padding(0);
            this.mazePathfinderLabel3.Name = "mazePathfinderLabel3";
            this.mazePathfinderLabel3.Size = new System.Drawing.Size(126, 41);
            this.mazePathfinderLabel3.TabIndex = 7;
            this.mazePathfinderLabel3.Text = "解析中　";
            this.mazePathfinderLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mazePathfinderLabel3.Visible = false;
            // 
            // goalPosCross
            // 
            this.goalPosCross.BackColor = System.Drawing.Color.Transparent;
            this.goalPosCross.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.goalPosCross.Image = global::MazePathfinder.Properties.Resources.goalPosCross;
            this.goalPosCross.Location = new System.Drawing.Point(331, 235);
            this.goalPosCross.Name = "goalPosCross";
            this.goalPosCross.Size = new System.Drawing.Size(20, 20);
            this.goalPosCross.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.goalPosCross.TabIndex = 16;
            this.goalPosCross.TabStop = false;
            this.goalPosCross.Visible = false;
            this.goalPosCross.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GoalPos_MouseDown);
            this.goalPosCross.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GoalPos_MouseMove);
            this.goalPosCross.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StartGoalPos_MouseUp);
            // 
            // startPosCross
            // 
            this.startPosCross.BackColor = System.Drawing.Color.Transparent;
            this.startPosCross.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.startPosCross.Image = global::MazePathfinder.Properties.Resources.startPosCross;
            this.startPosCross.Location = new System.Drawing.Point(132, 235);
            this.startPosCross.Name = "startPosCross";
            this.startPosCross.Size = new System.Drawing.Size(20, 20);
            this.startPosCross.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.startPosCross.TabIndex = 15;
            this.startPosCross.TabStop = false;
            this.startPosCross.Visible = false;
            this.startPosCross.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StartPos_MouseDown);
            this.startPosCross.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StartPos_MouseMove);
            this.startPosCross.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StartGoalPos_MouseUp);
            // 
            // startPos
            // 
            this.startPos.BackColor = System.Drawing.Color.Transparent;
            this.startPos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.startPos.Image = global::MazePathfinder.Properties.Resources.startPos;
            this.startPos.Location = new System.Drawing.Point(121, 225);
            this.startPos.Name = "startPos";
            this.startPos.Size = new System.Drawing.Size(40, 40);
            this.startPos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.startPos.TabIndex = 2;
            this.startPos.TabStop = false;
            this.startPos.Visible = false;
            this.startPos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StartPos_MouseDown);
            this.startPos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StartPos_MouseMove);
            this.startPos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StartGoalPos_MouseUp);
            this.startPos.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.StartPos_MouseWheel);
            // 
            // goalPos
            // 
            this.goalPos.BackColor = System.Drawing.Color.Transparent;
            this.goalPos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.goalPos.Image = global::MazePathfinder.Properties.Resources.goalPos;
            this.goalPos.Location = new System.Drawing.Point(321, 225);
            this.goalPos.Name = "goalPos";
            this.goalPos.Size = new System.Drawing.Size(40, 40);
            this.goalPos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.goalPos.TabIndex = 3;
            this.goalPos.TabStop = false;
            this.goalPos.Visible = false;
            this.goalPos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GoalPos_MouseDown);
            this.goalPos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GoalPos_MouseMove);
            this.goalPos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StartGoalPos_MouseUp);
            this.goalPos.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.GoalPos_MouseWheel);
            // 
            // mazePathfinderPictureBox
            // 
            this.mazePathfinderPictureBox.Image = global::MazePathfinder.Properties.Resources.uploadFile;
            this.mazePathfinderPictureBox.Location = new System.Drawing.Point(215, 80);
            this.mazePathfinderPictureBox.Name = "mazePathfinderPictureBox";
            this.mazePathfinderPictureBox.Size = new System.Drawing.Size(151, 104);
            this.mazePathfinderPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mazePathfinderPictureBox.TabIndex = 13;
            this.mazePathfinderPictureBox.TabStop = false;
            // 
            // loadingNow
            // 
            this.loadingNow.BackColor = System.Drawing.Color.White;
            this.loadingNow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.loadingNow.Image = global::MazePathfinder.Properties.Resources.MazePathfinderLoader;
            this.loadingNow.Location = new System.Drawing.Point(324, 155);
            this.loadingNow.Name = "loadingNow";
            this.loadingNow.Size = new System.Drawing.Size(40, 41);
            this.loadingNow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loadingNow.TabIndex = 8;
            this.loadingNow.TabStop = false;
            this.loadingNow.Visible = false;
            // 
            // calculationReject
            // 
            this.calculationReject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.calculationReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calculationReject.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.calculationReject.Location = new System.Drawing.Point(223, 319);
            this.calculationReject.Name = "calculationReject";
            this.calculationReject.Size = new System.Drawing.Size(136, 30);
            this.calculationReject.TabIndex = 17;
            this.calculationReject.Text = "解析中止";
            this.calculationReject.UseVisualStyleBackColor = false;
            this.calculationReject.Visible = false;
            this.calculationReject.Click += new System.EventHandler(this.CalculationReject_Click);
            // 
            // useBfsButton
            // 
            this.useBfsButton.AutoSize = true;
            this.useBfsButton.Checked = true;
            this.useBfsButton.Location = new System.Drawing.Point(439, 5);
            this.useBfsButton.Name = "useBfsButton";
            this.useBfsButton.Size = new System.Drawing.Size(122, 22);
            this.useBfsButton.TabIndex = 18;
            this.useBfsButton.TabStop = true;
            this.useBfsButton.Text = "幅優先探索を使用";
            this.useBfsButton.UseVisualStyleBackColor = true;
            this.useBfsButton.Visible = false;
            this.useBfsButton.CheckedChanged += new System.EventHandler(this.useBfsButton_CheckedChanged);
            // 
            // useDfsButton
            // 
            this.useDfsButton.AutoSize = true;
            this.useDfsButton.Location = new System.Drawing.Point(439, 33);
            this.useDfsButton.Name = "useDfsButton";
            this.useDfsButton.Size = new System.Drawing.Size(134, 22);
            this.useDfsButton.TabIndex = 19;
            this.useDfsButton.Text = "深さ優先探索を使用";
            this.useDfsButton.UseVisualStyleBackColor = true;
            this.useDfsButton.Visible = false;
            this.useDfsButton.CheckedChanged += new System.EventHandler(this.useDfsButton_CheckedChanged);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.useDfsButton);
            this.Controls.Add(this.useBfsButton);
            this.Controls.Add(this.calculationReject);
            this.Controls.Add(this.goalPosCross);
            this.Controls.Add(this.startPosCross);
            this.Controls.Add(this.startPos);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.goalPos);
            this.Controls.Add(this.goalLabel);
            this.Controls.Add(this.palletButton);
            this.Controls.Add(this.recalculationButton);
            this.Controls.Add(this.returnStartButton);
            this.Controls.Add(this.imageSaveButton);
            this.Controls.Add(this.calculationStartButton);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(this.mazePathfinderPictureBox);
            this.Controls.Add(this.mazePathfinderLabel2);
            this.Controls.Add(this.mazePathfinderLabel1);
            this.Controls.Add(this.loadingNow);
            this.Controls.Add(this.mazePathfinderLabel3);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MazePathfinder";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form_DragEnter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseEnter += new System.EventHandler(this.MainForm_MouseEnter);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.goalPosCross)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPosCross)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goalPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mazePathfinderPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadingNow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Extensions.OpacityLabel mazePathfinderLabel2;
        private Extensions.OpacityLabel startLabel;
        private Extensions.OpacityLabel goalLabel;
        private System.Windows.Forms.PictureBox startPos;
        private System.Windows.Forms.PictureBox goalPos;
        private System.Windows.Forms.PictureBox loadingNow;
        private System.Windows.Forms.PictureBox mazePathfinderPictureBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label mazePathfinderLabel3;
        private System.Windows.Forms.Label mazePathfinderLabel1;
        private System.Windows.Forms.Button imageSaveButton;
        private System.Windows.Forms.Button returnStartButton;
        private System.Windows.Forms.Button recalculationButton;
        private System.Windows.Forms.Button palletButton;
        private System.Windows.Forms.Button calculationStartButton;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.PictureBox startPosCross;
        private System.Windows.Forms.PictureBox goalPosCross;
        private System.Windows.Forms.Button calculationReject;
        private RadioButton useBfsButton;
        private RadioButton useDfsButton;
    }
}
