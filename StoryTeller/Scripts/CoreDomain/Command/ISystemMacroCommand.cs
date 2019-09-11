using System;
using System.Collections.Generic;
namespace StoryTeller
{
    public interface ISystemMacroCommand : ICommand
    {
        ICommand[] GetDerivedCommands(IList<ICommand> originalList);
    }
}
