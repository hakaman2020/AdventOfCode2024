
using System.Security.Cryptography.X509Certificates;

string inputFilePath = "./example.txt";
//string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

(List<char[]> map, List<char[]> movement) converted = ConvertToCharMap(fileLines);

// PrintMapAndMovement(converted.map, converted.movement);

Console.WriteLine($"Result of Task 1 is {Task1(converted.map, converted.movement)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

int Task1(List<char[]> map, List<char[]> movement){
    (int y, int x) robotPos = FindRobot(map);
    return 0;
}

int Task2(){
    return 0;
}

(int, int) FindRobot(List<char[]> map){
    for(int y = 0; y < map.Count; y++){
        for(int x = 0; x < map[0].Length; x++){
            if(map[y][x] == '@')
                return (y,x);
        }
    }
    return (-1,-1);
}



(List<char[]>, List<char[]> movement) ConvertToCharMap(List<string> fileLines){
    List<char[]> map = new();
    List<char[]> movement = new();
    foreach(string line in fileLines){
        if(line == string.Empty) continue;
        if(line[0] == '#')
            map.Add(line.ToArray());
        else if(line[0] != '\n')
            movement.Add(line.ToArray());
    }
    return (map, movement);
}


List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

#pragma warning disable CS8321
void PrintMapAndMovement(List<char[]> map, List<char[]> movement){

    foreach(char[] line in map){
        foreach(char c in line)
            Console.Write(c);
        Console.WriteLine();
    }

    Console.WriteLine("Movement");
    foreach(char[] line in movement){
        foreach(char c in line)
            Console.Write(c);
        Console.WriteLine();
    }
}
#pragma warning restore CS8321
