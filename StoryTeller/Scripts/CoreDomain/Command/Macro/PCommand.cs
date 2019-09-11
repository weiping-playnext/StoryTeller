using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*
--------------

[doc]
tag=p
group=メッセージ関連
title=改ページクリック待ち

[desc]
改ページをともなうクリック待ちを行います

[sample]
テキスト表示[p]
１度クリックを待って[l]
２度めのクリックで改ページ[p]

[param]

[_doc]
--------------------
 */

    //改ページをいれて、クリックを待つ [p]
    public class PCommand : ISystemMacroCommand
    {
        public PCommand()
        {
        }

        public bool ProceedInSameFrame { get { return false; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
        }

        public ICommand[] GetDerivedCommands(IList<ICommand> originalList)
        {
            return new ICommand[] { new LCommand(), new CmCommand() };
        }

        public int GetNextCommandIndex(int current)
        {
            throw new NotImplementedException();
        }
    }
}
