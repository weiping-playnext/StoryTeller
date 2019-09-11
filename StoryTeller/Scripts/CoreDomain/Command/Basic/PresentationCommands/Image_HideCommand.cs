using System;

namespace StoryTeller
{
    public class Image_HideCommand : ICommand
    {
        public string name = String.Empty;
        public string tag = String.Empty;

        public Image_HideCommand()
        {
        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            string tagToUse = string.IsNullOrEmpty(tag) ? name : tag;
            context.ScenePresenter.HideImage(tagToUse);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
