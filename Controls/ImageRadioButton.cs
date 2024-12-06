using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;

namespace Tests.Controls
{
    public static class RadioButtonGroup
    {
        public static readonly BindableProperty GroupNameProperty =
            BindableProperty.CreateAttached("GroupName", typeof(string), typeof(RadioButtonGroup), string.Empty, propertyChanged: OnGroupNameChanged);

        public static string GetGroupName(BindableObject view) => (string)view.GetValue(GroupNameProperty);
        public static void SetGroupName(BindableObject view, string value) => view.SetValue(GroupNameProperty, value);

        public static readonly BindableProperty SelectedValueProperty =
            BindableProperty.CreateAttached("SelectedValue", typeof(object), typeof(RadioButtonGroup), null, BindingMode.TwoWay, propertyChanged: OnSelectedValueChanged);

        public static object GetSelectedValue(BindableObject view) => view.GetValue(SelectedValueProperty);
        public static void SetSelectedValue(BindableObject view, object value) => view.SetValue(SelectedValueProperty, value);

        private static void OnGroupNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Layout<View> layout)
            {
                foreach (var child in layout.Children)
                {
                    if (child is ImageRadioButton radioButton)
                    {
                        radioButton.GroupName = (string)newValue;
                    }
                }
            }
        }

        private static void OnSelectedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Layout<View> layout)
            {
                foreach (var child in layout.Children)
                {
                    if (child is ImageRadioButton radioButton && radioButton.IsChecked)
                    {
                        radioButton.SelectedValue = newValue;
                    }
                }
            }
        }
    }
    public class ImageRadioButton : ContentView
    {
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(ImageRadioButton), false, BindingMode.TwoWay, propertyChanged: OnIsCheckedChanged);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ImageRadioButton), string.Empty);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(ImageRadioButton), default(ImageSource));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static readonly BindableProperty GroupNameProperty =
            BindableProperty.Create(nameof(GroupName), typeof(string), typeof(ImageRadioButton), string.Empty);

        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set => SetValue(GroupNameProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(object), typeof(ImageRadioButton), null);

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty SelectedValueProperty =
            BindableProperty.Create(nameof(SelectedValue), typeof(object), typeof(ImageRadioButton), null, BindingMode.TwoWay);

        public object SelectedValue
        {
            get => GetValue(SelectedValueProperty);
            set => SetValue(SelectedValueProperty, value);
        }

        private static void OnIsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ImageRadioButton)bindable;
            if ((bool)newValue)
            {
                control.SelectedValue = control.Value;
                var parent = control.Parent;
                while (parent != null && RadioButtonGroup.GetGroupName(parent) != control.GroupName)
                {
                    parent = parent.Parent;
                }
                if (parent != null)
                {
                    RadioButtonGroup.SetSelectedValue(parent, control.Value);
                }
            }
        }

        public ImageRadioButton()
        {
            var radioButton = new RadioButton();
            radioButton.SetBinding(RadioButton.IsCheckedProperty, new Binding(nameof(IsChecked), source: this));
            radioButton.SetBinding(RadioButton.GroupNameProperty, new Binding(nameof(GroupName), source: this));

            var image = new Image();
            image.SetBinding(Image.SourceProperty, new Binding(nameof(ImageSource), source: this));
            image.HorizontalOptions = LayoutOptions.Center;
            var label = new Label();
            label.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));
            label.HorizontalOptions = LayoutOptions.Center;

            var grid = new Microsoft.Maui.Controls.Grid
            {
                RowDefinitions =
                {
                    new RowDefinition(GridLength.Star),
                    new RowDefinition(GridLength.Star)
                }
            };
            grid.Add(image, 0, 0);
            grid.Add(label, 0, 1);
            
            var stackLayout = new HorizontalStackLayout
            {
                Children = { radioButton, grid }
            };
            Content = stackLayout;
        }
    }
}
