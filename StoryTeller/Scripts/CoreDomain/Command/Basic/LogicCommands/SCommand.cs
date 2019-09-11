using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*      
--------------

[doc]
tag=s
group=シナリオ関連
title=ゲームを停止する

[desc]

シナリオファイルの実行を停止します。
選択肢表示した直後などに配置して利用する方法があります。

[sample]

テストこの後はジャンプなどでsを飛び越える処理を記述して追う必要があります[p]
@jump target=label1

[s]

*label1
ジャンプで[s]を飛び越える

[param]


[_doc]
--------------------
*/

    public class SCommand : ICommand
    {
        public SCommand()
        {

        }

        public bool ProceedInSameFrame
        {
            get { return false; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.MessagePresenter.Interactable = false;
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
