using System;
using System.Collections.Generic;

namespace StoryTeller
{
    //マクロ呼出しを保持するクラス
    [Serializable]
    public class CallStackItem
    {

        public string scenarioNname;
        public int index;

        public CallStackItem()
        {
        }

        public CallStackItem(string scenario_name, int index)
        {
            this.scenarioNname = scenario_name;
            this.index = index;
        }


    }
}
