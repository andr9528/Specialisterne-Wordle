using Wordle.Uno.Presentation.Component.Logic;
using Wordle.Uno.Presentation.Component.ViewModel;

namespace Wordle.Uno.Presentation.Component.UserInterface;

public sealed class GuessScrollViewUserInterface
{
    private readonly GuessScrollViewLogic logic;
    private readonly GuessScrollViewViewModel viewModel;

    public GuessScrollViewUserInterface(GuessScrollViewLogic logic, GuessScrollViewViewModel viewModel)
    {
        this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
        this.viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }

    public UIElement CreateContent()
    {
        var stack = new StackPanel { Spacing = 8 };

        for (int i = 0; i < viewModel.MaxGuesses; i++)
        {
            stack.Children.Add(new GuessLine(new GuessLineViewModel(i +1, viewModel.WordLength)));
        }

        return new ScrollViewer
        {
            Content = stack,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
        };
    }
}
