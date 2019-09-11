using System;
using System.Collections.Generic;

namespace StoryTeller
{


    public class LabelCommand : ICommand
    {

        public string name = String.Empty;

        public LabelCommand()
        {

        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStacks, IStoryContext context)
        {
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
