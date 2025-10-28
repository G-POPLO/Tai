
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UI.Base.Color;
using UI.Controls.Base;

namespace UI.Controls.Navigation
{
    public class NavigationItem : Control
    {
        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }
        public static   DependencyProperty IDProperty =
            DependencyProperty.Register("ID",
                typeof(int),
                typeof(NavigationItem));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static   DependencyProperty CommandProperty =
            DependencyProperty.Register("Command",
                typeof(ICommand),
                typeof(NavigationItem));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static   DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter",
                typeof(object),
                typeof(NavigationItem));
        public IconTypes Icon
        {
            get { return (IconTypes)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static   DependencyProperty IconProperty =
            DependencyProperty.Register("Icon",
                typeof(IconTypes),
                typeof(NavigationItem), new PropertyMetadata(IconTypes.None));

        public IconTypes SelectedIcon
        {
            get { return (IconTypes)GetValue(SelectedIconIconProperty); }
            set { SetValue(SelectedIconIconProperty, value); }
        }
        public static   DependencyProperty SelectedIconIconProperty =
            DependencyProperty.Register("SelectedIconIcon",
                typeof(IconTypes),
                typeof(NavigationItem), new PropertyMetadata(IconTypes.None));
        public ColorTypes IconColor
        {
            get { return (ColorTypes)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }
        public static   DependencyProperty IconColorProperty =
            DependencyProperty.Register("IconColor",
                typeof(ColorTypes),
                typeof(NavigationItem), new PropertyMetadata(ColorTypes.Blue));
        public SolidColorBrush IconColorBrush
        {
            get { return (SolidColorBrush)GetValue(IconColorBrushProperty); }
            set { SetValue(IconColorBrushProperty, value); }
        }
        public static   DependencyProperty IconColorBrushProperty =
            DependencyProperty.Register("IconColorBrush",
                typeof(SolidColorBrush),
                typeof(NavigationItem), new PropertyMetadata(UI.Base.Color.Colors.GetColor(ColorTypes.Blue)));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static   DependencyProperty TitleProperty =
            DependencyProperty.Register("Title",
                typeof(string),
                typeof(NavigationItem), new PropertyMetadata(string.Empty));

        public string BadgeText
        {
            get { return (string)GetValue(BadgeTextProperty); }
            set { SetValue(BadgeTextProperty, value); }
        }
        public static   DependencyProperty BadgeTextProperty =
            DependencyProperty.Register("BadgeText",
                typeof(string),
                typeof(NavigationItem), new PropertyMetadata(string.Empty));
        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
        public static   DependencyProperty UriProperty =
            DependencyProperty.Register("Uri",
                typeof(string),
                typeof(NavigationItem), new PropertyMetadata(string.Empty));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public static   DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected",
                typeof(bool),
                typeof(NavigationItem), new PropertyMetadata(false));

        public delegate void NavigationEventHandler(object sender, MouseButtonEventArgs e);
        public event NavigationEventHandler MouseUp;
        public NavigationItem()
        {
            DefaultStyleKey = typeof(NavigationItem);
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            MouseUp?.Invoke(this, e);
            if (e.ChangedButton == MouseButton.Left)
            {
                Command?.Execute(CommandParameter);
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            VisualStateManager.GoToState(this, "MouseOver", true);
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            VisualStateManager.GoToState(this, "Normal", true);
        }
    }
}
