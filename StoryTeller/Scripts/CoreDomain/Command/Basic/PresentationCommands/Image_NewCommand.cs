using System;

namespace StoryTeller
{
    public class Image_NewCommand : ICommand
    {
        public string name = String.Empty;
        public string tag = String.Empty;
        public string asset = String.Empty;
        public string source = String.Empty;

        public Image_NewCommand()
        {
        }

public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            bool useExternalSource = string.IsNullOrEmpty(source);

            if (string.IsNullOrEmpty(name))
            {
                name = useExternalSource ? source : asset;
            }
            if (string.IsNullOrEmpty(tag))
            {
                tag = name;
            }
            
            context.ScenePresenter.RegisterImageFromAssetSource(name, tag, asset);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
