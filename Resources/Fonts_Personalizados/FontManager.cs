using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Prueba1_Login.Resources.Fonts_Personalizados
{
    public static class FontManager
    {
        private static readonly PrivateFontCollection fontCollection = new();
        private static readonly Dictionary<AppFont, FontFamily> loadedFonts = new();
        private static bool initialized = false;

        // 🧩 Inicializar fuentes personalizadas
        public static void Initialize()
        {
            if (initialized) return;

            try
            {
                // ✅ Ruta base del ejecutable (evita conflicto con namespace Application)
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // 🔠 Rutas de tus carpetas de fuentes
                string baseMontserrat = Path.Combine(basePath, "Tipografias", "Montserrat", "static");
                string baseRoboto = Path.Combine(basePath, "Tipografias", "Roboto");

                // Si las carpetas no existen (por ejemplo, en el diseñador)
                if (!Directory.Exists(baseMontserrat) && !Directory.Exists(baseRoboto))
                {
                    loadedFonts[AppFont.MontserratRegular] = SystemFonts.DefaultFont.FontFamily;
                    initialized = true;
                    return;
                }

                // 🟦 Montserrat
                if (Directory.Exists(baseMontserrat))
                {
                    LoadFont(AppFont.MontserratRegular, Path.Combine(baseMontserrat, "Montserrat-Regular.ttf"));
                    LoadFont(AppFont.MontserratBold, Path.Combine(baseMontserrat, "Montserrat-Bold.ttf"));
                }

                // 🟩 Roboto
                if (Directory.Exists(baseRoboto))
                {
                    LoadFont(AppFont.RobotoRegular, Path.Combine(baseRoboto, "Roboto-Regular.ttf"));
                    LoadFont(AppFont.RobotoBold, Path.Combine(baseRoboto, "Roboto-Bold.ttf"));
                }

                initialized = true;
            }
            catch (Exception ex)
            {
                // En modo diseñador, evita que falle
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                {
                    loadedFonts[AppFont.MontserratRegular] = SystemFonts.DefaultFont.FontFamily;
                    initialized = true;
                }
                else
                {
                    MessageBox.Show($"⚠️ Error cargando fuentes personalizadas:\n{ex.Message}",
                                    "FontManager",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
        }

        // 🧠 Cargar una fuente desde archivo
        private static void LoadFont(AppFont fontKey, string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"No se encontró la fuente: {path}");

            byte[] fontBytes = File.ReadAllBytes(path);
            nint fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            fontCollection.AddMemoryFont(fontData, fontBytes.Length);
            Marshal.FreeCoTaskMem(fontData);

            loadedFonts[fontKey] = fontCollection.Families[^1];
        }

        // 🎨 Obtener la fuente lista para usar
        public static Font GetFont(AppFont font, float size = 22f, FontStyle style = FontStyle.Regular)
        {
            if (!initialized)
                Initialize();

            if (loadedFonts.TryGetValue(font, out FontFamily? family))
                return new Font(family, size, style);

            return SystemFonts.DefaultFont;
        }
    }
}
