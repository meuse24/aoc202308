using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
class Program // Advend of Code 2023, Day 8
{             // Guenther Meusburger
    static void Main()
    {
        Stopwatch stopwatch = new Stopwatch(); stopwatch.Start();
        string path = @"C:\Users\guent\OneDrive\anp_GF07\aoc2023\C#\aoc202308\Input8.txt";
        var lines = File.ReadAllLines(path); string navCommands = lines[0];
        var dict = new Dictionary<string, string[]>();
        for (int i = 2; i < lines.Length; i++)
        {
            var splitLine = lines[i].Split('=');
            string key = splitLine[0].Trim(); string value = splitLine[1].Trim();
            value = value.Trim('(', ')');
            string[] node = value.Split(',');
            for (int j = 0; j < node.Length; j++) node[j] = node[j].Trim();
            dict[key] = node;
        }
        int curNavPos = 0; int steps = 0; string NodeAAA = "AAA"; long part1 = 0;
        List<string> curNodesA = dict.Keys.Where(key => key.EndsWith("A")).ToList();
        var allSteps = new List<long>();
        do
        {
            for (int i = 0; i < curNodesA.Count; i++)
            {
                if (curNodesA[i] != "")
                {
                    curNodesA[i] = dict[curNodesA[i]][navCommands[curNavPos] == 'L' ? 0 : 1];
                    if (curNodesA[i].EndsWith("Z")) { allSteps.Add(steps + 1); curNodesA[i] = ""; }
                }
            }
            if (NodeAAA != "") NodeAAA = dict[NodeAAA][navCommands[curNavPos] == 'L' ? 0 : 1];
            if (NodeAAA == "ZZZ") { NodeAAA = ""; part1 = steps + 1; }
            steps++; curNavPos++;
            if (curNavPos == navCommands.Length) curNavPos = 0;
        } while (allSteps.Count != curNodesA.Count);
        Console.WriteLine($"Part1: {part1}\nPart2: {LCM(allSteps)}"); ;
        stopwatch.Stop(); Console.WriteLine($"Laufzeit:{stopwatch.Elapsed.TotalSeconds} ");
    }
    public static long LCM(List<long> numbers) { return numbers.Aggregate((a, b) => a * b / GCD(a, b)); }
    public static long GCD(long a, long b) { return b == 0 ? a : GCD(b, a % b); }
}