using System;
using System.Text;
using System.Collections.Generic;

namespace StoryTeller
{
    [Serializable]
    public class ArgumentExpression
    {

        public string name;
        public string exp;

        public ArgumentExpression(string exp)
        {

            string str_left = "";
            string str_right = "";
            try
            {
                int eqIdx = exp.IndexOf('=');
                if (eqIdx > -1)
                {
                    str_left = exp.Remove(eqIdx);
                    if (eqIdx < exp.Length - 1)
                    {
                        str_right = exp.Substring(eqIdx + 1);
                    }
                }
                else
                {
                    str_left = exp;
                }

                string exp_str = str_right.Trim();

                this.name = str_left.Trim();
                this.exp = exp_str;
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to Parse : " + exp, ex);
            }

        }

        //変数を実際の値に置き換えて返却する
        public static string replaceVariable(string targetExpression, IVariableRepository variableRepository)
        {

            bool parsingVariable = false;

            string finalExpression = "";
            StringBuilder stringBuilder = new StringBuilder();

            Stack<string> var_stack = new Stack<string>(); //２重、３重カッコに対応

            for (var i = 0; i < targetExpression.Length; i++)
            {
                char c = targetExpression[i];
                switch(c)
                {
                    case '{':
                        var currentBatch = stringBuilder.ToString();
                        if(parsingVariable)
                        {
                            var_stack.Push(currentBatch);
                        }
                        else
                        {
                            finalExpression += currentBatch;
                            parsingVariable = true;
                        }
                        stringBuilder.Clear();
                        break;
                    case '}':
                        string variableName = stringBuilder.ToString();
                        if(parsingVariable)
                        {
                            string var_val = variableRepository.GetString(variableName);
                            if(var_stack.Count == 0)
                            {
                                parsingVariable = false;
                                stringBuilder.Clear();
                                finalExpression += var_val;
                            }
                            else
                            {
                                string stack_var = var_stack.Pop();
                                stringBuilder.Clear();
                                stringBuilder.Append(stack_var);
                                stringBuilder.Append(var_val);
                            }
                        }
                        break;
                    default:
                        stringBuilder.Append(c);
                        break;
                }
            }
            if(stringBuilder.Length > 0)
            {
                finalExpression += stringBuilder.ToString();
            }
            finalExpression = variableRepository.GetString(finalExpression);

            return finalExpression;

        }

        static readonly List<string> validExpressionDelimiters = new List<string>()
        {
            "==",
            "!=",
            "<=",
            ">=",
            "<",
            ">",
            "*",
            "/",
            "+",
            "-",
        };

        //式をを計算して結果を返す　評価はまた別
        public static string Calculate(string exp)
        {

            //calc 
            //比較計算とかの場合は、別途

            string[] expressions = new string[0];
            string delimFound = string.Empty;
            foreach(var delim in validExpressionDelimiters)
            {
                if(ParseExpressions(delim, exp, out expressions))
                {
                    delimFound = delim;
                    break;
                }
            }

            var evalFunc = GetEvaluateFunctions(delimFound);
            if(evalFunc == null || expressions.Length < 2)
            {
                return exp;
            }
            else
            {
                return evalFunc(expressions);
            }
        }


        static string CalculateEqualityCheck(string[] t, bool notEquals)
        {
            string left = t[0].Trim();
            string right = t[1].Trim();

            bool check = (left == right) ^ notEquals;
            return check.ToString();
        }

        static string CalculateLessCheck(string[] t, bool inverse)
        {
            string left = t[0].Trim();
            string right = t[1].Trim();

            bool check = (float.Parse(left) < float.Parse(right)) ^ inverse;
            return check.ToString();
        }

        static string CalculateGreaterCheck(string[] t, bool inverse)
        {
            string left = t[0].Trim();
            string right = t[1].Trim();

            bool check = (float.Parse(left) > float.Parse(right)) ^ inverse;
            return check.ToString();
        }

        static string CalculateMultiply(string[] t)
        {
            string left = t[0].Trim();
            string right = t[1].Trim();

            float k = float.Parse(left) * float.Parse(right);
            return k.ToString();
        }

        static string CalculateDivide(string[] t)
        {
            string left = t[0].Trim();
            string right = t[1].Trim();

            float k = float.Parse(left) / float.Parse(right);
            return k.ToString();
        }

        static string CalculateAdd(string[] t)
        {
            string left = t[0].Trim();
            string right = t[1].Trim();

            float k = float.Parse(left) + float.Parse(right);
            return k.ToString();
        }

        static string CalculateMinus(string[] t)
        {
            string left = t[0].Trim();
            string right = t[1].Trim();

            float k = float.Parse(left) - float.Parse(right);
            return k.ToString();
        }

        static bool ParseExpressions(string delimiter, string exp, out string[] expressions)
        {
            switch(delimiter)
            {
                case "+":
                case "-":
                    //数値の先頭が-で始まって、かつもう一つ-があれば、計算。それ以外は代入
                    int delimLen = delimiter.Length;
                    if (exp.IndexOf(delimiter,StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        bool single = exp.Substring(delimLen).IndexOf(delimiter,StringComparison.InvariantCultureIgnoreCase) == -1;
                        if(single)
                        {
                            expressions = new string[] { exp };
                            return false;
                        }
                        else
                        {
                            exp = exp.Substring(delimLen);
                        }
                    }
                    expressions = exp.Split(new string[] { delimiter }, StringSplitOptions.None);
                    return true;
                default:
                    expressions = exp.Split(new string[] { delimiter }, StringSplitOptions.None);
                    return true;
            }
        }

        static Func<string[], string> GetEvaluateFunctions(string delimter)
        {
            switch(delimter)
            {
                case "==":
                case "!=":
                    return (expressions) => CalculateEqualityCheck(expressions, delimter == "!=");
                case ">":
                case "<=":
                    return (expressions) => CalculateGreaterCheck(expressions, delimter == "<=");
                case "<":
                case ">=":
                    return (expressions) => CalculateLessCheck(expressions, delimter == ">=");
                case "+":
                    return CalculateAdd;
                case "-":
                    return CalculateMinus;
                case "*":
                    return CalculateMultiply;
                case "/":
                    return CalculateDivide;
                default:
                    return null;
            }
        }
    }
}
