using System;
using System.Collections.Generic;
using System.Linq;

namespace StoryTeller
{
    public interface IAssetDependenciesResolver
    {
        IReadOnlyList<string> GetRequiredAssets(IScenarioRepository scenarioRepository, string entryScenario);
    }

    public class AssetResolver : IAssetDependenciesResolver
    {
        public IReadOnlyList<string> GetRequiredAssets(IScenarioRepository scenarioRepository, string entryScenario)
        {
            List<string> returnList = new List<string>();
            List<string> closedScenarioList = new List<string>();
            List<string> openScenarioList = new List<string>();
            openScenarioList.Add(entryScenario);
            while (openScenarioList.Count > 0)
            {
                string currentScenarioName = openScenarioList[0];
                openScenarioList.Remove(currentScenarioName);
                closedScenarioList.Add(currentScenarioName);
                
                var scenario = scenarioRepository.GetScenario(currentScenarioName);
                var callCandidates = scenario
                    .Commands
                    .OfType<CallCommand>()
                    .Select((arg) => arg.file)
                    .Where((arg) => !string.IsNullOrEmpty(arg) && !openScenarioList.Contains(arg) && !closedScenarioList.Contains(arg))
                    .ToList();
                var jumpCandidates = scenario
                    .Commands
                    .OfType<JumpCommand>()
                    .Select((arg) => arg.file)
                    .Where((arg) => !string.IsNullOrEmpty(arg) && !openScenarioList.Contains(arg) && !closedScenarioList.Contains(arg))
                    .ToList();
                openScenarioList.AddRange(callCandidates.Union(jumpCandidates));
            }

            closedScenarioList.ForEach((obj) =>
            {
                var scenario = scenarioRepository.GetScenario(obj);
                var additionalAssets = scenario
                    .Commands
                    .OfType<AssetCommand>()
                    .Select((arg) => arg.source)
                    .Where((arg) => !string.IsNullOrEmpty(arg) && !returnList.Contains(arg))
                    .ToList();
                returnList.AddRange(additionalAssets);
            });
            
            return returnList;
        }
    }
}
