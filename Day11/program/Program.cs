
// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
List<long> stoneNumbers = fileLines[0].Split(' ').Select(n => long.Parse(n)).ToList();

int amountOfBlinksPart1 = 25;
int amountOfBlinksPart2 = 75;

Console.WriteLine($"Result of Task 1 is {GeneralTask(stoneNumbers, amountOfBlinksPart1)}");
Console.WriteLine($"Result of Task 2 is {GeneralTask(stoneNumbers, amountOfBlinksPart2)}");

long GeneralTask(List<long> stoneNumbers, int amountOfBlinks){
    long totalAmountOfStones = 0;
    Dictionary<(long,int),long> cache = new();

    foreach(long number in stoneNumbers){
        totalAmountOfStones += NextBlink(number, amountOfBlinks, cache);
    }

    return totalAmountOfStones;
}

long NextBlink(long stoneNumber, int blinksLeft, Dictionary<(long,int), long> cache){

    List<long> stonesNumbers = ApplyRules(stoneNumber);

    if(blinksLeft - 1 == 0)
        return stonesNumbers.Count;

    long totalAmountOfStones = 0;

    foreach(long stoneNb in stonesNumbers){
        if(cache.ContainsKey((stoneNb, blinksLeft - 1)))
            totalAmountOfStones += cache[(stoneNb, blinksLeft - 1)];
        else {
            long result = NextBlink(stoneNb, blinksLeft - 1, cache);
            cache.Add((stoneNb, blinksLeft -1), result);
            totalAmountOfStones += result;
        }
    }

    return totalAmountOfStones;
}

List<long> ApplyRules(long stoneNumber){

    if(stoneNumber == 0){
        return new List<long>(){1};
    }
    
    string stoneNumberString = stoneNumber.ToString();
    List<long> stoneNumbers = new();

    if(stoneNumberString.Length % 2 == 0){
        stoneNumbers.Add(long.Parse(stoneNumberString.Substring(0, stoneNumberString.Length / 2)));
        stoneNumbers.Add(long.Parse(stoneNumberString.Substring(stoneNumberString.Length / 2)));
    }
    else{
        stoneNumbers.Add(stoneNumber * 2024);
    }

    return stoneNumbers;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}