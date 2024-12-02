// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Console.WriteLine($"Result of Task 1 is {Task1(fileLines)}");
Console.WriteLine($"Result of Task 2 is {Task2(fileLines)}");

int Task1(List<string> fileLines){

    int amountSafeReports = 0;

    foreach(string line in fileLines){
        List<int> levels = line.Split(' ').Select(x => Convert.ToInt32(x)).ToList();
        if(isItSafe(levels)){
            amountSafeReports++;
        }
    }
    return amountSafeReports;
}

int Task2(List<string> fileLines){
    int amountSafeReports = 0;

    foreach(string line in fileLines){
        List<int> levels = line.Split(' ').Select(x => Convert.ToInt32(x)).ToList();
        if(isItSafeFaultTolerant(levels)){
            amountSafeReports++;
        }
    }
    return amountSafeReports;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

Boolean isItSafe(List<int> report) {
    
    int prevLevel = -1;
    int levelMotion = 0;

    for(int i = 0; i < report.Count; i++){
        
        int currentLevel = report[i];
        if(i == 0){
            prevLevel = currentLevel;
            levelMotion = currentLevel > report[1] ? -1 : 1; 
            continue;
        }

        if(currentLevel == prevLevel){
            return false;
        }
        
        int differenceLevel = currentLevel - prevLevel;
        int currentMotion = differenceLevel < 0 ? -1 : 1;
        
        if(currentMotion != levelMotion || Math.Abs(differenceLevel) > 3){
            return false;
        }
        prevLevel = currentLevel;
    }
    return true;
}

Boolean isItSafeFaultTolerant(List<int> report) {
    
    if(isItSafe(report)){
        return true;
    }
    for(int i = 0; i < report.Count; i++){
        List<int> tempList = new List<int>(report);
        tempList.RemoveAt(i);
        if(isItSafe(tempList)){
            return true;
        }
    }
    return false;
}

