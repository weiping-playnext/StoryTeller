using System;

namespace StoryTeller
{
    public class Bgm_PlayCommand : ICommand
    {
        public string name;
        public bool wait = false;
        public int fade;

        public bool ProceedInSameFrame
        {
            get { return !wait; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.AudioPlayer.PlayBgm(name, fade);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
