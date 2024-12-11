
// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
List<List<int>> map = ConvertToIntMap(fileLines);
(int uniqueReachedNineHeightPositions, int sumRating) results = CalculateResults(map);

Console.WriteLine($"Result of Task 1 is {results.uniqueReachedNineHeightPositions}");
Console.WriteLine($"Result of Task 2 is {results.sumRating}");

(int,int) CalculateResults(List<List<int>> map){
    int amountReachableNineHeightPositions = 0;
    int sumRating = 0;
    for(int y = 0; y < map.Count; y++){
        for(int x = 0; x < map[0].Count; x++){
            if(map[y][x] == 0){
                (int uniqueReachedNineHeightPositions, int rating) results = CalculateReachableNineHeightPositionsAndRatings(map,y,x);
                amountReachableNineHeightPositions += results.uniqueReachedNineHeightPositions;
                sumRating += results.rating;
            }
        }        
    }

    return (amountReachableNineHeightPositions, sumRating);
}

(int,int) CalculateReachableNineHeightPositionsAndRatings(List<List<int>> map, int startY, int startX){
    Dictionary<string, int> uniqueReachedNineHeightPositions = new();
    
    WalkTrail(map,startY, startX, uniqueReachedNineHeightPositions);
    int rating = uniqueReachedNineHeightPositions.Values.Sum();

    return (uniqueReachedNineHeightPositions.Count, rating);
}

void WalkTrail(List<List<int>> map, int y, int x, Dictionary<string, int> uniqueReachedNineHeightPositions){
    if(map[y][x] == 9){
        string coordString = $"({y},{x})";
        if(uniqueReachedNineHeightPositions.ContainsKey(coordString)){
            uniqueReachedNineHeightPositions[coordString]++;
        }
        else{
            uniqueReachedNineHeightPositions.Add(coordString,1);
        }
        return;
    }
       
    var directions = DetermineWalkableDirections(map,y,x);
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
                intline.Add(-1);
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