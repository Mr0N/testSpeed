using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Text.RegularExpressions;


string text = Regex.Match("8932Q12892891Q32332",
                    "Q(?<obj>[^Q]+)").Groups["obj"].Value;
Console.WriteLine(text);
string test1 = "8932Q12892891Q32332".Cut("Q", "Q");
Console.WriteLine(test1);
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);

public class Test
{



    string text1 = "";
    [Benchmark]
    public void RegexTest() {
        var obj = new string('-', 1000);
        string findStr = $"{obj}Q12345Q{obj}";
      
        for (int i = 0; i < 100000; i++)
        {
             text1 = Regex.Match(findStr, "Q(?<obj>[^Q]+)").Groups["obj"].Value;
           
        }
    }
    string text2 = "";
    [Benchmark]
    public void Cut() {
        var obj = new string('-', 1000);
        string findStr = $"{obj}Q12345Q{obj}";
       
        for (int i = 0; i < 10000; i++)
        {
            text2 = findStr.Cut("Q", "Q");
           // Console.WriteLine(text);
        }
    }
}
public static class Ext
{

    public static string Cut(this string text, string begin, string end, bool exception = false)
    {
        //if (string.IsNullOrEmpty(text)
        //    && string.IsNullOrEmpty(begin) &&
        //    string.IsNullOrEmpty(end))
        //{
        //    throw new NullReferenceException();
        //}
        int beginIndex = text.IndexOf(begin);

        if (beginIndex == -1 && exception == true)
            throw new Exception();
        else if (beginIndex == -1 && exception == false)
            return null;
        beginIndex = beginIndex + begin.Length;
        int endIndex = text.IndexOf(end, beginIndex);

        if (endIndex == -1 && exception == true)
            throw new Exception();
        else if (endIndex == -1 && exception == false)
            return null;

        
        return text.Substring(beginIndex, endIndex - beginIndex);
    }
}