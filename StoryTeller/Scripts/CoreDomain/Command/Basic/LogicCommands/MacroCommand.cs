using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*  
--------------

[doc]
tag=macro
group=シナリオ制御
title=マクロ定義

[desc]

マクロ記述を開始します。新しいタグを定義することが出来ます。
このタグから、endmacro タグまでにある文章やタグは
name 属性で指定されたタグとして登録され、以後使用できるようになります。

マクロには値を渡すことができます。渡された変数には mp という変数に格納され、アクセスすることが可能です。

[sample]



[macro name="newtag"]
    新しいタグです。[p]
    {mp.arg1}という値が渡されました。   
[endmacro]

[newtag arg1="テスト"]

[param]
name=ラベル名を指定してください


[_doc]
--------------------
 */

    //マクロを作成して管理する
    public class MacroCommand : ICommand
    {
        public string name = String.Empty;
        int endIndex;

        public MacroCommand ()
        {

        }

        public bool ProceedInSameFrame
        {
            get { return true; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            int currentIndex = callStack.CurrentCommandIndex;
            Scenario currentScenario = context.ScenarioRepository.GetCurrentRunningScenario();
            Macro macro = new Macro(name, currentScenario.name, currentIndex);
            context.ScenarioMacroRepository.RecordMacro(macro);
            for (int i = currentIndex; i < currentScenario.Commands.Count; i++)
            {
                ICommand command = currentScenario.Commands[i];
                if(command is EndmacroCommand)
                {
                    endIndex = i;
                }
            }
        }

        public int GetNextCommandIndex(int current)
        {
            return endIndex + 1;
        }
    }
}
