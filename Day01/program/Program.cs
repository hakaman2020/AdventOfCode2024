//string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

List<int> firstList = new();
List<int> secondList = new();

foreach(var line in fileLines){
    string[] numbers = line.Split("   ");
    firstList.Add(Convert.ToInt32(numbers[0]));
    secondList.Add(Convert.ToInt32(numbers[1]));
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

firstList.Sort();
secondList.Sort();

Console.WriteLine($"Answer to task 1 is {Task1(firstList, secondList)}");
Console.WriteLine($"Answer to task 2 is {Task2(firstList, secondList)}");

int Task1(List<int> firstList, List<int> secondList){

    int sumDifference = 0;
    for(int i = 0; i < firstList.Count; i++){
       sumDifference += Math.Abs(firstList[i] - secondList[i]); 
    }

    return sumDifference;
}

int Task2(List<int> firstList, List<int> secondList){
    int similarityScore = 0;

    
    foreach(var number in firstList){
        
        secondList.Count(nb => nb == number);

        List<int> similarNumbers = secondList.FindAll(x => x == number);
        similarityScore += number * similarNumbers.Count;

        //can be improved
        //similarityScore += number * secondList.Count(x => x == number); //Count with predicate is part of IEnumerable()
    }

    return similarityScore;
}