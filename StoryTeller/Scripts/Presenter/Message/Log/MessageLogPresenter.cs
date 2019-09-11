using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace StoryTeller.Presentation
{
    public class MessageLogPresenter : MonoBehaviour
        , IMessageLogPresenter
        , ILogger
        , System.IObserver<IList<ILogEntry>>
    {
        [SerializeField] MessageLogView messageLogView = null;

        IDisposable subscription = null;
        MessageLog messageLog = new MessageLog();

        IMessageFormatter messageFormatter = new MessageMarkupFormatter();

        public void HideLog()
        {
            messageLogView.Hide();
        }

        public void LogMessage(ILogEntry logEntry)
        {
            messageLog.AddEntry(logEntry);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(IList<ILogEntry> value)
        {
            messageLogView.ClearAllEntries();
            foreach (var logEntry in value)
            {
                var entryView = messageLogView.CreateNewEntry();
                entryView.Speaker = logEntry.Speaker;
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var msgBuff in logEntry.MessageBuffers)
                {
                    stringBuilder.Append(messageFormatter.Format(msgBuff.Message, msgBuff.FontData));
                }
                entryView.Message = stringBuilder.ToString();
            }
        }

        public void ShowLog()
        {
            Debug.Log("show log");
            messageLogView.Show();
        }

        // Start is called before the first frame update
        void Start()
        {
            subscription = messageLog.Subscribe(this);
        }

        void OnDestroy()
        {
            if (subscription != null)
            {
                subscription.Dispose();
            }
        }
    }
}