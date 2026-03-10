using System.Drawing;
using System.Windows.Forms;

namespace SelectMaster
{
    /// <summary>
    /// 单行显示且自动省略号的 Label，不会出现自动换行。
    /// </summary>
    public class SingleLineLabel : Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            // 不调用 base.OnPaint，以完全自绘文本，避免自动换行
            e.Graphics.Clear(BackColor);

            var flags = TextFormatFlags.EndEllipsis
                        | TextFormatFlags.SingleLine
                        | TextFormatFlags.NoPadding
                        | TextFormatFlags.VerticalCenter;

            // 水平对齐
            switch (TextAlign)
            {
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.TopCenter:
                case ContentAlignment.BottomCenter:
                    flags |= TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.MiddleRight:
                case ContentAlignment.TopRight:
                case ContentAlignment.BottomRight:
                    flags |= TextFormatFlags.Right;
                    break;
                default:
                    flags |= TextFormatFlags.Left;
                    break;
            }

            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                ClientRectangle,
                Enabled ? ForeColor : SystemColors.GrayText,
                BackColor,
                flags);
        }
    }
}
