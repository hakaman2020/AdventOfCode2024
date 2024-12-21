
// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
(Point startPoint, Point endPoint)? result = FindStartAndEndPoint(fileLines);
if(result == null){
    Console.WriteLine("Missing start and/or end point");
    return;
}
Console.WriteLine($"Result of Task 1 is {Task1(fileLines, result!.Value.startPoint, result.Value.endPoint)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

long Task1(List<string> map, Point startPoint, Point endPoint)
{
    Dictionary<Point, (long distance, Facing facing)> points = new(){{startPoint,(0, Facing.East)}};
    HashSet<Point> visited = new();
 
    bool done = false;
    while(!done){
        //take the lowest non visited vector
        var nextPoint = FindNextPoint(points, visited);
        if(nextPoint.Equals(new Point(-1,-1))){
            done = true;
            continue;
        }
        //get the non-visited neighbours
        List<(Point neighbor, Facing whatSide)> pointsToEvaluate = PointsToEvaluate(map, visited, nextPoint);
        
        //calculate the distance to the neighbour, if that distance is smaller than update that and update facing if needed
        foreach((Point point, Facing whatSide) neighbor in pointsToEvaluate){
            (long distance, Facing facing) currentPoint = points[nextPoint];
            long newDistance = currentPoint.distance + (currentPoint.facing == neighbor.whatSide ? 1 : 1001);

            if(!points.ContainsKey(neighbor.point)){
                points.Add(neighbor.point,(newDistance,neighbor.whatSide));
            }
            else{
                (long distance, Facing facing) neighbordict = points[neighbor.point];
                if(newDistance < neighbordict.distance){
                    points[neighbor.point] = (newDistance, neighbor.whatSide); 
                }
            }
        }       
        //mark the vector as visited
        visited.Add(nextPoint);
        //continue until all vectors are visited
    } 
    return points[endPoint].distance;
}

int Task2()
{
    return 0;
}

Point FindNextPoint(Dictionary<Point,(long distance, Facing facing)> points, HashSet<Point> visited){
    Point lowestValueKey = new Point(-1,-1);
    long lowestValue = long.MaxValue;

    foreach(var entry in points){
        if(visited.Contains(entry.Key)) continue;
        if(lowestValue > entry.Value.distance){
            lowestValueKey = entry.Key;
            lowestValue = entry.Value.distance;
        } 
    }
    return lowestValueKey;
}

List<(Point, Facing)> PointsToEvaluate(List<string> map, HashSet<Point> visited, Point centerPoint)
{
    List<(int y, int x)> directions = new(){(-1, 0), (0, -1), (1, 0), (0, 1)};
    List<Facing> directionsEnum = new(){Facing.North, Facing.West, Facing.South, Facing.East};
    List<(Point, Facing)> pointsToEvaluate = new();

    for(int i = 0; i < directions.Count; i++)
    {
        Point pointToEvaluate = new Point(centerPoint.Y + directions[i].y, centerPoint.X + directions[i].x);
        
        if(visited.Contains(pointToEvaluate)) continue;
        
        char charPos = map[pointToEvaluate.Y][pointToEvaluate.X];
        if(charPos == '.' || charPos == 'E')
        {
            pointsToEvaluate.Add((pointToEvaluate, directionsEnum[i]));
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

enum Facing{
    East,
    South,
    West,
    North
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