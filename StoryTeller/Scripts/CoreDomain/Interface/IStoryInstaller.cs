using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryTeller
{
    public interface IStoryInstaller 
    {
        void Install(StoryContext context);
    }
}
