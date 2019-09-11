using System;
using System.Collections.Generic;

namespace StoryTeller
{
    public interface ICommand
    {
        void Execute(ICallStack callStack, IStoryContext context);
        bool ProceedInSameFrame { get; }
        int GetNextCommandIndex(int current);
    }
}
