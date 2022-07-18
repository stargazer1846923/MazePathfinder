using System.ComponentModel;
using Timer = System.Windows.Forms.Timer;
using MazePathfinder.Extensions;

namespace MazePathfinder
{
    /// <summary>
    /// ���H��͂��s�����C���t�H�[���ł�
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>���H�摜��ǂݍ��ݍς݂��ǂ���</summary>
        private bool isFileLoaded = false;

        /// <summary>�}�E�X���h���b�O���Ă��邩�ǂ���</summary>
        private bool isDragging = false;

        /// <summary>�t�H�[����Ƀ}�E�X�J�[�\�������邩�ǂ���</summary>
        private bool isMouseEntered = false;

        /// <summary>mazePathfinderLabel2���x���̈ʒuY</summary>
        private int labelPosY;

        /// <summary>���H�z��̉����̑傫��</summary>
        private int mazeWidth;

        /// <summary>���H�z��̏c���̑傫��</summary>
        private int mazeHeight;

        /// <summary>�X�^�[�g�n�_�A�C�R���̑傫��</summary>
        private double startPointRatio = 1.0;

        /// <summary>�S�[���n�_�A�C�R���̑傫��</summary>
        private double goalPointRatio = 1.0;

        /// <summary>�X�^�[�g�n�_�A�C�R���̈ʒu</summary>
        private Point startPoint;

        /// <summary>�S�[���n�_�A�C�R���̈ʒu</summary>
        private Point goalPoint;

        /// <summary>�X�^�[�g�n�_�̈ʒu</summary>
        private Point startPosition = new Point(0, 0);

        /// <summary>�S�[���n�_�̈ʒu</summary>
        private Point goalPosition = new Point(0, 0);

        /// <summary>���H�摜�r�b�g�}�b�v�C���[�W</summary>
        private Bitmap mazeImage;

        /// <summary>�y�C���g�p�w�i�摜</summary>
        private Bitmap mazeBackGroundImage;

        /// <summary>�e�L�X�g�f�[�^�̍œK����臒l</summary>
        private float suitableTextData = 0.5f;

        /// <summary>�y�C���g�p���b�g�t�H�[��</summary>
        private PaintPallet paintPallet;

        /// <summary>�X�^�[�g�n�_�A�S�[���n�_�̒T���͈�(���a)�̍ő�l</summary>
        private int searchPositionMaxrange = 14;

        /// <summary>mazePathfinderLabel2���x��������������܂ł̃^�C�}�[</summary>
        private Timer labelToClearTimer;

        /// <summary>mazePathfinderLabel2���x�����񓧖�������܂ł̃^�C�}�[</summary>
        private Timer labelToNotClearTimer;

        /// <summary>���D��T�����g�p���ĒT������</summary>
        private bool isUseBfs = true;

        /// <summary>�摜�ǂݍ��ݎ���90�x��]�����邩�̏c�����臒l</summary>
        const float BITMAP_RATIO = 1.2f;

        /// <summary>mazePathfinderLabel2���x��������������܂ł̃C���^�[�o������</summary>
        const int ALPHA_INTERVAL = 50;

        /// <summary>��x�̃C���^�[�o���Ō��Z����A���t�@�l�i������0.0�A�񓧖���1.0�j</summary>
        const double DIFF_ALPHARATIO = 0.05;

        /// <summary>��x�̃}�E�X�z�C�[������ŕω�����X�^�[�g�n�_�A�S�[���n�_�̑傫��</summary>
        const double DIFF_POINTRATIO = 0.01;

        /// <summary>�X�^�[�g�ʒu�A�S�[���ʒu�̃A�C�R���̍ő�l</summary>
        const int POS_MAXSIZE = 50;

        /// <summary>�X�^�[�g�ʒu�A�S�[���ʒu�̃A�C�R���̍ŏ��l</summary>
        const int POS_MINSIZE = 10;

        /// <summary>�s�N�Z�����x�̏�������O���܂ł̕W�{���ϒl�A��������{���܂ł̕W�{���ϒl�̍�</summary>
        const float IMAGEAVG_DIFF = 0.001f;

        /// <summary>�F���O���[�X�P�[�����ǂ����A���U���画�f����臒l</summary>
        const double RGB_DISPERSION = 100.0;

        /// <summary>�W�{���ϒl�����߂邽�߂̃T���v����</summary>
        const int SAMPLE_NUMBERES = 400;

        /// <summary>�C���[�W���X�g</summary>
        public List<Bitmap> ImageList { set; get; }

        /// <summary>�C���[�W���X�g�C���f�b�N�X</summary>
        public int ImageListIndex { set; get; }

        /// <summary>�p���b�g���N�����Ă��邩�ǂ���</summary>
        public bool PalletFormExist { set; get; }

        /// <summary>�y����������</summary>
        public bool IsPenUsing { set; get; }

        /// <summary>�������p���H�摜</summary>
        public Bitmap MazeFileImage { set; get; }

        /// <summary>�y�C���g�p���b�g�ւ̃C���[�W�̎󂯓n��</summary>
        public Bitmap MazeImageValue
        {
            set { BackgroundImage = value; }
            get { return (Bitmap)BackgroundImage; }
        }

        /// <summary>�S�[���ł������ǂ���</summary>
        public static bool isGoaled = false;

        /// <summary>�T���𒆎~����</summary>
        public static bool isCalculationCancel = false;

        /// <summary>
        /// �R���X�g���N�^
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
        /// �y�C���g�p���b�g�����ɕ\������Ă��Ȃ������ꍇ�A�\������
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
        /// �t�H�[����Ƀ}�E�X�����邩�ǂ������f����
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
        /// ���H�摜�Ƀy�C���g����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (BackgroundImage != null && (e.Button & MouseButtons.Left) == MouseButtons.Left && paintPallet != null && paintPallet.MouseDrug == true && PalletFormExist == true)
            {
                paintPallet.MouseDrawed = true;
                if (IsPenUsing == true) // �y�C���g�p���b�g�Ńy����I�����Ă����ꍇ�̏���
                {
                    DrawLine(e);
                }
                else // �y�C���g�p���b�g�Œ�����I�����Ă����ꍇ�̏���
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
        /// ���H�摜�Ƀy�C���g����
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
                if (IsPenUsing == true) // �y�C���g�p���b�g�Ńy����I�����Ă����ꍇ�̏���
                {
                    DrawLine(e);
                }
                else // �y�C���g�p���b�g�Œ�����I�����Ă����ꍇ�̏���
                {
                    mazeBackGroundImage = (Bitmap)BackgroundImage;
                }
            }
            return;
        }

        /// <summary>
        /// ���H�摜�Ƀy�C���g����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && paintPallet != null && BackgroundImage != null && PalletFormExist == true)
            {
                // ��̃��X�g���폜
                paintPallet.RemoveListAfterIndex();

                paintPallet.MouseDrug = false;
                paintPallet.TrashButtonEnabled = true;

                // �}�E�X���{�^���𗣂������Ƀ��X�g�֒ǉ�
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
        /// ���H�摜�Ƀy���Ńy�C���g����
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
        /// ���H�摜�ǂݍ��݃_�C�A���O���J��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileButton_Click(object sender, EventArgs e)
        {
            if (isFileLoaded == false)
            {
                OpenFileDialog openImage = new OpenFileDialog();
                openImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                openImage.Filter = "�摜�t�@�C��(*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|���ׂẴt�@�C��(*.*)|*.*";
                openImage.FilterIndex = 1;
                openImage.Title = "���H�̉摜�t�@�C����I�����Ă�������";
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
        /// ���H�摜���t�H�[���Ƀh���b�O�A���h�h���b�v���ꂽ�ꍇ�A�ǂݍ���
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
        /// ���H�摜�ǂݍ��ݏ���������
        /// </summary>
        /// <param name="fileImage">���H�摜�p�X</param>
        private void OpenImageFile(string fileImage)
        {
            try
            {
                // �摜�f�[�^�ł͂Ȃ��ꍇ�A�ǂݍ��݂𒆒f����
                string cheakImage = Path.GetExtension(fileImage).ToUpper();
                if (cheakImage != ".BMP" && cheakImage != ".JPG" && cheakImage != ".JPEG" && cheakImage != ".PNG")
                {
                    return;
                }

                // �c���̉摜�̏ꍇ�A90�x�X����
                mazeImage = new Bitmap(fileImage);
                if ((float)mazeImage.Height / (float)mazeImage.Width > BITMAP_RATIO)
                {
                    mazeImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }

                // ���H�摜�ɂ��킹�ĉ�ʂ̃t�H�[���̑傫���𒲐�����
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

                // �t�H�[���̍������������ꂽ�ꍇ�̓A�C�R���̈ʒu�𒲐�����
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
                // �t�H�[���̏�Ԃ�������Ԃɖ߂�
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
                MessageBox.Show("�t�@�C���ǂݍ��ݎ��ɃG���[���������܂����B\n\n�ڍׁF" + ex.ToString(), "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return;
        }

        /// <summary>
        /// ���H�摜���t�H�[����Ƀh���b�O���Ă���ꍇ�A�J�[�\����ύX����
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
        /// mazePathfinderLabel2���x����Ƀ}�E�X������ꍇ�A����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MazePathfinderLabel2_MouseEnter(object sender, EventArgs e)
        {
            // mazePathfinderLabel2�𓧖�������
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
        /// mazePathfinderLabel2���x���ォ��}�E�X�����ꂽ�ꍇ�A�񓧖�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MazePathfinderLabel2_MouseLeave(object sender, EventArgs e)
        {
            // mazePathfinderLabel2��񓧖�������
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
        /// �X�^�[�g�n�_�̃A�C�R�����N���b�N�������ǂ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartPos_MouseDown(object sender, MouseEventArgs e)
        {
            // �E�N���b�N�����ꍇ�A�A�C�R����ύX����
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

            // startLabel������ꍇ�A���߂���
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
        /// �S�[���n�_�̃A�C�R�����N���b�N�������ǂ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoalPos_MouseDown(object sender, MouseEventArgs e)
        {
            // �E�N���b�N�����ꍇ�A�A�C�R����ύX����
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

            // goalLabel������ꍇ�A���߂���
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
        /// �X�^�[�g�n�_�̃A�C�R�����h���b�O�����ꍇ�A�A�C�R���ʒu�𓮂���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartPos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = startPos.Location.X - startPoint.X + e.X;
                int y = startPos.Location.Y - startPoint.Y + e.Y;

                // ��ʊO�ւ͂ݏo�Ȃ��悤�ɏ���
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
                calculationStartButton.Location = new Point(calculationStartButton.Location.X, -100); // ��͊J�n�{�^������ʊO�֑ޔ�
                palletButton.Location = new Point(palletButton.Location.X, -100); // �p���b�g�{�^������ʊO�֑ޔ�
                returnStartButton.Location = new Point(returnStartButton.Location.X, -100); // ������ʂɖ߂�{�^������ʊO�֑ޔ�
            }
            return;
        }

        /// <summary>
        /// �S�[���n�_�̃A�C�R�����h���b�O�����ꍇ�A�A�C�R���ʒu�𓮂���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoalPos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = goalPos.Location.X + e.X - goalPoint.X;
                int y = goalPos.Location.Y + e.Y - goalPoint.Y;

                // ��ʊO�ւ͂ݏo�Ȃ��悤�ɏ���
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
                calculationStartButton.Location = new Point(calculationStartButton.Location.X, -100); // ��͊J�n�{�^������ʊO�֑ޔ�
                palletButton.Location = new Point(palletButton.Location.X, -100); // �p���b�g�{�^������ʊO�֑ޔ�
                returnStartButton.Location = new Point(returnStartButton.Location.X, -100); // ������ʂɖ߂�{�^������ʊO�֑ޔ�
            }
            return;
        }

        /// <summary>
        /// �X�^�[�g�n�_�A�S�[���n�_�̃A�C�R�����h���b�v�������ǂ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGoalPos_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            calculationStartButton.Location = new Point(calculationStartButton.Location.X, ClientSize.Height - 40); // ��ʊO�֑ޔ������Ă�����͊J�n�{�^����ʒu�ɖ߂�
            palletButton.Location = new Point(palletButton.Location.X, ClientSize.Height - 40); // ��ʊO�֑ޔ������Ă����p���b�g�{�^����ʒu�ɖ߂�
            returnStartButton.Location = new Point(returnStartButton.Location.X, ClientSize.Height - 40); // ��ʊO�֑ޔ������Ă���������ʂɖ߂�{�^�����ʒu�ɖ߂�
            return;
        }

        /// <summary>
        /// �X�^�[�g�n�_�̃A�C�R����Ń}�E�X�̃z�C�[���𓮂����Ă���ꍇ�A�A�C�R�����g��k������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartPos_MouseWheel(object sender, MouseEventArgs e)
        {
            double prevRatio = Math.Max(Math.Min(startPointRatio, 1.1), 0.9); // �X�^�[�g�n�_�̃A�C�R���̑傫���̕ω��ʂ̍ŏ��l0.9�A�ő�l1.1
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
            double scalePosX = (double)(startPos.Location.X + (startPos.Width + startPos.Height) / 4); // ���a�ƕ��ς𓯎��ɋ��߂Ă��邽��4�Ŋ���
            double scalePosY = (double)(startPos.Location.Y + (startPos.Width + startPos.Height) / 4); // ���a�ƕ��ς𓯎��ɋ��߂Ă��邽��4�Ŋ���
            startPos.Width = Math.Max(Math.Min((int)(((double)(startPos.Width + startPos.Height) / 2 - scalePosX) * scaleRatio + scalePosX), POS_MAXSIZE), POS_MINSIZE);
            startPos.Height = Math.Max(Math.Min((int)(((double)(startPos.Width + startPos.Height) / 2 - scalePosY) * scaleRatio + scalePosY), POS_MAXSIZE), POS_MINSIZE);
            startPos.Location = new Point(startPos.Location.X - (startPos.Size.Width - prevSize.Width) / 2, startPos.Location.Y - (startPos.Size.Height - prevSize.Height) / 2);
            startPosCross.Location = new Point(startPos.Location.X + (startPos.Width - startPosCross.Width) / 2, startPos.Location.Y + (startPos.Height - startPosCross.Height) / 2);
            return;
        }

        /// <summary>
        /// �S�[���n�_�̃A�C�R����Ń}�E�X�̃z�C�[���𓮂����Ă���ꍇ�A�A�C�R�����g��k������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoalPos_MouseWheel(object sender, MouseEventArgs e)
        {
            double prevRatio = Math.Max(Math.Min(goalPointRatio, 1.1), 0.9); // �S�[���n�_�̃A�C�R���̑傫���̕ω��ʂ̍ŏ��l0.9�A�ő�l1.1
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
            double scalePosX = (double)(goalPos.Location.X + (goalPos.Width + goalPos.Height) / 4); // ���a�ƕ��ς𓯎��ɋ��߂Ă��邽��4�Ŋ���
            double scalePosY = (double)(goalPos.Location.Y + (goalPos.Width + goalPos.Height) / 4); // ���a�ƕ��ς𓯎��ɋ��߂Ă��邽��4�Ŋ���
            goalPos.Width = Math.Max(Math.Min((int)(((double)(goalPos.Width + goalPos.Height) / 2 - scalePosX) * scaleRatio + scalePosX), POS_MAXSIZE), POS_MINSIZE);
            goalPos.Height = Math.Max(Math.Min((int)(((double)(goalPos.Width + goalPos.Height) / 2 - scalePosY) * scaleRatio + scalePosY), POS_MAXSIZE), POS_MINSIZE);
            goalPos.Location = new Point(goalPos.Location.X - (goalPos.Size.Width - prevSize.Width) / 2, goalPos.Location.Y - (goalPos.Size.Height - prevSize.Height) / 2);
            goalPosCross.Location = new Point(goalPos.Location.X + (goalPos.Width - goalPosCross.Width) / 2, goalPos.Location.Y + (goalPos.Height - goalPosCross.Height) / 2);
            return;
        }

        /// <summary>
        /// ���H�̒T�����J�n����
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

            // �����_���T�[�`�Ŗ��x�̕W�{���ϒl�����߂�
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

            // �X�^�[�g�n�_�E�S�[���n�_�̃s�N�Z�����x�̍ő�l��臒l�Ƃ��ē�l����1��0����ݒ肷��
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
            if (countWall == 0 || countRoute == 0) // �P�F�̉摜�̏ꍇ�I��
            {
                calculationReject.Visible = false;
                MessageBox.Show("���̉摜�͖��H�ł͂Ȃ����߁A��͂ł��܂���B", "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // �X�^�[�g�n�_�A�S�[���n�_�̒T���͈�(���a)�̍ő�l��ǐ������`����i���a�̂��ߋ����ɂ���j
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

            // �ǂ̐F�����߂�
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

            // �ǂ̐F�̃��W�A�����擾����B�ŕp�l���g��Ȃ����R�͓����ŕp�l���������鎞�ƁA���݂��Ȃ����������邽�߁B
            Color wallColor = Color.FromArgb(255, imageR[(int)(imageR.Count / 2)], imageG[(int)(imageG.Count / 2)], imageB[(int)(imageB.Count / 2)]);
            int wallAvgColor = (wallColor.R + wallColor.G + wallColor.B) / 3; // RGB�̕��ϒl
            double wallDispersionColor = ((Math.Pow((double)(wallColor.R - wallAvgColor), 2) + Math.Pow((double)(wallColor.G - wallAvgColor), 2) + Math.Pow((double)(wallColor.B - wallAvgColor), 2)) / 3); // RGB�̕��U

            mazePathfinderLabel2.Visible = false;
            mazePathfinderLabel3.Visible = true;
            loadingNow.Visible = true;
            calculationStartButton.Visible = false;
            useBfsButton.Visible = false;
            useDfsButton.Visible = false;
            palletButton.Visible = false;
            returnStartButton.Visible = false;

            // ���x�������A�C�x���g�n���h���폜�i�X�^�[�g�n�_�A�S�[���n�_�j
            StartAndGoalPosImageEnabled(false);

            // �e�L�X�g�f�[�^�œK��
            await Task.Run(() =>
            {
                FindStartPosition(maze);
                FindGoalPosition(maze);
            });

            // ���~�����ꍇ�A�T���𒆎~����
            if (isCalculationCancel == true)
            {
                isCalculationCancel = false;
                MessageBox.Show("���H�̒T�������𒆎~���܂����B", "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            // �X�^�[�g�n�_�E�S�[���n�_��������Ȃ��ꍇ�͏I��
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

            // ���D��T���ōŒZ�o�H�����߂�
            await Task.Run(() => 
            {
                if(isUseBfs == true)
                {
                    // ���D��T��
                    MazeSolver.MazeSolverForBfs bfs = new MazeSolver.MazeSolverForBfs(maze, startPosition.X, startPosition.Y, goalPosition.X, goalPosition.Y);
                    bfs.Search();
                }
                else
                {
                    // �[���D��T��
                    MazeSolver.MazeSolverForDfs dfs = new MazeSolver.MazeSolverForDfs(maze, startPosition.X, startPosition.Y, goalPosition.X, goalPosition.Y);
                    dfs.Search();
                }
            });

            // ���~�����ꍇ�A�T���𒆎~����
            if (isCalculationCancel == true)
            {
                isCalculationCancel = false;
                MessageBox.Show("���H�̒T�������𒆎~���܂����B", "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            // ��͒����x����\��
            mazePathfinderLabel3.Visible = false;
            loadingNow.Visible = false;
            recalculationButton.Visible = true;
            returnStartButton.Visible = true;
            returnStartButton.Location = new Point(104, ClientSize.Height - 40);

            // �S�[�������ꍇ
            if (isGoaled == true)
            {
                // �ŒZ�o�H��̐F�����߂�
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

                // �o�H�̐F�̃��W�A�����擾����B�ŕp�l���g��Ȃ����R�͓����ŕp�l���������鎞�ƁA���݂��Ȃ����������邽�߁B
                Color routeColor = Color.FromArgb(255, imageR[(int)(imageR.Count / 2)], imageG[(int)(imageG.Count / 2)], imageB[(int)(imageB.Count / 2)]);
                int rootAvgColor = (routeColor.R + routeColor.G + routeColor.B) / 3; // RGB�̕��ϒl
                double rootDispersionColor = ((Math.Pow((double)(routeColor.R - rootAvgColor), 2) + Math.Pow((double)(routeColor.G - rootAvgColor), 2) + Math.Pow((double)(routeColor.B - rootAvgColor), 2)) / 3); // RGB�̕��U

                Color solvedColor;
                if (wallDispersionColor <= RGB_DISPERSION && rootDispersionColor <= RGB_DISPERSION) // �ǂƌo�H�̐F��RGB���O���[�X�P�[���ł���΁A�ŒZ�o�H��ԐF�ɂ���B
                {
                    solvedColor = Color.Red;
                }
                else if (wallDispersionColor > RGB_DISPERSION && rootDispersionColor <= RGB_DISPERSION) // �o�H�̐F���O���[�X�P�[���ŁA�ǂ̐F���O���[�X�P�[���łȂ��ꍇ�A�ǂ̕�F���ŒZ�o�H�̐F�Ƃ���B
                {
                    int sumColor = Math.Max(wallColor.R, Math.Max(wallColor.G, wallColor.B)) + Math.Min(wallColor.R, Math.Min(wallColor.G, wallColor.B));
                    solvedColor = Color.FromArgb(255, Math.Min((sumColor - wallColor.R), 200), Math.Min((sumColor - wallColor.G), 200), Math.Min((sumColor - wallColor.B), 200));
                }
                else // ��L�ȊO�̏ꍇ�͌o�H�̕�F���ŒZ�o�H�̐F�Ƃ���B
                {
                    int sumColor = Math.Max(routeColor.R, Math.Max(routeColor.G, routeColor.B)) + Math.Min(routeColor.R, Math.Min(routeColor.G, routeColor.B));
                    solvedColor = Color.FromArgb(255, Math.Min((sumColor - routeColor.R), 200), Math.Min((sumColor - routeColor.G), 200), Math.Min((sumColor - routeColor.B), 200));
                }

                // �ŒZ�o�H�Ƀy�C���g����B
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
                MessageBox.Show("�S�[���܂ł̃��[�g��������܂����I", "�T������");
            }
            else // �S�[�����Ȃ������ꍇ
            {
                calculationReject.Visible = false;
                returnStartButton.Location = new Point(154, ClientSize.Height - 40);
                MessageBox.Show("�S�[���܂ł̒T���������^�C���A�E�g�܂��̓��[�g��������܂���ł����B\r\n��͏�����ύX���܂����B�ĉ�͂��Ă��������B", "�T�����s", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return;
        }

        /// <summary>
        /// �X�^�[�g�n�_�S�[���n�_�̃A�C�R���̗L����/��L����������
        /// </summary>
        /// <param name="isEnabled">�L����/��L����</param>
        private void StartAndGoalPosImageEnabled(bool isEnabled)
        {
            startPos.Enabled = isEnabled;
            goalPos.Enabled = isEnabled;
            startPosCross.Enabled = isEnabled;
            goalPosCross.Enabled = isEnabled;
            return;
        }

        /// <summary>
        /// �T�������𒆎~����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculationReject_Click(object sender, EventArgs e)
        {
            isCalculationCancel = true;
            return;
        }

        /// <summary>
        /// ���H�摜��ۑ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageSaveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = @"MazeSolved.png";
            saveFileDialog.Filter = "PNG�`��|*.png|BMP�`��|*.bmp|JPEG�`��|*.jpeg";
            saveFileDialog.ShowDialog();
            saveFileDialog.RestoreDirectory = true;
            return;
        }

        /// <summary>
        /// �摜�ۑ��_�C�A���O���J��
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
        /// ���H���ĒT������
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
        /// ������ʂɖ߂�
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
        /// �X�^�[�g�n�_��T������
        /// </summary>
        /// <param name="Maze">���H�z��</param>
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

                        // ���~�����ꍇ�A�T���𒆎~����
                        if (isCalculationCancel == true)
                        {
                            return;
                        }
                    }
                }
            }
            if (startPosition.X != -1 && startPosition.Y != -1)
            {
                MessageBox.Show("�X�^�[�g�n�_��������܂���B\n�A�C�R���̈ʒu�𒲐����Ă��������B", "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        /// �S�[���n�_��T������
        /// </summary>
        /// <param name="Maze">���H�z��</param>
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

                        // ���~�����ꍇ�A�T���𒆎~����
                        if (isCalculationCancel == true)
                        {
                            return;
                        }
                    }
                }
            }
            if (goalPosition.X != -1 && goalPosition.Y != -1)
            {
                MessageBox.Show("�S�[���n�_��������܂���B\n�A�C�R���̈ʒu�𒲐����Ă��������B", "MazePathfinder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        /// ���D��T�����g�p����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void useBfsButton_CheckedChanged(object sender, EventArgs e)
        {
            isUseBfs = true;
            return;
        }

        /// <summary>
        /// �[���D��T�����g�p����
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
