using System;
using UnityEngine;

namespace StoryTeller
{
    public class Bgm_NewCommand : ICommand
    {
        public string name = String.Empty;
        public string asset = String.Empty;

        public Bgm_NewCommand()
        {
        }

        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.AudioPlayer.AddBgmAudioSource(name, asset);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
