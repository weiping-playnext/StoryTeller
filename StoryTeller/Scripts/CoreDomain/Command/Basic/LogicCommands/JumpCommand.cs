using System;
using System.Collections.Generic;

namespace StoryTeller
{
    /*  
--------------

[doc]
tag=jump
group=シナリオ制御
title=別のシナリオ位置へジャンプ

[desc]
このタグの場所に到達するとfileとtargetで指定された場所へジャンプします

ジャンプ命令はcallスタックに残りません。つまり、return で指定位置に戻ることができません。
jump先では標準でcaller_index と caller_file という変数が格納されています。
これは、jumpした地点のファイルとindexを保持しています。
mp.caller_index と mp.caller_file を使うことで元の位置に戻ることが可能です

jumpには好きなパラメータを渡すことが可能です。
jump先ではmp.arg1 のような形で変数にアクセスすることが可能です。

scene=new とすることで、全く新しいシーンを新たに生成した上でジャンプすることができます。
まっさらな状態になるので、もう一度背景やキャラクター情報などを定義する必要があります。

場面の切り替わりなどではscene=newでjumpすることにより、不要なデータを一掃することで
健全な状態を保ってゲームを進めることができるできます。
ですので、定期的にscene=new でジャンプを行うことをオススメします。


[sample]

[jump taget=*test]
ここは無視される[p]

*test

ここにジャンプする。

[param]
file=移動するシナリオファイル名を指定します。省略された場合は現在のシナリオファイルと見なされます
target=ジャンプ先のラベル名を指定します。省略すると先頭から実行されます
index=内部的に保持しているゲーム進行状況の数値を指定することができます。
scene=new を指定すると、新しくシーンを作成した上でジャンプします。


[_doc]
--------------------
 */


    public class JumpCommand : ICommand
    {
        public string target = String.Empty;

        public string file = String.Empty;

        public int index = -1;

        bool next = true;

        public JumpCommand()
        {
        
        }

        public bool ProceedInSameFrame
        {
            get { return next; }
        }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            callStack.ClearIfStack();
            if(string.IsNullOrEmpty(file))
            {
                file = context.ScenarioRepository.GetCurrentRunningScenario().name;
            }
            file = ArgumentExpression.replaceVariable(file, context.VariableRepository);

            context.ScenarioRepository.JumpToScenario(file);

            target = ArgumentExpression.replaceVariable(target, context.VariableRepository);

            if(index < 0)
            {
                index = context.ScenarioRepository.GetScenario(file).getIndex(target);
            }
        }

        public int GetNextCommandIndex(int current)
        {
            return index;
        }

    }

}
