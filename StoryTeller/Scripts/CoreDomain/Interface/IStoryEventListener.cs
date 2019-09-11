
namespace StoryTeller
{
    public interface IStoryEventListener
    {
        void OnAssetLoadStart();
        void OnAssetLoaded();
        void OnStoryStart();
        void OnStoryEnd();
        void OnCustomEvent(string eventName, params object[] args);
    }
}
