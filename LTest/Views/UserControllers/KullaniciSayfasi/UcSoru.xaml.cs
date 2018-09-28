using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using LTest.Classes;

namespace LTest.Views.UserControllers.KullaniciSayfasi
{
    /// <summary>
    /// Interaction logic for UcSoru.xaml
    /// </summary>
    public partial class UcSoru : UserControl
    {
        public TextBlock[] textBlocks;
        public Border[] borders;
        public TextBlock[] bordersDogru;

        public UcSoru(int cevapSayisi)
        {
            InitializeComponent();
            
            int gridCount = Convert.ToInt16(Math.Ceiling((decimal)cevapSayisi / 2));

            borders = new Border[cevapSayisi];
            bordersDogru = new TextBlock[cevapSayisi];
            textBlocks = new TextBlock[cevapSayisi];
            Rectangle[] rectangles = new Rectangle[cevapSayisi];
            Viewbox[] viewboxes = new Viewbox[cevapSayisi];
            DockPanel[] dockPanels = new DockPanel[cevapSayisi];
            var colors = Global.Colors();
            var icons = Global.Icons();
            Grid[] grids = new Grid[gridCount];

            //Cevap Sayısında göre row definition yapılıyor
            for (int i = 0; i < gridCount; i++)
            {
                CevapGrid.RowDefinitions.Add(new RowDefinition());
            }

            // oluşturulmuş gridlere row değerleri veriliyor.
            for (int i = 0; i < gridCount; i++)
            {
                grids[i] = new Grid();
                grids[i].SetValue(Grid.RowProperty, i);

                grids[i].ColumnDefinitions.Add(new ColumnDefinition());
                grids[i].ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < cevapSayisi; i++)
            {
                borders[i] = new Border
                {
                    Style = FindResource("BorderStyle") as Style,
                    Margin = new Thickness(2),
                    Background = colors[i],
                };
                bordersDogru[i] = new TextBlock
                {
                    Visibility = Visibility.Hidden
                };
                viewboxes[i] = new Viewbox
                {
                    MinHeight=24,
                    Stretch=Stretch.Uniform,
                };
                dockPanels[i] = new DockPanel();

                textBlocks[i] = new TextBlock
                {
                    Style = FindResource("TextBlock") as Style,
                    Foreground = Brushes.White,
                    FontFamily = new FontFamily("Cabin"),
                    Name = "cevap" + i,
                    TextAlignment = TextAlignment.Justify,
                    TextWrapping = TextWrapping.Wrap,
                    Height = double.NaN,
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment= VerticalAlignment.Center,
                    Width = 400,
                };

                 Binding myBinding = new Binding("CevapText");
                 BindingOperations.SetBinding(textBlocks[i], TextBox.TextProperty, myBinding);


                rectangles[i] = new Rectangle
                {
                    Style = FindResource("IconBox") as Style,
                    Margin = new Thickness(16, 0, 16, 0),
                    Width = 36,
                    Height = 36,
                    Fill = Brushes.White,
                    OpacityMask = new VisualBrush
                    {
                        Stretch=Stretch.Fill,                      
                        Visual = icons[i]
                    }
                };

            }

            int borderColumn = 0;

            for (int i = 0; i < cevapSayisi; i++)
            {
                dockPanels[i].Children.Add(rectangles[i]);
                dockPanels[i].Children.Add(textBlocks[i]);
                viewboxes[i].Child = dockPanels[i];
                borders[i].SetValue(Grid.ColumnProperty, borderColumn);
                borderColumn++;
                if (borderColumn==2)
                {
                    borderColumn = 0;
                }
            }


            int j = 0;
            for (int i = 0; i < gridCount; i++)
            {
                for (int k = j; k < j + 2; k++)
                {
                    if (k==cevapSayisi)
                    {
                        break;
                    }
                    borders[k].Child = viewboxes[k];
                    grids[i].Children.Add(borders[k]);
                }
                j = j + 2;
                CevapGrid.Children.Add(grids[i]);
            }
        }
    }
}
