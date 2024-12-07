
// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Console.WriteLine($"Result of Task 1 is {Task1(fileLines)}");
Console.WriteLine($"Result of Task 2 is {Task2(fileLines)}");

long Task1(List<string> fileLines){

    long sum = 0;

    foreach(string line in fileLines){
        var split = line.Split(' ');
        long target = long.Parse(split[0].Substring(0, split[0].Length-1));
        List<long> numbers = split.Skip(1).Select(n => long.Parse(n)).ToList();
        if(IsEquationPossible(target, numbers, false)){
            sum += target;
        }
    }

    return sum;
}

long Task2(List<string> fileLines){

    long sum = 0;

    foreach(string line in fileLines){
        var split = line.Split(' ');
        long target = long.Parse(split[0].Substring(0, split[0].Length-1));
        List<long> numbers = split.Skip(1).Select(n => long.Parse(n)).ToList();
        if(IsEquationPossible(target, numbers, true)){
            sum += target;
        }
    }

    return sum;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

bool IsEquationPossible(long target, List<long> numbers, bool concatOperatorActive){

    long result = numbers[0];

    if(BuildEquation(target, numbers, 1, result, Operator.Add, concatOperatorActive) 
        || BuildEquation(target, numbers, 1, result, Operator.Multiply, concatOperatorActive)
        || (BuildEquation(target, numbers, 1, result, Operator.Concat, true) && concatOperatorActive))
        return true;
    
    return false;
}

bool BuildEquation(long target, List<long> numbers, int index, long result, Operator op, bool concatOperatorActive){
    if(op == Operator.Add)
        result += numbers[index];
    else if(op == Operator.Multiply)
        result *= numbers[index];
    else if(concatOperatorActive && op == Operator.Concat)
        result = long.Parse(result.ToString() + numbers[index].ToString());
    if(index == numbers.Count - 1)
        return result == target ? true : false;
    if(BuildEquation(target, numbers, index + 1, result, Operator.Add, concatOperatorActive)
        || BuildEquation(target, numbers, index + 1, result, Operator.Multiply, concatOperatorActive)
        || (concatOperatorActive && BuildEquation(target, numbers, index + 1, result, Operator.Concat, concatOperatorActive)))
        return true;

    return false;
}

enum Operator{
    Add,
    Multiply,
    Concat
}