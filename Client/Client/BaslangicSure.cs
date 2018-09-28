using Entity;
using LTest.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Client
{
	public class BaslangicSure : ContentPage
	{
        private Label label = new Label
        {
            VerticalOptions = LayoutOptions.CenterAndExpand,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
        };
        public BaslangicSure ()
		{
            Content = new StackLayout {
				Children = {
                    label,
				}
			};
            int sure = ClientListener.Data.Sure.BaslangicSure;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                label.Text = "Lütfen bekleyin " + sure + " saniye sonra oyun başlayacak.";
                sure--;
                if (sure==0)
                {
                    Navigation.PushModalAsync(new SoruGoster());
                    return false;
                }
                return true; // True = Repeat again, False = Stop the timer
            });
        }
    }
}