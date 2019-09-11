using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*      
--------------

[doc]
tag=talk_name
group=システム関連
title=発言者欄の変更

[desc]

発言者欄の名前を変更します。
chara_newでjnameを定義している場合はその値が採用されます。

このタグは省略形が用意されています
以下の２つは同じ意味になります。

#yuko

[talk_name val=yuko ]


[sample]

@talk_name val=優子

以下のようにも書けます

#優子
優子がしゃべってます。

@talk_name val=""
消したい場合は空白を指定します

[param]
val=名前を表示します。キャラクター情報と絡めたい場合はchara_newした時のnameを指定してください。


[_doc]
--------------------
 */

    //メッセージを表示する
    public class Talk_NameCommand : ICommand
    {
        public string val = String.Empty;

        public Talk_NameCommand()
        {
        }

        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            string show_name = val;
            context.ActorPresenter.SetFocus(val);
            show_name = ArgumentExpression.replaceVariable(val, context.VariableRepository);
            //show_name = context.VariableRepository.GetString(val);
            context.MessagePresenter.SetTokenName(show_name);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
