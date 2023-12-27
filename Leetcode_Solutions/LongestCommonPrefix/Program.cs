// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

public class Solution
{
    public string LongestCommonPrefix(string[] strs)
    {
        Array.Sort(strs);
        string firstWord = strs[0];
        string lastWord = strs[strs.Length - 1];
        int index = 0;

        while (index < firstWord.Length && index < lastWord.Length)
        {
            if (firstWord[index] == lastWord[index])
            {
                index++;
            } 
        }

        return firstWord.Substring(0, index);
    }
}