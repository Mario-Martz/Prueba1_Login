using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace Prueba1_Login.Resources.Controls
{
    public class FormTextBox : UserControl
    {
        private readonly TextBox textBox;

        private bool isFocused = false;
        private Color borderColor = Color.LightGray;
        private Color borderFocusColor = Color.DeepSkyBlue;

        private bool IsInDesignMode =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase);

        public FormTextBox()
        {
            DoubleBuffered = true;

            BackColor = Color.White;
            Size = new Size(250, 34);

            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11f),
                ForeColor = Color.Black,
                BackColor = BackColor,
                Location = new Point(12, 8),
                Width = Width - 24,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom
            };

            if (!IsInDesignMode)
            {
                try { textBox.PlaceholderText = "Ingrese texto..."; }
                catch { }
            }

            textBox.GotFocus += (s, e) => { isFocused = true; Invalidate(); };
            textBox.LostFocus += (s, e) => { isFocused = false; Invalidate(); };

            Resize += (s, e) =>
            {
                textBox.Location = new Point(12, (Height - textBox.Height) / 2);
                textBox.Width = Width - 24;
            };

            Controls.Add(textBox);
        }

        // ---------------- PROPIEDADES ------------------

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PlaceholderText
        {
            get => textBox.PlaceholderText;
            set
            {
                try { textBox.PlaceholderText = value; }
                catch { }
            }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color TextColor
        {
            get => textBox.ForeColor;
            set => textBox.ForeColor = value;
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override Font Font
        {
            get => textBox.Font;
            set => textBox.Font = value;
        }

        [Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ReadOnly
        {
            get => textBox.ReadOnly;
            set => textBox.ReadOnly = value;
        }

        [Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public HorizontalAlignment TextAlign
        {
            get => textBox.TextAlign;
            set => textBox.TextAlign = value;
        }

        [Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int MaxLength
        {
            get => textBox.MaxLength;
            set => textBox.MaxLength = value;
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderFocusColor
        {
            get => borderFocusColor;
            set { borderFocusColor = value; Invalidate(); }
        }

        // ---------------- DIBUJO DE BORDE REDONDEADO ------------------

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int radius = 18;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            Color border = isFocused ? borderFocusColor : borderColor;

            using (GraphicsPath path = RoundedRect(rect, radius))
            using (Pen pen = new Pen(border, 1.5f))
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, Math.Max(height, 34), specified);
        }
    }
}