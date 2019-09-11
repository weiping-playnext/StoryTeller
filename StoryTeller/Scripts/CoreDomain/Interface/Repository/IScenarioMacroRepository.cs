using System;
namespace StoryTeller
{
    public interface IScenarioMacroRepository
    {
        void RecordMacro(Macro macro);
        Macro GetMacro(string macroName);
    }
}
