
string inputFilePath = "./example.txt";
// string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Dictionary<int,HashSet<int>>rules = new();
List<List<int>> updates = new();

SeperateSections(fileLines, rules, updates);

// //print the rules
Console.WriteLine("\nRules");
foreach(var set in rules){
    Console.Write(set.Key +  "|");
    var test= set.Value.ToList();
    test.ForEach(x => Console.Write(x + " "));
    Console.WriteLine();
}

// //print the updates
// Console.WriteLine("\nUpdates");
// foreach(List<int> update in updates){
//     update.ForEach(x => Console.Write(x + ","));
//     Console.WriteLine();
// }

(int sumTask1, List<List<int>> wrongUpdates) = Task1(rules,updates);

Console.WriteLine($"Result of Task 1 is {sumTask1}");
Console.WriteLine($"Result of Task 2 is {Task2(rules, wrongUpdates)}");

(int,List<List<int>>) Task1(Dictionary<int,HashSet<int>> rules, List<List<int>> updates){

    int sum = 0;
    //keep track of wrong updates for task 2
    List<List<int>> wrongUpdates = new();

    foreach(var update in updates){
        if(IsUpdateCorrect(rules, update)){
            int middleIndex = update.Count / 2;
            sum += update[middleIndex];
        }
        else{
            wrongUpdates.Add(update);
        }
    }
    return (sum, wrongUpdates);
}

int Task2(Dictionary<int, HashSet<int>> rules, List<List<int>> updates){

    Console.WriteLine("Wrong Updates");
    foreach(List<int> update in updates){
        update.ForEach(x => Console.Write(x + ","));
        Console.WriteLine();
    }
    return 0;
}

List<int> TopologicalSortUpdate(Dictionary<int, HashSet<int>> rules, List<int> update){
    //will use Topological Sorting to sort
    //start with all the numbers without dependencies
    //add them to the queue
    //remove those numbers from the numbers that are the dependant on numbers that where added to the queue
    //repeat until no numbers are left

    
    //count dependencies
    while(update.Count > 0){
        
    }
}


bool IsUpdateCorrect(Dictionary<int,HashSet<int>> rules, List<int> update){

    if(update.Count == 1) return true;

    for(int i = 1; i < update.Count; i++){
        int checkingNumber = update[i];
        if(rules.ContainsKey(checkingNumber)){
            for(int j = 0; j < i; j++){
                if(rules[checkingNumber].Contains(update[j])){
                    return false;
                }
            }
        }
    }

    return true;
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