﻿using System;
using System.Collections.Generic;

namespace StoryTeller
{
    public class ElseCommand : ISystemMacroCommand
    {
        public ElseCommand()
        {
        }

        public bool ProceedInSameFrame => throw new NotImplementedException();

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            throw new NotImplementedException();
        }

        public ICommand[] GetDerivedCommands(IList<ICommand> originalList)
        {
            int currentIndex = originalList.IndexOf(this);
            string expression = string.Empty;
            for(int i = currentIndex - 1; i >= 0; i--)
            {
                if(originalList[i] is IfCommand)
                {
                    expression = ((IfCommand)originalList[i]).exp;
                    break;
                }
            }
            return new ICommand[] { new EndifCommand(), new _InverseIfCommand() { MainExp = expression } };
        }

        public int GetNextCommandIndex(int current)
        {
            throw new NotImplementedException();
        }
    }
}
