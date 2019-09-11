using System;
namespace StoryTeller
{
    public class Select_ShowCommand : ICommand
    {
        public Select_ShowCommand()
        {
        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.SelectionPresenter.ShowSelections();
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
