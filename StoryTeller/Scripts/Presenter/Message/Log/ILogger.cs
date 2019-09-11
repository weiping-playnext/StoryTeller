using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller.Presentation
{
    public interface ILogEntry
    {
        int ID { get; }
        string Speaker { get; }
        MessageBuffer[] MessageBuffers { get; }
    }

    public interface ILogger
    {
        void LogMessage(ILogEntry logEntry);
        void ShowLog();
        void HideLog();
    }
}