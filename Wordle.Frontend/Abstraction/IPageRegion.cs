namespace Wordle.Frontend.Abstraction;

public interface IPageRegion
{
    /// <summary>
    /// Display name shown in the NavigationView.
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Icon for the NavigationView item (can be SymbolIcon, FontIcon, etc.).
    /// </summary>
    Microsoft.UI.Xaml.Controls.IconElement Icon { get; }

    /// <summary>
    /// Creates the region control (usually a Border wrapping the UI grid).
    /// </summary>
    Microsoft.UI.Xaml.UIElement CreateControl(IServiceProvider services);
}
