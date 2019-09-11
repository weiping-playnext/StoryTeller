using System;
namespace StoryTeller
{
    public interface IStoryEventDispatcher
    {
        void AddEventListener(IStoryEventListener[] storyEventListeners);
    }
}
