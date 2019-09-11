using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryTeller.Presentation;
using StoryTeller.Data;

namespace StoryTeller
{
    public class SingleStoryInstaller : MonoBehaviour, IStoryInstaller 
    {
        [SerializeField] TextAsset scenario = null;
        [SerializeField] ScenePresenter scenePresenter = null;
        [SerializeField] MessagePresenter messagePresenter = null;
        [SerializeField] ActorPresenter actorPresenter = null;
        [SerializeField] SystemSound systemSound = null;
        [SerializeField] AudioPlayer audioPlayer = null;
        [SerializeField] SelectionPresenter selectionPresenter = null;
        [SerializeField] MessageLogPresenter messageLogPresenter = null;

        public string GetScenarioName() { return scenario.name; }

        public virtual void Install(StoryContext context)
        {
            context.AssetSource = new ResourceAssetManager();

            var scenarioRepository =  new ScenarioRepository();
            scenarioRepository.AddScript(scenario.name, scenario.text);
            context.ScenarioRepository = scenarioRepository;

            context.VariableRepository = new TempVariableRepository();

            context.ScenarioMacroRepository = new ScenarioMacroRepository();

            var actorFactory = new ActorFactory<SpriteActor>();
            context.ActorFactory = actorFactory;

            context.ScenePresenter = scenePresenter;

            context.ActorPresenter = actorPresenter;

            context.SystemSound = systemSound;

            audioPlayer.assetSource = context.AssetSource;
            context.AudioPlayer = audioPlayer;

            context.MessagePresenter = messagePresenter;

            context.SystemPresenter = scenePresenter;
            context.SelectionPresenter = selectionPresenter;
            context.MessageLogger = messageLogPresenter;
        }
    }
}
