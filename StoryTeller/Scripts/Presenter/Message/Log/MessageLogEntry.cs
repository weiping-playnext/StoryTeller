using System;
namespace StoryTeller.Presentation
{
    public class MessageLogEntry : ILogEntry
    {
        public string Speaker { get; set; }

        public MessageBuffer[] MessageBuffers { get; set; }

        public int ID { get; set; }
    }
}
