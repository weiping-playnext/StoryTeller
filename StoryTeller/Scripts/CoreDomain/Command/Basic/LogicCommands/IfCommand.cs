using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*  
--------------

[doc]
tag=if
group=シナリオ関連
title=条件分岐

[desc]

式を評価し、その結果が true ( または 0 以外 ) ならば、 elsif・else・endif のいずれかまでにある
文章やタグを実行し、そうでない場合は無視します。

[sample]


; 例1
[if exp="false"]
ここは表示されない
[else]
ここは表示される
[endif]

; 例2
[if exp="false"]
ここは表示されない
[elsif exp="false"]
ここは表示されない
[else]
ここは表示される
[endif]

; 例3
[if exp="false"]
ここは表示されない
[elsif exp="true"]
ここは表示される
[else]
ここは表示されない
[endif]

; 例4
[if exp="true"]
ここは表示される
[elsif exp="true"]
ここは表示されない
[else]
ここは表示されない
[endif]


[param]
exp=評価する式を指定します。この式の結果が false ( または 0 な らば、elsif・else・endif タグまでの文章やタグが無視されます。

[_doc]
--------------------
 */

    public class IfCommand : ILogicControlCommand
    {
        public string exp = String.Empty;
        int nextIndex;

        public IfCommand()
        {

        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {

            bool result = bool.Parse(ArgumentExpression.Calculate(exp));
            bool resultingIf = callStack.ResultingIf;
            callStack.AddIfStack(result);
            if (result && resultingIf)
            {
                nextIndex = callStack.CurrentCommandIndex + 1;
            }
            else
            {
                var currentScenario = context.ScenarioRepository.GetCurrentRunningScenario();
                for (int i = callStack.CurrentCommandIndex + 1; i < currentScenario.Commands.Count; i++)
                {
                    if(typeof(ILogicControlCommand).IsAssignableFrom(currentScenario.Commands[i].GetType()))
                    {
                        nextIndex = i;
                        break;
                    }
                }
            }
        }

        public int GetNextCommandIndex(int current)
        {
            return nextIndex;
        }
    }

}
