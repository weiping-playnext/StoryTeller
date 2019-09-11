using System;
using System.Collections.Generic;

namespace StoryTeller.Data
{
    //変数などバリアブルを保持する
    [Serializable]
    public class Variable
    {

        public Dictionary<string, Dictionary<string, string>> dicVar = new Dictionary<string, Dictionary<string, string>>();

        public void set(string key, string val)
        {

            key = key.Replace("{", "").Replace("}", "");

            string[] tmp = key.Split('.');

            string type = tmp[0].Trim();
            string variable_name = tmp[1].Trim();

            if (!this.dicVar.ContainsKey(type))
            {
                //this.dicVar = new Dictionary<string,Dictionary<string,string>> ();
                this.dicVar[type] = new Dictionary<string, string>();
            }

            this.dicVar[type][variable_name] = val;

            //グローバルなら即効反映
            if (type == "global")
            {

                //if (NovelSingleton.GameManager.globalSetting.globalVar == null)
                //{
                //    NovelSingleton.GameManager.globalSetting.globalVar = new Dictionary<string, string>();
                //}

                //NovelSingleton.GameManager.globalSetting.globalVar[variable_name] = val;
                //NovelSingleton.GameManager.saveManager.saveGlobal(NovelSingleton.GameManager.globalSetting);

            }
        }


        public string get(string exp)
        {

            exp = exp.Replace("{", "").Replace("}", "");

            string default_val = "null"; //default_val は nullという文字列を入れる
            if (exp.IndexOf("|") != -1)
            {
                string[] tmp_default = exp.Split('|');
                exp = tmp_default[0];
                default_val = tmp_default[1];

            }

            string[] tmp = exp.Split('.');

            string type = tmp[0].Trim();
            string variable_name = tmp[1].Trim();

            if (this.dicVar.ContainsKey(type) && this.dicVar[type].ContainsKey(variable_name))
            {
                return this.dicVar[type][variable_name];
            }
            else
            {
                return default_val;
                //return "";
            }

        }

        public bool hasKey(string key)
        {

            string val = this.get(key);

            if (val == "null")
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public Dictionary<string, string> getType(string type)
        {
            if (!this.dicVar.ContainsKey(type))
            {
                //this.dicVar = new Dictionary<string,Dictionary<string,string>> ();
                return new Dictionary<string, string>();
            }
            else
            {
                return this.dicVar[type];
            }
        }

        //すべてのtypeパラメータを丸ごと置き換えます
        public void replaceAll(string type, Dictionary<string, string> dicVal)
        {

            this.dicVar[type] = dicVal;

        }


        //特定の変数をすべてクリアします。
        public void remove(string type)
        {

            this.dicVar[type] = new Dictionary<string, string>();

        }


        public void trace(string type)
        {

            string str = "[trace]" + type + "\n";

            foreach (KeyValuePair<string, string> kvp in dicVar[type])
            {
                str += kvp.Key + "=" + dicVar[type][kvp.Key] + "\n";
            }

            str += "-----------------";

            UnityEngine.Debug.Log("<color=green>" + str + "</color>");

        }

    }
}
