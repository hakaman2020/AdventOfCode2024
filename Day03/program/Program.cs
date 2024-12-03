using System.Linq.Expressions;
using System.Text.RegularExpressions;

string inputFilePath = "./example.txt";
//string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Console.WriteLine($"Result of Task 1 is {Task1(fileLines)}");
Console.WriteLine($"Result of Task 2 is {Task2(fileLines)}");

int Task1(List<string> fileLines){
    
    int sum = 0;
    string pattern = @"mul\(\d{1,3},\d{1,3}\)";
    
    foreach(var line in fileLines){
        foreach(Match match in Regex.Matches(line,pattern)){
            string digitspattern = @"\d{1,3}";
            List<int> numbers = Regex.Matches(match.ToString(), digitspattern).Select(i => Convert.ToInt32(i.ToString())).ToList();
            sum += numbers[0] * numbers[1];
        }
    }
    
    return sum;
}

int Task2(List<string> fileLines){
    foreach (var line in fileLines){
        PreprocesLine(line, false);
    }
    return 0;
}


string PreprocesLine(string line, Boolean isDoActive){
    
    int start = 0;

    int index = line.IndexOf(@"don't()");


    Console.WriteLine(line.Substring(start,index - start));
    start = index + 1;

    index = line.IndexOf(@"do()");
    Console.WriteLine(line.Substring(start,index - start));
    start = start + index - start;
    Console.WriteLine(line.Substring(start));
    return "";
}


List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}