using System;

namespace StoryTeller
{
    public interface IActorPresenter
    {
        void RegisterNewActor(string name, string tag, string assetTag);
        void RegisterActorFace(string name, string face, string assetTag);
        void SetActorFace(string name, string face);
        void SetRegion(string name, string region);

        void TweenRegion(string name, string region, float time, Action completion);

        void SetSize(string name, string sizeKey, float time = 0);
        void ShowActor(string name, float time, bool wait = false);
        void HideActor(string name, float time, bool wait = false);
        void SetFocus(string tag);

        void ShakeActor(string name, bool wait);
    }
}
