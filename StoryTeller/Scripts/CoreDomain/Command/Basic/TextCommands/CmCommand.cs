using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*
--------------

[doc]
tag=cm
group=メッセージ関連
title=改ページクリック無し

[desc]
このタグに到達した時点でメッセージをクリアします

[sample]

テキスト表示[p]
ここはクリック待たないで消える[cm]
ここはクリック待ち[p]

[param]

[_doc]
--------------------
 */

    public class CmCommand : ICommand
    {
        public CmCommand()
        {
        }

        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.MessagePresenter.ClearMessage();
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }

}
