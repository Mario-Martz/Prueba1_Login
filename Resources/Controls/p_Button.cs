using Prueba1_Login.Resources.Fonts_Personalizados;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Prueba1_Login.Resources.Controls
{
    public class p_Button : Button
    {
        private int borderSize = 0;
        private int borderRadius = 22;
        private Color borderColor = Color.White;
        private AppFont fuente = AppFont.MontserratRegular;

        [Category("Diseño Personalizado")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public int BorderSize
        {
            get => borderSize;
            set { borderSize = value; Invalidate(); }
        }

        [Category("Diseño Personalizado")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; Invalidate(); }
        }

        [Category("Diseño Personalizado")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }

        [Category("Diseño Personalizado")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public Color BackgroundColor
        {
            get => BackColor;
            set => BackColor = value;
        }

        [Category("Diseño Personalizado")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public Color TextColor
        {
            get => ForeColor;
            set => ForeColor = value;
        }

        [Category("Estilo Tipográfico")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public AppFont FuentePersonalizada
        {
            get => fuente;
            set
            {
                fuente = value;
                if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                    Font = FontManager.GetFont(fuente, Font.Size, FontStyle.Regular);
                Invalidate();
            }
        }

        [Category("Estilo Tipográfico")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public float TamañoFuente
        {
            get => Font.Size;
            set
            {
                if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                    Font = FontManager.GetFont(fuente, value, Font.Style);
                else
                    Font = new Font(Font.FontFamily, value, Font.Style);
                Invalidate();
            }
        }

        // 🧱 Constructor
        public p_Button()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Size = new Size(150, 40);
            BackColor = Color.MediumSlateBlue;
            ForeColor = Color.White;
            DoubleBuffered = true;

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                try
                {
                    FontManager.Initialize();
                    Font = FontManager.GetFont(fuente, 22f, FontStyle.Regular);
                }
                catch
                {
                    Font = new Font("Segoe UI", 12f, FontStyle.Regular);
                }
            }
            else
            {
                Font = new Font("Segoe UI", 12f, FontStyle.Regular);
            }

            Resize += (s, e) =>
            {
                if (borderRadius > Height)
                    borderRadius = Height;
            };
        }

        // 🟢 Este método ahora combina ambos anteriores correctamente
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (!DesignMode)
            {
                try
                {
                    Font = FontManager.GetFont(fuente, Font.Size, FontStyle.Regular);
                }
                catch
                {
                    Font = new Font("Segoe UI", 12f, FontStyle.Regular);
                }
            }

            if (Parent != null)
                Parent.BackColorChanged += (s, ev) => Invalidate();
        }

        // 🎨 Dibujo del botón
        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectSurface = ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);

            if (borderRadius > 2)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(Parent?.BackColor ?? Color.White, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    Region = new Region(pathSurface);
                    pevent.Graphics.DrawPath(penSurface, pathSurface);
                    if (borderSize >= 1)
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;
                Region = new Region(rectSurface);
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, Width - 1, Height - 1);
                    }
                }
            }

            base.OnPaint(pevent);
        }

        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
        {
            GraphicsPath path = new();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
