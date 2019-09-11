using System;
namespace StoryTeller
{
    public interface IVariableRepository
    {
        string GetString(string key);
        void SetString(string key, string val);
        void RemoveString(string key);
    }
}
