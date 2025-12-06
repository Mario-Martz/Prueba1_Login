using Prueba1_Login.Resources.Fonts_Personalizados;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba1_Login.Views
{
    public partial class Volantas_Home : Form
    {
        public Volantas_Home(int tabIndex = 0)
        {
            InitializeComponent();
            FontManager.Initialize();
            AplicarFuenteAControles(this);

            // 🔹 Oculta la barra de pestañas para que no se vea el encabezado
            //tabVolantas.Appearance = TabAppearance.FlatButtons;

            tabVolantas.ItemSize = new Size(0, 1);
            
            tabVolantas.SizeMode = TabSizeMode.Fixed;

            // 🔹 Quita todas las pestañas excepto la seleccionada
            for (int i = tabVolantas.TabPages.Count - 1; i >= 0; i--)
            {
                if (i != tabIndex)
                    tabVolantas.TabPages.RemoveAt(i);
            }

            // 🔹 Selecciona la pestaña indicada
            tabVolantas.SelectedIndex = 0; // ahora solo queda una pestaña visible
        }
        // ============================================
        // 🔵 MÉTODOS GENERALES DEL FORM
        // ============================================
        private void AplicarFuenteAControles(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Label lbl)
                    lbl.Font = FontManager.GetFont(AppFont.MontserratRegular, 22f);
                else if (ctrl is Button btn)
                    btn.Font = FontManager.GetFont(AppFont.MontserratBold, 26f);
                else if (ctrl is TextBox txt)
                    txt.Font = FontManager.GetFont(AppFont.MontserratRegular, 18f);

                if (ctrl.HasChildren)
                    AplicarFuenteAControles(ctrl);
            }
        }
    }
}

