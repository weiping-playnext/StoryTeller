using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*  
--------------

[doc]
tag=eval
group=システム関連
title=数式の評価

[desc]
expで示された式を評価します。変数への値の代入などに使用されます。
文字列はこのタグでは扱うことはできません。文字列は[flag]タグを使用します

[sample]


[eval exp="f.test=500"]
;↑変数 test に数値を代入している

[eval exp="sf.test2=400"]
;↑システム変数 test に数値を代入している

[eval exp="f.test2={f.test}*3"]
;↑ゲーム変数 test2 に ゲーム変数 test の 3 倍の数値を代入している

{f.test2}[p]


[param]
exp=数式を指定します


[_doc]
--------------------
 */

    public class EvalCommand : ICommand
    {
        public string exp;

        public EvalCommand()
        {

        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {

            ArgumentExpression eo = new ArgumentExpression(exp);

            string result = ArgumentExpression.Calculate(eo.exp);
            context.VariableRepository.SetString(eo.name, result);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
