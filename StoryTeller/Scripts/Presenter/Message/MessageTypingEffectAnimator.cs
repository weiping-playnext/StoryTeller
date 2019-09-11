using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Linq;

namespace StoryTeller.Presentation
{
    public class MessageTypingEffectAnimator
    {
        MonoBehaviour runner;
        IMessageRenderer messageRenderer;
        GameObject pagingIndicator;
        float speed = 1f;
        bool isPlaying = false;
        List<MessageBuffer> messageBuffers = new List<MessageBuffer>();
        int currentBufferIndex = 0;
        int currentRenderCharIndex = 0;
        int fullMessageLength = 0;
        int curMessageCharIndex = 0;
        Coroutine typingRoutine = null;
        IMessageFormatter messageFormatter;

        public MessageTypingEffectAnimator(
            IMessageRenderer messageRenderer,
            IMessageFormatter messageFormatter,
            GameObject pagingIndicator,
            MonoBehaviour runner)
        {
            this.messageRenderer = messageRenderer;
            this.messageFormatter = messageFormatter;
            this.pagingIndicator = pagingIndicator;
            this.runner = runner;
        }

        public bool IsPlaying
        {
            get
            {
                return isPlaying;

            }
            private set
            {
                isPlaying = value;
                pagingIndicator.SetActive(!isPlaying);
            }
        }

        public void SetMessage(string message)
        {
            messageRenderer.SetMessage(message);
        }

        public void SetMessageBuffers(IReadOnlyList<MessageBuffer> messageBuffers)
        {
            this.messageBuffers = new List<MessageBuffer>(messageBuffers);
            fullMessageLength = messageBuffers.Sum((arg) => arg.Message.Length);
        }

        string MarkupMessage(MessageBuffer messageBuffer, int renderLen)
        {
            string message = messageBuffer.Message.Substring(0, renderLen);
            return messageFormatter.Format(message, messageBuffer.FontData);
        }

        public void Play()
        {
            if (IsPlaying) return;
            IsPlaying = true;
            typingRoutine = runner.StartCoroutine(TypingEffectRoutine());
        }

        public void Clear()
        {
            messageRenderer.SetMessage(string.Empty);
            currentBufferIndex = 0;
            currentRenderCharIndex = 0;
            curMessageCharIndex = 0;
        }

        IEnumerator TypingEffectRoutine()
        {
            if(messageBuffers.Count < 1) { isPlaying = false; yield break; }
            MessageBuffer msgBuffer = messageBuffers[currentBufferIndex];
            string bufferedMessage = "";
            while (currentRenderCharIndex != fullMessageLength)
            {
                if(curMessageCharIndex == msgBuffer.Message.Length)
                {
                    currentBufferIndex++;
                    bufferedMessage += MarkupMessage(msgBuffer, msgBuffer.Message.Length);
                    if (currentBufferIndex == messageBuffers.Count) { isPlaying = false; yield break; }
                    msgBuffer = messageBuffers[currentBufferIndex];
                    curMessageCharIndex = 0;
                }
                currentRenderCharIndex++;
                curMessageCharIndex++;
                SetMessage(bufferedMessage + MarkupMessage(msgBuffer,curMessageCharIndex));
                yield return new WaitForSeconds(0.032f * speed * 2);
            }
            bufferedMessage += MarkupMessage(msgBuffer, msgBuffer.Message.Length);
            SetMessage(bufferedMessage);
            IsPlaying = false;
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        public void ForceComplete()
        {
            if(typingRoutine != null)
            {
                runner.StopCoroutine(typingRoutine);
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var mb in messageBuffers)
            {
                stringBuilder.Append(MarkupMessage(mb, mb.Message.Length));
            }
            IsPlaying = false;
            SetMessage(stringBuilder.ToString());
        }
    }
}
