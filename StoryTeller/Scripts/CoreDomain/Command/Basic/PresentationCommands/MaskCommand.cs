using System;
using UnityEngine;
namespace StoryTeller
{
    /// <summary>
    /// [mask]　スクリーンマスク表示
    /// レイヤ関連
    /// ゲーム画面全体を豊富な効果とともに暗転させることができます。
    /// 暗転中にゲーム画面を再構築して[mask_off] タグでゲームを再開する使い方ができます。
    /// </summary>
    public class MaskCommand : ICommand
    {
        public float time = .2f;
        public string color = "#000000";
        public bool wait = false;

        public MaskCommand()
        {
        }

        public bool ProceedInSameFrame { get { return !wait; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            Color maskColor = Color.black;
            ColorUtility.TryParseHtmlString(color, out maskColor);
            context.ScenePresenter.MaskOn(time, maskColor, wait);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}
