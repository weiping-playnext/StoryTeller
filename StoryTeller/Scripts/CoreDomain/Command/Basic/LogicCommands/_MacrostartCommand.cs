using System;
using System.Collections.Generic;

namespace StoryTeller
{

    //マクロを実行するためのタグ
    public class _MacrostartCommand : ICommand
    {
        public string name;

        int nextIndex;
        public _MacrostartCommand()
        {
        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            nextIndex = callStack.Push();
            var macro = context.ScenarioMacroRepository.GetMacro(name);
            if(macro != null)
            {
                nextIndex = macro.index;
                context.ScenarioRepository.JumpToScenario(macro.file_name);
            }
            else
            {
                nextIndex += 1;
            }
        }

        public int GetNextCommandIndex(int current)
        {
            return nextIndex;
        }
    }

}
