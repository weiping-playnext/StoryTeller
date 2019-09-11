using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoryTeller;

namespace StoryTeller.Example
{
    public class GameEntry : MonoBehaviour, IStoryEntry
    {
        [SerializeField] SingleStoryInstaller installer = null;

        void Start()
        {

            StoryContext context = new StoryContext();
            context.StoryEntry = this;
            StoryRunner runner = new StoryRunner();
            context.StoryPlayer = runner;

            installer.Install(context);
            context.Resolve();
            runner.Run(installer.GetScenarioName());
        }


        public void OnAssetLoadStart(){}
        public void OnAssetLoaded(){}

        public void OnStoryStart()
        {
            Debug.Log("Story Playing Started");
        }
        public void OnCustomEvent(string eventName, params object[] args) {}

        public void OnStoryEnd()
        {
            Debug.Log("Story Playing Finished");
        }
    }
}