using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*  
--------------

[doc]
tag=emb
group=メッセージ関連
title=変数の展開

[desc]

メッセージ中に変数の中身を展開して表示することができます。
省略形として{ } で括る方法もあります。

[sample]


[eval exp="f.value1='変数の値だよ～ん'"]
とどこかで書いておいて、
[emb exp="f.value1"]
と書くと、この emb タグが 変数の値だよ～ん という内容に置き換わります。

[param]
exp=評価する変数を格納します。


[_doc]
--------------------
 */

    public class EmbCommand : ICommand
    {
        public string exp;

        public EmbCommand()
        {

        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            string val = exp;
            val = context.VariableRepository.GetString(exp);
            context.MessagePresenter.RenderMessage(val);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }

}
