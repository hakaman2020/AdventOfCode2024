// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
List<long> stoneNumbers = fileLines[0].Split(' ').Select(n => long.Parse(n)).ToList();

int amountOfBlinksPart1 = 6;
int amountOfBlinksPart2 = 75;

Console.WriteLine($"Result of Task 1 with recursion/memoization is {GeneralTask(stoneNumbers, amountOfBlinksPart1)}");
Console.WriteLine($"Result of Task 2 with recursion/memoization is {GeneralTask(stoneNumbers, amountOfBlinksPart2)}");
Console.WriteLine($"Result of Task 1 with a dictionary approach is {CalculateAmountStoneNumbers(stoneNumbers,amountOfBlinksPart1)}");
Console.WriteLine($"Result of Task 2 with a dictionary approach is {CalculateAmountStoneNumbers(stoneNumbers,amountOfBlinksPart2)}");

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

long CalculateAmountStoneNumbers(List<long> stoneNumbers, int amountOfBlinks){
    Dictionary<long,long> stoneNumbersAmount = new();
    
    foreach(var number in stoneNumbers)
        AddNumber(stoneNumbersAmount, number, 1);
    for(int i = 0; i < amountOfBlinks; i++)
        stoneNumbersAmount = Blink(stoneNumbersAmount);
    long totalAmountOfStones = stoneNumbersAmount.Sum(s => s.Value);
    
    return totalAmountOfStones;
}

Dictionary<long,long> Blink(Dictionary<long,long> stoneNumbers){
    Dictionary<long,long> temp = new();

    foreach(var number in stoneNumbers){
        if(number.Key == 0){
            AddNumber(temp, 1, number.Value);
            continue;
        }

        string numberString = number.Key.ToString();

        if(numberString.Length % 2 == 0){
            long leftNumber = long.Parse(numberString.Substring(0, numberString.Length / 2));
            long rightNumber = long.Parse(numberString.Substring(numberString.Length / 2));
           
            AddNumber(temp, leftNumber, number.Value);
            AddNumber(temp, rightNumber, number.Value); 
        }
        else
            AddNumber(temp, number.Key * 2024, number.Value);
    }

    return temp;
}

void AddNumber(Dictionary<long,long> numbers, long number, long value){
    if(numbers.ContainsKey(number))
        numbers[number]+= value;
    else
        numbers.Add(number, value); 
}