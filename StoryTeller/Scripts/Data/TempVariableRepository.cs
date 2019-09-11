using System;
using System.Collections.Generic;
namespace StoryTeller.Data
{
    public class TempVariableRepository : IVariableRepository
    {
        Dictionary<string, string> variables = new Dictionary<string, string>();

        public TempVariableRepository()
        {
        }

        public string GetString(string key)
        {
            if (variables.ContainsKey(key))
                return variables[key];
            return key;
        }

        public void RemoveString(string key)
        {
            variables.Remove(key);
        }

        public void SetString(string key, string val)
        {
            variables[key] = val;
        }
    }
}
