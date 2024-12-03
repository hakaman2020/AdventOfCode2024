using System.Text;
using System.Text.RegularExpressions;

//string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

string combinedString = CombineStrings(fileLines);

Console.WriteLine($"Result of Task 1 is {Task1(combinedString)}");
Console.WriteLine($"Result of Task 2 is {Task2(combinedString)}");
Console.WriteLine($"Result of Task 2 calculated with a expanded regex pattern is {Task2ExpandedRegex(combinedString)}");

int Task1(string line){
    return CalculateSumFromLine(line);
}

int Task2(string line){
    string processedLine = PreprocessLine(line);
    Console.WriteLine(CalculateSumFromLineWithToggle(processedLine));
    return CalculateSumFromLine(processedLine);    
}

int Task2ExpandedRegex(string line){
    return CalculateSumFromLineWithToggle(line);
}


string CombineStrings(List<string> fileLines){
    StringBuilder sb = new StringBuilder();
    foreach (var line in fileLines){
        sb.AppendLine(line);
    }
    return sb.ToString();
    //might be better to use string.Join() when you have a array of string ready
    // return string.Join(Environment.NewLine, fileLines);
}

int CalculateSumFromLine(string line){
    int sum = 0;
    string pattern = @"mul\(\d{1,3},\d{1,3}\)";
    
    foreach(Match match in Regex.Matches(line,pattern)){
        string digitspattern = @"\d{1,3}";
        List<int> numbers = Regex.Matches(match.ToString(), digitspattern).Select(i => Convert.ToInt32(i.ToString())).ToList();
        sum += numbers[0] * numbers[1];

        //alternative
        //int num1 = int.Parse(match.Groups[1].Value);
        //int num2 = int.Parse(match.Groups[2].Value);
        //sum += num1 * num2;
    }
    
    return sum;
}

string PreprocessLine(string line){
    //consider using stringbuilder
    int start = 0;
    bool enabled = true;
    string processedLine = "";
    int index = line.IndexOf("don't()");

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

//this function uses an expanded regex pattern so you don't have to do the line processing in 
//to handle the don't and do. 
int CalculateSumFromLineWithToggle(string line){
    int sum = 0;
    string pattern = @"don't\(\)|mul\(\d{1,3},\d{1,3}\)|do\(\)";
    bool enabled = true;

    foreach(Match match in Regex.Matches(line,pattern)){
        if(match.Value.Equals("don't()")){
            enabled = false;
        }
        else if(match.Value.Equals("do()")){
            enabled = true;
        }
        else if (enabled){
            string digitspattern = @"\d{1,3}";
            List<int> numbers = Regex.Matches(match.ToString(), digitspattern).Select(i => Convert.ToInt32(i.ToString())).ToList();
            sum += numbers[0] * numbers[1];
        }

        //alternative
        //int num1 = int.Parse(match.Groups[1].Value);
        //int num2 = int.Parse(match.Groups[2].Value);
        //sum += num1 * num2;
    }
    
    return sum;
}