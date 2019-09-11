using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller.Presentation
{
    public class SelectionView : MonoBehaviour
    {
        [SerializeField] SelectionElementView selectionElementViewPrefab = null;
        [SerializeField] Transform layout = null;

        public SelectionElementView CreateView()
        {
            var element = Instantiate(selectionElementViewPrefab, layout);
            return element;
        }
    }
}