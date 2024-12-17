
// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Console.WriteLine($"Result of Task 1 is {Task1(fileLines)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");
//should implement dijkstra

long Task1(List<string> map)
{
    
}

int Task2()
{
    return 0;
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