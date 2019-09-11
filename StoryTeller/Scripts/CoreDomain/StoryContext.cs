using System.Collections.Generic;
using StoryTeller.Presentation;

namespace StoryTeller
{

    public class StoryContext : IStoryContext
        , IStoryEventDispatcher
    {
        public IScenarioRepository ScenarioRepository { get; set; }
        public IActorPresenter ActorPresenter { get; set; }
        public IMessagePresenter MessagePresenter { get; set; }
        public IVariableRepository VariableRepository { get; set; }
        public IScenarioMacroRepository ScenarioMacroRepository { get; set; }
        public ISystemPresenter SystemPresenter { get; set; }
        public IAssetSource AssetSource { get; set; }
        public IScenePresenter ScenePresenter { get; set; }
        public IActorFactory ActorFactory { get; set; }
        public ISystemSound SystemSound { get; set; }
        public IAudioPlayer AudioPlayer { get; set; }
        public IStoryPlayer StoryPlayer { get; set; }
        public ISelectionPresenter SelectionPresenter { get; set; }
        public IMessageLogPresenter MessageLogPersenter {get; set;}
        public ILogger MessageLogger { get; set;}

        public IStoryEntry StoryEntry { get; set; }

        List<IStoryEventListener> storyEventListeners = new List<IStoryEventListener>();

        public StoryContext()
        {
        }

        public void OnAssetLoadStart()
        {
            foreach(var listener in storyEventListeners)
            {
                listener.OnAssetLoadStart();
            }
        }

        public void OnAssetLoaded()
        {
            foreach (var listener in storyEventListeners)
            {
                listener.OnAssetLoaded();
            }
        }

        public void OnStoryStart()
        {
            foreach (var listener in storyEventListeners)
            {
                listener.OnStoryStart();
            }
        }

        public void OnStoryEnd()
        {
            foreach (var listener in storyEventListeners)
            {
                listener.OnStoryEnd();
            }
        }

        public void OnCustomEvent(string eventName, params object[] args)
        {
            foreach (var listener in storyEventListeners)
            {
                listener.OnCustomEvent(eventName, args);
            }
        }

        public void AddEventListener(IStoryEventListener[] storyEventListeners)
        {
            this.storyEventListeners = new List<IStoryEventListener>(storyEventListeners);
        }
    }
}
