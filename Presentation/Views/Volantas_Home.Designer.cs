namespace Prueba1_Login.Views
{
    partial class Volantas_Home
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
            tabVolantas = new TabControl();
            tabPage1 = new TabPage();
            label2 = new Label();
            label_Estructura_premios = new Label();
            tabPage2 = new TabPage();
            comboBox1 = new ComboBox();
            customIconButton1 = new Prueba1_Login.Resources.Controls.CustomIconButton();
            dateLabelPicker2 = new CustomControls.DateLabelPicker();
            dateLabelPicker1 = new CustomControls.DateLabelPicker();
            label_Calendario_Sorteos = new Label();
            tabPage3 = new TabPage();
            customIconButton2 = new Prueba1_Login.Resources.Controls.CustomIconButton();
            tabVolantas.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // tabVolantas
            // 
            tabVolantas.Controls.Add(tabPage1);
            tabVolantas.Controls.Add(tabPage2);
            tabVolantas.Controls.Add(tabPage3);
            tabVolantas.Dock = DockStyle.Fill;
            tabVolantas.Location = new Point(0, 0);
            tabVolantas.Name = "tabVolantas";
            tabVolantas.SelectedIndex = 0;
            tabVolantas.Size = new Size(998, 622);
            tabVolantas.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label_Estructura_premios);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(990, 594);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Estructura de premios";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 12);
            label2.Name = "label2";
            label2.Size = new Size(0, 15);
            label2.TabIndex = 1;
            // 
            // label_Estructura_premios
            // 
            label_Estructura_premios.AutoSize = true;
            label_Estructura_premios.Font = new Font("Georgia", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_Estructura_premios.Location = new Point(307, 12);
            label_Estructura_premios.Name = "label_Estructura_premios";
            label_Estructura_premios.Size = new Size(423, 41);
            label_Estructura_premios.TabIndex = 0;
            label_Estructura_premios.Text = "Estructura de premios";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(customIconButton2);
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Controls.Add(customIconButton1);
            tabPage2.Controls.Add(dateLabelPicker2);
            tabPage2.Controls.Add(dateLabelPicker1);
            tabPage2.Controls.Add(label_Calendario_Sorteos);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(990, 594);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Calendario de sorteos";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(737, 59);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 4;
            // 
            // customIconButton1
            // 
            customIconButton1.BackColor = Color.FromArgb(0, 123, 255);
            customIconButton1.BaseColor = Color.FromArgb(0, 123, 255);
            customIconButton1.BorderRadius = 10;
            customIconButton1.FlatAppearance.BorderSize = 0;
            customIconButton1.FlatStyle = FlatStyle.Flat;
            customIconButton1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            customIconButton1.ForeColor = Color.White;
            customIconButton1.FuentePersonalizada = Resources.Fonts_Personalizados.AppFont.MontserratBold;
            customIconButton1.Icon = Resources.Properties.Resources.search;
            customIconButton1.IconPadding = 10;
            customIconButton1.IconSize = 20;
            customIconButton1.Location = new Point(860, 131);
            customIconButton1.Name = "customIconButton1";
            customIconButton1.Size = new Size(110, 40);
            customIconButton1.TabIndex = 3;
            customIconButton1.TamañoFuente = 11F;
            customIconButton1.Text = "Buscar";
            customIconButton1.TextColor = Color.White;
            customIconButton1.UseVisualStyleBackColor = false;
            // 
            // dateLabelPicker2
            // 
            dateLabelPicker2.BackgroundImageLayout = ImageLayout.Center;
            dateLabelPicker2.DatePickerVisible = true;
            dateLabelPicker2.LabelText = "Fecha fin";
            dateLabelPicker2.LabelVisible = true;
            dateLabelPicker2.Location = new Point(350, 53);
            dateLabelPicker2.Name = "dateLabelPicker2";
            dateLabelPicker2.Size = new Size(330, 40);
            dateLabelPicker2.Spacing = 90;
            dateLabelPicker2.TabIndex = 2;
            dateLabelPicker2.Value = new DateTime(2025, 12, 5, 8, 16, 36, 237);
            // 
            // dateLabelPicker1
            // 
            dateLabelPicker1.DatePickerVisible = true;
            dateLabelPicker1.LabelText = "Fecha inicio";
            dateLabelPicker1.LabelVisible = true;
            dateLabelPicker1.Location = new Point(8, 53);
            dateLabelPicker1.Name = "dateLabelPicker1";
            dateLabelPicker1.Size = new Size(330, 40);
            dateLabelPicker1.Spacing = 90;
            dateLabelPicker1.TabIndex = 1;
            dateLabelPicker1.Value = new DateTime(2025, 12, 5, 8, 16, 28, 377);
            // 
            // label_Calendario_Sorteos
            // 
            label_Calendario_Sorteos.AutoSize = true;
            label_Calendario_Sorteos.FlatStyle = FlatStyle.Flat;
            label_Calendario_Sorteos.Font = new Font("Georgia", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_Calendario_Sorteos.Location = new Point(3, 3);
            label_Calendario_Sorteos.Name = "label_Calendario_Sorteos";
            label_Calendario_Sorteos.Size = new Size(339, 34);
            label_Calendario_Sorteos.TabIndex = 0;
            label_Calendario_Sorteos.Text = "Calendario de sorteos";
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(990, 594);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Loteria tradicional";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // customIconButton2
            // 
            customIconButton2.BackColor = Color.FromArgb(0, 123, 255);
            customIconButton2.BaseColor = Color.FromArgb(0, 123, 255);
            customIconButton2.BorderRadius = 10;
            customIconButton2.FlatAppearance.BorderSize = 0;
            customIconButton2.FlatStyle = FlatStyle.Flat;
            customIconButton2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            customIconButton2.ForeColor = Color.White;
            customIconButton2.FuentePersonalizada = Resources.Fonts_Personalizados.AppFont.MontserratBold;
            customIconButton2.Icon = Resources.Properties.Resources.add;
            customIconButton2.IconPadding = 10;
            customIconButton2.IconSize = 20;
            customIconButton2.Location = new Point(679, 131);
            customIconButton2.Name = "customIconButton2";
            customIconButton2.Size = new Size(139, 40);
            customIconButton2.TabIndex = 5;
            customIconButton2.TamañoFuente = 11F;
            customIconButton2.Text = "Cargra calendario";
            customIconButton2.TextColor = Color.White;
            customIconButton2.UseVisualStyleBackColor = false;
            // 
            // Volantas_Home
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 622);
            Controls.Add(tabVolantas);
            MaximumSize = new Size(1014, 661);
            MinimumSize = new Size(1014, 661);
            Name = "Volantas_Home";
            Text = "Volantas_Home";
            tabVolantas.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabVolantas;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Label label2;
        private Label label_Estructura_premios;
        private Label label_Calendario_Sorteos;
        private CustomControls.DateLabelPicker dateLabelPicker2;
        private CustomControls.DateLabelPicker dateLabelPicker1;
        private ComboBox comboBox1;
        private Resources.Controls.CustomIconButton customIconButton1;
        private Resources.Controls.CustomIconButton customIconButton2;
    }
}