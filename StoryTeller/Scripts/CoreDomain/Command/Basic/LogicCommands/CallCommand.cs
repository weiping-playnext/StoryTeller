using System;
using System.Collections.Generic;

namespace StoryTeller
{

    /*  
--------------

[doc]
tag=call
group=シナリオ制御
title=サブルーチンの呼び出し

[desc]
指定されたシナリオファイルの指定されたラベルで示される サブルーチンを呼び出します。
呼び出されたサブルーチンは、 return タグで 呼び出し元や任意の場所に戻ることができます


[sample]

[call taget=*test]
サブルーチンが終わるとここに戻ってきます[p]

*test

ここにジャンプする。

[return]

[param]
file=呼び出したいサブルーチンのあるのシナリオファイルを 指定します。省略すると、現在 のシナリオファイル内であると見なされます
target=呼び出すサブルーチンのラベルを指定します。省略すると、ファイルの先頭から実行されます。


[_doc]
--------------------
 */

    //Call は Jumpと同様に　ストレージを移動する。ただし、呼び出しは スタックトレースに保存され、return で元の位置に戻ります
    public class CallCommand : ICommand
    {
        public string target = String.Empty;
        public string file = String.Empty;
        public int index = 0;

        public CallCommand()
        {

        }

        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            callStack.Push();
            if (string.IsNullOrEmpty(file))
            {
                file = context.ScenarioRepository.GetCurrentRunningScenario().name;
            }
            file = ArgumentExpression.replaceVariable(file, context.VariableRepository);
            context.ScenarioRepository.JumpToScenario(file);
            target = ArgumentExpression.replaceVariable(target, context.VariableRepository);
            if (!string.IsNullOrEmpty(target))
            {
                index = context.ScenarioRepository.GetScenario(file).getIndex(target);
                if (index < 0)
                {
                    index = 0;
                }
            }
        }

        public int GetNextCommandIndex(int current)
        {
            return index;
        }
    }
}
