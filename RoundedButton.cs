using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SelectMaster
{
    /// <summary>圆角按钮，可设置圆角半径与扁平样式。</summary>
    public class RoundedButton : Button
    {
        private int _cornerRadius = 8;

        public RoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
        }

        /// <summary>圆角半径（像素）</summary>
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; UpdateRegion(); Invalidate(); }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateRegion();
        }

        private static GraphicsPath CreateRoundedPath(RectangleF rect, int radius)
        {
            var path = new GraphicsPath();
            if (radius <= 0) { path.AddRectangle(rect); return path; }
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }

        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            var rect = new RectangleF(0, 0, Width, Height);
            using (var path = CreateRoundedPath(rect, _cornerRadius))
                Region = new Region(path);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new RectangleF(0, 0, Width - 1, Height - 1);
            using (var path = CreateRoundedPath(rect, _cornerRadius))
            {
                // 背景
                using (var brush = new SolidBrush(BackColor))
                    e.Graphics.FillPath(brush, path);

                // 边框
                if (FlatAppearance.BorderSize > 0)
                {
                    using (var pen = new Pen(FlatAppearance.BorderColor, FlatAppearance.BorderSize))
                    {
                        pen.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawPath(pen, path);
                    }
                }

                // 文字
                var textRect = new Rectangle(0, 0, Width, Height);
                TextRenderer.DrawText(e.Graphics, Text, Font, textRect, ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            }
        }
    }
}
