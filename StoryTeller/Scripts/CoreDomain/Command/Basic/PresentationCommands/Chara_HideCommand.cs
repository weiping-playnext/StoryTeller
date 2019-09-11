using System;
namespace StoryTeller
{
    public class Chara_HideCommand : ICommand
    {
        public string name = String.Empty;
        public float time = 0.2f;
        public bool wait = false;
        public Chara_HideCommand()
        {
        }

        public bool ProceedInSameFrame { get { return !wait; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.ActorPresenter.HideActor(name, time, wait);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
            //throw new NotImplementedException();
        }
    }
}
