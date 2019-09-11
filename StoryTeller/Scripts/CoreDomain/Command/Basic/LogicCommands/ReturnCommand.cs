using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*  
--------------

[doc]
tag=return
group=シナリオ制御
title=サブルーチンから戻る

[desc]
サブルーチンから呼び出し元に戻ります。
return時にfileとtargetを指定することでスタックを消費した上で
任意の場所に戻ることもできます。

[sample]

[call taget=*test]
サブルーチンが終わるとここに戻ってきます[p]

*test

ここにジャンプする。

[return]

[param]
file=サブルーチンの呼び出し元に戻らずに、指定したファイルへ移動することできます。
target=サブルーチンの呼び出し元に戻らずに、指定したラベルへ移動することできます。


[_doc]
--------------------
 */

    public class ReturnCommand : ICommand
    {
        int index = 0;

        public ReturnCommand()
        {
        
        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            if (callStack.Count > 0)
            {
                var stackItem = callStack.Pop();
                context.ScenarioRepository.JumpToScenario(stackItem.scenarioNname);
                index = stackItem.index;
            }
        }

        public int GetNextCommandIndex(int current)
        {
            return index + 1;
        }
    }
}
