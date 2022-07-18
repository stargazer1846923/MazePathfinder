using System.ComponentModel;
using Timer = System.Windows.Forms.Timer;
using MazePathfinder.Extensions;

namespace MazePathfinder
{
    /// <summary>
    /// 迷路解析を行うメインフォームです
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>迷路画像を読み込み済みかどうか</summary>
        private bool isFileLoaded = false;

        /// <summary>マウスをドラッグしているかどうか</summary>
        private bool isDragging = false;

        /// <summary>フォーム上にマウスカーソルがあるかどうか</summary>
        private bool isMouseEntered = false;

        /// <summary>mazePathfinderLabel2ラベルの位置Y</summary>
        private int labelPosY;

        /// <summary>迷路配列の横幅の大きさ</summary>
        private int mazeWidth;

        /// <summary>迷路配列の縦軸の大きさ</summary>
        private int mazeHeight;

        /// <summary>スタート地点アイコンの大きさ</summary>
        private double startPointRatio = 1.0;

        /// <summary>ゴール地点アイコンの大きさ</summary>
        private double goalPointRatio = 1.0;

        /// <summary>スタート地点アイコンの位置</summary>
        private Point startPoint;

        /// <summary>ゴール地点アイコンの位置</summary>
        private Point goalPoint;

        /// <summary>スタート地点の位置</summary>
        private Point startPosition = new Point(0, 0);

        /// <summary>ゴール地点の位置</summary>
        private Point goalPosition = new Point(0, 0);

        /// <summary>迷路画像ビットマップイメージ</summary>
        private Bitmap mazeImage;

        /// <summary>ペイント用背景画像</summary>
        private Bitmap mazeBackGroundImage;

        /// <summary>テキストデータの最適化の閾値</summary>
        private float suitableTextData = 0.5f;

        /// <summary>ペイントパレットフォーム</summary>
        private PaintPallet paintPallet;

        /// <summary>スタート地点、ゴール地点の探索範囲(直径)の最大値</summary>
        private int searchPositionMaxrange = 14;

        /// <summary>mazePathfinderLabel2ラベルが透明化するまでのタイマー</summary>
        private Timer labelToClearTimer;

        /// <summary>mazePathfinderLabel2ラベルが非透明化するまでのタイマー</summary>
        private Timer labelToNotClearTimer;

        /// <summary>幅優先探索を使用して探索する</summary>
        private bool isUseBfs = true;

        /// <summary>画像読み込み時に90度回転させるかの縦横比の閾値</summary>
        const float BITMAP_RATIO = 1.2f;

        /// <summary>mazePathfinderLabel2ラベルが透明化するまでのインターバル時間</summary>
        const int ALPHA_INTERVAL = 50;

        /// <summary>一度のインターバルで減算するアルファ値（透明は0.0、非透明は1.0）</summary>
        const double DIFF_ALPHARATIO = 0.05;

        /// <summary>一度のマウスホイール操作で変化するスタート地点、ゴール地点の大きさ</summary>
        const double DIFF_POINTRATIO = 0.01;

        /// <summary>スタート位置、ゴール位置のアイコンの最大値</summary>
        const int POS_MAXSIZE = 50;

        /// <summary>スタート位置、ゴール位置のアイコンの最小値</summary>
        const int POS_MINSIZE = 10;

        /// <summary>ピクセル明度の初項から前項までの標本平均値、初項から本項までの標本平均値の差</summary>
        const float IMAGEAVG_DIFF = 0.001f;

        /// <summary>色がグレースケールかどうか、分散から判断する閾値</summary>
        const double RGB_DISPERSION = 100.0;

        /// <summary>標本平均値を求めるためのサンプル数</summary>
        const int SAMPLE_NUMBERES = 400;

        /// <summary>イメージリスト</summary>
        public List<Bitmap> ImageList { set; get; }

        /// <summary>イメージリストインデックス</summary>
        public int ImageListIndex { set; get; }

        /// <summary>パレットが起動しているかどうか</summary>
        public bool PalletFormExist { set; get; }

        /// <summary>ペンか直線か</summary>
        public bool IsPenUsing { set; get; }

        /// <summary>初期化用迷路画像</summary>
        public Bitmap MazeFileImage { set; get; }

        /// <summary>ペイントパレットへのイメージの受け渡し</summary>
        public Bitmap MazeImageValue
        {
            set { BackgroundImage = value; }
            get { return (Bitmap)BackgroundImage; }
        }

        /// <summary>ゴールできたかどうか</summary>
        public static bool isGoaled = false;

        /// <summary>探索を中止する</summary>
        public static bool isCalculationCancel = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            startPoint = new Point(startPos.Location.X + startPos.Width / 2, startPos.Location.Y + startPos.Height / 2);
            goalPoint = new Point(goalPos.Location.X + goalPos.Width / 2, goalPos.Location.Y + goalPos.Height / 2);
            ImageList = new List<Bitmap>();
            ImageListIndex = 0;
            PalletFormExist = false;
            IsPenUsing = true;
        }

        /// <summary>
        /// ペイントパレットが既に表示されていなかった場合、表示する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PalletButton_Click(object sender, EventArgs e)
        {
            if (PalletFormExist == false)
            {
                paintPallet = new PaintPallet(this);
                paintPallet.Show();
                paintPallet.TopMost = true;
            }
            return;
        }

        /// <summary>
        /// フォーム上にマウスがあるかどうか判断する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseEnter(object sender, EventArgs e)
        {
            if (BackgroundImage != null && paintPallet != null && PalletFormExist == true)
            {
                isMouseEntered = true;
            }
            return;
        }

        /// <summary>
        /// 迷路画像にペイントする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (BackgroundImage != null && (e.Button & MouseButtons.Left) == MouseButtons.Left && paintPallet != null && paintPallet.MouseDrug == true && PalletFormExist == true)
            {
                paintPallet.MouseDrawed = true;
                if (IsPenUsing == true) // ペイントパレットでペンを選択していた場合の処理
                {
                    DrawLine(e);
                }
                else // ペイントパレットで直線を選択していた場合の処理
                {
                    BackgroundImage = mazeBackGroundImage;
                    Bitmap canvas = new Bitmap(mazeBackGroundImage);
                    Pen objPen = new Pen(paintPallet.BackButtonColor, paintPallet.LineWeight);
                    Graphics objGrp = Graphics.FromImage(canvas);
                    if (isMouseEntered)
                    {
                        objGrp.DrawLine(objPen, paintPallet.LinePrevX, paintPallet.LinePrevY, e.Location.X, e.Location.Y);
                    }
                    else
                    {
                        objGrp.DrawLine(objPen, paintPallet.LinePrevX, paintPallet.LinePrevY, e.Location.X + mazePathfinderLabel2.Location.X, e.Location.Y + mazePathfinderLabel2.Location.Y);
                    }
                    objPen.Dispose();
                    objGrp.Dispose();
                    BackgroundImage = canvas;
                    paintPallet.UndoButtonEnabled = true;
                }
            }
            return;
        }

        /// <summary>
        /// 迷路画像にペイントする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (BackgroundImage != null && (e.Button & MouseButtons.Left) == MouseButtons.Left && paintPallet != null && PalletFormExist == true)
            {
                paintPallet.MouseDrug = true;
                if (isMouseEntered)
                {
                    paintPallet.LinePrevX = e.Location.X;
                    paintPallet.LinePrevY = e.Location.Y;
                }
                else
                {
                    paintPallet.LinePrevX = e.Location.X + mazePathfinderLabel2.Location.X;
                    paintPallet.LinePrevY = e.Location.Y + mazePathfinderLabel2.Location.Y;
                }
                if (IsPenUsing == true) // ペイントパレットでペンを選択していた場合の処理
                {
                    DrawLine(e);
                }
                else // ペイントパレットで直線を選択していた場合の処理
                {
                    mazeBackGroundImage = (Bitmap)BackgroundImage;
                }
            }
            return;
        }

        /// <summary>
        /// 迷路画像にペイントする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && paintPallet != null && BackgroundImage != null && PalletFormExist == true)
            {
                // 後のリストを削除
                paintPallet.RemoveListAfterIndex();

                paintPallet.MouseDrug = false;
                paintPallet.TrashButtonEnabled = true;

                // マウス左ボタンを離した時にリストへ追加
                if (paintPallet.MouseDrawed)
                {
                    paintPallet.MouseDrawed = false;
                    ImageList.Add((Bitmap)BackgroundImage);
                    ImageListIndex = ImageList.Count - 1;
                }
            }
            return;
        }

        /// <summary>
        /// 迷路画像にペンでペイントする
        /// </summary>
        /// <param name="e"></param>
        private void DrawLine(MouseEventArgs e)
        {
            if (paintPallet != null && PalletFormExist == true)
            {
                if (IsPenUsing == true)
                {
                    Bitmap canvas = new Bitmap(BackgroundImage);
                    Pen objPen = new Pen(paintPallet.BackButtonColor, paintPallet.LineWeight);
                    Graphics objGrp = Graphics.FromImage(canvas);
                    if (isMouseEntered == true)
                    {
                        objGrp.DrawLine(objPen, paintPallet.LinePrevX, paintPallet.LinePrevY, e.Location.X, e.Location.Y);
                        objGrp.FillEllipse(new SolidBrush(paintPallet.BackButtonColor), (paintPallet.LinePrevX - objPen.Width / 2), (paintPallet.LinePrevY - objPen.Width / 2), objPen.Width, objPen.Width);
                        paintPallet.LinePrevX = e.Location.X;
                        paintPallet.LinePrevY = e.Location.Y;
                    }
                    else
                    {
                        objGrp.DrawLine(objPen, paintPallet.LinePrevX, paintPallet.LinePrevY, e.Location.X + mazePathfinderLabel2.Location.X, e.Location.Y + mazePathfinderLabel2.Location.Y);
                        objGrp.FillEllipse(new SolidBrush(paintPallet.BackButtonColor), (paintPallet.LinePrevX - objPen.Width / 2), (paintPallet.LinePrevY - objPen.Width / 2), objPen.Width, objPen.Width);
                        paintPallet.LinePrevX = e.Location.X + mazePathfinderLabel2.Location.X;
                        paintPallet.LinePrevY = e.Location.Y + mazePathfinderLabel2.Location.Y;
                    }
                    objPen.Dispose();
                    objGrp.Dispose();
                    BackgroundImage = canvas;
                    paintPallet.UndoButtonEnabled = true;
                }
            }
            return;
        }

        /// <summary>
        /// 迷路画像読み込みダイアログを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileButton_Click(object sender, EventArgs e)
        {
            if (isFileLoaded == false)
            {
                OpenFileDialog openImage = new OpenFileDialog();
                openImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                openImage.Filter = "画像ファイル(*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|すべてのファイル(*.*)|*.*";
                openImage.FilterIndex = 1;
                openImage.Title = "迷路の画像ファイルを選択してください";
                openImage.RestoreDirectory = true;
                if (openImage.ShowDialog() == DialogResult.OK)
                {
                    OpenImageFile(openImage.FileName);
                    isFileLoaded = true;
                }
            }
            return;
        }

        /// <summary>
        /// 迷路画像がフォームにドラッグアンドドロップされた場合、読み込む
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            if (isFileLoaded == false && e != null)
            {
                OpenImageFile(((string[])e.Data.GetData(DataFormats.FileDrop, false))[0]);
            }
            return;
        }

        /// <summary>
        /// 迷路画像読み込み処理をする
        /// </summary>
        /// <param name="fileImage">迷路画像パス</param>
        private void OpenImageFile(string fileImage)
        {
            try
            {
                // 画像データではない場合、読み込みを中断する
                string cheakImage = Path.GetExtension(fileImage).ToUpper();
                if (cheakImage != ".BMP" && cheakImage != ".JPG" && cheakImage != ".JPEG" && cheakImage != ".PNG")
                {
                    return;
                }

                // 縦長の画像の場合、90度傾ける
                mazeImage = new Bitmap(fileImage);
                if ((float)mazeImage.Height / (float)mazeImage.Width > BITMAP_RATIO)
                {
                    mazeImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }

                // 迷路画像にあわせて画面のフォームの大きさを調整する
                ClientSize = new Size(ClientSize.Width, ((mazeImage.Height * ClientSize.Width) / mazeImage.Width));
                mazeImage = new Bitmap(mazeImage, ClientSize.Width, ClientSize.Height);
                MazeFileImage = new Bitmap(mazeImage, ClientSize.Width, ClientSize.Height);
                BackgroundImage = mazeImage;
                BackgroundImageLayout = ImageLayout.None;
                labelPosY = ClientSize.Height / 2 - mazePathfinderLabel2.Size.Height / 2;
                mazePathfinderLabel2.Location = new Point(mazePathfinderLabel2.Location.X, labelPosY);
                mazePathfinderLabel3.Location = new Point(mazePathfinderLabel3.Location.X, labelPosY);
                loadingNow.Location = new Point(loadingNow.Location.X, labelPosY);
                mazePathfinderLabel1.Visible = false;
                mazePathfinderPictureBox.Visible = false;
                fileButton.Visible = false;
                mazePathfinderLabel2.Visible = true;
                isFileLoaded = true;
                startPos.Visible = true;
                goalPos.Visible = true;
                startLabel.Visible = true;
                goalLabel.Visible = true;
                calculationStartButton.Visible = true;
                useBfsButton.Visible = true;
                useDfsButton.Visible = true;
                palletButton.Visible = true;
                returnStartButton.Visible = true;

                // フォームの高さが調整された場合はアイコンの位置を調整する
                startPos.Location = new Point(startPos.Location.X, labelPosY + 70);
                startLabel.Location = new Point(startLabel.Location.X, labelPosY + 80);
                goalPos.Location = new Point(goalPos.Location.X, labelPosY + 70);
                goalLabel.Location = new Point(goalLabel.Location.X, labelPosY + 80);
                startPosCross.Location = new Point(startPos.Location.X + (startPos.Width - startPosCross.Width) / 2, startPos.Location.Y + (startPos.Height - startPosCross.Height) / 2);
                goalPosCross.Location = new Point(goalPos.Location.X + (goalPos.Width - goalPosCross.Width) / 2, goalPos.Location.Y + (goalPos.Height - goalPosCross.Height) / 2);
                calculationStartButton.Location = new Point(calculationStartButton.Location.X, ClientSize.Height - 40);
                palletButton.Location = new Point(palletButton.Location.X, ClientSize.Height - 40);
                imageSaveButton.Location = new Point(imageSaveButton.Location.X, ClientSize.Height - 40);
                returnStartButton.Location = new Point(72, ClientSize.Height - 40);
                recalculationButton.Location = new Point(recalculationButton.Location.X, ClientSize.Height - 40);
                calculationReject.Location = new Point(calculationReject.Location.X, ClientSize.Height - 40);
            }
            catch (Exception ex)
            {
                // フォームの状態を初期状態に戻す
                isFileLoaded = false;
                isDragging = false;
                returnStartButton.Visible = false;
                BackgroundImage = null;
                startPos.Visible = false;
                goalPos.Visible = false;
                calculationStartButton.Visible = false;
                useBfsButton.Visible = false;
                useDfsButton.Visible = false;
                palletButton.Visible = false;
                mazePathfinderLabel2.Visible = false;
                mazePathfinderLabel1.Visible = true;
                mazePathfinderPictureBox.Visible = true;
                fileButton.Visible = true;
                ClientSize = new Size(584, 361);
                startPos.Location = new Point(121, 225);
                startPosCross.Location = new Point(132, 235);
                goalPos.Location = new Point(321, 225);
                goalPosCross.Location = new Point(331, 235);
                startLabel.Visible = false;
                goalLabel.Visible = false;
                recalculationButton.Location = new Point(308, ClientSize.Height - 40);
                returnStartButton.Location = new Point(154, ClientSize.Height - 40);
                MessageBox.Show("ファイル読み込み時にエラーが発生しました。\n\n詳細：" + ex.ToString(), "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        /// <summary>
        /// 迷路画像をフォーム上にドラッグしている場合、カーソルを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) == true && isFileLoaded == false)
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            return;
        }

        /// <summary>
        /// mazePathfinderLabel2ラベル上にマウスがある場合、透明化する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MazePathfinderLabel2_MouseEnter(object sender, EventArgs e)
        {
            // mazePathfinderLabel2を透明化する
            double alphaRatio = mazePathfinderLabel2.Opacity;
            if (labelToNotClearTimer != null)
            {
                labelToNotClearTimer.Enabled = false;
            }
            isMouseEntered = false;
            labelToClearTimer = new Timer() { Interval = ALPHA_INTERVAL };
            labelToClearTimer.Tick += (s, ea) =>
            {
                alphaRatio = Math.Min(Math.Max(alphaRatio - DIFF_ALPHARATIO, 0.0), 1.0);
                mazePathfinderLabel2.Opacity = alphaRatio;
                mazePathfinderLabel2.BackColor = Color.FromArgb((int)(255.0 * alphaRatio), 255, 255, 255);
                if (mazePathfinderLabel2.Opacity <= 0.0)
                {
                    mazePathfinderLabel2.Opacity = 0.0;
                    labelToClearTimer.Enabled = false;
                }
            };
            labelToClearTimer.Enabled = true;
            return;
        }

        /// <summary>
        /// mazePathfinderLabel2ラベル上からマウスが離れた場合、非透明化する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MazePathfinderLabel2_MouseLeave(object sender, EventArgs e)
        {
            // mazePathfinderLabel2を非透明化する
            if (isDragging == false)
            {
                double alphaRatio = mazePathfinderLabel2.Opacity;
                if (labelToClearTimer != null)
                {
                    labelToClearTimer.Enabled = false;
                }
                labelToNotClearTimer = new Timer() { Interval = ALPHA_INTERVAL };
                labelToNotClearTimer.Tick += (s, ea) =>
                {
                    alphaRatio = Math.Min(Math.Max(alphaRatio + DIFF_ALPHARATIO, 0.0), 1.0);
                    mazePathfinderLabel2.Opacity = alphaRatio;
                    mazePathfinderLabel2.BackColor = Color.FromArgb((int)(255.0 * alphaRatio), 255, 255, 255);
                    if (mazePathfinderLabel2.Opacity >= 1.0)
                    {
                        mazePathfinderLabel2.Opacity = 1.0;
                        labelToNotClearTimer.Enabled = false;
                    }
                };
                labelToNotClearTimer.Enabled = true;
            }
            return;
        }

        /// <summary>
        /// スタート地点のアイコンをクリックしたかどうか
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartPos_MouseDown(object sender, MouseEventArgs e)
        {
            // 右クリックした場合、アイコンを変更する
            if (e.Button == MouseButtons.Right)
            {
                if (startPos.Visible)
                {
                    startPos.Visible = false;
                    startPosCross.Visible = true;
                }
                else
                {
                    startPos.Visible = true;
                    startPosCross.Visible = false;
                }
            }

            // startLabelがある場合、透過する
            if (e.Button == MouseButtons.Left)
            {
                if (startLabel.Visible == true)
                {
                    double alphaRatio = 1.0;
                    startLabel.Opacity = alphaRatio;
                    Timer tm = new Timer() { Interval = ALPHA_INTERVAL };
                    tm.Tick += (s, ea) =>
                    {
                        alphaRatio -= DIFF_ALPHARATIO;
                        startLabel.Opacity = alphaRatio;
                        startLabel.BackColor = Color.FromArgb((int)(255.0 * alphaRatio), 255, 255, 255);
                        if (startLabel.Opacity <= 0.0)
                        {
                            startLabel.Opacity = 0.0;
                            startLabel.Visible = false;
                            tm.Enabled = false;
                        }
                    };
                    tm.Enabled = true;
                }
                startPoint = new Point(e.X, e.Y);
                isDragging = true;
            }
            return;
        }

        /// <summary>
        /// ゴール地点のアイコンをクリックしたかどうか
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoalPos_MouseDown(object sender, MouseEventArgs e)
        {
            // 右クリックした場合、アイコンを変更する
            if (e.Button == MouseButtons.Right)
            {
                if (goalPos.Visible)
                {
                    goalPos.Visible = false;
                    goalPosCross.Visible = true;
                }
                else
                {
                    goalPos.Visible = true;
                    goalPosCross.Visible = false;
                }
            }

            // goalLabelがある場合、透過する
            if (e.Button == MouseButtons.Left)
            {
                if (goalLabel.Visible == true)
                {
                    double alphaRatio = 1.0;
                    goalLabel.Opacity = alphaRatio;
                    Timer tm = new Timer() { Interval = ALPHA_INTERVAL };
                    tm.Tick += (s, ea) =>
                    {
                        alphaRatio -= DIFF_ALPHARATIO;
                        goalLabel.Opacity = alphaRatio;
                        goalLabel.BackColor = Color.FromArgb((int)(255.0 * alphaRatio), 255, 255, 255);
                        if (goalLabel.Opacity <= 0.0)
                        {
                            goalLabel.Opacity = 0.0;
                            goalLabel.Visible = false;
                            tm.Enabled = false;
                        }
                    };
                    tm.Enabled = true;
                }
                goalPoint = new Point(e.X, e.Y);
                isDragging = true;
            }
            return;
        }

        /// <summary>
        /// スタート地点のアイコンをドラッグした場合、アイコン位置を動かす
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartPos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = startPos.Location.X - startPoint.X + e.X;
                int y = startPos.Location.Y - startPoint.Y + e.Y;

                // 画面外へはみ出ないように処理
                if (x <= 0)
                {
                    x = 0;
                }
                else if (x >= (ClientSize.Width - startPos.Width))
                {
                    x = (ClientSize.Width - startPos.Width);
                }
                if (y <= 0)
                {
                    y = 0;
                }
                else if (y >= (ClientSize.Height - startPos.Height))
                {
                    y = (ClientSize.Height - startPos.Height);
                }
                startPos.Location = new Point(x, y);
                startPosCross.Location = new Point(startPos.Location.X + (startPos.Width - startPosCross.Width) / 2, startPos.Location.Y + (startPos.Height - startPosCross.Height) / 2);
                calculationStartButton.Location = new Point(calculationStartButton.Location.X, -100); // 解析開始ボタンを画面外へ退避
                palletButton.Location = new Point(palletButton.Location.X, -100); // パレットボタンを画面外へ退避
                returnStartButton.Location = new Point(returnStartButton.Location.X, -100); // 初期画面に戻るボタンを画面外へ退避
            }
            return;
        }

        /// <summary>
        /// ゴール地点のアイコンをドラッグした場合、アイコン位置を動かす
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoalPos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = goalPos.Location.X + e.X - goalPoint.X;
                int y = goalPos.Location.Y + e.Y - goalPoint.Y;

                // 画面外へはみ出ないように処理
                if (x <= 0)
                {
                    x = 0;
                }
                else if (x >= (ClientSize.Width - goalPos.Width))
                {
                    x = (ClientSize.Width - goalPos.Width);
                }
                if (y <= 0)
                {
                    y = 0;
                }
                else if (y >= (ClientSize.Height - goalPos.Height))
                {
                    y = (ClientSize.Height - goalPos.Height);
                }
                goalPos.Location = new Point(x, y);
                goalPosCross.Location = new Point(goalPos.Location.X + (goalPos.Width - goalPosCross.Width) / 2, goalPos.Location.Y + (goalPos.Height - goalPosCross.Height) / 2);
                calculationStartButton.Location = new Point(calculationStartButton.Location.X, -100); // 解析開始ボタンを画面外へ退避
                palletButton.Location = new Point(palletButton.Location.X, -100); // パレットボタンを画面外へ退避
                returnStartButton.Location = new Point(returnStartButton.Location.X, -100); // 初期画面に戻るボタンを画面外へ退避
            }
            return;
        }

        /// <summary>
        /// スタート地点、ゴール地点のアイコンをドロップしたかどうか
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGoalPos_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            calculationStartButton.Location = new Point(calculationStartButton.Location.X, ClientSize.Height - 40); // 画面外へ退避させていた解析開始ボタン定位置に戻す
            palletButton.Location = new Point(palletButton.Location.X, ClientSize.Height - 40); // 画面外へ退避させていたパレットボタン定位置に戻す
            returnStartButton.Location = new Point(returnStartButton.Location.X, ClientSize.Height - 40); // 画面外へ退避させていた初期画面に戻るボタンを定位置に戻す
            return;
        }

        /// <summary>
        /// スタート地点のアイコン上でマウスのホイールを動かしている場合、アイコンを拡大縮小する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartPos_MouseWheel(object sender, MouseEventArgs e)
        {
            double prevRatio = Math.Max(Math.Min(startPointRatio, 1.1), 0.9); // スタート地点のアイコンの大きさの変化量の最小値0.9、最大値1.1
            startPointRatio = Math.Max(Math.Min(startPointRatio, 1.1), 0.9);
            if (e.Delta > 0)
            {
                startPointRatio += DIFF_POINTRATIO;
            }
            else
            {
                startPointRatio -= DIFF_POINTRATIO;
            }
            double scaleRatio = prevRatio / startPointRatio;
            Size prevSize = startPos.Size;
            double scalePosX = (double)(startPos.Location.X + (startPos.Width + startPos.Height) / 4); // 半径と平均を同時に求めているため4で割る
            double scalePosY = (double)(startPos.Location.Y + (startPos.Width + startPos.Height) / 4); // 半径と平均を同時に求めているため4で割る
            startPos.Width = Math.Max(Math.Min((int)(((double)(startPos.Width + startPos.Height) / 2 - scalePosX) * scaleRatio + scalePosX), POS_MAXSIZE), POS_MINSIZE);
            startPos.Height = Math.Max(Math.Min((int)(((double)(startPos.Width + startPos.Height) / 2 - scalePosY) * scaleRatio + scalePosY), POS_MAXSIZE), POS_MINSIZE);
            startPos.Location = new Point(startPos.Location.X - (startPos.Size.Width - prevSize.Width) / 2, startPos.Location.Y - (startPos.Size.Height - prevSize.Height) / 2);
            startPosCross.Location = new Point(startPos.Location.X + (startPos.Width - startPosCross.Width) / 2, startPos.Location.Y + (startPos.Height - startPosCross.Height) / 2);
            return;
        }

        /// <summary>
        /// ゴール地点のアイコン上でマウスのホイールを動かしている場合、アイコンを拡大縮小する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoalPos_MouseWheel(object sender, MouseEventArgs e)
        {
            double prevRatio = Math.Max(Math.Min(goalPointRatio, 1.1), 0.9); // ゴール地点のアイコンの大きさの変化量の最小値0.9、最大値1.1
            goalPointRatio = Math.Max(Math.Min(goalPointRatio, 1.1), 0.9);
            if (e.Delta > 0)
            {
                goalPointRatio += DIFF_POINTRATIO;
            }
            else
            {
                goalPointRatio -= DIFF_POINTRATIO;
            }
            double scaleRatio = prevRatio / goalPointRatio;
            Size prevSize = goalPos.Size;
            double scalePosX = (double)(goalPos.Location.X + (goalPos.Width + goalPos.Height) / 4); // 半径と平均を同時に求めているため4で割る
            double scalePosY = (double)(goalPos.Location.Y + (goalPos.Width + goalPos.Height) / 4); // 半径と平均を同時に求めているため4で割る
            goalPos.Width = Math.Max(Math.Min((int)(((double)(goalPos.Width + goalPos.Height) / 2 - scalePosX) * scaleRatio + scalePosX), POS_MAXSIZE), POS_MINSIZE);
            goalPos.Height = Math.Max(Math.Min((int)(((double)(goalPos.Width + goalPos.Height) / 2 - scalePosY) * scaleRatio + scalePosY), POS_MAXSIZE), POS_MINSIZE);
            goalPos.Location = new Point(goalPos.Location.X - (goalPos.Size.Width - prevSize.Width) / 2, goalPos.Location.Y - (goalPos.Size.Height - prevSize.Height) / 2);
            goalPosCross.Location = new Point(goalPos.Location.X + (goalPos.Width - goalPosCross.Width) / 2, goalPos.Location.Y + (goalPos.Height - goalPosCross.Height) / 2);
            return;
        }

        /// <summary>
        /// 迷路の探索を開始する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CalculationStartButton_Click(object sender, EventArgs e)
        {
            calculationReject.Visible = true;
            if (paintPallet != null)
            {
                ImageList.Clear();
                paintPallet.UndoButtonEnabled = false;
                paintPallet.RedoButtonEnabled = false;
                paintPallet.TrashButtonEnabled = false;
                ImageListIndex = 0;
                PalletFormExist = false;
                paintPallet.Close();
            }

            mazeImage = (Bitmap)BackgroundImage;
            int[,] maze = new int[mazeImage.Size.Height, mazeImage.Size.Width];
            int x;
            int y;
            mazeWidth = maze.GetLength(1) - 1;
            mazeHeight = maze.GetLength(0) - 1;
            List<float> imageBrightness = new List<float>();

            // ランダムサーチで明度の標本平均値を求める
            float imageAbg = 0.0f;
            for (int i = 0; i < SAMPLE_NUMBERES + 100; i++)
            {
                imageBrightness.Add(mazeImage.GetPixel(RandomDigit.GetRandomNumber(mazeWidth - 2), RandomDigit.GetRandomNumber(mazeHeight - 2)).GetBrightness());
                if (i >= SAMPLE_NUMBERES)
                {
                    if (Math.Abs(imageBrightness.Select(s => s).Average() - imageAbg) <= IMAGEAVG_DIFF)
                    {
                        break;
                    }
                    imageAbg = imageBrightness.Select(s => s).Average();
                }
            }

            // スタート地点・ゴール地点のピクセル明度の最大値を閾値として二値化の1か0かを設定する
            int boolBritness;
            if (imageAbg < Math.Max(mazeImage.GetPixel(startPos.Location.X + startPos.Width / 2, startPos.Location.Y + startPos.Height / 2).GetBrightness(), mazeImage.GetPixel(goalPos.Location.X + goalPos.Width / 2, goalPos.Location.Y + goalPos.Height / 2).GetBrightness()))
            {
                boolBritness = 0;
            }
            else
            {
                boolBritness = 1;
            }

            int countWall = 0;
            int countRoute = 0;
            for (y = 0; y < mazeHeight; y++)
            {
                for (x = 0; x < mazeWidth; x++)
                {
                    if (imageAbg <= mazeImage.GetPixel(x + 1, y + 1).GetBrightness())
                    {
                        maze[y, x] = boolBritness;
                        countWall++;
                    }
                    else
                    {
                        maze[y, x] = Math.Abs(boolBritness - 1);
                        countRoute++;
                    }
                }
            }
            if (countWall == 0 || countRoute == 0) // 単色の画像の場合終了
            {
                calculationReject.Visible = false;
                MessageBox.Show("この画像は迷路ではないため、解析できません。", "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // スタート地点、ゴール地点の探索範囲(直径)の最大値を壁数から定義する（直径のため偶数にする）
            countWall = 0;
            countRoute = 0;
            for (y = 0; y < mazeHeight; y++)
            {
                for (x = 0; x < mazeWidth - 1; x++)
                {
                    if ((maze[y, x] - maze[y, x + 1]) == -1 || (maze[y, x] - maze[y, x + 1]) == 1)
                    {
                        countWall++;
                    }
                }
            }
            if (countWall > 15000)
            {
                searchPositionMaxrange = 14;
            }
            else if (countWall <= 15000 && countWall > 10000)
            {
                searchPositionMaxrange = 20;
            }
            else if (countWall <= 10000 && countWall > 5000)
            {
                searchPositionMaxrange = 30;
            }
            else
            {
                searchPositionMaxrange = 40;
            }

            // 壁の色を求める
            List<int> imageR = new List<int>();
            List<int> imageG = new List<int>();
            List<int> imageB = new List<int>();
            int j = 0;
            while (j < SAMPLE_NUMBERES)
            {
                x = RandomDigit.GetRandomNumber(mazeWidth - 2);
                y = RandomDigit.GetRandomNumber(mazeHeight - 2);
                if (maze[y, x] == 1)
                {
                    imageR.Add(mazeImage.GetPixel(x, y).R);
                    imageG.Add(mazeImage.GetPixel(x, y).G);
                    imageB.Add(mazeImage.GetPixel(x, y).B);
                    j++;
                }
            }
            imageR.Sort();
            imageG.Sort();
            imageB.Sort();

            // 壁の色のメジアンを取得する。最頻値を使わない理由は同じ最頻値が複数ある時と、存在しない時をさけるため。
            Color wallColor = Color.FromArgb(255, imageR[(int)(imageR.Count / 2)], imageG[(int)(imageG.Count / 2)], imageB[(int)(imageB.Count / 2)]);
            int wallAvgColor = (wallColor.R + wallColor.G + wallColor.B) / 3; // RGBの平均値
            double wallDispersionColor = ((Math.Pow((double)(wallColor.R - wallAvgColor), 2) + Math.Pow((double)(wallColor.G - wallAvgColor), 2) + Math.Pow((double)(wallColor.B - wallAvgColor), 2)) / 3); // RGBの分散

            mazePathfinderLabel2.Visible = false;
            mazePathfinderLabel3.Visible = true;
            loadingNow.Visible = true;
            calculationStartButton.Visible = false;
            useBfsButton.Visible = false;
            useDfsButton.Visible = false;
            palletButton.Visible = false;
            returnStartButton.Visible = false;

            // ラベル消去、イベントハンドラ削除（スタート地点、ゴール地点）
            StartAndGoalPosImageEnabled(false);

            // テキストデータ最適化
            await Task.Run(() =>
            {
                FindStartPosition(maze);
                FindGoalPosition(maze);
            });

            // 中止した場合、探索を中止する
            if (isCalculationCancel == true)
            {
                isCalculationCancel = false;
                MessageBox.Show("迷路の探索処理を中止しました。", "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                calculationReject.Visible = false;
                mazePathfinderLabel2.Visible = true;
                mazePathfinderLabel3.Visible = false;
                loadingNow.Visible = false;
                calculationStartButton.Visible = true;
                useBfsButton.Visible = true;
                useDfsButton.Visible = true;
                palletButton.Visible = true;
                returnStartButton.Visible = true;
                StartAndGoalPosImageEnabled(true);
                return;
            }

            // スタート地点・ゴール地点が見つからない場合は終了
            if (startPosition.Y == 0 && startPosition.X == 0 || goalPosition.Y == 0 && goalPosition.X == 0)
            {
                calculationReject.Visible = false;
                mazePathfinderLabel2.Visible = true;
                mazePathfinderLabel3.Visible = false;
                loadingNow.Visible = false;
                calculationStartButton.Visible = true;
                useBfsButton.Visible = true;
                useDfsButton.Visible = true;
                palletButton.Visible = true;
                returnStartButton.Visible = true;
                StartAndGoalPosImageEnabled(true);
                return;
            }

            // 幅優先探索で最短経路を求める
            await Task.Run(() => 
            {
                if(isUseBfs == true)
                {
                    // 幅優先探索
                    MazeSolver.MazeSolverForBfs bfs = new MazeSolver.MazeSolverForBfs(maze, startPosition.X, startPosition.Y, goalPosition.X, goalPosition.Y);
                    bfs.Search();
                }
                else
                {
                    // 深さ優先探索
                    MazeSolver.MazeSolverForDfs dfs = new MazeSolver.MazeSolverForDfs(maze, startPosition.X, startPosition.Y, goalPosition.X, goalPosition.Y);
                    dfs.Search();
                }
            });

            // 中止した場合、探索を中止する
            if (isCalculationCancel == true)
            {
                isCalculationCancel = false;
                MessageBox.Show("迷路の探索処理を中止しました。", "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                calculationReject.Visible = false;
                mazePathfinderLabel2.Visible = true;
                mazePathfinderLabel3.Visible = false;
                loadingNow.Visible = false;
                calculationStartButton.Visible = true;
                useBfsButton.Visible = true;
                useDfsButton.Visible = true;
                palletButton.Visible = true;
                returnStartButton.Visible = true;
                StartAndGoalPosImageEnabled(true);
                return;
            }

            // 解析中ラベルを表示
            mazePathfinderLabel3.Visible = false;
            loadingNow.Visible = false;
            recalculationButton.Visible = true;
            returnStartButton.Visible = true;
            returnStartButton.Location = new Point(104, ClientSize.Height - 40);

            // ゴールした場合
            if (isGoaled == true)
            {
                // 最短経路上の色を求める
                imageR.Clear();
                imageG.Clear();
                imageB.Clear();
                for (y = 0; y < mazeHeight - 1; y++)
                {
                    for (x = 0; x < mazeWidth - 1; x++)
                    {
                        if (maze[y, x] == 2)
                        {
                            imageR.Add(mazeImage.GetPixel(x, y).R);
                            imageG.Add(mazeImage.GetPixel(x, y).G);
                            imageB.Add(mazeImage.GetPixel(x, y).B);
                        }
                    }
                }
                imageR.Sort();
                imageG.Sort();
                imageB.Sort();

                // 経路の色のメジアンを取得する。最頻値を使わない理由は同じ最頻値が複数ある時と、存在しない時をさけるため。
                Color routeColor = Color.FromArgb(255, imageR[(int)(imageR.Count / 2)], imageG[(int)(imageG.Count / 2)], imageB[(int)(imageB.Count / 2)]);
                int rootAvgColor = (routeColor.R + routeColor.G + routeColor.B) / 3; // RGBの平均値
                double rootDispersionColor = ((Math.Pow((double)(routeColor.R - rootAvgColor), 2) + Math.Pow((double)(routeColor.G - rootAvgColor), 2) + Math.Pow((double)(routeColor.B - rootAvgColor), 2)) / 3); // RGBの分散

                Color solvedColor;
                if (wallDispersionColor <= RGB_DISPERSION && rootDispersionColor <= RGB_DISPERSION) // 壁と経路の色のRGBがグレースケールであれば、最短経路を赤色にする。
                {
                    solvedColor = Color.Red;
                }
                else if (wallDispersionColor > RGB_DISPERSION && rootDispersionColor <= RGB_DISPERSION) // 経路の色がグレースケールで、壁の色がグレースケールでない場合、壁の補色を最短経路の色とする。
                {
                    int sumColor = Math.Max(wallColor.R, Math.Max(wallColor.G, wallColor.B)) + Math.Min(wallColor.R, Math.Min(wallColor.G, wallColor.B));
                    solvedColor = Color.FromArgb(255, Math.Min((sumColor - wallColor.R), 200), Math.Min((sumColor - wallColor.G), 200), Math.Min((sumColor - wallColor.B), 200));
                }
                else // 上記以外の場合は経路の補色を最短経路の色とする。
                {
                    int sumColor = Math.Max(routeColor.R, Math.Max(routeColor.G, routeColor.B)) + Math.Min(routeColor.R, Math.Min(routeColor.G, routeColor.B));
                    solvedColor = Color.FromArgb(255, Math.Min((sumColor - routeColor.R), 200), Math.Min((sumColor - routeColor.G), 200), Math.Min((sumColor - routeColor.B), 200));
                }

                // 最短経路にペイントする。
                for (y = 0; y < mazeHeight - 1; y++)
                {
                    for (x = 0; x < mazeWidth - 1; x++)
                    {
                        if (maze[y, x] == 2)
                        {
                            mazeImage.SetPixel(x, y, solvedColor);
                            if (x != mazeWidth - 1)
                            {
                                mazeImage.SetPixel(x + 1, y, solvedColor);
                            }
                            if (y != mazeHeight - 1)
                            {
                                mazeImage.SetPixel(x, y + 1, solvedColor);
                            }
                            if (x != 0)
                            {
                                mazeImage.SetPixel(x - 1, y, solvedColor);
                            }
                            if (y != 0)
                            {
                                mazeImage.SetPixel(x, y - 1, solvedColor);
                            }
                        }
                    }
                }
                Invalidate();

                imageSaveButton.Visible = true;
                startLabel.Visible = false;
                goalLabel.Visible = false;
                calculationReject.Visible = false;
                recalculationButton.Location = new Point(377, ClientSize.Height - 40);
                MessageBox.Show("ゴールまでのルートが見つかりました！", "探索成功");
            }
            else // ゴールしなかった場合
            {
                calculationReject.Visible = false;
                returnStartButton.Location = new Point(154, ClientSize.Height - 40);
                MessageBox.Show("ゴールまでの探索処理がタイムアウトまたはルートが見つかりませんでした。\r\n解析条件を変更しました。再解析してください。", "探索失敗", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return;
        }

        /// <summary>
        /// スタート地点ゴール地点のアイコンの有効化/非有効化をする
        /// </summary>
        /// <param name="isEnabled">有効化/非有効化</param>
        private void StartAndGoalPosImageEnabled(bool isEnabled)
        {
            startPos.Enabled = isEnabled;
            goalPos.Enabled = isEnabled;
            startPosCross.Enabled = isEnabled;
            goalPosCross.Enabled = isEnabled;
            return;
        }

        /// <summary>
        /// 探索処理を中止する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculationReject_Click(object sender, EventArgs e)
        {
            isCalculationCancel = true;
            return;
        }

        /// <summary>
        /// 迷路画像を保存する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageSaveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = @"MazeSolved.png";
            saveFileDialog.Filter = "PNG形式|*.png|BMP形式|*.bmp|JPEG形式|*.jpeg";
            saveFileDialog.ShowDialog();
            saveFileDialog.RestoreDirectory = true;
            return;
        }

        /// <summary>
        /// 画像保存ダイアログを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string fileExtension = Path.GetExtension(saveFileDialog.FileName);
            Graphics g = Graphics.FromImage(mazeImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(mazeImage, 0, 0, ClientSize.Width, ClientSize.Height);
            g.DrawImage(startPos.Image, startPos.Location.X + (startPos.Width - 20) / 2, startPos.Location.Y + (startPos.Height - 20) / 2, 20, 20);
            g.DrawImage(goalPos.Image, goalPos.Location.X + (goalPos.Width - 20) / 2, goalPos.Location.Y + (goalPos.Height - 20) / 2, 20, 20);

            switch (fileExtension.ToUpper())
            {
                case ".BMP":
                    mazeImage.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case ".JPEG":
                    mazeImage.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case ".PNG":
                    mazeImage.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }
            return;
        }

        /// <summary>
        /// 迷路を再探索する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecalculationButton_Click(object sender, EventArgs e)
        {
            returnStartButton.Visible = false;
            recalculationButton.Visible = false;
            StartAndGoalPosImageEnabled(true);
            mazePathfinderLabel3.Visible = false;
            loadingNow.Visible = false;
            calculationStartButton.Visible = true;
            useBfsButton.Visible = true;
            useDfsButton.Visible = true;
            palletButton.Visible = true;
            mazePathfinderLabel1.Visible = false;
            mazePathfinderPictureBox.Visible = false;
            fileButton.Visible = false;
            mazePathfinderLabel2.Visible = true;
            recalculationButton.Visible = false;
            imageSaveButton.Visible = false;
            if (suitableTextData > 0.1f)
            {
                suitableTextData -= 0.1f;
            }
            returnStartButton.Visible = true;
            returnStartButton.Location = new Point(72, ClientSize.Height - 40);
            recalculationButton.Location = new Point(308, ClientSize.Height - 40);
            if (paintPallet != null)
            {
                ImageList.Clear();
                paintPallet.UndoButtonEnabled = false;
                paintPallet.RedoButtonEnabled = false;
                paintPallet.TrashButtonEnabled = false;
                ImageListIndex = 0;
                PalletFormExist = false;
                paintPallet.Close();
            }
            BackgroundImage = MazeFileImage;
            BackgroundImageLayout = ImageLayout.None;
            mazeImage = MazeFileImage;
            MazeFileImage = new Bitmap(mazeImage, ClientSize.Width, ClientSize.Height);
            return;
        }

        /// <summary>
        /// 初期画面に戻る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnStartButton_Click(object sender, EventArgs e)
        {
            if (paintPallet != null)
            {
                ImageList.Clear();
                paintPallet.UndoButtonEnabled = false;
                paintPallet.RedoButtonEnabled = false;
                paintPallet.TrashButtonEnabled = false;
                ImageListIndex = 0;
                PalletFormExist = false;
                paintPallet.Close();
            }
            if (palletButton.Visible == false)
            {
                StartAndGoalPosImageEnabled(true);
            }
            isFileLoaded = false;
            isDragging = false;
            startPointRatio = 1.0;
            goalPointRatio = 1.0;
            returnStartButton.Visible = false;
            imageSaveButton.Visible = false;
            recalculationButton.Visible = false;
            BackgroundImage = null;
            startPos.Visible = false;
            goalPos.Visible = false;
            startPosCross.Visible = false;
            goalPosCross.Visible = false;
            mazePathfinderLabel3.Visible = false;
            loadingNow.Visible = false;
            calculationStartButton.Visible = false;
            useBfsButton.Visible = false;
            useDfsButton.Visible = false;
            palletButton.Visible = false;
            mazePathfinderLabel2.Visible = false;
            mazePathfinderLabel1.Visible = true;
            mazePathfinderPictureBox.Visible = true;
            fileButton.Visible = true;
            ClientSize = new Size(584, 361);
            startPos.Location = new Point(121, 225);
            startPos.Size = new Size(40, 40);
            startPosCross.Location = new Point(132, 235);
            goalPos.Location = new Point(321, 225);
            goalPos.Size = new Size(40, 40);
            goalPosCross.Location = new Point(331, 235);
            startPoint = new Point(startPos.Location.X + startPos.Width / 2, startPos.Location.Y + startPos.Height / 2);
            goalPoint = new Point(goalPos.Location.X + goalPos.Width / 2, goalPos.Location.Y + goalPos.Height / 2);
            startPosition.Y = 0;
            startPosition.X = 0;
            goalPosition.Y = 0;
            goalPosition.X = 0;
            startLabel.Opacity = 1.0;
            goalLabel.Opacity = 1.0;
            startLabel.Visible = false;
            goalLabel.Visible = false;
            suitableTextData = 1.0f;
            startLabel.BackColor = Color.FromArgb(255, 255, 255, 255);
            goalLabel.BackColor = Color.FromArgb(255, 255, 255, 255);
            recalculationButton.Location = new Point(308, ClientSize.Height - 40);
            returnStartButton.Location = new Point(154, ClientSize.Height - 40);
            return;
        }

        /// <summary>
        /// スタート地点を探索する
        /// </summary>
        /// <param name="Maze">迷路配列</param>
        private void FindStartPosition(int[,] Maze)
        {
            for (int i = 3; i < searchPositionMaxrange; i += 2)
            {
                for (int x = ((int)(i / 2) * -1); x <= (int)(i / 2); x++)
                {
                    for (int y = ((int)(i / 2) * -1); y <= (int)(i / 2); y++)
                    {
                        if (((startPos.Location.Y + startPos.Height / 2) + y) <= 0 || ((startPos.Location.X + startPos.Width / 2) + x) <= 0)
                        {
                            continue;
                        }
                        if (Maze[(startPos.Location.Y + startPos.Height / 2) + y, (startPos.Location.X + startPos.Width / 2) + x] == 0)
                        {
                            startPosition.Y = (startPos.Location.Y + startPos.Height / 2) + y;
                            startPosition.X = (startPos.Location.X + startPos.Width / 2) + x;
                            return;
                        }

                        // 中止した場合、探索を中止する
                        if (isCalculationCancel == true)
                        {
                            return;
                        }
                    }
                }
            }
            if (startPosition.X != -1 && startPosition.Y != -1)
            {
                MessageBox.Show("スタート地点が見つかりません。\nアイコンの位置を調整してください。", "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => calculationReject.Visible = false));
            }
            else
            {
                calculationReject.Visible = false;
            }
            startPosition.X = 0;
            startPosition.Y = 0;
            goalPosition.X = -1;
            goalPosition.Y = -1;
            return;
        }

        /// <summary>
        /// ゴール地点を探索する
        /// </summary>
        /// <param name="Maze">迷路配列</param>
        private void FindGoalPosition(int[,] Maze)
        {
            for (int i = 3; i < searchPositionMaxrange; i += 2)
            {
                for (int x = ((int)(i / 2) * -1); x <= (int)(i / 2); x++)
                {
                    for (int y = ((int)(i / 2) * -1); y <= (int)(i / 2); y++)
                    {
                        if (((goalPos.Location.Y + goalPos.Height / 2) + y) <= 0 || ((goalPos.Location.X + goalPos.Width / 2) + x) <= 0)
                        {
                            continue;
                        }
                        if (Maze[(goalPos.Location.Y + goalPos.Height / 2) + y, (goalPos.Location.X + goalPos.Width / 2) + x] == 0)
                        {
                            goalPosition.Y = (goalPos.Location.Y + goalPos.Height / 2) + y;
                            goalPosition.X = (goalPos.Location.X + goalPos.Width / 2) + x;
                            return;
                        }

                        // 中止した場合、探索を中止する
                        if (isCalculationCancel == true)
                        {
                            return;
                        }
                    }
                }
            }
            if (goalPosition.X != -1 && goalPosition.Y != -1)
            {
                MessageBox.Show("ゴール地点が見つかりません。\nアイコンの位置を調整してください。", "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => calculationReject.Visible = false));
            }
            else
            {
                calculationReject.Visible = false;
            }
            goalPosition.X = 0;
            goalPosition.Y = 0;
            return;
        }

        /// <summary>
        /// 幅優先探索を使用する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void useBfsButton_CheckedChanged(object sender, EventArgs e)
        {
            isUseBfs = true;
            return;
        }

        /// <summary>
        /// 深さ優先探索を使用する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void useDfsButton_CheckedChanged(object sender, EventArgs e)
        {
            isUseBfs = false;
            return;
        }
    }
}
