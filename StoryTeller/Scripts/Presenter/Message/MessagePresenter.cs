using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace StoryTeller.Presentation
{

    public class MessagePresenter : MonoBehaviour,
        IMessagePresenter,
        IAutoPlayer,
        IMessageViewEventListener,
        IStoryContextInjectable
    {
        [SerializeField] MessageView messageView = null;

        List<MessageBuffer> messageBuffer = new List<MessageBuffer>();
        MessageTypingEffectAnimator typingEffectAnimator;

        int renderBufferCount = 0;
        int logID = 0;

        FontOverrideData currentFontData = FontOverrideData.Default;
        bool autoplayTriggered = false;

        IMessageFormatter messageFormatter = new MessageMarkupFormatter();
        ISystemSound systemSound = null;
        IAudioPlayer audioPlayer = null;
        IStoryPlayer storyPlayer = null;

        ILogger messageLogger = null;
        string currentTokenName = string.Empty;
        List<MessageBuffer> logEntryBuffer = new List<MessageBuffer>();

        void Awake()
        {
            messageView.gameObject.SetActive(false);
            messageView.AddEventListener(this);
            typingEffectAnimator = new MessageTypingEffectAnimator(
                messageView.MessageRenderer,
                messageFormatter,
                messageView.PagingIndicator,
                this);
            typingEffectAnimator.Clear();
        }

        public bool Interactable { get; set;}
        public bool Autoplay { get; set; }

        public void Inject(IStoryContext context)
        {
            systemSound = context.SystemSound;
            audioPlayer = context.AudioPlayer;
            storyPlayer = context.StoryPlayer;
            messageLogger = context.MessageLogger;
        }

        public void NewLine()
        {
            var buffer = new MessageBuffer() { Message = System.Environment.NewLine, FontData = currentFontData };
            messageBuffer.Add(buffer);
            logEntryBuffer.Add(buffer);
        }

        public void ClearMessage()
        {
            if (!(logEntryBuffer.Count == 0))
            {
                var logEntry = new MessageLogEntry() { Speaker = currentTokenName, MessageBuffers = logEntryBuffer.ToArray(), ID = ++logID };
                messageLogger.LogMessage(logEntry);
                logEntryBuffer.Clear();
            }
            messageBuffer.Clear();
            renderBufferCount = 0;
            typingEffectAnimator.Clear();
        }

        public void RenderMessage(string message)
        {
            var buffer = new MessageBuffer() { Message = message, FontData = currentFontData };
            messageBuffer.Add(buffer);
            logEntryBuffer.Add(buffer);
        }

        public void ShowMessage(float time)
        {
            messageView.gameObject.SetActive(true);
        }

        void Update()
        {
            if (storyPlayer != null)
            {
                if (!typingEffectAnimator.IsPlaying && !storyPlayer.IsFinished && Interactable && Autoplay && !autoplayTriggered)
                {
                    autoplayTriggered = true;
                    StartCoroutine(AutoPlayAction());
                }
            }
        }

        IEnumerator AutoPlayAction()
        {
            yield return new WaitForSeconds(.5f);
            OnClick();
            autoplayTriggered = false;
        }

        void LateUpdate()
        {
            if (messageBuffer.Count != renderBufferCount)
            {
                messageView.gameObject.SetActive(true);
                typingEffectAnimator.SetMessageBuffers(messageBuffer);
                typingEffectAnimator.Play();
                renderBufferCount = messageBuffer.Count;
            }
        }

        public void HideMessage(float time)
        {
            messageView.gameObject.SetActive(false);
        }

        public void SetMessageSpeed(float speed)
        {

        }

        public void SetTokenName(string tokenName)
        {
            if (!(logEntryBuffer.Count == 0))
            {
                var logEntry = new MessageLogEntry() { Speaker = currentTokenName, MessageBuffers = logEntryBuffer.ToArray(), ID = ++logID };
                messageLogger.LogMessage(logEntry);
                logEntryBuffer.Clear();
            }
            currentTokenName = tokenName;
            messageView.TokenNameText.text = tokenName;
        }

        public void OnClick()
        {
            if (Interactable)
            {
                if (typingEffectAnimator.IsPlaying)
                {
                    typingEffectAnimator.ForceComplete();
                }
                else
                {
                    if (systemSound != null)
                    {
                        systemSound.PlayClickSound();
                    }
                    if (audioPlayer != null)
                    {
                        audioPlayer.StopSound();
                    }
                    storyPlayer.Play();
                }
            }
        }

        public void Shake(bool wait)
        {
            if (!messageView.isActiveAndEnabled)
            {
                return;
            }

            if (wait)
            {
                StartCoroutine(_shake());
            }
            else
            {
                messageView.PlayShakeAnimation();
            }
        }

        public IEnumerator _shake()
        {
            Interactable = false;
            messageView.PlayShakeAnimation();
            while (messageView.isPlaying)
            {
                yield return null;
            }

            Interactable = true;
        }

        public void SetFont(FontOverrideData font)
        {
            currentFontData = font;
        }

        public void ResetFont()
        {
            currentFontData = FontOverrideData.Default;
        }
    }
}
