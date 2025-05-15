using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class DreamyGlassPanel : Panel
{
    public int CornerRadius { get; set; } = 20; // Bo góc mặc định 20
    public Color GradientTopColor { get; set; } = ColorTranslator.FromHtml("#B983FF");   // Tím pastel (top)
    public Color GradientBottomColor { get; set; } = ColorTranslator.FromHtml("#94DAFF"); // Xanh lam nhẹ (bottom)
    public float Opacity { get; set; } = 0.4f; // Độ mờ nhẹ (0-1)

    public DreamyGlassPanel()
    {
        this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                      ControlStyles.UserPaint |
                      ControlStyles.ResizeRedraw |
                      ControlStyles.OptimizedDoubleBuffer, true);
        this.BackColor = Color.Transparent;  // Để panel trong suốt nền
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rect = this.ClientRectangle;
        rect.Inflate(-1, -1);

        // Bo góc
        GraphicsPath path = RoundedRect(rect, CornerRadius);

        // Gradient nền (Top → Bottom, ảo ảo thơ mộng)
        using (LinearGradientBrush brush = new LinearGradientBrush(rect, GradientTopColor, GradientBottomColor, LinearGradientMode.Vertical))
        {
            ColorBlend blend = new ColorBlend(3);
            blend.Colors = new Color[] {
                Color.FromArgb((int)(Opacity * 255), GradientTopColor),
                Color.FromArgb((int)(Opacity * 255), Color.White),
                Color.FromArgb((int)(Opacity * 255), GradientBottomColor)
            };
            blend.Positions = new float[] { 0f, 0.5f, 1f };
            brush.InterpolationColors = blend;

            g.FillPath(brush, path);
        }

        // Viền xanh sáng pastel (theo palette)
        using (Pen borderPen = new Pen(ColorTranslator.FromHtml("#99FEFF"), 2))
        {
            g.DrawPath(borderPen, path);
        }

        path.Dispose();
    }

    private GraphicsPath RoundedRect(Rectangle bounds, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;

        Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
        path.AddArc(arc, 180, 90);

        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270, 90);

        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0, 90);

        arc.X = bounds.Left;
        path.AddArc(arc, 90, 90);

        path.CloseFigure();
        return path;
    }
}

