
// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
List<long> stoneNumbers = fileLines[0].Split(' ').Select(n => long.Parse(n)).ToList();

// foreach (var stoneNumber in stoneNumbers){
//     Console.Write(stoneNumber + " ");
// }
// Console.WriteLine();
int amountOfBlinksPart1 = 75;
int amountOfBlinksPart2 = 6;
Console.WriteLine($"Result of Task 1 is {GeneralTask(stoneNumbers, amountOfBlinksPart1)}");
// Console.WriteLine($"Result of Task 2 is {GeneralTask(stoneNumbers, amountOfBlinksPart2 )}");

long GeneralTask(List<long> stoneNumbers, int amountOfBlinks){
    long totalAmountOfStones = 0;
    Dictionary<(long,int),long> memory = new();

    foreach(long number in stoneNumbers){
         Console.WriteLine("Processing :" + number);
        totalAmountOfStones += nextBlink(number, amountOfBlinks, memory);
    }
    // totalAmountOfStones += nextBlink(125,amountOfBlinks,memory);
    return totalAmountOfStones;
}

long nextBlink(long stoneNumber, int blinksLeft,Dictionary<(long,int),long> memory){
    
    blinksLeft--;
    string stoneNumberString = stoneNumber.ToString();
    long stoneNumber2 = -1; 
    
    if(stoneNumber == 0){
        stoneNumber = 1;
    }
    else if(stoneNumberString.Length % 2 == 0){
        stoneNumber = long.Parse(stoneNumberString.Substring(0, stoneNumberString.Length / 2));
        stoneNumber2 = long.Parse(stoneNumberString.Substring(stoneNumberString.Length / 2));
    }
    else{
        stoneNumber *= 2024;
    }

    if(blinksLeft == 0){
        // Console.WriteLine(stoneNumber);
        if(stoneNumber2 != -1){
            // Console.WriteLine(stoneNumber2);
            return 2;
        }
        return 1;
    }

    long totalAmountOfStones = 0;

    if(memory.ContainsKey((stoneNumber,blinksLeft))){
        // Console.WriteLine("Memory used (" + stoneNumber + " " + blinksLeft + "):" + memory[(stoneNumber,blinksLeft)]);
        totalAmountOfStones = memory[(stoneNumber,blinksLeft)];
    }
    else{
        totalAmountOfStones = nextBlink(stoneNumber, blinksLeft, memory);
        memory.Add((stoneNumber,blinksLeft), totalAmountOfStones);
        // Console.WriteLine("Memory stored (" + stoneNumber + " " + blinksLeft + "):" + memory[(stoneNumber,blinksLeft)]);
    }

    if(stoneNumber2 != -1){
        if(memory.ContainsKey((stoneNumber2,blinksLeft))){
            totalAmountOfStones += memory[(stoneNumber2,blinksLeft)];
            // Console.WriteLine("Memory used (" + stoneNumber2 + " " + blinksLeft + "):" + memory[(stoneNumber2,blinksLeft)]);   
        }
        else{
            long result = nextBlink(stoneNumber2, blinksLeft, memory);    
            totalAmountOfStones += result;
            memory.Add((stoneNumber2, blinksLeft),result);
            // Console.WriteLine("Memory stored (" + stoneNumber2 + " " + blinksLeft + "):" + memory[(stoneNumber2,blinksLeft)]);
        }
    }
    return totalAmountOfStones;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}