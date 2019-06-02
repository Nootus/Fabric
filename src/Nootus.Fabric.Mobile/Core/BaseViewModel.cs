using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Core
{
    public abstract class BaseViewModel : BaseNotifyPropertyChanged
    {
        protected Page Page;

        string title = string.Empty;
        public string Title
        {
            get => title; 
            set => SetProperty(ref title, value); 
        }

        public virtual async Task InitializeAsync(Page page)
        {
            Page = page;
            await Task.FromResult(true);
        }

        protected virtual void OnAppearing()
        {
            
        }

        public void PageAppearing(object sender, EventArgs e)
        {
            OnAppearing();
        }

        public object NavigationData { get; set; }
    }
}
