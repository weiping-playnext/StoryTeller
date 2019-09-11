using System;
using System.Collections.Generic;

namespace StoryTeller
{
/*

[doc]
tag=story
title=テキスト表示
group=メッセージ関連

[desc]
テキストを表示するタグです。
通常シナリオはタグを使用せずに記述しますが
タグを使用することも可能です

[sample]

;下記２つは全く同一の動作
ストーリーを記述[p]
[story val="ストーリーを記述"]

[param]

val=表示するテキストを指定します

[_doc]

 */
    //IComponentTextはテキストを流すための機能を保持するためのインターフェース
    public class StoryCommand : ICommand
    {
        string val = String.Empty;

        public StoryCommand()
        {

        }

        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStacks, IStoryContext context)
        {
            val = ArgumentExpression.replaceVariable(val, context.VariableRepository);
            context.MessagePresenter.RenderMessage(val);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }

        public string getText()
        {
            return val;
        }
    }
}
