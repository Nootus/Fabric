using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Dialog
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DialogView: ContentPage
	{
        public int DialogState { get; set; }
        public string Message { get; set; }

		public DialogView()
		{
			InitializeComponent ();
		}

        public void InitializeLoading()
        {
            InitializeDialog();          
        }

        private void InitializeDialog()
        {
            // build the loading page with native base
            this.Parent = Xamarin.Forms.Application.Current.MainPage;

            this.Layout(new Rectangle(0, 0,
                Xamarin.Forms.Application.Current.MainPage.Width,
                Xamarin.Forms.Application.Current.MainPage.Height));
        }

        private void HideAllStacks()
        {
            LoadingStack.IsVisible = false;
            AlertStack.IsVisible = false;

        }
    }
}