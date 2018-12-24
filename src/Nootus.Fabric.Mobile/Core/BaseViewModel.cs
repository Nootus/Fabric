using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public virtual async Task InitializeAsync(Page page)
        {
            Page = page;
            await Task.FromResult(true);
        }
    }
}
