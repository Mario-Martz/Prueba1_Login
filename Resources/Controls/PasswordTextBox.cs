using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Prueba1_Login.Resources.Controls
{
    public class PasswordTextBox : UserControl
    {
        private readonly TextBox textBox;
        private readonly PictureBox eyeIcon;

        private bool isPasswordVisible = false;
        private bool isFocused = false;

        private Color borderColor = Color.LightGray;
        private Color borderFocusColor = Color.DeepSkyBlue;

        // -------------------------
        // NUEVA PROPIEDAD LABELTEXT
        // -------------------------
        private string placeholderText = "Contraseña...";

        [Category("Appearance")]
        [Description("Texto que se muestra como placeholder en el cuadro de contraseña.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string LabelText
        {
            get => placeholderText;
            set
            {
                placeholderText = value;
                try { textBox.PlaceholderText = value; } catch { }
                Invalidate();
            }
        }

        private bool IsInDesignMode =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            Process.GetCurrentProcess().ProcessName.Equals("devenv", StringComparison.OrdinalIgnoreCase);

        public PasswordTextBox()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            Size = new Size(280, 36);

            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11f),
                ForeColor = Color.Black,
                BackColor = Color.White,
                UseSystemPasswordChar = true,
                Location = new Point(12, 9),
                Width = Width - 50,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };

            // Usa el placeholder dinámico
            if (!IsInDesignMode)
            {
                try { textBox.PlaceholderText = placeholderText; } catch { }
            }

            textBox.GotFocus += (s, e) => { isFocused = true; Invalidate(); };
            textBox.LostFocus += (s, e) => { isFocused = false; Invalidate(); };

            eyeIcon = new PictureBox
            {
                Size = new Size(24, 24),
                Location = new Point(Width - 30, 6),
                Cursor = Cursors.Hand,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            LoadEyeIcon();
            eyeIcon.Click += (s, e) => TogglePasswordVisibility();

            Controls.Add(textBox);
            Controls.Add(eyeIcon);

            Resize += (s, e) => Reposition();
        }

        private void TogglePasswordVisibility()
        {
            isPasswordVisible = !isPasswordVisible;
            textBox.UseSystemPasswordChar = !isPasswordVisible;
            LoadEyeIcon();
        }

        private void LoadEyeIcon()
        {
            if (IsInDesignMode)
            {
                eyeIcon.Image = null;
                return;
            }

            try
            {
                eyeIcon.Image = isPasswordVisible
                    ? Properties.Resources.eye_open
                    : Properties.Resources.eye_closed;
            }
            catch
            {
                eyeIcon.Image = null;
            }
        }

        private void Reposition()
        {
            textBox.Location = new Point(12, (Height - textBox.Height) / 2);
            textBox.Width = Width - 50;
            eyeIcon.Location = new Point(Width - 30, (Height - 24) / 2);
        }

        // ------ PROPIEDAD PASSWORDVALUE -----

        [Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string PasswordValue
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }

        // --------------- DIBUJO ----------------

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
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        private GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);

            path.CloseFigure();
            return path;
        }

        protected override void SetBoundsCore(int x, int y, int w, int h, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, w, Math.Max(h, 36), specified);
        }
    }
}
