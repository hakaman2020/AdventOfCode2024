
string inputFilePath = "./example.txt";
// string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
(Point startPoint, Point endPoint)? result = FindStartAndEndPoint(fileLines);
if(result == null){
    Console.WriteLine("Missing start and/or end point");
}
HashSet<Point> visited = new();
Console.WriteLine($"Result of Task 1 is {Task1(fileLines, result!.Value.startPoint, result.Value.endPoint, visited)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

//should implement dijkstra
long Task1(List<string> map, Point startPoint, Point endPoint, HashSet<Point> visited)
{
    Dictionary<Point, int> points = new();

    bool done = false;
    while(!done){
        //take the lowest non visited vector
        var nextPoint = FindNextPoint(points, visited);
        if(nextPoint.Y == -1 && nextPoint.X == -1){
            done = true;
            continue;
        }
        //get the non-visited neighbours
        var pointsToEvaluate = PointsToEvaluate(map, visited, nextPoint);
        
        //calculate the distance to the neighbour, if that distance is smaller than update that distance
        foreach(var point in pointsToEvaluate){
             
        }       
        //mark the vector as visited
        //continue until all vectors are visited
    } 
}

int Task2()
{
    return 0;
}

Point FindNextPoint(Dictionary<Point,int> points, HashSet<Point> visited){
    Point lowestValueKey = new Point(-1,-1);
    int lowestValue = int.MaxValue;

    foreach(var entry in points){
        if(visited.Contains(entry.Key)) continue;
        if(lowestValue > entry.Value){
            lowestValueKey = entry.Key;
            lowestValue = entry.Value;
        } 
    }
    return lowestValueKey;
}

List<Point> PointsToEvaluate(List<string> map, HashSet<Point> visited, Point centerPoint)
{
    List<(int y, int x)> directions = new(){(-1, 0), (0, -1), (1, 0), (0, 1)};
    List<Point> pointsToEvaluate = new();

    foreach(var direction in directions)
    {
        Point pointToEvaluate = new Point(centerPoint.Y + direction.y, centerPoint.X + direction.x);
        
        if(visited.Contains(pointToEvaluate)) continue;
        
        char charPos = map[pointToEvaluate.Y][pointToEvaluate.X];
        if(charPos == '.' || charPos == 'E')
        {
            pointsToEvaluate.Add(pointToEvaluate);
        }
    }
    return pointsToEvaluate;
}

(Point, Point)? FindStartAndEndPoint(List<string> map)
{
    bool startPointFound = false;
    bool endPointFound = false;

    Point startPoint = new(-1,-1);
    Point endPoint = new(-1,-1);

    for(int y = 0; y < map.Count; y++)
    {
        for(int x = 0; x < map[0].Length; x++)
        {
            if(map[y][x] == 'S')
            {
                startPoint.Y = y;
                startPoint.X = x; 
                startPointFound = true;
            }
            else if(map[y][x] == 'E')
            {
                endPoint.Y = y;
                endPoint.X = x;
                endPointFound = true;
            }
        }
    }
    if(startPointFound && endPointFound)
    {
        return (startPoint, endPoint);
    }
    return null;
}

List<string> ReadFileLines(string inputFile)
{
    return File.ReadLines(inputFile).ToList();
}

public class Point
{
    public Point(int y, int x)
    {
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