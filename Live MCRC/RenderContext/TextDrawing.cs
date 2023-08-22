using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Live_MCRC.RenderContext
{
    public class TextDrawing : Label
    {
        public TextDrawing()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            AutoSize = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(takeComponentScreenShot(this.Parent), 0, 0);

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            using (Brush brush = new SolidBrush(ForeColor))
            {
                e.Graphics.DrawString(Text, Font, brush, ClientRectangle);
            }
        }

        // https://stackoverflow.com/questions/4639482/how-to-do-a-background-for-a-label-will-be-without-color
        private Bitmap takeComponentScreenShot(Control control)
        {
            Rectangle rect = control.RectangleToScreen(this.Bounds);
            if (rect.Width == 0 || rect.Height == 0)
            {
                return null;
            }
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);

            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

            return bmp;
        }
    }
}