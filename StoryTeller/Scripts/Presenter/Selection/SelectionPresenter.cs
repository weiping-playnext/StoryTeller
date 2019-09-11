using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller.Presentation
{
    public class SelectionPresenter : MonoBehaviour, ISelectionPresenter,
        IStoryContextInjectable
    {
        [SerializeField] SelectionView selectionView = null;
        IStoryContext context = null;

        List<SelectionElementView> selectionElementViews = new List<SelectionElementView>();

        void Start()
        {
            selectionView.gameObject.SetActive(false);
        }

        public void AddSelection(string displayText, ISelectionEventListener selectionEventListener)
        {
            var element = selectionView.CreateView();
            element.DisplayText = displayText;
            element.SelectedEvent
                .AddListener(() =>
                {
                    ClearSelections();
                    Hide();
                    selectionEventListener.OnSelected(displayText);
                });
        }

        public void ShowSelections()
        {
            context.MessagePresenter.Interactable = false;
            selectionView.gameObject.SetActive(true);
        }

        void ClearSelections()
        {
            selectionElementViews.ForEach((obj) => Destroy(obj.gameObject));
            selectionElementViews.Clear();
        }

        void Hide()
        {
            selectionView.gameObject.SetActive(false);
            context.MessagePresenter.Interactable = true;
        }

        public void Inject(IStoryContext context)
        {
            this.context = context;
        }
    }
}