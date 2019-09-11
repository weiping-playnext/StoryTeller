using System;

namespace StoryTeller
{
    public class Chara_ShakeCommand : ICommand
    {
        public bool wait = false;
        public string name = String.Empty;
        
        public Chara_ShakeCommand()
        {
        }
        
        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.ActorPresenter.ShakeActor(name, wait);
        }

        public bool ProceedInSameFrame { get { return !wait; } }
        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}