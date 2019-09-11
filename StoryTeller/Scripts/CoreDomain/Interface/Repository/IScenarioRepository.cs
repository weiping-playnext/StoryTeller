using System;
namespace StoryTeller
{
    public interface IScenarioRepository
    {
        Scenario GetScenario(string name);
        void AddScript(string name, string scriptData);
        Scenario GetCurrentRunningScenario();
        void JumpToScenario(string name);
    }
}
