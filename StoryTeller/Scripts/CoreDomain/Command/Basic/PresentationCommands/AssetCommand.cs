using System;
namespace StoryTeller
{
    public class AssetCommand : ICommand
    {
        public string tag = String.Empty;
        public string source = String.Empty;
        public AssetCommand()
        {
        }

        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.AssetSource.AddTag(source, tag);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
