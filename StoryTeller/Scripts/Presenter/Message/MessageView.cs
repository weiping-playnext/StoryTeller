using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StoryTeller.Presentation
{
    public class MessageView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Text tokenNameText = null;
        [SerializeField] MessageRenderer messageRenderer = null;
        [SerializeField] GameObject pagingIndicator = null;
        [SerializeField] Animator MessageShake = null;

            List<IMessageViewEventListener> eventListeners = new List<IMessageViewEventListener>();

        public Text TokenNameText { get { return tokenNameText; } }
        public MessageRenderer MessageRenderer { get { return messageRenderer; } }

        public GameObject PagingIndicator { get { return pagingIndicator; } }
        
        public bool isPlaying
        {
            get { return MessageShake.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f; }
        }

        public void AddEventListener(IMessageViewEventListener listener)
        {
            eventListeners.Add(listener);
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            eventListeners.ForEach(l => l.OnClick());
        }

        public void PlayShakeAnimation()
        {
            MessageShake.Play("MessageShake");
        }
    }
}