using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller.Presentation
{
    public class MockJapaneaseHyphenator : IMessageHyphenator
    {
        public string Hyphenation(string message)
        {
            return message;
        }
    }
}
