using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*
 --------------

 [doc]
 tag=r
 group=メッセージ関連
 title=改行する

 [desc]
 改行をします。

 [sample]

 テキスト表示[r]
 ２行目テキスト表示[r]
 ３行目テキスト表示[r]

 [param]

 [_doc]
 --------------------
  */

    //改行命令 [r]
    public class RCommand : ICommand
    {
        public RCommand()
        {

        }

        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStacks, IStoryContext context)
        {
            context.MessagePresenter.NewLine();
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
