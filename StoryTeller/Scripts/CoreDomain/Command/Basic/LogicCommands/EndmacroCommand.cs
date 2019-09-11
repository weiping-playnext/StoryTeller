using System;
using System.Collections.Generic;
namespace StoryTeller
{
    /*  
--------------

[doc]
tag=endmacro
group=シナリオ制御
title=マクロの終端

[desc]

マクロ終了を表します

[sample]


[macro name="newtag"]
    新しいタグです。[p]
    {mp.arg1}という値が渡されました。   
[endmacro]


[_doc]
--------------------
 */


    //マクロを作成して管理する
    public class EndmacroCommand : ICommand
    {
        int jumpIndex;
        public EndmacroCommand()
        {

        }

        public bool ProceedInSameFrame { get { return true; } }


        public void Execute(ICallStack callStack, IStoryContext context)
        {
            var prevCall = callStack.Pop();
            jumpIndex = prevCall.index;
            //throw new NotImplementedException();
            context.ScenarioRepository.JumpToScenario(prevCall.scenarioNname);
        }

        public int GetNextCommandIndex(int current)
        {
            return jumpIndex + 1;
        }
    }
}
