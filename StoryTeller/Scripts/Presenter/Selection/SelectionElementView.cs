using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryTeller.Presentation
{
    public class SelectionElementView : MonoBehaviour
    {
        [SerializeField] Text selectionText = null;

        [SerializeField] Button triggerButton = null;

        public Button.ButtonClickedEvent SelectedEvent
        {
            get
            {
                return triggerButton.onClick;
            }
        }

        public string DisplayText
        {
            set
            {
                selectionText.text = value;
            }
            get
            {
                return selectionText.text;
            }
        }
    }
}