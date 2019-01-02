using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Nootus.Fabric.Mobile.Controls
{
    public partial class FloatingMenu : AbsoluteLayout
    {

        bool isRevealed = false;
        bool raised = false;
        double buttonHeight = 60, buttonWidth = 60;

        public static readonly BindableProperty BGColorProperty = BindableProperty.Create(nameof(BGColor), typeof(Color), typeof(FloatingMenu), default(Color), Xamarin.Forms.BindingMode.OneWay);
        public Color BGColor
        {
            get
            {
                return (Color)GetValue(BGColorProperty);
            }

            set
            {
                SetValue(BGColorProperty, value);
            }
        }

        public static readonly BindableProperty OpenIconProperty = BindableProperty.Create(nameof(OpenIcon), typeof(string), typeof(FloatingMenu), default(string), Xamarin.Forms.BindingMode.OneWay);
        public string OpenIcon
        {
            get
            {
                return (string)GetValue(OpenIconProperty);
            }

            set
            {
                SetValue(OpenIconProperty, value);
            }
        }

        public static readonly BindableProperty CloseIconProperty = BindableProperty.Create(nameof(CloseIcon), typeof(string), typeof(FloatingMenu), default(string), Xamarin.Forms.BindingMode.OneWay);
        public string CloseIcon
        {
            get
            {
                return (string)GetValue(CloseIconProperty);
            }

            set
            {
                SetValue(CloseIconProperty, value);
            }
        }

        public static readonly BindableProperty AnimationTimeProperty = BindableProperty.Create(nameof(AnimationTime), typeof(int), typeof(FloatingMenu), 250, Xamarin.Forms.BindingMode.OneWay);
        public int AnimationTime
        {
            get
            {
                return (int)GetValue(AnimationTimeProperty);
            }

            set
            {
                SetValue(AnimationTimeProperty, value);
            }
        }

        public static readonly BindableProperty ExtraCommandProperty = BindableProperty.Create(nameof(ExtraCommand), typeof(ICommand), typeof(FloatingButton), null, Xamarin.Forms.BindingMode.OneWay);
        public ICommand ExtraCommand
        {
            get
            {
                return (ICommand)GetValue(ExtraCommandProperty);
            }

            set
            {
                SetValue(ExtraCommandProperty, value);
            }
        }

        public FloatingMenu()
        {
            InitializeComponent();

            ChildAdded += ArrangeChildren;

            MainButton.Command = new Command(() =>
            {
                if (isRevealed)
                {
                    Collapse(AnimationTime);
                }
                else
                {
                    Expand(AnimationTime);
                }
                isRevealed = !isRevealed;
                if (ExtraCommand != null)
                    ExtraCommand.Execute(null);
            });
        }

        void ArrangeChildren(object sender, EventArgs evt)
        {
            Rectangle bounds = GetLayoutBounds(this);
            buttonHeight = bounds.Height;
            buttonWidth = bounds.Width;

            for (int i = 1; i < Children.Count; i++)
            {
                Children[i].Scale = 0.7;
                SetLayoutBounds(Children[i], new Rectangle(0, (buttonHeight * i), buttonWidth, buttonHeight));
                SetLayoutFlags(this, GetLayoutFlags(this));
                Children[i].Rotation = 180;
                ((FloatingButton)Children[i]).ExtraCommand = new Command(() => { Collapse(AnimationTime); });
                Children[i].IsVisible = false;
                Children[i].InputTransparent = true;
            }
        }


        public async void Collapse(int time)
        {
            double adj = buttonHeight * Children.Count;
            SetLayoutBounds(this, new Rectangle(this.X, this.Y + adj, buttonWidth, buttonHeight));
            SetLayoutFlags(this, AbsoluteLayoutFlags.None);

            int raisInd = raised ? 1 : 0;
            for (int i = 1 - raisInd; i < Children.Count - raisInd; i++)
            {
                await Children[i].TranslateTo(0, -buttonHeight * (i + raisInd), (uint)time);
            }
            await Task.Delay(time);
            for (int i = 1 - raisInd; i < Children.Count - raisInd; i++)
            {
                Children[i].IsVisible = false;
                Children[i].InputTransparent = true;
            }

            isRevealed = false;
            MainButton.IconSrc = OpenIcon;

        }

        public void Expand(int time)
        {
            MainButton.IconSrc = CloseIcon;

            double adj = buttonHeight * Children.Count;
            SetLayoutBounds(this, new Rectangle(this.X, this.Y - adj, buttonWidth, buttonHeight + adj));
            SetLayoutFlags(this, AbsoluteLayoutFlags.None);

            RaiseChild(MainButton);
            raised = true;

            for (int i = 0; i < Children.Count - 1; i++)
            {
                Children[i].IsVisible = true;
                Children[i].TranslateTo(0, 0, (uint)time);
                Children[i].InputTransparent = false;
            }

        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == BGColorProperty.PropertyName)
            {
                MainButton.BGColor = BGColor;
            }
            else if (propertyName == OpenIconProperty.PropertyName)
            {
                if (!isRevealed)
                    MainButton.IconSrc = OpenIcon;
            }
            else if (propertyName == CloseIconProperty.PropertyName)
            {
                if (CloseIcon == default(string))
                    CloseIcon = OpenIcon;
                if (isRevealed)
                    MainButton.IconSrc = CloseIcon;
            }

        }
    }
}
