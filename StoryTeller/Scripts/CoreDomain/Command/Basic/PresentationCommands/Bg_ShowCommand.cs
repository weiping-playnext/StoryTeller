using System;

namespace StoryTeller
{
    public class Bg_ShowCommand : ICommand
    {
        public string name = String.Empty;
        public string tag = String.Empty;

        public Bg_ShowCommand()
        {
        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            string tagToUse = string.IsNullOrEmpty(tag) ? name : tag;
            context.ScenePresenter.ShowBackground(tagToUse);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
