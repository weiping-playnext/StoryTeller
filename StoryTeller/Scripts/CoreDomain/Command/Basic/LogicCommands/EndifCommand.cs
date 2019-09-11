using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*      
--------------

[doc]
tag=endif
group=シナリオ関連
title=if文を終了します

[desc]

if文を終了します。必ずif文の終わりに記述する必要があります

[sample]


[param]


[_doc]
--------------------
 */

    public class EndifCommand : ILogicControlCommand
    {
        public EndifCommand()
        {

        }

        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            callStack.PopIfStack();
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }


}
