
using System.ComponentModel;

string inputFilePath = "./example.txt";
//string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Console.WriteLine($"Result of Task 1 is {Task1(fileLines)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

int Task1(List<string> map){
    HashSet<(int,int)> visited = new();

    
    return 0;
}

int Task2(){
    return 0;
}

// (int area, int perimeter) DetermineRegionAndCalculateAreaAndPerimeter((int y, int x, List<string> map, HashSet<(int, int)> visited)){

// }


List<(int, int)> availableDirections(int y, int x, List<string> map, HashSet<(int,int)> visited, bool enableVisitedCheck){
    int maxHeight = map.Count;
    int maxWidth = map[0].Length;
    List<(int, int)> defaultDirections = new(){(1,0),(0,1),(-1,0),(0,-1)};
    List<(int,int)> availableDirections = new();

    foreach(var direction in defaultDirections){
        (int y, int x) newPosition = (y + direction.Item1, x + direction.Item2);

        if(newPosition.y < maxHeight && newPosition.y >= 0
            && newPosition.x >= 0 && newPosition.x < maxWidth){
                if(enableVisitedCheck && visited.Contains(newPosition))
                    continue;
                availableDirections.Add(direction);
            }
    }

    return availableDirections;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}