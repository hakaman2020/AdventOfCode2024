﻿
// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

List<List<int>> map = ConvertToIntMap(fileLines);

// foreach(List<int> line in map){
//     line.ForEach(n => Console.Write(n));
//     Console.WriteLine();
// }
Console.WriteLine($"Result of Task 1 is {Task1(map)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

int Task1(List<List<int>> map){
    int amountReachableNineHeightPositions = 0;
    for(int y = 0; y < map.Count; y++){
        for(int x = 0; x < map[0].Count; x++){
            if(map[y][x] == 0)
                amountReachableNineHeightPositions += CalculateReachableNineHeightPositions(map,y,x);
        }        
    }

    return amountReachableNineHeightPositions;
}

int Task2(){
    return 0;
}

int CalculateReachableNineHeightPositions(List<List<int>> map, int startY, int startX){
    HashSet<(int,int)> uniqueReachedNineHeightPositions = new();

    WalkTrail(map,startY, startX, uniqueReachedNineHeightPositions);

    return uniqueReachedNineHeightPositions.Count;
}

void WalkTrail(List<List<int>> map, int y, int x, HashSet<(int,int)> uniqueReachedNineHeightPositions){
    
    if(map[y][x] == 9){
        uniqueReachedNineHeightPositions.Add((y,x));
        return;
    }
       
    var directions = DetermineWalkableDirections(map,y,x);
    // foreach(var direction in directions){
    //     Console.WriteLine($"({direction.Item1},{direction.Item2})");
    // }
    foreach(var direction in directions){
        WalkTrail(map, y + direction.Item1, x + direction.Item2, uniqueReachedNineHeightPositions);
    }
}

List<(int,int)> DetermineWalkableDirections(List<List<int>> map,int y, int x){
    
    List<(int,int)> availabelDirections = new(){(0,1),(1,0),(0,-1),(-1,0)};
    List<(int,int)> acceptableDirections = new();
    int maxY = map.Count - 1;
    int maxX = map[0].Count - 1;
    int startingHeight = map[y][x];

    foreach(var direction in availabelDirections){
        if(direction.Item1 + y < 0 || direction.Item1 + y > maxY
            || direction.Item2 + x < 0 || direction.Item2 + x > maxX){
                continue;
            }
        if(map[y + direction.Item1][x + direction.Item2] == map[y][x] + 1){
            acceptableDirections.Add(direction);
        }
    }
    return acceptableDirections;
}


List<List<int>> ConvertToIntMap(List<string> inputLines){
    List<List<int>> map = new();

    foreach(string line in inputLines){
        List<int> intline = new();
        foreach(char c in line){
            if(c == '.'){
                intline.Add(0);
                continue;
            }
            intline.Add(Convert.ToInt32(c + ""));
        }
        map.Add(intline);
    }
    return map;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}