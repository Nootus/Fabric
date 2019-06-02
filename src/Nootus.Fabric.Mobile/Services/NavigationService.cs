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
                return (Application.Current.MainPage as BaseNavigationPage).Navigation;
            }
        }

        public static async Task NavigateToAsync<TViewModel>(object navigationData = null) where TViewModel : BaseViewModel
        {
            Page page = await CreatePage(typeof(TViewModel));
            ((TViewModel)page.BindingContext).NavigationData = navigationData;
            
            if (Application.Current.MainPage is BaseNavigationPage mainPage)
            {
                await mainPage.PushAsync(page);
            }
            else
            {
                Application.Current.MainPage = new BaseNavigationPage(page);
            }
        }

        public static async Task PushModalAsync<TViewModel>(object navigationData = null) where TViewModel : BaseViewModel
        {
            Page page = await CreatePage(typeof(TViewModel));
            ((TViewModel)page.BindingContext).NavigationData = navigationData;

            await Application.Current.MainPage.Navigation.PushModalAsync(page, true);
        }

        public static async Task PushModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }

        public static async Task InsertPageBeforeLastAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            Page page = await CreatePage(typeof(TViewModel));
            BaseNavigationPage mainPage = Application.Current.MainPage as BaseNavigationPage;
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
            await BindViewModel(page, viewModel);
        }

        private static async Task BindViewModel(Page page, BaseViewModel viewModel)
        {
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

            BaseViewModel viewModel = DependencyInjection.Container.Resolve(viewModelType) as BaseViewModel;
            Page page = DependencyInjection.Container.Resolve(pageType) as Page;
            NavigationPage.SetHasNavigationBar(page, false);
            
            await BindViewModel(page, viewModel);

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
