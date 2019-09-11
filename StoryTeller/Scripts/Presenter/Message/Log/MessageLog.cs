using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller.Presentation
{
    public interface IMessageLog
    {
        IList<ILogEntry> Entries { get; }
    }

    public class MessageLog : IMessageLog, System.IObservable<IList<ILogEntry>>
    {
        internal class DisposableEvent : IDisposable
        {
            IObserver<IList<ILogEntry>> observer;
            MessageLog messageLog;

            internal DisposableEvent(MessageLog messageLog, IObserver<IList<ILogEntry>> observer)
            {
                this.observer = observer;
                this.messageLog = messageLog;
            }

            public void Dispose()
            {
                messageLog.RemoveObserver(observer);
            }
        }

        public IList<ILogEntry> Entries { get; set; }

        List<ILogEntry> logEntries = new List<ILogEntry>();
        List<IObserver<IList<ILogEntry>>> observerList = new List<IObserver<IList<ILogEntry>>>();

        public void AddEntry(ILogEntry logEntry)
        {
            if(!logEntries.Exists((ILogEntry obj) => obj.ID == logEntry.ID))
            {
                logEntries.Add(logEntry);
                observerList.ForEach((obj) => { if (obj != null) obj.OnNext(logEntries); });
            }
        }

        internal void RemoveObserver(IObserver<IList<ILogEntry>> observer)
        {
            observerList.Remove(observer);
        }

        public IDisposable Subscribe(IObserver<IList<ILogEntry>> observer)
        {
            observerList.Add(observer);
            return new DisposableEvent(this, observer);
        }
    }
}