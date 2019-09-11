using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller
{
    public struct FontOverrideData 
    {
        public string ColorCode;
        public float Size;
        public bool Bold;
        public bool Italics;

        public FontOverrideData(FontOverrideData data)
        {
            ColorCode = data.ColorCode;
            Size = data.Size;
            Bold = data.Bold;
            Italics = data.Italics;
        }

        public static FontOverrideData Default
        {
            get
            {
                return new FontOverrideData()
                {
                    ColorCode = string.Empty,
                    Size = 0f,
                    Bold = false,
                    Italics = false,
                };
            }
        }
    }
}