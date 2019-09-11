using System;

namespace StoryTeller
{
    public class Image_ShowCommand : ICommand
    {
        public string name = String.Empty;
        public string tag = String.Empty;

        public Image_ShowCommand()
        {
        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            string tagToUse = string.IsNullOrEmpty(tag) ? name : tag;
            context.ScenePresenter.ShowImage(tagToUse);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
