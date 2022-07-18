namespace MazePathfinder
{
    partial class PaintPallet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PaintPallet));
            this.colorButton = new System.Windows.Forms.Button();
            this.penSizeBox = new System.Windows.Forms.ComboBox();
            this.penLabel = new System.Windows.Forms.Label();
            this.lineLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.colorLabel = new System.Windows.Forms.Label();
            this.trashButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.redoButton = new System.Windows.Forms.Button();
            this.undoButton = new System.Windows.Forms.Button();
            this.lineButton = new System.Windows.Forms.Button();
            this.penButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // colorButton
            // 
            this.colorButton.BackColor = System.Drawing.SystemColors.WindowText;
            this.colorButton.Location = new System.Drawing.Point(174, 62);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(30, 30);
            this.colorButton.TabIndex = 2;
            this.colorButton.UseVisualStyleBackColor = false;
            this.colorButton.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // penSizeBox
            // 
            this.penSizeBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
            this.penSizeBox.Location = new System.Drawing.Point(220, 68);
            this.penSizeBox.MaxLength = 2;
            this.penSizeBox.Name = "penSizeBox";
            this.penSizeBox.Size = new System.Drawing.Size(43, 20);
            this.penSizeBox.TabIndex = 3;
            this.penSizeBox.Text = "3";
            this.penSizeBox.TextChanged += new System.EventHandler(this.PenSizeBox_TextChanged);
            this.penSizeBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PenSizeBox_KeyPress);
            // 
            // penLabel
            // 
            this.penLabel.AutoSize = true;
            this.penLabel.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.penLabel.Location = new System.Drawing.Point(36, 110);
            this.penLabel.Name = "penLabel";
            this.penLabel.Size = new System.Drawing.Size(32, 18);
            this.penLabel.TabIndex = 7;
            this.penLabel.Text = "ペン";
            // 
            // lineLabel
            // 
            this.lineLabel.AutoSize = true;
            this.lineLabel.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lineLabel.Location = new System.Drawing.Point(112, 110);
            this.lineLabel.Name = "lineLabel";
            this.lineLabel.Size = new System.Drawing.Size(32, 18);
            this.lineLabel.TabIndex = 8;
            this.lineLabel.Text = "直線";
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sizeLabel.Location = new System.Drawing.Point(225, 102);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(32, 18);
            this.sizeLabel.TabIndex = 9;
            this.sizeLabel.Text = "太さ";
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.colorLabel.Location = new System.Drawing.Point(179, 102);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(20, 18);
            this.colorLabel.TabIndex = 10;
            this.colorLabel.Text = "色";
            // 
            // trashButton
            // 
            this.trashButton.BackColor = System.Drawing.SystemColors.Window;
            this.trashButton.BackgroundImage = global::MazePathfinder.Properties.Resources.MazePathfinderTrash;
            this.trashButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.trashButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.trashButton.Location = new System.Drawing.Point(70, 3);
            this.trashButton.Name = "trashButton";
            this.trashButton.Size = new System.Drawing.Size(30, 30);
            this.trashButton.TabIndex = 11;
            this.trashButton.UseVisualStyleBackColor = false;
            this.trashButton.Click += new System.EventHandler(this.TrashButton_Click);
            this.trashButton.MouseHover += new System.EventHandler(this.TrashButton_MouseHover);
            // 
            // closeButton
            // 
            this.closeButton.BackgroundImage = global::MazePathfinder.Properties.Resources.MazePathfinderClose;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Location = new System.Drawing.Point(258, 5);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(20, 20);
            this.closeButton.TabIndex = 6;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // redoButton
            // 
            this.redoButton.BackColor = System.Drawing.SystemColors.Window;
            this.redoButton.BackgroundImage = global::MazePathfinder.Properties.Resources.MazePathfinderRedo;
            this.redoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.redoButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.redoButton.Location = new System.Drawing.Point(37, 3);
            this.redoButton.Name = "redoButton";
            this.redoButton.Size = new System.Drawing.Size(30, 30);
            this.redoButton.TabIndex = 5;
            this.redoButton.UseVisualStyleBackColor = false;
            this.redoButton.Click += new System.EventHandler(this.RedoButton_Click);
            this.redoButton.MouseHover += new System.EventHandler(this.RedoButton_MouseHover);
            // 
            // undoButton
            // 
            this.undoButton.BackColor = System.Drawing.SystemColors.Window;
            this.undoButton.BackgroundImage = global::MazePathfinder.Properties.Resources.MazePathfinderUndo;
            this.undoButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.undoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.undoButton.Location = new System.Drawing.Point(4, 3);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(30, 30);
            this.undoButton.TabIndex = 4;
            this.undoButton.UseVisualStyleBackColor = false;
            this.undoButton.Click += new System.EventHandler(this.UndoButton_Click);
            this.undoButton.MouseHover += new System.EventHandler(this.UndoButton_MouseHover);
            // 
            // lineButton
            // 
            this.lineButton.BackgroundImage = global::MazePathfinder.Properties.Resources.MazePathfinderLine;
            this.lineButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.lineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lineButton.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lineButton.Location = new System.Drawing.Point(98, 47);
            this.lineButton.Name = "lineButton";
            this.lineButton.Size = new System.Drawing.Size(60, 60);
            this.lineButton.TabIndex = 1;
            this.lineButton.UseVisualStyleBackColor = true;
            this.lineButton.Click += new System.EventHandler(this.LineButton_Click);
            // 
            // penButton
            // 
            this.penButton.BackColor = System.Drawing.SystemColors.Window;
            this.penButton.BackgroundImage = global::MazePathfinder.Properties.Resources.MazePathfinderPen;
            this.penButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.penButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.penButton.Location = new System.Drawing.Point(21, 47);
            this.penButton.Name = "penButton";
            this.penButton.Size = new System.Drawing.Size(60, 60);
            this.penButton.TabIndex = 0;
            this.penButton.UseVisualStyleBackColor = false;
            this.penButton.Click += new System.EventHandler(this.PenButton_Click);
            // 
            // PaintPallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(283, 138);
            this.Controls.Add(this.trashButton);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.sizeLabel);
            this.Controls.Add(this.lineLabel);
            this.Controls.Add(this.penLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.redoButton);
            this.Controls.Add(this.undoButton);
            this.Controls.Add(this.penSizeBox);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.lineButton);
            this.Controls.Add(this.penButton);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaintPallet";
            this.Text = "PaintPallet";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PaintPallet_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PaintPallet_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button penButton;
        private System.Windows.Forms.Button lineButton;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.ComboBox penSizeBox;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.Button redoButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label penLabel;
        private System.Windows.Forms.Label lineLabel;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Label colorLabel;
        private System.Windows.Forms.Button trashButton;
    }
}
