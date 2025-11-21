using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Prueba1_Login.Resources.Fonts_Personalizados;

namespace Prueba1_Login.Resources.Controls
{
    public class P_TextBox : UserControl
    {
        // ============================================================
        // 🔹 Campos privados
        // ============================================================
        private TextBox textBox;
        private Label labelError;
        private PictureBox iconWarning;

        private Color borderColor = Color.White;
        private Color errorColor = Color.Gold;
        private int borderSize = 2;
        private bool isPassword = false;

        private AppFont fuente = AppFont.MontserratRegular;

        // ============================================================
        // 🔹 Propiedades públicas
        // ============================================================
        [Category("P_TextBox")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PlaceholderText
        {
            get => textBox.PlaceholderText;
            set => textBox.PlaceholderText = value;
        }

        [Category("P_TextBox")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ErrorText
        {
            get => labelError.Text;
            set => labelError.Text = value;
        }

        [Category("P_TextBox")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                isPassword = value;
                textBox.UseSystemPasswordChar = value;
            }
        }

        [Category("P_TextBox")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AppFont FuentePersonalizada
        {
            get => fuente;
            set
            {
                fuente = value;
                if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                {
                    textBox.Font = FontManager.GetFont(fuente, 18f);
                    labelError.Font = FontManager.GetFont(fuente, 16f);
                }
            }
        }

        [Category("P_TextBox")]
        public override string? Text
        {
            get => textBox.Text;
            set => textBox.Text = value ?? string.Empty;
        }

        // ============================================================
        // 🧱 Constructor
        // ============================================================
        public P_TextBox()
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                FontManager.Initialize();

            BackColor = Color.FromArgb(167, 13, 42); // Rojo vino
            Size = new Size(400, 70);

            // 🔹 TextBox principal
            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                ForeColor = Color.White,
                BackColor = BackColor,
                Font = LicenseManager.UsageMode != LicenseUsageMode.Designtime
                        ? FontManager.GetFont(fuente, 18f)
                        : new Font("Segoe UI", 16),
                Location = new Point(0, 5),
                Width = Width - 35
            };

            // 🔹 Label de error
            labelError = new Label
            {
                ForeColor = errorColor,
                Font = LicenseManager.UsageMode != LicenseUsageMode.Designtime
                        ? FontManager.GetFont(fuente, 16f)
                        : new Font("Segoe UI", 12),
                AutoSize = true,
                Visible = false,
                Location = new Point(0, 40)
            };

            // 🔹 Icono de advertencia
            iconWarning = new PictureBox
            {
                Size = new Size(20, 20),
                Location = new Point(Width - 25, 8),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Visible = false
            };

            try
            {
                if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                    iconWarning.Image = Properties.Resources.warning_icon;
            }
            catch
            {
                iconWarning.Image = null;
            }

            Controls.Add(textBox);
            Controls.Add(labelError);
            Controls.Add(iconWarning);

            textBox.TextChanged += (s, e) =>
            {
                HideError();
                OnTextChanged(e);
            };

            Resize += (s, e) => AdjustLayout();
        }

        // ============================================================
        // 🎨 Métodos visuales
        // ============================================================
        private void AdjustLayout()
        {
            textBox.Width = Width - 35;
            iconWarning.Location = new Point(Width - 25, 8);
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
        // ⚠️ Métodos de error
        // ============================================================
        public void ShowError(string message)
        {
            labelError.Text = message;
            labelError.Visible = true;
            iconWarning.Visible = true;
            Invalidate();
        }

        public void HideError()
        {
            labelError.Visible = false;
            iconWarning.Visible = false;
            Invalidate();
        }
    }
}
