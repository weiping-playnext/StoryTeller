using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller.Presentation
{
    public interface IMessageHyphenator
    {
        string Hyphenation(string message);
    }
}
