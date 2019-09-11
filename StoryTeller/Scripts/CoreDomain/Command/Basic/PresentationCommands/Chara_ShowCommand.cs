using System;
namespace StoryTeller
{
    public class Chara_ShowCommand : ICommand
    {
        public string name = String.Empty;
        public float time = .2f;
        public bool wait = false;
        public string face = String.Empty;
        public string region = String.Empty;
        public string size = String.Empty;

        public Chara_ShowCommand()
        {
        }

        public bool ProceedInSameFrame { get { return !wait; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            if (!string.IsNullOrEmpty(face))
            {
                context.ActorPresenter.SetActorFace(name, face);
            }
            if(!string.IsNullOrEmpty(size))
            {
                context.ActorPresenter.SetSize(name, size, time);
            }
            context.ActorPresenter.SetRegion(name, region);
            context.ActorPresenter.ShowActor(name, time, wait);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
