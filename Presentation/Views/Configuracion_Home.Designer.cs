using Prueba1_Login.Resources.Controls;
using Prueba1_Login.Resources.Fonts_Personalizados;

namespace Prueba1_Login.Views
{
    partial class Configuracion_Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tab_ModificarU = new TabPage();
            Box_Edit_Repit_password = new PasswordTextBox();
            comboBox_Mod_Perfil = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            btn_cancelEditUser = new CustomIconButton();
            btn_Modi_User = new CustomIconButton();
            TextBoxMod_Apell_M = new FormTextBox();
            TextBoxMod_Apell_P = new FormTextBox();
            Box_Edit_password = new PasswordTextBox();
            Box_Edit_password_after = new PasswordTextBox();
            TextBoxMod_Nombre = new FormTextBox();
            tab_CrearUusuario = new TabPage();
            comboBox_Pefl_Users = new ComboBox();
            cancelar_CretUser = new CustomIconButton();
            add_user_CreateU = new CustomIconButton();
            txtCre__Apell_M = new FormTextBox();
            txtCre__Apell_P = new FormTextBox();
            txtCre_Repit_password = new PasswordTextBox();
            txtCre_password = new PasswordTextBox();
            txtCre_Nombre = new FormTextBox();
            label_fechaCreacion = new Label();
            label_Crear_Uusuario = new Label();
            tab_PanelAdministracionU = new TabPage();
            btn_Crear = new p_Button();
            userTable = new Prueba1_Login.Presentation.Components.UserTableControl();
            tab_Parametros = new TabPage();
            tabConfiguraciones = new TabControl();
            tab_ModificarU.SuspendLayout();
            tab_CrearUusuario.SuspendLayout();
            tab_PanelAdministracionU.SuspendLayout();
            tabConfiguraciones.SuspendLayout();
            SuspendLayout();
            // 
            // tab_ModificarU
            // 
            tab_ModificarU.Controls.Add(Box_Edit_Repit_password);
            tab_ModificarU.Controls.Add(comboBox_Mod_Perfil);
            tab_ModificarU.Controls.Add(label2);
            tab_ModificarU.Controls.Add(label1);
            tab_ModificarU.Controls.Add(btn_cancelEditUser);
            tab_ModificarU.Controls.Add(btn_Modi_User);
            tab_ModificarU.Controls.Add(TextBoxMod_Apell_M);
            tab_ModificarU.Controls.Add(TextBoxMod_Apell_P);
            tab_ModificarU.Controls.Add(Box_Edit_password);
            tab_ModificarU.Controls.Add(Box_Edit_password_after);
            tab_ModificarU.Controls.Add(TextBoxMod_Nombre);
            tab_ModificarU.Location = new Point(4, 24);
            tab_ModificarU.Name = "tab_ModificarU";
            tab_ModificarU.Size = new Size(990, 594);
            tab_ModificarU.TabIndex = 3;
            tab_ModificarU.Text = "Modificar Usuario";
            tab_ModificarU.UseVisualStyleBackColor = true;
            // 
            // Box_Edit_Repit_password
            // 
            Box_Edit_Repit_password.BackColor = Color.White;
            Box_Edit_Repit_password.LabelText = "Repetir Contraseña...";
            Box_Edit_Repit_password.Location = new Point(60, 480);
            Box_Edit_Repit_password.Name = "Box_Edit_Repit_password";
            Box_Edit_Repit_password.PasswordValue = "";
            Box_Edit_Repit_password.Size = new Size(345, 36);
            Box_Edit_Repit_password.TabIndex = 31;
            // 
            // comboBox_Mod_Perfil
            // 
            comboBox_Mod_Perfil.FormattingEnabled = true;
            comboBox_Mod_Perfil.Location = new Point(530, 130);
            comboBox_Mod_Perfil.Name = "comboBox_Mod_Perfil";
            comboBox_Mod_Perfil.Size = new Size(150, 23);
            comboBox_Mod_Perfil.TabIndex = 30;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Georgia", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(530, 200);
            label2.Name = "label2";
            label2.Size = new Size(288, 29);
            label2.TabIndex = 29;
            label2.Text = "Fecha de modificacion";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI Semibold", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(318, 50);
            label1.TabIndex = 28;
            label1.Text = "Modificar usuario";
            // 
            // btn_cancelEditUser
            // 
            btn_cancelEditUser.BackColor = Color.Red;
            btn_cancelEditUser.BaseColor = Color.Red;
            btn_cancelEditUser.BorderRadius = 10;
            btn_cancelEditUser.FlatAppearance.BorderSize = 0;
            btn_cancelEditUser.FlatStyle = FlatStyle.Flat;
            btn_cancelEditUser.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_cancelEditUser.ForeColor = Color.White;
            btn_cancelEditUser.FuentePersonalizada = AppFont.MontserratBold;
            btn_cancelEditUser.Icon = Resources.Properties.Resources.cancel;
            btn_cancelEditUser.IconPadding = 10;
            btn_cancelEditUser.IconSize = 20;
            btn_cancelEditUser.Location = new Point(850, 13);
            btn_cancelEditUser.Name = "btn_cancelEditUser";
            btn_cancelEditUser.Size = new Size(120, 40);
            btn_cancelEditUser.TabIndex = 27;
            btn_cancelEditUser.TamañoFuente = 11F;
            btn_cancelEditUser.Text = "Cancelar";
            btn_cancelEditUser.TextColor = Color.White;
            btn_cancelEditUser.UseVisualStyleBackColor = false;
            // 
            // btn_Modi_User
            // 
            btn_Modi_User.BackColor = Color.FromArgb(0, 123, 255);
            btn_Modi_User.BaseColor = Color.FromArgb(0, 123, 255);
            btn_Modi_User.BorderRadius = 10;
            btn_Modi_User.FlatAppearance.BorderSize = 0;
            btn_Modi_User.FlatStyle = FlatStyle.Flat;
            btn_Modi_User.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_Modi_User.ForeColor = Color.White;
            btn_Modi_User.FuentePersonalizada = AppFont.MontserratBold;
            btn_Modi_User.Icon = Resources.Properties.Resources.edit;
            btn_Modi_User.IconPadding = 10;
            btn_Modi_User.IconSize = 20;
            btn_Modi_User.Location = new Point(831, 502);
            btn_Modi_User.Name = "btn_Modi_User";
            btn_Modi_User.Size = new Size(139, 47);
            btn_Modi_User.TabIndex = 26;
            btn_Modi_User.TamañoFuente = 11F;
            btn_Modi_User.Text = "Modificar Usuario";
            btn_Modi_User.TextColor = Color.White;
            btn_Modi_User.UseVisualStyleBackColor = false;
            btn_Modi_User.Click += btn_Modi_User_Click;
            // 
            // TextBoxMod_Apell_M
            // 
            TextBoxMod_Apell_M.BackColor = Color.White;
            TextBoxMod_Apell_M.BorderColor = Color.LightGray;
            TextBoxMod_Apell_M.BorderFocusColor = Color.DeepSkyBlue;
            TextBoxMod_Apell_M.Location = new Point(60, 270);
            TextBoxMod_Apell_M.MaxLength = 32767;
            TextBoxMod_Apell_M.Name = "TextBoxMod_Apell_M";
            TextBoxMod_Apell_M.PlaceholderText = "Apellido Materno...";
            TextBoxMod_Apell_M.ReadOnly = false;
            TextBoxMod_Apell_M.Size = new Size(345, 40);
            TextBoxMod_Apell_M.TabIndex = 25;
            TextBoxMod_Apell_M.TextAlign = HorizontalAlignment.Left;
            TextBoxMod_Apell_M.TextColor = Color.Black;
            // 
            // TextBoxMod_Apell_P
            // 
            TextBoxMod_Apell_P.BackColor = Color.White;
            TextBoxMod_Apell_P.BorderColor = Color.LightGray;
            TextBoxMod_Apell_P.BorderFocusColor = Color.DeepSkyBlue;
            TextBoxMod_Apell_P.Location = new Point(60, 200);
            TextBoxMod_Apell_P.MaxLength = 32767;
            TextBoxMod_Apell_P.Name = "TextBoxMod_Apell_P";
            TextBoxMod_Apell_P.PlaceholderText = "Apellido Paterno...";
            TextBoxMod_Apell_P.ReadOnly = false;
            TextBoxMod_Apell_P.Size = new Size(345, 40);
            TextBoxMod_Apell_P.TabIndex = 24;
            TextBoxMod_Apell_P.TextAlign = HorizontalAlignment.Left;
            TextBoxMod_Apell_P.TextColor = Color.Black;
            // 
            // Box_Edit_password
            // 
            Box_Edit_password.BackColor = Color.White;
            Box_Edit_password.LabelText = "Nueva Contraseña...";
            Box_Edit_password.Location = new Point(60, 410);
            Box_Edit_password.Name = "Box_Edit_password";
            Box_Edit_password.PasswordValue = "";
            Box_Edit_password.Size = new Size(345, 36);
            Box_Edit_password.TabIndex = 23;
            // 
            // Box_Edit_password_after
            // 
            Box_Edit_password_after.BackColor = Color.White;
            Box_Edit_password_after.LabelText = "Contraseña anterior...";
            Box_Edit_password_after.Location = new Point(60, 340);
            Box_Edit_password_after.Name = "Box_Edit_password_after";
            Box_Edit_password_after.PasswordValue = "";
            Box_Edit_password_after.Size = new Size(345, 36);
            Box_Edit_password_after.TabIndex = 22;
            // 
            // TextBoxMod_Nombre
            // 
            TextBoxMod_Nombre.BackColor = Color.White;
            TextBoxMod_Nombre.BorderColor = Color.LightGray;
            TextBoxMod_Nombre.BorderFocusColor = Color.DeepSkyBlue;
            TextBoxMod_Nombre.Location = new Point(60, 130);
            TextBoxMod_Nombre.MaxLength = 30000;
            TextBoxMod_Nombre.Name = "TextBoxMod_Nombre";
            TextBoxMod_Nombre.PlaceholderText = "Nombre";
            TextBoxMod_Nombre.ReadOnly = false;
            TextBoxMod_Nombre.Size = new Size(345, 40);
            TextBoxMod_Nombre.TabIndex = 21;
            TextBoxMod_Nombre.TextAlign = HorizontalAlignment.Left;
            TextBoxMod_Nombre.TextColor = Color.Black;
            // 
            // tab_CrearUusuario
            // 
            tab_CrearUusuario.Controls.Add(comboBox_Pefl_Users);
            tab_CrearUusuario.Controls.Add(cancelar_CretUser);
            tab_CrearUusuario.Controls.Add(add_user_CreateU);
            tab_CrearUusuario.Controls.Add(txtCre__Apell_M);
            tab_CrearUusuario.Controls.Add(txtCre__Apell_P);
            tab_CrearUusuario.Controls.Add(txtCre_Repit_password);
            tab_CrearUusuario.Controls.Add(txtCre_password);
            tab_CrearUusuario.Controls.Add(txtCre_Nombre);
            tab_CrearUusuario.Controls.Add(label_fechaCreacion);
            tab_CrearUusuario.Controls.Add(label_Crear_Uusuario);
            tab_CrearUusuario.Location = new Point(4, 24);
            tab_CrearUusuario.Name = "tab_CrearUusuario";
            tab_CrearUusuario.Size = new Size(990, 594);
            tab_CrearUusuario.TabIndex = 2;
            tab_CrearUusuario.Text = "Crear usuario";
            tab_CrearUusuario.UseVisualStyleBackColor = true;
            // 
            // comboBox_Pefl_Users
            // 
            comboBox_Pefl_Users.FormattingEnabled = true;
            comboBox_Pefl_Users.Location = new Point(530, 130);
            comboBox_Pefl_Users.Name = "comboBox_Pefl_Users";
            comboBox_Pefl_Users.Size = new Size(150, 23);
            comboBox_Pefl_Users.TabIndex = 23;
            // 
            // cancelar_CretUser
            // 
            cancelar_CretUser.BackColor = Color.FromArgb(192, 0, 0);
            cancelar_CretUser.BaseColor = Color.FromArgb(192, 0, 0);
            cancelar_CretUser.BorderRadius = 10;
            cancelar_CretUser.FlatAppearance.BorderSize = 0;
            cancelar_CretUser.FlatStyle = FlatStyle.Flat;
            cancelar_CretUser.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cancelar_CretUser.ForeColor = Color.White;
            cancelar_CretUser.FuentePersonalizada = AppFont.MontserratBold;
            cancelar_CretUser.Icon = Resources.Properties.Resources.cancel;
            cancelar_CretUser.IconPadding = 10;
            cancelar_CretUser.IconSize = 20;
            cancelar_CretUser.Location = new Point(850, 13);
            cancelar_CretUser.Name = "cancelar_CretUser";
            cancelar_CretUser.Size = new Size(120, 40);
            cancelar_CretUser.TabIndex = 22;
            cancelar_CretUser.TamañoFuente = 11F;
            cancelar_CretUser.Text = "Cancelar";
            cancelar_CretUser.TextColor = Color.White;
            cancelar_CretUser.UseVisualStyleBackColor = false;
            // 
            // add_user_CreateU
            // 
            add_user_CreateU.BackColor = Color.FromArgb(0, 123, 255);
            add_user_CreateU.BaseColor = Color.FromArgb(0, 123, 255);
            add_user_CreateU.BorderRadius = 10;
            add_user_CreateU.FlatAppearance.BorderSize = 0;
            add_user_CreateU.FlatStyle = FlatStyle.Flat;
            add_user_CreateU.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            add_user_CreateU.ForeColor = Color.White;
            add_user_CreateU.FuentePersonalizada = AppFont.MontserratBold;
            add_user_CreateU.Icon = Resources.Properties.Resources.add;
            add_user_CreateU.IconPadding = 10;
            add_user_CreateU.IconSize = 20;
            add_user_CreateU.Location = new Point(850, 500);
            add_user_CreateU.Name = "add_user_CreateU";
            add_user_CreateU.Size = new Size(120, 40);
            add_user_CreateU.TabIndex = 21;
            add_user_CreateU.TamañoFuente = 11F;
            add_user_CreateU.Text = "Crear usuario";
            add_user_CreateU.TextColor = Color.White;
            add_user_CreateU.UseVisualStyleBackColor = false;
            add_user_CreateU.Click += add_user_CreateU_Click;
            // 
            // txtCre__Apell_M
            // 
            txtCre__Apell_M.BackColor = Color.White;
            txtCre__Apell_M.BorderColor = Color.LightGray;
            txtCre__Apell_M.BorderFocusColor = Color.DeepSkyBlue;
            txtCre__Apell_M.Location = new Point(60, 270);
            txtCre__Apell_M.MaxLength = 32767;
            txtCre__Apell_M.Name = "txtCre__Apell_M";
            txtCre__Apell_M.PlaceholderText = "Apellido Materno...";
            txtCre__Apell_M.ReadOnly = false;
            txtCre__Apell_M.Size = new Size(345, 40);
            txtCre__Apell_M.TabIndex = 20;
            txtCre__Apell_M.TextAlign = HorizontalAlignment.Left;
            txtCre__Apell_M.TextColor = Color.Black;
            // 
            // txtCre__Apell_P
            // 
            txtCre__Apell_P.BackColor = Color.White;
            txtCre__Apell_P.BorderColor = Color.LightGray;
            txtCre__Apell_P.BorderFocusColor = Color.DeepSkyBlue;
            txtCre__Apell_P.Location = new Point(60, 200);
            txtCre__Apell_P.MaxLength = 32767;
            txtCre__Apell_P.Name = "txtCre__Apell_P";
            txtCre__Apell_P.PlaceholderText = "Apellido Paterno...";
            txtCre__Apell_P.ReadOnly = false;
            txtCre__Apell_P.Size = new Size(345, 40);
            txtCre__Apell_P.TabIndex = 19;
            txtCre__Apell_P.TextAlign = HorizontalAlignment.Left;
            txtCre__Apell_P.TextColor = Color.Black;
            // 
            // txtCre_Repit_password
            // 
            txtCre_Repit_password.BackColor = Color.White;
            txtCre_Repit_password.LabelText = "Repetor Contraseña...";
            txtCre_Repit_password.Location = new Point(60, 410);
            txtCre_Repit_password.Name = "txtCre_Repit_password";
            txtCre_Repit_password.PasswordValue = "";
            txtCre_Repit_password.Size = new Size(345, 36);
            txtCre_Repit_password.TabIndex = 18;
            // 
            // txtCre_password
            // 
            txtCre_password.BackColor = Color.White;
            txtCre_password.LabelText = "Contraseña...";
            txtCre_password.Location = new Point(60, 340);
            txtCre_password.Name = "txtCre_password";
            txtCre_password.PasswordValue = "";
            txtCre_password.Size = new Size(345, 36);
            txtCre_password.TabIndex = 17;
            // 
            // txtCre_Nombre
            // 
            txtCre_Nombre.BackColor = Color.White;
            txtCre_Nombre.BorderColor = Color.LightGray;
            txtCre_Nombre.BorderFocusColor = Color.DeepSkyBlue;
            txtCre_Nombre.Location = new Point(60, 130);
            txtCre_Nombre.MaxLength = 30000;
            txtCre_Nombre.Name = "txtCre_Nombre";
            txtCre_Nombre.PlaceholderText = "Nombre";
            txtCre_Nombre.ReadOnly = false;
            txtCre_Nombre.Size = new Size(345, 40);
            txtCre_Nombre.TabIndex = 16;
            txtCre_Nombre.TextAlign = HorizontalAlignment.Left;
            txtCre_Nombre.TextColor = Color.Black;
            // 
            // label_fechaCreacion
            // 
            label_fechaCreacion.AutoSize = true;
            label_fechaCreacion.FlatStyle = FlatStyle.Flat;
            label_fechaCreacion.Font = new Font("Georgia", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_fechaCreacion.Location = new Point(530, 200);
            label_fechaCreacion.Name = "label_fechaCreacion";
            label_fechaCreacion.Size = new Size(234, 29);
            label_fechaCreacion.TabIndex = 15;
            label_fechaCreacion.Text = "Fecha de creación";
            // 
            // label_Crear_Uusuario
            // 
            label_Crear_Uusuario.AutoSize = true;
            label_Crear_Uusuario.FlatStyle = FlatStyle.Flat;
            label_Crear_Uusuario.Font = new Font("Segoe UI Semibold", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_Crear_Uusuario.Location = new Point(3, 3);
            label_Crear_Uusuario.Name = "label_Crear_Uusuario";
            label_Crear_Uusuario.Size = new Size(247, 50);
            label_Crear_Uusuario.TabIndex = 9;
            label_Crear_Uusuario.Text = "Crear usuario";
            // 
            // tab_PanelAdministracionU
            // 
            tab_PanelAdministracionU.Controls.Add(btn_Crear);
            tab_PanelAdministracionU.Controls.Add(userTable);
            tab_PanelAdministracionU.Font = new Font("Segoe UI Historic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tab_PanelAdministracionU.Location = new Point(4, 24);
            tab_PanelAdministracionU.Name = "tab_PanelAdministracionU";
            tab_PanelAdministracionU.Padding = new Padding(3);
            tab_PanelAdministracionU.Size = new Size(990, 594);
            tab_PanelAdministracionU.TabIndex = 1;
            tab_PanelAdministracionU.Text = "Usuarios";
            tab_PanelAdministracionU.UseVisualStyleBackColor = true;
            // 
            // btn_Crear
            // 
            btn_Crear.BackColor = Color.FromArgb(0, 126, 242);
            btn_Crear.BackgroundColor = Color.FromArgb(0, 126, 242);
            btn_Crear.BorderColor = Color.White;
            btn_Crear.BorderRadius = 10;
            btn_Crear.BorderSize = 0;
            btn_Crear.FlatAppearance.BorderSize = 0;
            btn_Crear.FlatStyle = FlatStyle.Flat;
            btn_Crear.Font = new Font("Microsoft Sans Serif", 22F);
            btn_Crear.ForeColor = Color.White;
            btn_Crear.FuentePersonalizada = AppFont.MontserratRegular;
            btn_Crear.Location = new Point(794, 52);
            btn_Crear.Name = "btn_Crear";
            btn_Crear.Size = new Size(176, 39);
            btn_Crear.TabIndex = 15;
            btn_Crear.TamañoFuente = 22F;
            btn_Crear.Text = "Agregar empleado";
            btn_Crear.TextColor = Color.White;
            btn_Crear.UseVisualStyleBackColor = false;
            btn_Crear.Click += btn_Crear_Click_1;
            // 
            // userTable
            // 
            userTable.Location = new Point(17, 103);
            userTable.Name = "userTable";
            userTable.Size = new Size(953, 400);
            userTable.TabIndex = 14;
            // 
            // tab_Parametros
            // 
            tab_Parametros.Location = new Point(4, 24);
            tab_Parametros.Name = "tab_Parametros";
            tab_Parametros.Padding = new Padding(3);
            tab_Parametros.Size = new Size(990, 594);
            tab_Parametros.TabIndex = 0;
            tab_Parametros.Text = "Parametros";
            tab_Parametros.UseVisualStyleBackColor = true;
            // 
            // tabConfiguraciones
            // 
            tabConfiguraciones.Controls.Add(tab_Parametros);
            tabConfiguraciones.Controls.Add(tab_PanelAdministracionU);
            tabConfiguraciones.Controls.Add(tab_CrearUusuario);
            tabConfiguraciones.Controls.Add(tab_ModificarU);
            tabConfiguraciones.Dock = DockStyle.Fill;
            tabConfiguraciones.Location = new Point(0, 0);
            tabConfiguraciones.Name = "tabConfiguraciones";
            tabConfiguraciones.SelectedIndex = 0;
            tabConfiguraciones.Size = new Size(998, 622);
            tabConfiguraciones.TabIndex = 0;
            // 
            // Configuracion_Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 622);
            Controls.Add(tabConfiguraciones);
            MaximumSize = new Size(1014, 661);
            MinimumSize = new Size(1014, 661);
            Name = "Configuracion_Home";
            Text = "Configuracion_Home";
            tab_ModificarU.ResumeLayout(false);
            tab_ModificarU.PerformLayout();
            tab_CrearUusuario.ResumeLayout(false);
            tab_CrearUusuario.PerformLayout();
            tab_PanelAdministracionU.ResumeLayout(false);
            tabConfiguraciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabConfiguraciones; //Tab Global (Padre)

        private TabPage tab_Parametros; //Tab Hijo

        private TabPage tab_PanelAdministracionU; //Tab Hijo

            private Label label_Crear_Uusuario;
       
            private Presentation.Components.UserTableControl userTable;

            private p_Button btn_Crear;

        private TabPage tab_CrearUusuario; //Tab Hijo

            private Label label_fechaCreacion;

            private FormTextBox txtCre_Nombre;
            private FormTextBox txtCre__Apell_M;
            private FormTextBox txtCre__Apell_P;

            private PasswordTextBox Box_Edit_password_after;
            private PasswordTextBox Box_Edit_password;

            private ComboBox comboBox_Pefl_Users;

            private CustomIconButton add_user_CreateU;
            private CustomIconButton cancelar_CretUser;

        private TabPage tab_ModificarU; //Tab Hijo

            private FormTextBox TextBoxMod_Nombre;
            private FormTextBox TextBoxMod_Apell_P;
            private FormTextBox TextBoxMod_Apell_M;
            private PasswordTextBox txtCre_password;
            private PasswordTextBox txtCre_Repit_password;
            private CustomIconButton btn_Modi_User;
            private CustomIconButton btn_cancelEditUser;
        private ComboBox comboBox_Mod_Perfil;
        private Label label2;
        private Label label1;
        private PasswordTextBox Box_Edit_Repit_password;
    }
}