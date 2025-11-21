using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Prueba1_Login.Resources.Fonts_Personalizados;

namespace Prueba1_Login.Resources.Controls
{
    public class P_PasswordBox : UserControl
    {
        // ============================================================
        // 🔹 Campos privados
        // ============================================================
        private TextBox textBox;
        private Label labelError;
        private PictureBox iconWarning;
        private PictureBox iconEye;

        private Color borderColor = Color.White;
        private Color errorColor = Color.Gold;
        private int borderSize = 2;
        private bool isPasswordVisible = false;

        private AppFont fuente = AppFont.MontserratRegular;

        // ============================================================
        // 🔹 Propiedades públicas
        // ============================================================
        [Category("P_PasswordBox")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ErrorText
        {
            get => labelError.Text;
            set => labelError.Text = value;
        }

        [Category("P_PasswordBox")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AppFont FuentePersonalizada
        {
            get => fuente;
            set
            {
                fuente = value;
                if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                {
                    textBox.Font = FontManager.GetFont(fuente, 16);
                    labelError.Font = FontManager.GetFont(fuente, 12);
                }
            }
        }

        [Category("P_PasswordBox")]
        public override string? Text
        {
            get => textBox.Text;
            set => textBox.Text = value ?? string.Empty;
        }

        // ============================================================
        // 🧱 Constructor
        // ============================================================
        public P_PasswordBox()
        {
            // Carga la fuente solo en ejecución
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                FontManager.Initialize();

            BackColor = Color.FromArgb(167, 13, 42);
            Size = new Size(400, 70);

            // TextBox
            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                ForeColor = Color.White,
                BackColor = BackColor,
                UseSystemPasswordChar = true,
                Font = LicenseManager.UsageMode != LicenseUsageMode.Designtime
                        ? FontManager.GetFont(fuente, 16)
                        : new Font("Segoe UI", 14),
                Location = new Point(0, 5),
                Width = Width - 60
            };

            // Label de error
            labelError = new Label
            {
                ForeColor = errorColor,
                Font = LicenseManager.UsageMode != LicenseUsageMode.Designtime
                        ? FontManager.GetFont(fuente, 12)
                        : new Font("Segoe UI", 10),
                AutoSize = true,
                Visible = false,
                Location = new Point(0, 35)
            };

            // Icono de advertencia
            iconWarning = new PictureBox
            {
                Size = new Size(20, 20),
                Location = new Point(Width - 50, 5),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Visible = false
            };

            // Icono de ojo (mostrar/ocultar contraseña)
            iconEye = new PictureBox
            {
                Size = new Size(20, 20),
                Location = new Point(Width - 25, 5),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.Hand
            };

            // ✅ Carga segura de imágenes
            try
            {
                if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                {
                    iconWarning.Image = Properties.Resources.warning_icon;
                    iconEye.Image = Properties.Resources.eye_closed;
                }
            }
            catch
            {
                // En diseñador no cargamos imágenes
                iconWarning.Image = null;
                iconEye.Image = null;
            }

            // Agregar controles
            Controls.Add(textBox);
            Controls.Add(labelError);
            Controls.Add(iconWarning);
            Controls.Add(iconEye);

            // Eventos
            textBox.TextChanged += (s, e) =>
            {
                HideError();
                OnTextChanged(e);
            };

            iconEye.Click += TogglePasswordVisibility;
            Resize += (s, e) => AdjustLayout();
        }

        // ============================================================
        // 🎨 Métodos visuales
        // ============================================================
        private void AdjustLayout()
        {
            textBox.Width = Width - 60;
            iconWarning.Location = new Point(Width - 50, 5);
            iconEye.Location = new Point(Width - 25, 5);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen pen = new Pen(borderColor, borderSize))
            {
                e.Graphics.DrawLine(pen, 0, textBox.Bottom + 5, Width, textBox.Bottom + 5);
            }
        }

        // ============================================================
        // 👁️ Mostrar / Ocultar contraseña
        // ============================================================
        private void TogglePasswordVisibility(object? sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            textBox.UseSystemPasswordChar = !isPasswordVisible;

            try
            {
                iconEye.Image = isPasswordVisible
                    ? Properties.Resources.eye_open
                    : Properties.Resources.eye_closed;
            }
            catch
            {
                iconEye.Image = null;
            }
        }

        // ============================================================
        // ⚠️ Métodos de error
        // ============================================================
        public void ShowError(string message)
        {
            labelError.Text = message;
            labelError.Visible = true;
            iconWarning.Visible = true;
            Invalidate();
        }

        private void InitializeComponent()
        {

        }

        public void HideError()
        {
            labelError.Visible = false;
            iconWarning.Visible = false;
            Invalidate();
        }
    }
}
