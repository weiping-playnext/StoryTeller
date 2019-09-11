using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*      
--------------

[doc]
tag=wait
group=システム関連
title=ウェイトを入れる

[desc]

ウェイトを入れます。time属性で指定した時間、操作できなくなります。

[sample]

;２.5秒間　処理を停止します
[wait time=2.5]

[param]
time=停止する時間を秒で指定します


[_doc]
--------------------
 */

    //メッセージを表示する
    public class WaitCommand : ICommand
    {
        public float time = 0.2f;
        public WaitCommand()
        {
        }

        public bool ProceedInSameFrame { get { return false; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.SystemPresenter.Wait(time);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }

}
