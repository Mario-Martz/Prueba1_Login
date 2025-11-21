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
    }
}

