using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*      
--------------

[doc]
tag=hidemessage
group=システム関連
title=メッセージ非表示

[desc]

メッセージウィンドウを非表示にします。
[showmessage]を明示的に実行するまで表示されません。

[sample]

[hidemessage]

[wait time=5 ]

[showmessage]
シナリオ再開

[param]


[_doc]
--------------------
*/

    //メッセージを削除する showMessage を行わないと表示されない
    public class Hide_MessageCommand : ICommand
    {
        public float time = 0.5f;

        public Hide_MessageCommand()
        {

        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.MessagePresenter.HideMessage(time);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
