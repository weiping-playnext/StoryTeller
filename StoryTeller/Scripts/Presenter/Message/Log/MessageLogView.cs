using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StoryTeller.Presentation
{
    public class MessageLogView : MonoBehaviour
    {
        [SerializeField] MessageLogEntryView entryViewPrefab = null;
        [SerializeField] ScrollRect scrollRect = null;

        public MessageLogEntryView CreateNewEntry()
        {
            var newEntry = Instantiate(entryViewPrefab, scrollRect.content);
            return newEntry;
        }

        public void ClearAllEntries()
        {
            if(scrollRect != null)
            {
                var contentRect = scrollRect.content;
                for(int i = 0; i < contentRect.childCount; i++)
                {
                    Destroy(contentRect.GetChild(i).gameObject);
                }
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}