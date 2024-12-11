
// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Dictionary<int,HashSet<int>>rules = new();
List<List<int>> updates = new();

SeperateSections(fileLines, rules, updates);

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
    int sum = 0;
    
    foreach(List<int> update in updates){
        List<int> sortedUpdate = TopologicalSortUpdate(rules, update);
        sum += sortedUpdate[sortedUpdate.Count/2];
    }
    
    return sum;
}

List<int> TopologicalSortUpdate(Dictionary<int, HashSet<int>> rules, List<int> update){
    //use Topological Sorting to sort
    List<int> sortedList = new();
    List<int> numbersToBeRemoved = new();

    while(update.Count > 0){
        numbersToBeRemoved.Clear();

        for(int i = 0; i < update.Count; i++){
            int checkingNumber = update[i];
            int amountDependencies = CountDependencies(rules, update, checkingNumber);
            if(amountDependencies == 0){
                numbersToBeRemoved.Add(checkingNumber);
            }
        }

        foreach(int number in numbersToBeRemoved){
            update.Remove(number);
            sortedList.Add(number);
        }
    }
    sortedList.Reverse();
    
    return sortedList;
}

int CountDependencies(Dictionary<int, HashSet<int>> rules,List<int> update, int number){
    int count = 0;

    if(!rules.ContainsKey(number))return 0;
    for( int i = 0; i < update.Count; i++){
        if(rules[number].Contains(update[i])){
            count++;
        }
    }
    
    return count;
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