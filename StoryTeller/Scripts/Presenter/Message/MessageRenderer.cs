using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryTeller.Presentation
{
    public class MessageRenderer : MonoBehaviour, IMessageRenderer
    {
        [SerializeField] Text messageText = null;

        IMessageHyphenator hyphenator = new MockJapaneaseHyphenator();

        string hyphenatedMessage;
        int renderLength;
        int actuallyLength;

        public void SetMessage(string message)
        {
            hyphenatedMessage = hyphenator.Hyphenation(message);
            Render(); 
        }

        // TODO: message内のタグが絡む場合の計算をコンポーネント化する
        public int ActuallyLength
        {
            get
            {
                return hyphenatedMessage.Length; 
            }
        }

        public int RenderLength
        {
            set { renderLength = value; Render(); }
            get { return renderLength; }
        }

        void Render()
        {
            // TODO: タグの処理
            messageText.text = hyphenatedMessage;//.Substring(0, renderLength);
        }
    }
}