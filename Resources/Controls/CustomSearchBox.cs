using System.ComponentModel;

namespace Prueba1_Login.Resources.Controls
{
    public class CustomSearchBox : UserControl
    {
        private readonly TextBox textBox;
        private readonly PictureBox searchIcon;

        // Eventos públicos
        public event EventHandler? SearchClicked;
        public event EventHandler? TextChangedRealTime;

        // Colores personalizables
        private Color borderColor = Color.LightGray;
        private Color borderFocusColor = Color.DeepSkyBlue;
        private bool isFocused = false;

        public CustomSearchBox()
        {
            DoubleBuffered = true;
            BackColor = Color.WhiteSmoke;
            Size = new Size(280, 36);
            Padding = new Padding(8);

            // ---- TextBox ----
            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11f, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.Black,
                BackColor = BackColor,
                PlaceholderText = "Buscar...",
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };

            // Eventos de texto y foco
            textBox.TextChanged += (s, e) => TextChangedRealTime?.Invoke(this, EventArgs.Empty);
            textBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchClicked?.Invoke(this, EventArgs.Empty);
                    e.SuppressKeyPress = true;
                }
            };
            textBox.GotFocus += (s, e) => { isFocused = true; Invalidate(); };
            textBox.LostFocus += (s, e) => { isFocused = false; Invalidate(); };

            // ---- Icono de búsqueda ----
            searchIcon = new PictureBox
            {
                Size = new Size(22, 22),
                SizeMode = PictureBoxSizeMode.Zoom,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent
            };

            // Imagen (usa tu recurso o ícono embebido)
            try
            {
                searchIcon.Image = Properties.Resources.search;
            }
            catch
            {
                // Ícono de fallback si no existe en recursos
                searchIcon.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (Pen pen = new Pen(Color.Gray, 2))
                    {
                        e.Graphics.DrawEllipse(pen, 4, 4, 12, 12);
                        e.Graphics.DrawLine(pen, 13, 13, 18, 18);
                    }
                };
            }

            searchIcon.Click += (s, e) => SearchClicked?.Invoke(this, EventArgs.Empty);

            // ---- Eventos de tamaño ----
            Resize += (s, e) => RepositionElements();

            // ---- Agregar controles ----
            Controls.Add(textBox);
            Controls.Add(searchIcon);

            // Posicionar correctamente
            RepositionElements();
        }

        private void RepositionElements()
        {
            textBox.Width = Width - 60;
            textBox.Location = new Point(12, (Height - textBox.Height) / 2);
            searchIcon.Location = new Point(Width - searchIcon.Width - 12, (Height - searchIcon.Height) / 2);
        }

        // ---- Propiedades públicas ----
        [Browsable(true)]
        [Category("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        public string TextValue
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        public Image? SearchIcon
        {
            get => searchIcon.Image;
            set => searchIcon.Image = value;
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        public Color BorderFocusColor
        {
            get => borderFocusColor;
            set { borderFocusColor = value; Invalidate(); }
        }

        // ---- Dibujar borde redondeado con suavizado ----
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int radius = 18;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            Color border = isFocused ? borderFocusColor : borderColor;
            using (var path = RoundedRect(rect, radius))
            {
                g.FillPath(new SolidBrush(BackColor), path);
                using (Pen pen = new Pen(border, 1.6f))
                    g.DrawPath(pen, path);
            }
        }

        private System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}