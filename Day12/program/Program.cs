// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Console.WriteLine($"Result of Task 1 is {Task1(fileLines)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

long Task1(List<string> map){
    HashSet<(int,int)> visited = new();

    long totalPrice = 0;

    for(int y = 0; y < map.Count; y++){
        for(int x = 0; x < map[0].Length; x++){
            if(visited.Contains((y,x))) continue;

            (int area, int perimeter) result = DetermineRegionAndCalculateAreaAndPerimeter(y,x,map[y][x], map,visited);
            totalPrice += result.area * result.perimeter;
        }
    }

    return totalPrice;
}

int Task2(){
    return 0;
}

(int area, int perimeter, int corners) DetermineRegionAndCalculateAreaAndPerimeter(int y, int x, char c, List<string> map, HashSet<(int, int)> visited){

    int amountNeighbours = availableDirections(y, x, c, map, visited, false).Count;
    int perimeter = 4 - amountNeighbours;
    int area = 1;
    int corners = 0;
    if(perimeter == 2) corners = 1;

    visited.Add((y,x));
    List<(int,int)> directions = availableDirections(y, x, c, map, visited, true);
    foreach((int y, int x) direction in directions){
        if(visited.Contains((y + direction.y, x + direction.x))) continue;
        (int area, int perimeter) result = DetermineRegionAndCalculateAreaAndPerimeter(y + direction.y, x + direction.x, c, map, visited);
        area += result.area;
        perimeter += result.perimeter;
    }

    return (area, perimeter);
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