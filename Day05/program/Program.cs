﻿
using System.Security;

string inputFilePath = "./example.txt";
//string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Console.WriteLine($"Result of Task 1 is {Task1()}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

int Task1(){
    return 0;
}

int Task2(){
    return 0;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

void SeperateList(List<string> inputLines, Dictionary<int,HashSet<int>> rules, List<List<int>> updates){
    bool endrules = false;
    foreach (string line in inputLines){
        if(line == string.Empty){
            endrules = true;
            continue;
        }
        if(!endrules){
            
            rules.ContainsKey()
            rules.
            continue;   
        }

    }
} 