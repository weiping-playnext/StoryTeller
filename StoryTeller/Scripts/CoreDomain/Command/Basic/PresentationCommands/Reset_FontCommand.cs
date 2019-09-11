using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller
{
    public class Reset_FontCommand : ICommand
    {
        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.MessagePresenter.ResetFont();
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}