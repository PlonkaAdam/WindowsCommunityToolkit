﻿// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System.Collections.Generic;
using System.Linq;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Microsoft.Toolkit.Uwp.UI.Controls
{
    /// <summary>
    /// The HamburgerMenu is based on a SplitView control. By default it contains a HamburgerButton and a ListView to display menu items.
    /// </summary>
    [TemplatePart(Name = "HamburgerButton", Type = typeof(Button))]
    [TemplatePart(Name = "ButtonsListView", Type = typeof(Windows.UI.Xaml.Controls.ListViewBase))]
    [TemplatePart(Name = "OptionsListView", Type = typeof(Windows.UI.Xaml.Controls.ListViewBase))]
    public partial class HamburgerMenu : ContentControl
    {
        private static bool? _isNavViewSupported;

        private Button _hamburgerButton;
        private Windows.UI.Xaml.Controls.ListViewBase _buttonsListView;
        private Windows.UI.Xaml.Controls.ListViewBase _optionsListView;

        private ControlTemplate _previousTemplateUsed;

        private NavigationView _navigationView;

        /// <summary>
        /// Gets the <see cref="NavigationView"/>. Returns null when device does not support
        /// NavigationView or when <see cref="UseNavigationViewWhenPossible"/> is set to false
        /// </summary>
        public NavigationView NavigationView
        {
            get { return _navigationView; }
        }

        private bool UsingNavView => UseNavigationViewWhenPossible && IsNavViewSupported;

        /// <summary>
        /// Gets a value indicating whether <see cref="NavigationView"/> is supported
        /// </summary>
        public static bool IsNavViewSupported => (bool)(_isNavViewSupported ??
            (_isNavViewSupported = ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 5)));

        /// <summary>
        /// Initializes a new instance of the <see cref="HamburgerMenu"/> class.
        /// </summary>
        public HamburgerMenu()
        {
            DefaultStyleKey = typeof(HamburgerMenu);
        }

        /// <summary>
        /// Override default OnApplyTemplate to capture children controls
        /// </summary>
        protected override void OnApplyTemplate()
        {
            if (!UsingNavView && PaneForeground == null)
            {
                PaneForeground = Foreground;
            }

            if (_hamburgerButton != null)
            {
                _hamburgerButton.Click -= HamburgerButton_Click;
            }

            if (_buttonsListView != null)
            {
                _buttonsListView.ItemClick -= ButtonsListView_ItemClick;
            }

            if (_optionsListView != null)
            {
                _optionsListView.ItemClick -= OptionsListView_ItemClick;
            }

            if (UsingNavView && _navigationView != null)
            {
                _navigationView.ItemInvoked -= NavigationViewItemInvoked;
                _navigationView.SelectionChanged -= NavigationViewSelectionChanged;
                _navigationView.Loaded -= NavigationViewLoaded;
            }

            _hamburgerButton = (Button)GetTemplateChild("HamburgerButton");
            _buttonsListView = (Windows.UI.Xaml.Controls.ListViewBase)GetTemplateChild("ButtonsListView");
            _optionsListView = (Windows.UI.Xaml.Controls.ListViewBase)GetTemplateChild("OptionsListView");

            if (UsingNavView)
            {
                _navigationView = (NavigationView)GetTemplateChild("NavView");

                if (_navigationView != null)
                {
                    _navigationView.ItemInvoked += NavigationViewItemInvoked;
                    _navigationView.SelectionChanged += NavigationViewSelectionChanged;
                    _navigationView.Loaded += NavigationViewLoaded;
                    _navigationView.MenuItemTemplateSelector = new HamburgerMenuNavViewItemTemplateSelector(this);

                    OnItemsSourceChanged(this, null);
                }
            }

            if (_hamburgerButton != null)
            {
                _hamburgerButton.Click += HamburgerButton_Click;
            }

            if (_buttonsListView != null)
            {
                _buttonsListView.ItemClick += ButtonsListView_ItemClick;
            }

            if (_optionsListView != null)
            {
                _optionsListView.ItemClick += OptionsListView_ItemClick;
            }

            base.OnApplyTemplate();
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var hm = d as HamburgerMenu;

            if (hm.UsingNavView && hm._navigationView != null)
            {
                var items = hm.ItemsSource as IEnumerable<object>;
                var options = hm.OptionsItemsSource as IEnumerable<object>;

                List<object> combined = new List<object>();

                if (items != null)
                {
                    foreach (var item in items)
                    {
                        combined.Add(item);
                    }
                }

                if (options != null)
                {
                    if (options.Count() > 0)
                    {
                        combined.Add(new NavigationViewItemSeparator());
                    }

                    foreach (var option in options)
                    {
                        combined.Add(option);
                    }
                }

                hm._navigationView.MenuItemsSource = combined;
            }
        }

        private static void OnUseNavigationViewWhenPossibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var menu = d as HamburgerMenu;

            if (menu.UseNavigationViewWhenPossible && HamburgerMenu.IsNavViewSupported)
            {
                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new System.Uri("ms-appx:///Microsoft.Toolkit.Uwp.UI.Controls/Themes/Generic.xaml");
                menu._previousTemplateUsed = menu.Template;
                menu.Template = dict["HamburgerMenuNavViewTemplate"] as ControlTemplate;
            }
            else if (!menu.UseNavigationViewWhenPossible &&
                     e.OldValue is bool oldValue &&
                     oldValue &&
                     menu._previousTemplateUsed != null)
            {
                menu.Template = menu._previousTemplateUsed;
            }
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var menu = d as HamburgerMenu;
            if (menu.UsingNavView)
            {
                menu._navigationView.SelectedItem = e.NewValue;
            }
        }

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var menu = d as HamburgerMenu;
            if (menu.UsingNavView)
            {
                var items = menu.ItemsSource as IEnumerable<object>;
                if (items != null)
                {
                    menu._navigationView.SelectedItem = (int)e.NewValue >= 0 ? items.ElementAt((int)e.NewValue) : null;
                }
            }
        }

        private static void OnSelectedOptionsIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var menu = d as HamburgerMenu;
            if (menu.UsingNavView)
            {
                var options = menu.ItemsSource as IEnumerable<object>;
                if (options != null)
                {
                    menu._navigationView.SelectedItem = (int)e.NewValue >= 0 ? options.ElementAt((int)e.NewValue) : null;
                }
            }
        }

        private void NavigationViewLoaded(object sender, RoutedEventArgs e)
        {
            _navigationView.Loaded -= NavigationViewLoaded;
            var hamburgerButton = _navigationView.FindDescendantByName("TogglePaneButton") as Button;

            if (hamburgerButton != null)
            {
                var templateBinding = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(HamburgerMenuTemplate)),
                    Mode = BindingMode.OneWay
                };

                var heightBinding = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(HamburgerHeight)),
                    Mode = BindingMode.OneWay
                };

                var widthBinding = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(HamburgerWidth)),
                    Mode = BindingMode.OneWay
                };

                var marginBinding = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(HamburgerMargin)),
                    Mode = BindingMode.OneWay
                };

                var foregroundMargin = new Binding()
                {
                    Source = this,
                    Path = new PropertyPath(nameof(PaneForeground)),
                    Mode = BindingMode.OneWay
                };

                hamburgerButton.SetBinding(Button.ContentTemplateProperty, templateBinding);
                hamburgerButton.SetBinding(Button.HeightProperty, heightBinding);
                hamburgerButton.SetBinding(Button.WidthProperty, widthBinding);
                hamburgerButton.SetBinding(Button.MarginProperty, marginBinding);
                hamburgerButton.SetBinding(Button.ForegroundProperty, foregroundMargin);
            }
        }

        private void NavigationViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                SelectedItem = null;
                SelectedIndex = -1;
                SelectedOptionsItem = null;
                SelectedOptionsIndex = -1;
            }
            else if (args.SelectedItem != null)
            {
                var items = ItemsSource as IEnumerable<object>;
                var options = OptionsItemsSource as IEnumerable<object>;
                if (items != null && items.Contains(args.SelectedItem))
                {
                    SelectedItem = args.SelectedItem;
                    SelectedIndex = items.ToList().IndexOf(SelectedItem);
                    SelectedOptionsItem = null;
                    SelectedOptionsIndex = -1;
                }
                else if (options != null && options.Contains(args.SelectedItem))
                {
                    SelectedItem = null;
                    SelectedIndex = -1;
                    SelectedOptionsItem = args.SelectedItem;
                    SelectedOptionsIndex = options.ToList().IndexOf(SelectedItem);
                }
            }
        }
    }
}
