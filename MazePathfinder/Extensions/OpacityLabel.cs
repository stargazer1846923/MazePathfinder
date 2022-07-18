namespace MazePathfinder.Extensions
{
    /// <summary>
    /// LabelのOnPaintをオーバーライドしてOpacity(透明化)プロパティを追加したクラスです
    /// </summary>
    public class OpacityLabel : Label
    {
        public double Opacity{set; get;}

        protected override void OnPaint(PaintEventArgs e)
        {
            Brush br = new SolidBrush(Color.FromArgb((int)(255.0*Opacity),this.ForeColor));
            e.Graphics.DrawString(this.Text, this.Font, br, new Point(0,0));
        }
    }
}
