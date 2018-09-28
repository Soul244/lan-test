using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;
using Entity;
using LTest.Properties;
using Application = System.Windows.Application;

namespace LTest.Classes
{
    public static class Global
    {
        public enum Durum
        {
            OdaOffline,
            OdaOnline,
            TestBaslatildi,
            TestBitirildi,
            TestOlusturuldu,
            TestDuzenleSecildi,
            TestBitti
        };

        public static Durum GenelDurum { get; set; }


        public static Path Warning { get; } = (Path)Application.Current.FindResource("Warning");
        public static Path Done { get; } = (Path)Application.Current.FindResource("Done");
        public static Path Failed { get; } = (Path)Application.Current.FindResource("Failed");

        public static ServerKullanici ServerKullanici;

        public static void AktifKullanici(ServerKullanici serverKullanici)
        {
            ServerKullanici = serverKullanici;
        }

        public static List<Path> Icons()
        {
            return new List<Path>
            {
                (Path)Application.Current.FindResource("Square"),
                (Path)Application.Current.FindResource("Rectangle"),
                (Path)Application.Current.FindResource("Triangle"),
                (Path)Application.Current.FindResource("Pentagon"),
                (Path)Application.Current.FindResource("Hexagon"),
                (Path)Application.Current.FindResource("Rhombus"),
                (Path)Application.Current.FindResource("Parallelogram"),
                (Path)Application.Current.FindResource("Asteriks"),
                (Path)Application.Current.FindResource("Rectangle2")
            };
        }

        public static List<SolidColorBrush> Colors()
        {
            return new List<SolidColorBrush>
            {
                Brushes.Red,
                Brushes.CornflowerBlue,
                Brushes.Orange,
                Brushes.MediumVioletRed,
                Brushes.ForestGreen,
                Brushes.Gold,
                Brushes.HotPink,
                Brushes.Teal
            };
        }

        public static Color ChangeColorBrightness(SolidColorBrush solid, float correctionFactor)
        {
            float red = solid.Color.R;
            float green = solid.Color.G;
            float blue = solid.Color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(solid.Color.A, (byte)red, (byte)green, (byte)blue);
        }

        public static void ChangeColour(SolidColorBrush solidColor)
        {

            Settings.Default.HexColor = solidColor.Color.ToString();
            Settings.Default.Save();

            Application.Current.Resources["MainColor"] = solidColor;
            Application.Current.Resources["MainColorAnimation"] = solidColor;

            Application.Current.Resources["MainColorMedium"] = new SolidColorBrush(ChangeColorBrightness(solidColor, 0.20f)); ;
            Application.Current.Resources["MainColorMediumAnimation"] = new SolidColorBrush(ChangeColorBrightness(solidColor, 0.25f));

            Application.Current.Resources["MainColorSoft"] = new SolidColorBrush(ChangeColorBrightness(solidColor, 0.30f));
            Application.Current.Resources["MainColorSoftAnimation"] = new SolidColorBrush(ChangeColorBrightness(solidColor, 0.35f));
        }
    }
}
