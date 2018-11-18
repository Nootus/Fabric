using Nootus.Fabric.Mobile.Behaviors;
using Nootus.Fabric.Mobile.Converters;
using Nootus.Fabric.Mobile.Validations;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nootus.Fabric.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FormField : ContentView
	{
		public FormField ()
		{
			InitializeComponent ();
		}

        public string HeaderText
        {
            get { return Header.Text; }
            set { Header.Text = value; }
        }

        public Keyboard Keyboard
        {
            get { return Entry.Keyboard; }
            set { Entry.Keyboard = value; }
        }

        public static readonly BindableProperty HeaderStyleProperty = BindableProperty.Create(
                                                         propertyName: "HeaderStyle",
                                                         returnType: typeof(Style),
                                                         declaringType: typeof(FormField),
                                                         defaultBindingMode: BindingMode.OneTime,
                                                         propertyChanged: HeaderStyleChanged);

        private static void HeaderStyleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            FormField control = (FormField)bindable;
            control.Header.Style = (Style)newValue;
        }

        public static readonly BindableProperty ErrorStyleProperty = BindableProperty.Create(
                                                         propertyName: "ErrorStyle",
                                                         returnType: typeof(Style),
                                                         declaringType: typeof(FormField),
                                                         defaultBindingMode: BindingMode.OneTime,
                                                         propertyChanged: ErrorStyleChanged);

        private static void ErrorStyleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            FormField control = (FormField)bindable;
            control.Error.Style = (Style)newValue;
        }

        public Color ErrorLineColor { get; set; }
        public static readonly BindableProperty ErrorLineColorProperty = BindableProperty.Create(
                                                         propertyName: "ErrorLineColor",
                                                         returnType: typeof(Color),
                                                         declaringType: typeof(FormField),
                                                         defaultBindingMode: BindingMode.OneTime,
                                                         propertyChanged: ErrorLineColorChanged);

        private static void ErrorLineColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            FormField control = (FormField)bindable;
            control.ErrorLineColor = (Color)newValue;
        }

        public static readonly BindableProperty FieldProperty = BindableProperty.Create(
                                                         propertyName: "Field",
                                                         returnType: typeof(ValidatableText<string>),
                                                         declaringType: typeof(FormField),
                                                         defaultBindingMode: BindingMode.OneWay,
                                                         propertyChanged: FieldPropertyChanged);

        public ValidatableText<string> Field
        {
            get { return (ValidatableText<string>)base.GetValue(FieldProperty); }
            set { base.SetValue(FieldProperty, value); }
        }

        private static void FieldPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            FormField control = (FormField)bindable;
            ValidatableText<string> field = (ValidatableText<string>) newValue;
            control.Entry.SetBinding(Entry.TextProperty, new Binding("Value", source: field));            
            control.Error.SetBinding(Label.TextProperty, new Binding("Errors", source: field, converter: new FirstValidationErrorConverter()));
            DataTrigger trigger = new DataTrigger(typeof(Entry))
            {
                Binding = new Binding("IsValid", source: field),
                Value = false
            };


            Setter setter = new Setter
            {
                Property = LineColorBehavior.LineColorProperty,
                Value = control.ErrorLineColor
            };

            trigger.Setters.Add(setter);
            control.Entry.Triggers.Add(trigger);
        }

        public static readonly BindableProperty TextChangedCommandProperty = BindableProperty.Create(
                                                         propertyName: "TextChangedCommand",
                                                         returnType: typeof(ICommand),
                                                         declaringType: typeof(FormField),
                                                         defaultBindingMode: BindingMode.OneTime,
                                                         propertyChanged: TextChangedCommandPropertyChanged);

        private static void TextChangedCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            FormField control = (FormField)bindable;
            control.CommandBehavior.Command = (ICommand) newValue;
        }
    }
}

/*
 * 
 * 
        public static readonly BindableProperty ValidatableEntryProperty = BindableProperty.Create(
                                                         propertyName: "ValidatableEntry",
                                                         returnType: typeof(ValidatableText<string>),
                                                         declaringType: typeof(FormField),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: ValidatableEntryPropertyChanged);

        public ValidatableText<string> ValidatableEntry
        {
            get { return (ValidatableText<string>)base.GetValue(ValidatableEntryProperty); }
            set { base.SetValue(ValidatableEntryProperty, value); }
        }

        private static void ValidatableEntryPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FormField)bindable;
            ValidatableText<string> entry = (ValidatableText<string>)newValue;
            control.Entry.Text = entry.Value;
        }
 * 
 * 
 */
