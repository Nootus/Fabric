﻿using Nootus.Fabric.Mobile.Behaviors;
using Nootus.Fabric.Mobile.Converters;
using Nootus.Fabric.Mobile.Validations;
using System;
using System.Windows.Input;
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

        private string headerText;
        public string HeaderText
        {
            get { return headerText;  }
            set { Header.Text = headerText = value; }
        }

        private Style headerStyle;
        public Style HeaderStyle
        {
            get { return headerStyle; }
            set { Header.Style = headerStyle = value; }
        }

        private Style errorStyle;
        public Style ErrorStyle
        {
            get { return errorStyle; }
            set { Error.Style = errorStyle = value; }
        }

        private Color errorLineColor;
        public Color ErrorLineColor
        {
            get { return errorLineColor; }
            set { errorLineColor = value; }
        }

        public static readonly BindableProperty FieldProperty = BindableProperty.Create(
                                                         propertyName: "Field",
                                                         returnType: typeof(ValidatableText<string>),
                                                         declaringType: typeof(FormField),
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: FieldPropertyChanged);

        public ValidatableText<string> Field
        {
            get { return (ValidatableText<string>)base.GetValue(FieldProperty); }
            set { base.SetValue(FieldProperty, value); }
        }

        private static void FieldPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FormField)bindable;
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
            var control = (FormField)bindable;
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
