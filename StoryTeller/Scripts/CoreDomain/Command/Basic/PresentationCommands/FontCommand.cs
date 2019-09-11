using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller
{
    public class FontCommand : ICommand
    {
        public string color = string.Empty;
        public float size = 0f;
        public bool bold = false;
        public bool italics = false;

        public bool ProceedInSameFrame { get { return true; } }

        public void Execute(ICallStack callStack, IStoryContext context)
        {
            FontOverrideData fontOverrideData = FontOverrideData.Default;
            if(!string.IsNullOrEmpty(color))
            {
                fontOverrideData.ColorCode = color;
            }
            if(size > float.Epsilon)
            {
                fontOverrideData.Size = size;
            }
            fontOverrideData.Bold = bold;
            fontOverrideData.Italics = italics;
            context.MessagePresenter.SetFont(fontOverrideData);
        }

        public int GetNextCommandIndex(int current)
        {
            return current + 1;
        }
    }
}