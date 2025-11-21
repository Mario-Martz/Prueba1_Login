using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Prueba1_Login.Resources.Fonts_Personalizados;

namespace Prueba1_Login.Resources.Controls
{
    public class CustomIconButton : Button
    {
        private Image? icon;
        private int borderRadius = 15;
        private int iconSize = 20;
        private int iconPadding = 10;
        private Color baseColor = Color.FromArgb(0, 123, 255);
        private Color hoverColor;
        private Color textColor = Color.White;
        private AppFont fuente = AppFont.MontserratBold;
        private float tamañoFuente = 11f; // 🟢 valor base

        [Browsable(true)]
        [Category("Custom")]
        [Description("Icono mostrado a la izquierda del texto.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image? Icon
        {
            get => icon;
            set { icon = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Custom")]
        [Description("Color base del botón.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public Color BaseColor
        {
            get => baseColor;
            set
            {
                baseColor = value;
                hoverColor = ControlPaint.Light(value, 0.2f);
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Custom")]
        [Description("Color del texto.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public Color TextColor
        {
            get => textColor;
            set { textColor = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Custom")]
        [Description("Radio de los bordes redondeados.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = Math.Max(0, value); Invalidate(); }
        }

        [Browsable(true)]
        [Category("Custom")]
        [Description("Tamaño del icono.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public int IconSize
        {
            get => iconSize;
            set { iconSize = Math.Max(10, value); Invalidate(); }
        }

        [Browsable(true)]
        [Category("Custom")]
        [Description("Espaciado horizontal entre el icono y el borde.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public int IconPadding
        {
            get => iconPadding;
            set { iconPadding = Math.Max(0, value); Invalidate(); }
        }

        [Browsable(true)]
        [Category("Estilo Tipográfico")]
        [Description("Fuente personalizada del botón.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public AppFont FuentePersonalizada
        {
            get => fuente;
            set
            {
                fuente = value;
                AplicarFuente();
            }
        }

        [Browsable(true)]
        [Category("Estilo Tipográfico")]
        [Description("Tamaño de la fuente personalizada.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public float TamañoFuente
        {
            get => tamañoFuente;
            set
            {
                tamañoFuente = Math.Max(6f, value);
                AplicarFuente();
            }
        }

        // =====================================================
        // 🧱 Constructor
        // =====================================================
        public CustomIconButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            DoubleBuffered = true;
            hoverColor = ControlPaint.Light(baseColor, 0.2f);
            ForeColor = textColor;
            BackColor = baseColor;

            AplicarFuente();
        }

        // =====================================================
        // 🔤 Método que aplica la fuente (seguro en diseño/ejecución)
        // =====================================================
        private void AplicarFuente()
        {
            try
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                {
                    // 🧱 En el diseñador usa Segoe UI para no romper la vista
                    Font = new Font("Segoe UI", tamañoFuente, FontStyle.Regular);
                }
                else
                {
                    FontManager.Initialize();
                    Font = FontManager.GetFont(fuente, tamañoFuente, FontStyle.Regular);
                }
            }
            catch
            {
                Font = new Font("Segoe UI", tamañoFuente, FontStyle.Regular);
            }
            Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode)
                AplicarFuente();

            if (Parent != null)
                Parent.BackColorChanged += (s, ev) => Invalidate();
        }

        // =====================================================
        // 🎨 Dibujo visual
        // =====================================================
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectSurface = new Rectangle(0, 0, Width - 1, Height - 1);

            using (GraphicsPath pathSurface = GetRoundedPath(rectSurface, borderRadius))
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                Region = new Region(pathSurface);
                e.Graphics.FillPath(brush, pathSurface);
            }

            using (Pen pen = new Pen(Color.FromArgb(50, 0, 0, 0), 1f))
            {
                pen.Alignment = PenAlignment.Inset;
                e.Graphics.DrawPath(pen, GetRoundedPath(rectSurface, borderRadius));
            }

            DrawContent(e.Graphics);
        }

        private void DrawContent(Graphics g)
        {
            // 🔹 Icono
            if (icon != null)
            {
                int iconY = (Height - iconSize) / 2;
                g.DrawImage(icon, new Rectangle(iconPadding, iconY, iconSize, iconSize));
            }

            // 🔹 Texto
            using (SolidBrush brushText = new SolidBrush(textColor))
            {
                SizeF textSize = g.MeasureString(Text, Font);
                float textX = icon != null ? iconPadding + iconSize + 8 : 10;
                float textY = (Height - textSize.Height) / 2;
                g.DrawString(Text, Font, brushText, textX, textY);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            int curveSize = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            BackColor = hoverColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            BackColor = baseColor;
        }
    }
}
