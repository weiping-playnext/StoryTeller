using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*      
--------------

[doc]
tag=clearvar
group=システム関連
title=変数を削除。

[desc]

変数を削除します。


[sample]

[calc exp="tmp.val=2"]
[flag exp="tmp.name=シケモク"]

;tmpの中身を確認できる
[trace exp="tmp"]

;tmp変数をすべて初期化します
[clearvar name="tmp"]

[param]
name=削除する変数名を指定してください。


[_doc]
--------------------
 */


    //変数クリア
    public class ClearvarCommand : ICommand
    {
        public string name;
        public ClearvarCommand ()
        {
        }

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            context.VariableRepository.RemoveString(name);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
