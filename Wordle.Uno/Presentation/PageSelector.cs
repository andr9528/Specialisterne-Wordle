using Wordle.Uno.Abstraction;

namespace Wordle.Uno.Presentation;

/// <summary>
/// Page navigation menu that hosts "regions" (pages) and swaps Content based on selection.
/// </summary>
public sealed partial class PageSelector : NavigationView
{
    private readonly IServiceProvider serviceProvider;
    private ListView menuList = null!;

    public PageSelector(IServiceProvider sp, IEnumerable<IPageRegion> regionDefinitions)
    {
        serviceProvider = sp ?? throw new ArgumentNullException(nameof(sp));

        IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
        IsPaneToggleButtonVisible = false;
        IsSettingsVisible = false;

        PaneDisplayMode = NavigationViewPaneDisplayMode.LeftCompact;
        CompactPaneLength = 200;

        var regions = CreateMenuList(regionDefinitions ?? throw new ArgumentNullException(nameof(regionDefinitions)));
        PaneCustomContent = menuList;

        // Default selection
        if (regions.Any())
        {
            menuList.SelectedIndex = 0;
        }
    }

    private List<IPageRegion> CreateMenuList(IEnumerable<IPageRegion> regionDefinitions)
    {
        var regions = regionDefinitions.ToList();

        menuList = new ListView
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 32, 32, 32)),
            SelectionMode = ListViewSelectionMode.Single,
            ItemsSource = regions,
            ItemTemplate = CreateMenuItemTemplate(),
            ItemContainerStyle = CreateMenuItemStyle(),
            Margin = new Thickness(10),
        };

        menuList.SelectionChanged += MenuList_SelectionChanged;
        return regions;
    }

    private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (menuList.SelectedItem is IPageRegion region)
        {
            DispatcherQueue.TryEnqueue(() => Content = region.CreateControl(serviceProvider));
        }
    }

    private DataTemplate CreateMenuItemTemplate()
    {
        return new DataTemplate(() =>
        {
            var grid = new Grid
            {
                Width = CompactPaneLength,
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var iconPresenter = new ContentPresenter
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };
            iconPresenter.SetBinding(ContentPresenter.ContentProperty, new Binding { Path = new PropertyPath(nameof(IPageRegion.Icon)) });

            var text = new TextBlock
            {
                Foreground = new SolidColorBrush(Colors.White),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(5, 0, 0, 0),
            };
            text.SetBinding(TextBlock.TextProperty, new Binding { Path = new PropertyPath(nameof(IPageRegion.DisplayName)) });

            grid.Children.Add(iconPresenter);
            Grid.SetColumn(iconPresenter, 0);

            grid.Children.Add(text);
            Grid.SetColumn(text, 1);

            return grid;
        });
    }

    private Style CreateMenuItemStyle()
    {
        var style = new Style(typeof(ListViewItem));
        style.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
        style.Setters.Add(new Setter(BorderThicknessProperty, new Thickness(0)));
        style.Setters.Add(new Setter(PaddingProperty, new Thickness(10)));
        style.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));
        style.Setters.Add(new Setter(ForegroundProperty, new SolidColorBrush(Colors.White)));
        return style;
    }
}
