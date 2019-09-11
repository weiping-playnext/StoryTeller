using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace StoryTeller.Data
{

    public class ScenarioRepository : IScenarioRepository
    {
        IStoryParser parser = new Parser("StoryTeller");
        Dictionary<string, string> rawScenarioScripts = new Dictionary<string, string>();
        Dictionary<string, Scenario> scenarios = new Dictionary<string, Scenario>();

        string currentScenarioName;

        public Scenario GetScenario(string name)
        {
            if(scenarios.ContainsKey(name)) return scenarios[name];

            if(rawScenarioScripts.ContainsKey(name))
            {
                var scenario = new Scenario(parser.Parse(rawScenarioScripts[name]));
                scenario.name = name;
                scenarios.Add(name, scenario);
                return scenario;
            }

            throw new Exception(string.Format("Scenario Scripts Not Found : {0} ", name)); 

        }

        public void AddScript(string name, string scriptData)
        {
            if (rawScenarioScripts.ContainsKey(name))
            {
                rawScenarioScripts.Remove(name);
            }
            rawScenarioScripts.Add(name, scriptData);
        }

        public Scenario GetCurrentRunningScenario()
        {
            return GetScenario(currentScenarioName);
        }

        public void JumpToScenario(string newScenario)
        {
            currentScenarioName = newScenario;
        }
    }
}
