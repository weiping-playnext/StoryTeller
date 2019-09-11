using System;
namespace StoryTeller
{
    public class Bg_NewCommand : ICommand
    {
        public string name = String.Empty;
        public string tag = String.Empty;
        public string asset = String.Empty;
        public string source = String.Empty;

        public Bg_NewCommand()
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
            if (useExternalSource)
            {
                context.ScenePresenter.RegisterBackgroundFromAssetSource(name, tag, asset);
            }
            else
            {
                context.ScenePresenter.RegisterBackgroundFromExternalSource(name, tag, source);
            }
            //context.ScenePresenter.ShowBackground(tag);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
