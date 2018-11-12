using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FormField : ContentView
	{
		public FormField ()
		{
			InitializeComponent ();
		}

        public static readonly BindableProperty EntryHeaderTextProperty = BindableProperty.Create(
                                                         propertyName: "EntryHeaderText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(FormField),
                                                         defaultBindingMode: BindingMode.OneTime,
                                                         propertyChanged: EntryHeaderTextPropertyChanged);

        public string EntryHeaderText
        {
            get { return (string) base.GetValue(EntryHeaderTextProperty); }
            set { base.SetValue(EntryHeaderTextProperty, value); }
        }

        private static void EntryHeaderTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FormField)bindable;
            control.EntryHeader.Text = (string) newValue;
        }


        public static readonly BindableProperty EntryHeaderStyleProperty = BindableProperty.Create(
                                                         propertyName: "EntryHeaderText",
                                                         returnType: typeof(Style),
                                                         declaringType: typeof(FormField),
                                                         defaultBindingMode: BindingMode.OneTime,
                                                         propertyChanged: EntryHeaderStylePropertyChanged);

        public Style EntryHeaderStyle
        {
            get { return (Style) base.GetValue(EntryHeaderStyleProperty); }
            set { base.SetValue(EntryHeaderStyleProperty, value); }
        }

        private static void EntryHeaderStylePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FormField)bindable;
            control.EntryHeader.Style = (Style) newValue;
        }
    }
}