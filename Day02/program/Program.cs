
using System.Runtime;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using System.Transactions;

//string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Console.WriteLine($"Result of Task 1 is {Task1(fileLines)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

int Task1(List<string> fileLines){

    int amountSafeReports = 0;

    foreach(string line in fileLines){
        List<int> levels = line.Split(' ').Select(x => Convert.ToInt32(x)).ToList();
        if(isItSafe(levels, false)){
            amountSafeReports++;
        }
    }
    return amountSafeReports;
}

int Task2(){
    int amountSafeReports = 0;

    foreach(string line in fileLines){
        List<int> levels = line.Split(' ').Select(x => Convert.ToInt32(x)).ToList();
        if(isItSafe(levels,true)){
            amountSafeReports++;
        }
    }
    return amountSafeReports;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

Boolean isItSafe(List<int> report, bool allowedFaulty) {
    
    int prevLevel = -1;
    int levelMotion = reportAscDesc(report, allowedFaulty);
    int faultyCount = 0;
    
    if (levelMotion == 0) {
        return false;
    }

    for(int i = 0; i < report.Count; i++){
        
        int currentLevel = report[i];
                
        if(i == 0){
            prevLevel = currentLevel;
            continue;
        }

        if(currentLevel == prevLevel){
            if(allowedFaulty && faultyCount == 0){
                faultyCount = 1;
                continue;
            }
            return false;
        }
        
        int differenceLevel = currentLevel - prevLevel;
        int currentMotion = differenceLevel < 0 ? -1 : 1;
        
        if(currentMotion != levelMotion){
            if(allowedFaulty && faultyCount == 0){
                faultyCount = 1;
                continue;
            }
            return false;
        }
        if(Math.Abs(differenceLevel) > 3){
            if(allowedFaulty && faultyCount == 0){
                faultyCount = 1;
                continue;
            }
            return false;
        }
        prevLevel = currentLevel;
    }
    return true;
}

int reportAscDesc(List<int> report, bool allowedFaulty){
    int prevLevel = 0;
    int countAsc = 0;
    int countDesc = 0;

    for(int i = 0 ; i < report.Count; i++){
        int currentLevel = report[i];
        if(i == 0){
            prevLevel = currentLevel;
            continue;
        }    
        if( currentLevel < prevLevel){
            countDesc++;
        }
        else if (currentLevel > prevLevel){
            countAsc++;
        }
        else return 0;
    }

    

    if(countAsc == 0 && countDesc > 1
        || countAsc == 1  && countDesc > 1 && allowedFaulty)
        return -1;
    if(countDesc == 0 && countAsc > 1
        || countDesc == 1  && countAsc > 1 && allowedFaulty)
        return 1;
    return 0; //return 0 is an false
}