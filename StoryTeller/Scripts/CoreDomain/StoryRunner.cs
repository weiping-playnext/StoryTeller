using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller
{


    public interface IStoryPlayer
    {
        bool IsFinished { get; }
        void Play();
        void Reset();
    }

    public class StoryRunner : IStoryPlayer
        , IAssetSourceEventListener
        , IStoryContextInjectable
    {
        List<IStoryInstaller> installers = new List<IStoryInstaller>();
        List<IStoryEventListener> eventListeners = new List<IStoryEventListener>();

        IStoryContext context;
        Scenario scenario = null;

        CallStack callStack;
        bool isFinished = false;
        public bool IsFinished { get { return isFinished; } }

        public void Inject(IStoryContext context)
        {
            this.context = context;
            callStack = new CallStack(context);
        }

        public void Run(string name)
        {
            context.ScenarioRepository.JumpToScenario(name);
            Reset();
            eventListeners.ForEach(listener => listener.OnStoryStart());
            Play();
        }

        public void Play()
        {
            scenario = context.ScenarioRepository.GetCurrentRunningScenario();
            int commandCount = scenario.Commands.Count;

            if (callStack.CurrentCommandIndex == commandCount && callStack.Count > 0)
            {
                var callItem = callStack.Pop();
                context.ScenarioRepository.JumpToScenario(callItem.scenarioNname);
                callStack.CurrentCommandIndex = callItem.index + 1;
                scenario = context.ScenarioRepository.GetCurrentRunningScenario();
                commandCount = scenario.Commands.Count;
            }

            while (callStack.CurrentCommandIndex < commandCount)
            {
                var command = scenario.Commands[callStack.CurrentCommandIndex];
                command.Execute(callStack, context);
                callStack.CurrentCommandIndex = command.GetNextCommandIndex(callStack.CurrentCommandIndex);
                if(!command.ProceedInSameFrame) break;
                scenario = context.ScenarioRepository.GetCurrentRunningScenario(); 
                commandCount = scenario.Commands.Count;
                if(callStack.CurrentCommandIndex == commandCount && callStack.Count > 0)
                {
                    var callItem = callStack.Pop();
                    context.ScenarioRepository.JumpToScenario(callItem.scenarioNname);
                    callStack.CurrentCommandIndex = callItem.index + 1;
                    scenario = context.ScenarioRepository.GetCurrentRunningScenario();
                    commandCount = scenario.Commands.Count;
                }
            }

            // TODO: 最後Input待ち等もあるので番兵としてEndコマンドを追加する
            if(callStack.CurrentCommandIndex == scenario.Commands.Count)
            {
                context.MessagePresenter.Interactable = false;
                context.OnStoryEnd();
                isFinished = true;
            }
        }

        public void OnAssetLoadStart()
        {
            // eventListeners.ForEach(listener => listener.OnAssetLoaded());
        }

        public void OnAssetLoadComplete()
        {
            eventListeners.ForEach(listener => listener.OnAssetLoaded());
            Play();
        }

        public void Reset()
        {
            callStack.CurrentCommandIndex = 0;
            isFinished = false;
        }
    }
}
