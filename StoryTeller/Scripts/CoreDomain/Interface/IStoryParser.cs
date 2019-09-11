using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller
{
    public interface IStoryParser
    {
        IList<ICommand> Parse(string scriptData);
    }
}
