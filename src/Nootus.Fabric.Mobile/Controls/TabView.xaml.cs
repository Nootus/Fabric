using System;
using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabView : ContentView
    {

        #region ItemsSource
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(TabView), default(IEnumerable), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((TabView)bindable).tabs.ItemsSource = (IEnumerable)newValue;
                }
        );

        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourceProperty);
            }

            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
        #endregion

        #region TabHeaders
        public static readonly BindableProperty TabHeadersProperty = BindableProperty.Create(nameof(TabHeaders), typeof(IEnumerable), typeof(TabView), default(IEnumerable), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    TabView view = (TabView)bindable;
                    StackLayout tabHeaders = view.tabHeaders;
                    tabHeaders.Children.Clear();

                    string[] headerLabels = (string[])newValue;
                    for(int column = 0; column < headerLabels.Length; column ++)
                    {
                        Label lbl = new Label();
                        lbl.Text = headerLabels[column];
                        lbl.Style = view.TabHeaderStyle;
                        VisualStateManager.GoToState(lbl, "Unselected");
                        TapGestureRecognizer tap = new TapGestureRecognizer();
                        tap.Tapped += view.TabHeader_Tapped;
                        tap.CommandParameter = column;
                        lbl.GestureRecognizers.Add(tap);

                        tabHeaders.Children.Add(lbl);
                    }
                }
        );

        public IEnumerable TabHeaders
        {
            get
            {
                return (IEnumerable)GetValue(TabHeadersProperty);
            }

            set
            {
                SetValue(TabHeadersProperty, value);
            }
        }
        #endregion

        #region IsPanInteractionEnabled
        public static readonly BindableProperty IsPanInteractionEnabledProperty = BindableProperty.Create(nameof(IsPanInteractionEnabled), typeof(bool), typeof(TabView), default(bool), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    ((TabView)bindable).tabs.IsPanInteractionEnabled = (bool)newValue;
                }
        );

        public bool IsPanInteractionEnabled
        {
            get
            {
                return (bool)GetValue(IsPanInteractionEnabledProperty);
            }

            set
            {
                tabs.IsPanInteractionEnabled = value;
                SetValue(IsPanInteractionEnabledProperty, value);
            }
        }
        #endregion

        #region TabHeaderStyle
        public static readonly BindableProperty TabHeaderStyleProperty = BindableProperty.Create(nameof(TabHeaderStyle), typeof(Style), typeof(TabView), default(Style), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    TabView view = (TabView)bindable;
                    StackLayout tabHeaders = view.tabHeaders;
                    Style style = (Style)newValue;

                    foreach(Label lbl in tabHeaders.Children)
                    {
                        lbl.Style = style;
                    }
                }
        );

        public Style TabHeaderStyle
        {
            get
            {
                return (Style)GetValue(TabHeaderStyleProperty);
            }

            set
            {
                SetValue(TabHeaderStyleProperty, value);
            }
        }
        #endregion

        #region TabHeaderStackStyle
        public static readonly BindableProperty TabHeaderStackStyleProperty = BindableProperty.Create(nameof(TabHeaderStackStyle), typeof(Style), typeof(TabView), default(Style), BindingMode.OneWay,
            propertyChanged:
                (BindableObject bindable, object oldValue, object newValue) =>
                {
                    TabView view = (TabView)bindable;
                    view.tabHeaders.Style = (Style)newValue;
                }
        );

        public Style TabHeaderStackStyle
        {
            get
            {
                return (Style)GetValue(TabHeaderStackStyleProperty);
            }

            set
            {
                SetValue(TabHeaderStackStyleProperty, value);
            }
        }
        #endregion

        public TabView()
        {
            InitializeComponent();

            this.BindingContext = this;
            tabs.ItemAppearing += Tabs_ItemAppearing;
        }

        public PanCardView.CarouselView Tabs => tabs;

        private void Tabs_ItemAppearing(PanCardView.CardsView view, PanCardView.EventArgs.ItemAppearingEventArgs args)
        {
            SelectTab(tabs.SelectedIndex);
        }

        private void SelectTab(int selectedIndex)
        {
            Label lbl;
            for(int index = 0; index < tabHeaders.Children.Count; index++)
            {
                lbl = (Label) tabHeaders.Children[index];
                if(index == selectedIndex)
                {
                    VisualStateManager.GoToState(lbl, "Selected");

                }
                else
                {
                    VisualStateManager.GoToState(lbl, "Unselected");
                }
            }
        }

        private void TabHeader_Tapped(object sender, System.EventArgs e)
        {
            Label lbl = (Label)sender;
            int index = Convert.ToInt32(((TapGestureRecognizer)lbl.GestureRecognizers[0]).CommandParameter);
            tabs.SelectedIndex = index;
        }
    }
}
