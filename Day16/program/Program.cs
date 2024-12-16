
using System.Runtime.CompilerServices;

string inputFilePath = "./example.txt";
//string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Console.WriteLine($"Result of Task 1 is {Task1(fileLines)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

int Task1(List<string> map){
    (Point startPos, Point endPos)? result = FindStartAndEndPoint(map);
    if (result == null){
        Console.WriteLine("Missing starting point and/or ending point");
        return -1;
    }
}




int TaskX(List<string> map){

    (Point startPos, Point endPos)? result = FindStartAndEndPoint(map);
    if (result == null){
        Console.WriteLine("Missing starting point and/or ending point");
        return -1;
    }

    List<Point>? path = DeterminePath(map, result.Value.startPos, result.Value.endPos);
    if (path == null){
        Console.WriteLine("No path found");
    }
    else{
        foreach(var point in path){
            Console.WriteLine(point.ToString());
        }
    }
    return CalculateScore(path);
}

int Task2(){
    return 0;
}

int CalculateScore(List<Point>? path){
    if (path == null) return -1;
    int score = 0;
    Point currentFacing = new Point(0,1);

    for(int i = 1; i < path.Count; i++){
        score++;
        Point nextPoint = path[i];
        Point currentPoint = path[i - 1];
        Point difference = new Point(nextPoint.Y - currentPoint.Y, nextPoint.X - currentPoint.X);
        if (!difference.Equals(currentFacing)){
            currentFacing = difference;
            score += 1000;
        }
    }

    return score;
}

List<Point>? DeterminePath(List<string> map, Point startPos, Point endPos)
{
    HashSet<Point> visited = new HashSet<Point>(){startPos};
    Dictionary<Point, Point> connectedPoints = new();
    Queue<Point> toEvaluateQueue = new Queue<Point>();
    toEvaluateQueue.Enqueue(startPos); 

    while(toEvaluateQueue.Count > 0){
        
        var currentPoint = toEvaluateQueue.Dequeue();

        if (endPos.Equals(currentPoint)){
            return BuildPath(connectedPoints, endPos);
        }

        AddPointsToQueue(map, currentPoint, visited, toEvaluateQueue, connectedPoints);
    }
    return null;
}

List<Point> BuildPath(Dictionary<Point,Point> connectedPoints, Point endPos){
    List<Point> path = new(){endPos};
    while(connectedPoints.TryGetValue(endPos, out var point)){
        endPos = point;
        path.Insert(0,endPos);
    }
    return path;
}

void AddPointsToQueue(List<string> map, Point centerPoint, HashSet<Point> visited, Queue<Point> toEvaluateQueue, Dictionary<Point,Point> connectedPoints){
    List<(int y, int x)> directions = new(){(-1, 0), (0, -1), (1, 0), (0, 1)};
    foreach(var direction in directions){
        Point pointToEvaluate = new Point(centerPoint.Y + direction.y, centerPoint.X + direction.x);
        if(visited.Contains(pointToEvaluate)) continue;
        char charPos = map[pointToEvaluate.Y][pointToEvaluate.X];
        if(charPos == '.' || charPos == 'E'){
            toEvaluateQueue.Enqueue(pointToEvaluate);
            visited.Add(pointToEvaluate);
            connectedPoints.Add(pointToEvaluate,centerPoint);
        }
    }
}
(Point, Point)? FindStartAndEndPoint(List<string> map){
    bool startPointFound = false;
    bool endPointFound = false;

    Point startPoint = new(-1,-1);
    Point endPoint = new(-1,-1);

    for(int y = 0; y < map.Count; y++){
        for(int x = 0; x < map[0].Length; x++){
            if(map[y][x] == 'S'){
                startPoint.Y = y;
                startPoint.X = x; 
                startPointFound = true;
            }
            else if(map[y][x] == 'E'){
                endPoint.Y = y;
                endPoint.X = x;
                endPointFound = true;
            }
        }
    }
    if(startPointFound && endPointFound){
        return (startPoint, endPoint);
    }
    return null;
}
List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

public class Point{
    public Point(int y, int x){
        this.Y = y;
        this.X = x;
    }
    public int Y {get; set;}
    public int X {get; set;}

    public override bool Equals(object? obj)
    {
        if(obj is Point item)
        {
            return item.Y == this.Y && item.X == this.X;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Y, X);
    }

    public override string ToString()
    {
        return $"({Y},{X})";
    }
}