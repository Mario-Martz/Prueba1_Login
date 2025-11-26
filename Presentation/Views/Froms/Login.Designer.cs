using Prueba1_Login.Resources.Controls;
using Prueba1_Login.Resources.Fonts_Personalizados;

namespace Prueba1_Login
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnl_login = new Panel();
            txt_Pass_Login = new P_PasswordBox();
            txt_User_Login = new P_TextBox();
            btn_inicio_Login = new p_Button();
            pnl_img_login = new Panel();
            picture_logo_login = new PictureBox();
            pnl_login.SuspendLayout();
            pnl_img_login.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picture_logo_login).BeginInit();
            SuspendLayout();
            // 
            // pnl_login
            // 
            pnl_login.BackColor = Color.FromArgb(167, 13, 42);
            pnl_login.Controls.Add(txt_Pass_Login);
            pnl_login.Controls.Add(txt_User_Login);
            pnl_login.Controls.Add(btn_inicio_Login);
            pnl_login.Dock = DockStyle.Left;
            pnl_login.Location = new Point(0, 0);
            pnl_login.Name = "pnl_login";
            pnl_login.Size = new Size(320, 292);
            pnl_login.TabIndex = 0;
            pnl_login.Paint += pnl_login_Paint;
            // 
            // txt_Pass_Login
            // 
            txt_Pass_Login.BackColor = Color.FromArgb(167, 13, 42);
            txt_Pass_Login.Font = new Font("Arial", 22F);
            txt_Pass_Login.Location = new Point(17, 124);
            txt_Pass_Login.Name = "txt_Pass_Login";
            txt_Pass_Login.Size = new Size(274, 62);
            txt_Pass_Login.TabIndex = 2;
            // 
            // txt_User_Login
            // 
            txt_User_Login.BackColor = Color.FromArgb(167, 13, 42);
            txt_User_Login.Font = new Font("Arial", 22F);
            txt_User_Login.Location = new Point(17, 37);
            txt_User_Login.Name = "txt_User_Login";
            txt_User_Login.Size = new Size(274, 62);
            txt_User_Login.TabIndex = 1;
            // 
            // btn_inicio_Login
            // 
            btn_inicio_Login.BackColor = Color.Black;
            btn_inicio_Login.BackgroundColor = Color.Black;
            btn_inicio_Login.BorderColor = Color.White;
            btn_inicio_Login.BorderRadius = 22;
            btn_inicio_Login.BorderSize = 0;
            btn_inicio_Login.FlatAppearance.BorderSize = 0;
            btn_inicio_Login.FlatStyle = FlatStyle.Flat;
            btn_inicio_Login.Font = new Font("Microsoft Sans Serif", 20F);
            btn_inicio_Login.ForeColor = Color.White;
            btn_inicio_Login.FuentePersonalizada = AppFont.MontserratRegular;
            btn_inicio_Login.Location = new Point(43, 212);
            btn_inicio_Login.Name = "btn_inicio_Login";
            btn_inicio_Login.Size = new Size(203, 48);
            btn_inicio_Login.TabIndex = 0;
            btn_inicio_Login.TamañoFuente = 20F;
            btn_inicio_Login.Text = "Inisiar sesiòn";
            btn_inicio_Login.TextColor = Color.White;
            btn_inicio_Login.UseVisualStyleBackColor = false;
            btn_inicio_Login.Click += btn_inicio_Login_Click;
            // 
            // pnl_img_login
            // 
            pnl_img_login.Controls.Add(picture_logo_login);
            pnl_img_login.Dock = DockStyle.Fill;
            pnl_img_login.Location = new Point(320, 0);
            pnl_img_login.Name = "pnl_img_login";
            pnl_img_login.Size = new Size(615, 292);
            pnl_img_login.TabIndex = 1;
            // 
            // picture_logo_login
            // 
            picture_logo_login.BackColor = Color.White;
            picture_logo_login.Dock = DockStyle.Fill;
            picture_logo_login.Image = Resources.Properties.Resources.imagotipo_Loteria_v2;
            picture_logo_login.Location = new Point(0, 0);
            picture_logo_login.Name = "picture_logo_login";
            picture_logo_login.Size = new Size(615, 292);
            picture_logo_login.SizeMode = PictureBoxSizeMode.Zoom;
            picture_logo_login.TabIndex = 0;
            picture_logo_login.TabStop = false;
            // 
            // Login
            // 
            ClientSize = new Size(935, 292);
            Controls.Add(pnl_img_login);
            Controls.Add(pnl_login);
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            pnl_login.ResumeLayout(false);
            pnl_img_login.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picture_logo_login).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnl_login;
        private P_PasswordBox txt_Pass_Login;
        private P_TextBox txt_User_Login;
        private p_Button btn_inicio_Login;
        private Panel pnl_img_login;
        private PictureBox picture_logo_login;
    }
}
