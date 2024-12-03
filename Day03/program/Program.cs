using System.Text;
using System.Text.RegularExpressions;

//string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

string combinedString = CombineStrings(fileLines);

Console.WriteLine($"Result of Task 1 is {Task1(combinedString)}");
Console.WriteLine($"Result of Task 2 is {Task2(combinedString)}");

int Task1(string line){
    return CalculateSumFromLine(line);
}

int Task2(string line){
    string processedLine = PreprocessLine(line);
    return CalculateSumFromLine(processedLine);    
}

string CombineStrings(List<string> fileLines){
    StringBuilder sb = new StringBuilder();
    foreach (var line in fileLines){
        sb.AppendLine(line);
    }
    return sb.ToString();
}

int CalculateSumFromLine(string line){
    int sum = 0;
    string pattern = @"mul\(\d{1,3},\d{1,3}\)";
    
    foreach(Match match in Regex.Matches(line,pattern)){
        string digitspattern = @"\d{1,3}";
        List<int> numbers = Regex.Matches(match.ToString(), digitspattern).Select(i => Convert.ToInt32(i.ToString())).ToList();
        sum += numbers[0] * numbers[1];
    }
    
    return sum;
}

string PreprocessLine(string line){
    
    int start = 0;
    bool enabled = true;
    int index = line.IndexOf("don't()");

    string processedLine = "";
    while(index != -1)
    {
        if(enabled){
            index = line.IndexOf("don't()", start);
            if(index == -1){
                processedLine += line.Substring(start);
                break;
            }
            processedLine += line.Substring(start, index - start);
            start = index + 1;
            enabled = false;
            continue;
        }
        
        index = line.IndexOf("do()", start);
        if (index == -1){
            break;
        }
        start = index + 1;
        enabled = true;
    }

   return processedLine;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}