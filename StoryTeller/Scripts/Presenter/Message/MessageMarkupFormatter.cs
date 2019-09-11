using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace StoryTeller.Presentation
{
    public class MessageMarkupFormatter : IMessageFormatter
    {
        public string Format(string message, FontOverrideData fontOverrideData)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool sizeSet = fontOverrideData.Size >= 1f;
            if (sizeSet)
            {
                stringBuilder.AppendFormat("<size={0}>", fontOverrideData.Size);
            }
            bool colorSet = !string.IsNullOrEmpty(fontOverrideData.ColorCode);
            if (colorSet)
            {
                stringBuilder.AppendFormat("<color={0}>", fontOverrideData.ColorCode);
            }
            if (fontOverrideData.Bold)
            {
                stringBuilder.Append("<b>");
            }
            if (fontOverrideData.Italics)
            {
                stringBuilder.Append("<i>");
            }
            stringBuilder.Append(message);
            if (fontOverrideData.Italics)
            {
                stringBuilder.Append("</i>");
            }
            if (fontOverrideData.Bold)
            {
                stringBuilder.Append("</b>");
            }
            if (colorSet)
            {
                stringBuilder.Append("</color>");
            }
            if (sizeSet)
            {
                stringBuilder.Append("</size>");
            }
            return stringBuilder.ToString();
        }
    }
}
