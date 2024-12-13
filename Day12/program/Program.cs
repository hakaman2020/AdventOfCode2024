string inputFilePath = "./example.txt";
// string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

(long totalPricePerimeter, long totalPriceCorners) result = GeneralTask(fileLines);

Console.WriteLine($"Result of Task 1 is {result.totalPricePerimeter}");
Console.WriteLine($"Result of Task 2 is {result.totalPriceCorners}");

(long, long) GeneralTask(List<string> map){
    HashSet<(int,int)> visited = new();

    long totalPricePerimeter = 0;
    long totalPriceCorners = 0;
    for(int y = 0; y < map.Count; y++){
        for(int x = 0; x < map[0].Length; x++){
            if(visited.Contains((y,x))) continue;

            (int area, int perimeter, int corners) result = DetermineRegionAndCalculateAreaAndPerimeter(y,x,map[y][x], map,visited);
            totalPricePerimeter += result.area * result.perimeter;
            totalPriceCorners += result.area * result.corners;
            Console.WriteLine($"{map[y][x]}: {result.area} * {result.corners} = {totalPriceCorners}");
        }
    }

    return (totalPricePerimeter, totalPriceCorners);
}

int Task2(){
    return 0;
}

(int area, int perimeter, int corners) DetermineRegionAndCalculateAreaAndPerimeter(int y, int x, char c, List<string> map, HashSet<(int, int)> visited){

    List<(int y,int x)> directionNeighbours = availableDirections(y, x, c, map, visited, false);
    int amountNeighbours = directionNeighbours.Count;
    int perimeter = 4 - amountNeighbours;
    int corners = 0;
    if(perimeter == 2 && !AreTwoDirectionsOppositeOfEachOther(directionNeighbours[0], directionNeighbours[1]))
        corners = 1;
    else if (perimeter == 3)
        corners = 2;
    else if (perimeter == 4)
        corners = 4;
    int area = 1;
        
    visited.Add((y,x));
    List<(int,int)> directions = availableDirections(y, x, c, map, visited, true);
    foreach((int y, int x) direction in directions){
        if(visited.Contains((y + direction.y, x + direction.x))) continue;
        (int area, int perimeter, int corners) result = DetermineRegionAndCalculateAreaAndPerimeter(y + direction.y, x + direction.x, c, map, visited);
        area += result.area;
        perimeter += result.perimeter;
        corners += result.corners;
    }

    return (area, perimeter, corners);
}





bool AreTwoDirectionsOppositeOfEachOther((int y, int x) dir1, (int y, int x) dir2) {
    if(dir1.x == -dir2.x && dir1.x != 0 && dir2.x !=0) return true;
    if(dir1.y == -dir2.y && dir1.y != 0 && dir2.y != 0) return true;
    return false;
}   

List<(int, int)> availableDirections(int y, int x,char c, List<string> map, HashSet<(int,int)> visited, bool enableVisitedCheck){
    int maxHeight = map.Count;
    int maxWidth = map[0].Length;
    List<(int, int)> defaultDirections = new(){(1,0),(0,1),(-1,0),(0,-1)};
    List<(int,int)> availableDirections = new();

    foreach(var direction in defaultDirections){
        (int y, int x) newPosition = (y + direction.Item1, x + direction.Item2);

        if(newPosition.y < maxHeight && newPosition.y >= 0
            && newPosition.x >= 0 && newPosition.x < maxWidth && map[newPosition.y][newPosition.x] == c){
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