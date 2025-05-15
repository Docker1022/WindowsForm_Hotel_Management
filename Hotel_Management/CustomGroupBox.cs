using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CustomGradientGroupBox : GroupBox
{
    public Color[] BorderColors { get; set; } = new Color[]
    {
        Color.FromArgb(185, 131, 255), // #B983FF
        Color.FromArgb(148, 179, 253), // #94B3FD
        Color.FromArgb(148, 218, 255), // #94DAFF
        Color.FromArgb(153, 254, 255)  // #99FEFF
    };

    public int BorderRadius { get; set; } = 12;
    public int BorderThickness { get; set; } = 2;
    public float GradientAngle { get; set; } = 45f;

    private Timer hoverTimer;
    private bool isHovering = false;
    private int borderAlpha = 100;

    public CustomGradientGroupBox()
    {
        this.DoubleBuffered = true;
        this.BackColor = Color.Transparent;
        this.ForeColor = Color.FromArgb(60, 60, 60);
        this.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        this.Padding = new Padding(10);

        this.hoverTimer = new Timer();
        this.hoverTimer.Interval = 15;
        this.hoverTimer.Tick += HoverTimer_Tick;

        this.MouseEnter += (s, e) => { isHovering = true; hoverTimer.Start(); };
        this.MouseLeave += (s, e) => { isHovering = false; hoverTimer.Start(); };
    }

    private void HoverTimer_Tick(object sender, EventArgs e)
    {
        if (isHovering)
        {
            borderAlpha = Math.Min(255, borderAlpha + 10);
        }
        else
        {
            borderAlpha = Math.Max(100, borderAlpha - 10);
        }

        this.Invalidate();

        // Dừng timer khi đạt giới hạn
        if ((isHovering && borderAlpha == 255) || (!isHovering && borderAlpha == 100))
        {
            hoverTimer.Stop();
        }
    }


    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle rect = this.ClientRectangle;
        rect.Inflate(-1, -1);

        // Fill background with more transparent white
        using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(60, 255, 255, 255)))
        {
            FillRoundedRectangle(e.Graphics, bgBrush, rect, BorderRadius);
        }

        // Create gradient border with current alpha
        using (GraphicsPath path = RoundedRect(rect, BorderRadius))
        using (LinearGradientBrush brush = new LinearGradientBrush(rect, BorderColors[0], BorderColors[BorderColors.Length - 1], GradientAngle))
        {
            ColorBlend blend = new ColorBlend
            {
                Colors = new Color[]
                {
                    Color.FromArgb(borderAlpha, BorderColors[0]),
                    Color.FromArgb(borderAlpha, BorderColors[1]),
                    Color.FromArgb(borderAlpha, BorderColors[2]),
                    Color.FromArgb(borderAlpha, BorderColors[3])
                },
                Positions = new float[] { 0f, 0.33f, 0.66f, 1f }
            };
            brush.InterpolationColors = blend;

            using (Pen pen = new Pen(brush, BorderThickness))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        // Draw text
        SizeF stringSize = e.Graphics.MeasureString(this.Text, this.Font);
        Rectangle textRect = new Rectangle(12, 0, (int)stringSize.Width + 4, (int)stringSize.Height);

        using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
        {
            e.Graphics.DrawString(this.Text, this.Font, textBrush, textRect);
        }
    }

    private GraphicsPath RoundedRect(Rectangle bounds, int radius)
    {
        int d = radius * 2;
        GraphicsPath path = new GraphicsPath();
        path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
        path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
        path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        return path;
    }

    private void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle bounds, int radius)
    {
        using (GraphicsPath path = RoundedRect(bounds, radius))
        {
            graphics.FillPath(brush, path);
        }
    }
}
