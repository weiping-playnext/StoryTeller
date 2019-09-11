using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryTeller.Presentation
{
    public class MessageLogEntryView : MonoBehaviour
    {
        [SerializeField] Text speakerText = null;
        [SerializeField] Text messageText = null;

        public string Speaker
        { 
            set 
            { 
                speakerText.text = value; 
            } 
        }

        public string Message
        {
            set
            {
                messageText.text = value;
            }
        }
    }
}
