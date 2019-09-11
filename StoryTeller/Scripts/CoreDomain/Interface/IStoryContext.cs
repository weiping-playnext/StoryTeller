using System;
using StoryTeller.Presentation;

namespace StoryTeller
{

    public interface IStoryContext : IStoryEventListener
    {
        IScenarioRepository ScenarioRepository { get; }
        IActorPresenter ActorPresenter { get; }
        IActorFactory ActorFactory { get; }
        IMessagePresenter MessagePresenter { get; }
        IVariableRepository VariableRepository { get; }
        IScenarioMacroRepository ScenarioMacroRepository { get; }
        ISystemPresenter SystemPresenter { get; }
        IAssetSource AssetSource { get; }
        IScenePresenter ScenePresenter { get; }
        ISystemSound SystemSound { get; set; }
        IAudioPlayer AudioPlayer { get; set; }

        ISelectionPresenter SelectionPresenter { get; }
        ILogger MessageLogger { get; }

        IStoryPlayer StoryPlayer { get; }
        IStoryEntry StoryEntry { get; }
    }
}
