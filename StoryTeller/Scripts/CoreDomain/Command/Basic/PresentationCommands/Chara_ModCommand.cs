using System;
namespace StoryTeller
{
    public class Chara_ModCommand : ICommand
    {
        public string name = String.Empty;
        public string face = String.Empty;
        public bool wait = false;
        public string region = String.Empty;
        public float time = 0.2f;
        public string size = String.Empty;

        public Chara_ModCommand()
        {
        }

        public bool ProceedInSameFrame
        {
            get { return !wait; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            if(!string.IsNullOrEmpty(face))
            {
                context.ActorPresenter.SetActorFace(name, face);
            }
            if(!string.IsNullOrEmpty(region))
            {
                context.ActorPresenter.TweenRegion(name, region, time, null);
                // context.ActorPresenter.SetRegion(name, region, time, wait);
            }

            if(!string.IsNullOrEmpty(size))
            {
                context.ActorPresenter.SetSize(name, size, time);
            }
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
