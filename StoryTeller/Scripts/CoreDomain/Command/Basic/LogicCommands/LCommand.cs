using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*
--------------

[doc]
tag=l
group=メッセージ関連
title=クリック待ち

[desc]
このタグの位置でクリック待ちを行います

[sample]
いち[l]に[l]さん[l][r]


[param]

[_doc]
--------------------
*/

    public class LCommand : ICommand
    {
        public LCommand()
        {

        }

        public bool ProceedInSameFrame
        {
            get { return false; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.MessagePresenter.Interactable = true;
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
