
string inputFilePath = "./example.txt";
//string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
List<long> stoneNumbers = fileLines[0].Split(' ').Select(n => long.Parse(n)).ToList();

// foreach (var stoneNumber in stoneNumbers){
//     Console.Write(stoneNumber + " ");
// }
// Console.WriteLine();

Console.WriteLine($"Result of Task 1 is {Task1(stoneNumbers)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

int Task1(List<long> stoneNumbers){
    long totalAmountOfStones = 0;
    Console.WriteLine(nextBlink(125,1,1));
    // foreach(long number in stoneNumbers){

    // }
    return 0;
}

int Task2(){
    return 0;
}

long nextBlink(long stoneNumber, int blinksLeft, long totalAmountOfStones){
    
    blinksLeft--;
    string stoneNumberString = stoneNumber.ToString();
    long stoneNumber2 = -1; 
    
    if(stoneNumber == 0){
        stoneNumber = 1;
    }
    else if(stoneNumberString.Length % 2 == 0){
        stoneNumber = long.Parse(stoneNumberString.Substring(0, stoneNumberString.Length / 2));
        stoneNumber2 = long.Parse(stoneNumberString.Substring(stoneNumberString.Length / 2));
        totalAmountOfStones++;
    }
    else{
        stoneNumber *= 2024;
    }

    if(blinksLeft == 0)
        return totalAmountOfStones;
        


    return 0;
}



List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}