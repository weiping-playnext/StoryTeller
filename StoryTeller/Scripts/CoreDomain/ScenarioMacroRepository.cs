using System;
using System.Collections.Generic;

namespace StoryTeller
{
    public class ScenarioMacroRepository : IScenarioMacroRepository
    {
        Dictionary<string, Macro> macroDic = new Dictionary<string, Macro>();

        public ScenarioMacroRepository()
        {
        }

        public Macro GetMacro(string macroName)
        {
            if(macroDic.ContainsKey(macroName))
            {
                return macroDic[macroName];
            }
            return null;
        }

        public void RecordMacro(Macro macro)
        {
            if (macro != null)
            {
                macroDic[macro.name] = macro;
            }
        }
    }
}
