using Autofac;
using Nootus.Fabric.Mobile.Core;
using Nootus.Fabric.Mobile.Views;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Services
{
    public static class NavigationService
    {
        public static INavigation Navigation
        {
            get
            {
                return (Application.Current.MainPage as CustomNavigationView).Navigation;
            }
        }

        public static async Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            Page page = await CreatePage(typeof(TViewModel));

            if (Application.Current.MainPage is CustomNavigationView mainPage)
            {
                await mainPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new CustomNavigationView(page);
            }
        }

        public static async Task InsertPageBeforeLastAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            Page page = await CreatePage(typeof(TViewModel));
            CustomNavigationView mainPage = Application.Current.MainPage as CustomNavigationView;
            Page currentPage = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 1];
            mainPage.Navigation.InsertPageBefore(page, currentPage);
            await mainPage.PopAsync();
        }

        public static async Task BindViewModel<TViewModel>(Page page)
        {
            await BindViewModel(page, typeof(TViewModel));
        }

        private static async Task BindViewModel(Page page, Type viewModelType)
        {
            BaseViewModel viewModel = DependencyInjection.Container.Resolve(viewModelType) as BaseViewModel;
            page.BindingContext = viewModel;
            page.Appearing += viewModel.PageAppearing;
            await viewModel.InitializeAsync(page);
        }


        private static async Task<Page> CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new System.Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = DependencyInjection.Container.Resolve(pageType) as Page;
            await BindViewModel(page, viewModelType);

            return page;
        }

        private static Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }
    }
}
