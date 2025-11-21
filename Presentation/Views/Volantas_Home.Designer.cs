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
            label_Calendario_Sorteos = new Label();
            tabPage3 = new TabPage();
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
            tabPage2.Controls.Add(label_Calendario_Sorteos);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1080, 828);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Calendario de sorteos";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label_Calendario_Sorteos
            // 
            label_Calendario_Sorteos.AutoSize = true;
            label_Calendario_Sorteos.FlatStyle = FlatStyle.Flat;
            label_Calendario_Sorteos.Font = new Font("Georgia", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_Calendario_Sorteos.Location = new Point(386, 79);
            label_Calendario_Sorteos.Name = "label_Calendario_Sorteos";
            label_Calendario_Sorteos.Size = new Size(339, 34);
            label_Calendario_Sorteos.TabIndex = 0;
            label_Calendario_Sorteos.Text = "Calendario de sorteos";
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1080, 828);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Loteria tradicional";
            tabPage3.UseVisualStyleBackColor = true;
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
    }
}