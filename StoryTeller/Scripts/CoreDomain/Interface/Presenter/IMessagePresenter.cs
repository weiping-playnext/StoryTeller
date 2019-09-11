using System;
using System.Collections;
using StoryTeller.Presentation;


namespace StoryTeller
{
    public interface IMessagePresenter
    {
        void NewLine();
        void ClearMessage();

        void RenderMessage(string message);
        void ShowMessage(float time);
        // void SetMessage(string message);
        void HideMessage(float time);
        void SetMessageSpeed(float speed);

        void SetTokenName(string tokenName);

        bool Interactable { get; set; }

        void Shake(bool wait);

        IEnumerator _shake();

        void SetFont(FontOverrideData font);
        void ResetFont();
    }
}
