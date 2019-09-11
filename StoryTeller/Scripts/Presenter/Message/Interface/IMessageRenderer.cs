using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller.Presentation
{
    public interface IMessageRenderer
    {
        void SetMessage(string message);
        int ActuallyLength { get; }
        int RenderLength { set; get; }
    }
}
