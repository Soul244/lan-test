using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace LTest.Classes
{
    class TestGoster:FrameworkElement
    {
        private static int _soru;
        private static int _cevap;
        public readonly Border[] Borders;
        public readonly StackPanel[] StackPanels;
        public readonly DockPanel[] DockPanelSoru;
        public readonly Label[] Label;
        public readonly DockPanel[] DockPanelCevap;
        public readonly TextBox[] SoruTextBoxes;
        public readonly TextBox[,] CevapTextboxes;
        public readonly CheckBox[,] CevapCheckBoxes;
        public TestGoster(int soru, int cevap)
        {
            _soru = soru;
            _cevap = cevap;
            Borders = new Border[_soru];
            StackPanels = new StackPanel[_soru];
            DockPanelSoru = new DockPanel[_soru];
            Label = new Label[_soru];
            DockPanelCevap = new DockPanel[_soru];
            SoruTextBoxes = new TextBox[_soru];
            CevapTextboxes = new TextBox[_soru, _cevap];
            CevapCheckBoxes = new CheckBox[_soru, _cevap];
        }

        public void ControlCreation()//Kontroller Burada Tanımlandı.
        {
            for (var i = 0; i < _soru; i++)
            {
                StackPanels[i] = new StackPanel();
                Borders[i] = new Border
                {
                    Style = FindResource("BorderStyle") as Style,
                    Padding=new Thickness(20)
                };
                DockPanelSoru[i] = new DockPanel
                {
                    Margin = new Thickness(0, 0, 0, 10),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Width = double.NaN
                };
                Label[i] = new Label
                {
                    Content = "Soru " + (i + 1) + ":",
                    Style = FindResource("Label") as Style,
                    Width = 120,
                    Foreground = Brushes.White,
                    Height = double.NaN, // Auto için bunu yazdık. Niye acaba?Cevap : Onu öyle yapmışlar. Yapana sormak gerek.
                    Background = (SolidColorBrush)FindResource("MainColor")
                };
                SoruTextBoxes[i] = new TextBox
                {
                    Style = FindResource("TextBox") as Style,
                    FontFamily = new FontFamily("Titillium Web SemiBold"),
                    Name = $"SoruTextBox{i}",
                    Height=double.NaN
                };


                DockPanelCevap[i] = new DockPanel
                {
                    Name = $"stackPanel{i}",
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(0, 5, 5, 0)
                };
                for (var k = 0; k < _cevap; k++)
                {
                    CevapCheckBoxes[i, k] = new CheckBox
                    {
                        Name = $"Soru{i}Cevap{k}",
                        Margin = new Thickness(0, 5, 0, 5)
                    };
                    //_checkBox[i, k].Checked += CheckBox_Checked;
                    CevapTextboxes[i, k] = new TextBox
                    {
                        Style = FindResource("TextBox") as Style,
                        MinWidth = 600,
                        Height = double.NaN,
                        
                    };
                    CevapCheckBoxes[i, k].Content = CevapTextboxes[i, k];
                }
            }
        }
        public void AddControlsToDockPanel(StackPanel stack)
        {
            for (var i = 0; i < _soru; i++) // Kontroller Burada DockPanellere Eklendi.
            {
                DockPanel.SetDock(DockPanelSoru[i], Dock.Top);
                DockPanel.SetDock(DockPanelCevap[i], Dock.Top);

                DockPanelSoru[i].Children.Add(Label[i]);
                DockPanelSoru[i].Children.Add(SoruTextBoxes[i]);
                StackPanels[i].Children.Add(DockPanelSoru[i]);
                StackPanels[i].Children.Add(DockPanelCevap[i]);
                Borders[i].Child=StackPanels[i];
                stack.Children.Add(Borders[i]);
                for (var k = 0; k < _cevap; k++)
                {
                    System.Windows.Controls.DockPanel.SetDock(CevapCheckBoxes[i, k], Dock.Top);
                    DockPanelCevap[i].Children.Add(CevapCheckBoxes[i, k]);

                }
            }
        }
    }
}
