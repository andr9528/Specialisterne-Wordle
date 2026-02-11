using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component.UserInterface;

public sealed class GuessLineUserInterface
{
    private readonly GuessLineLogic logic;
    private readonly GuessLineViewModel viewModel;

    public GuessLineUserInterface(GuessLineLogic logic, GuessLineViewModel viewModel)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public UIElement CreateContent()
    {
        var panel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 6,
            HorizontalAlignment = HorizontalAlignment.Center,
        };

        foreach (var vm in viewModel.Characters)
        {
            var tile = new CharacterIndicator(vm)
            {
                Width = 44,
                Height = 44,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(6),
                Margin = new Thickness(0),
            };

            panel.Children.Add(tile);
        }

        return panel;
    }
}
