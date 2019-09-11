namespace StoryTeller
{
    public class Message_ShakeCommand : ICommand
    {
        public bool wait = false;
        
        public Message_ShakeCommand()
        {
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.MessagePresenter.Shake(wait);
        }

        public bool ProceedInSameFrame { get { return !wait; } }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}