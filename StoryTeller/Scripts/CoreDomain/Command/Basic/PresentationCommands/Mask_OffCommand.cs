using System;
namespace StoryTeller
{
    public class Mask_OffCommand : ICommand
    {
        public bool wait = false;
        public float time = .2f;
        public Mask_OffCommand()
        {
        }

        public bool ProceedInSameFrame { get { return !wait; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.ScenePresenter.MaskOff(time, wait);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
