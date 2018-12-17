using Nootus.Fabric.Mobile.Controls;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Dialog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DialogView: ContentPage
	{
        IDialogService service;

        public DialogView(IDialogService service)
		{
			InitializeComponent();
            this.service = service;
		}

        public void InitializeLoading()
        {
            InitializeDialog();
            HideAllStacks();
            LoadingStack.IsVisible = true;
        }

        public void InitializeAlert(AlertMode mode, string message)
        {
            ParentStack.VerticalOptions = new LayoutOptions(LayoutAlignment.Center, false);
            AlertButton.IsVisible = true;
            InitializeAlertToast(mode, message);
        }

        public void InitializeToast(AlertMode mode, string message)
        {
            ParentStack.VerticalOptions = new LayoutOptions(LayoutAlignment.End, true);
            AlertButton.IsVisible = false;
            InitializeAlertToast(mode, message);
        }

        private void InitializeAlertToast(AlertMode mode, string message)
        {
            InitializeDialog();
            HideAllStacks();
            AlertStack.IsVisible = true;
            switch (mode)
            {
                case AlertMode.Error:
                    AlertIcon.Source = SvgIcon.GetSvgImageSource("Kheling.Mobile.Resources.error.svg", 32, 32, Color.Red);
                    AlertMessage.TextColor = Color.Red;
                    break;

                case AlertMode.Warning:
                    AlertIcon.Source = SvgIcon.GetSvgImageSource("Kheling.Mobile.Resources.warning.svg", 32, 32, Color.Yellow);
                    AlertMessage.TextColor = Color.Black;
                    break;

                case AlertMode.Info:
                    AlertIcon.Source = SvgIcon.GetSvgImageSource("Kheling.Mobile.Resources.info.svg", 32, 32, Color.Green);
                    AlertMessage.TextColor = Color.Black;
                    break;
            }

            AlertMessage.Text = message;
        }

        public bool IsInitialized => Application.Current.MainPage != null;

        private void InitializeDialog()
        {
            Page mainPage = Application.Current.MainPage;

            Parent = mainPage;
            Layout(new Rectangle(0, 0, mainPage.Width, mainPage.Height));
        }

        private void HideAllStacks()
        {
            LoadingStack.IsVisible = false;
            AlertStack.IsVisible = false;
        }

        public void Hide()
        {
            HideAllStacks();
        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            service.Hide();
        }
    }
}
