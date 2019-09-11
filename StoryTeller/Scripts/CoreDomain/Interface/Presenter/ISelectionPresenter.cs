using System;

namespace StoryTeller
{
    public interface ISelectionPresenter 
    {
        void AddSelection(string displayText, ISelectionEventListener selectionEventListener);
        void ShowSelections();
    }

    public interface ISelectionEventListener
    {
        void OnSelected(string selection);
    }
}