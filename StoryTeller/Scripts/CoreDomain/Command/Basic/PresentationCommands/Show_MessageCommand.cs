using System;
namespace StoryTeller
{
    public class Show_MessageCommand : ICommand
    {
        public Show_MessageCommand()
        {
        }

        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.MessagePresenter.ShowMessage(0f);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
