namespace MazePathfinder
{
    /// <summary>
    /// ペイントパレットフォーム
    /// </summary>
    public partial class PaintPallet : Form
    {
        /// <summary>メインフォーム</summary>
        private readonly MainForm mainForm;

        /// <summary>マウスカーソルの位置</summary>
        private Point mousePoint;

        /// <summary>ツールチップ（元に戻すボタン）</summary>
        private readonly ToolTip undoToolTip;

        /// <summary>ツールチップ（やり直しボタン）</summary>
        private readonly ToolTip redoToolTip;

        /// <summary>ツールチップ（全削除ボタン）</summary>
        private readonly ToolTip trashToolTip;

        /// <summary>ペンと直線の太さ</summary>
        public int LineWeight { set; get; }

        /// <summary>ペンと直線の位置X</summary>
        public int LinePrevX { set; get; }

        /// <summary>ペンと直線の位置Y</summary>
        public int LinePrevY { set; get; }

        /// <summary>色ボタンの色</summary>
        public Color BackButtonColor { set; get; }

        /// <summary>マウスをドラッグしているか</summary>
        public bool MouseDrug { set; get; }

        /// <summary>マウスをドラッグしたか</summary>
        public bool MouseDrawed { set; get; }

        /// <summary>
        /// 元に戻すボタンが有効かどうか
        /// </summary>
        public bool UndoButtonEnabled
        {
            set { undoButton.Enabled = value; }
            get { return undoButton.Enabled; }
        }

        /// <summary>
        /// やり直しボタンが有効かどうか
        /// </summary>
        public bool RedoButtonEnabled
        {
            set { redoButton.Enabled = value; }
            get { return redoButton.Enabled; }
        }

        /// <summary>
        /// 全削除ボタンが有効かどうか
        /// </summary>
        public bool TrashButtonEnabled
        {
            set { trashButton.Enabled = value; }
            get { return trashButton.Enabled; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PaintPallet(MainForm MazePathfinder)
        {
            InitializeComponent();
            mainForm = MazePathfinder;
            LineWeight = 3;
            MouseDrug = false;
            MouseDrawed = false;
            mainForm.PalletFormExist = true;
            BackButtonColor = Color.Black;
            colorButton.BackColor = BackButtonColor;
            undoToolTip = new ToolTip();
            redoToolTip = new ToolTip();
            trashToolTip = new ToolTip();

            if (!mainForm.ImageList.Any())
            {
                undoButton.Enabled = false;
                redoButton.Enabled = false;
                trashButton.Enabled = false;
                mainForm.ImageList.Add(mainForm.MazeImageValue);
            }
            else
            {
                if (mainForm.ImageListIndex == 0)
                {
                    undoButton.Enabled = false;
                    redoButton.Enabled = true;
                    trashButton.Enabled = true;
                }
                else if (mainForm.ImageListIndex == mainForm.ImageList.Count - 1)
                {
                    undoButton.Enabled = true;
                    redoButton.Enabled = false;
                    trashButton.Enabled = true;
                }
                else
                {
                    undoButton.Enabled = true;
                    redoButton.Enabled = true;
                    trashButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// フォームを移動する（マウスを左クリックした時）
        /// </summary>
        private void PaintPallet_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) mousePoint = new Point(e.X, e.Y);
            return;
        }

        /// <summary>
        /// フォームを移動する（マウスをドラッグしている時）
        /// </summary>
        private void PaintPallet_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) Location = new Point(Location.X + e.X - mousePoint.X, Location.Y + e.Y - mousePoint.Y);
            return;
        }

        /// <summary>
        /// ペイントパレットを閉じる
        /// </summary>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (mainForm.ImageList.Count == 1)
            {
                mainForm.ImageList.RemoveAt(0);
                mainForm.ImageListIndex = 0;
            }
            mainForm.PalletFormExist = false;
            Close();
            return;
        }

        /// <summary>
        /// カラーパレットを表示する
        /// </summary>
        private void ColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                BackButtonColor = colorDialog.Color;
                colorButton.BackColor = colorDialog.Color;
            }
            return;
        }

        /// <summary>
        /// コンボボックスの入力制限（数字のみ）をする
        /// </summary>
        private void PenSizeBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (penSizeBox.Text == "") return;
            if (penSizeBox.Text == " " || penSizeBox.Text == "　")
            {
                penSizeBox.Text = "";
                return;
            }
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b') e.Handled = true;
            return;
        }

        /// <summary>
        /// コンボボックスの数値を取得する
        /// </summary>
        private void PenSizeBox_TextChanged(object sender, EventArgs e)
        {
            if (penSizeBox.Text == "") return;
            if (penSizeBox.Text == " " || penSizeBox.Text == "　")
            {
                penSizeBox.Text = "";
                return;
            }
            try
            {
                LineWeight = int.Parse(penSizeBox.Text);
            }
            catch (Exception)
            {
                penSizeBox.Text = "";
            }
            return;
        }

        /// <summary>
        /// ペイントリストを一つ前に戻る
        /// </summary>
        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (mainForm.ImageListIndex > 0)
            {
                redoButton.Enabled = true;
                mainForm.ImageListIndex--;
            }
            if (mainForm.ImageListIndex == 0) undoButton.Enabled = false;
            mainForm.MazeImageValue = new Bitmap(mainForm.ImageList[mainForm.ImageListIndex], mainForm.ClientSize.Width, mainForm.ClientSize.Height);
            return;
        }

        /// <summary>
        /// ペイントリストを一つ先に進む
        /// </summary>
        private void RedoButton_Click(object sender, EventArgs e)
        {
            if (mainForm.ImageListIndex < mainForm.ImageList.Count - 1)
            {
                undoButton.Enabled = true;
                mainForm.ImageListIndex++;
            }
            if (mainForm.ImageListIndex == mainForm.ImageList.Count - 1) redoButton.Enabled = false;
            mainForm.MazeImageValue = new Bitmap(mainForm.ImageList[mainForm.ImageListIndex], mainForm.ClientSize.Width, mainForm.ClientSize.Height);
            return;
        }

        /// <summary>
        /// ペイントリストを全削除する
        /// </summary>
        private void TrashButton_Click(object sender, EventArgs e)
        {
            mainForm.MazeImageValue = new Bitmap(mainForm.MazeFileImage, mainForm.ClientSize.Width, mainForm.ClientSize.Height);
            mainForm.ImageList.Clear();
            mainForm.ImageList.Add(mainForm.MazeImageValue);
            mainForm.ImageListIndex = 1;
            redoButton.Enabled = false;
            undoButton.Enabled = false;
            trashButton.Enabled = false;
            return;
        }

        /// <summary>
        /// 後のリストを全て削除する
        /// </summary>
        public void RemoveListAfterIndex()
        {
            while (mainForm.ImageListIndex + 1 < mainForm.ImageList.Count)
            {
                mainForm.ImageList.RemoveAt(mainForm.ImageListIndex + 1);
            }
            redoButton.Enabled = false;
            trashButton.Enabled = true;
            return;
        }

        /// <summary>
        /// 元に戻すボタンツールチップを表示する
        /// </summary>
        private void UndoButton_MouseHover(object sender, EventArgs e)
        {
            undoToolTip.SetToolTip(undoButton, "元に戻す");
            return;
        }

        /// <summary>
        /// やり直しボタンツールチップを表示する
        /// </summary>
        private void RedoButton_MouseHover(object sender, EventArgs e)
        {
            redoToolTip.SetToolTip(redoButton, "やり直し");
            return;
        }

        /// <summary>
        /// 全削除ボタンツールチップを表示する
        /// </summary>
        private void TrashButton_MouseHover(object sender, EventArgs e)
        {
            trashToolTip.SetToolTip(trashButton, "全削除");
            return;
        }

        /// <summary>
        /// ペンを選択する
        /// </summary>
        private void PenButton_Click(object sender, EventArgs e)
        {
            mainForm.IsPenUsing = true;
            return;
        }

        /// <summary>
        /// 直線を選択する
        /// </summary>
        private void LineButton_Click(object sender, EventArgs e)
        {
            mainForm.IsPenUsing = false;
            return;
        }
    }
}
