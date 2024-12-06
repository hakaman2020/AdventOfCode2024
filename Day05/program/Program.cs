
using System.Security;

string inputFilePath = "./example.txt";
//string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Dictionary<int,HashSet<int>>rules = new();
List<List<int>> updates = new();

SeperateSections(fileLines, rules, updates);

foreach(var set in rules){
    Console.Write(set.Key +  "|");
    var test= set.Value.ToList();
    test.ForEach(x => Console.Write(x + " "));
    Console.WriteLine("");
}


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

void SeperateSections(List<string> inputLines, Dictionary<int,HashSet<int>> rules, List<List<int>> updates){
    bool endrules = false;
    foreach (string line in inputLines){
        if(line == string.Empty){
            endrules = true;
            continue;
        }
        if(!endrules){
            List<int> rule = line.Split('|').Select(n => int.Parse(n)).ToList();
            if(rules.ContainsKey(rule[0]))
                rules[rule[0]].Add(rule[1]);
            else
                rules.Add(rule[0],new HashSet<int>{rule[1]});
            continue;   
        }
        List<int> update = line.Split(',').Select(n => int.Parse(n)).ToList();
        updates.Add(update);
    }
} 