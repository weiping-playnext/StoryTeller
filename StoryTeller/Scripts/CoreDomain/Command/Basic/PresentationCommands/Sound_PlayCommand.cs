using System;

namespace StoryTeller
{
    public class Sound_PlayCommand : ICommand
    {
        public string name;
        public bool wait = false;

        public bool ProceedInSameFrame
        {
            get { return !wait; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.AudioPlayer.PlaySound(name);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
