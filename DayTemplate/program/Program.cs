


string inputFilePath = "./example.txt";
//string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

foreach(var line in fileLines){
    Console.WriteLine(line);
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}
