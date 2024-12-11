//string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
    
(int, int) guardPositionYX = FindGuard(fileLines);
if (guardPositionYX.Item1 == -1){
    Console.WriteLine("No guard found");
    return;
}

Console.WriteLine($"Result of Task 1 is {Task1(fileLines, guardPositionYX)}");
Console.WriteLine($"Result of Task 2 is {Task2(fileLines, guardPositionYX)}");

int Task1(List<string> map, (int, int) guardPositionYX ){
    
    List<char[]> convertedMap = ConvertMap(map);
    SimulateMovementGuardAndDoesItLoop(convertedMap, guardPositionYX.Item1, guardPositionYX.Item2);
    int totalDistinctPositions = CountDistinctPositions(convertedMap);

    return totalDistinctPositions;
}

int Task2(List<string> map, (int, int) guardPositionYx){

    List<char[]> convertedMap = ConvertMap(map);
    int amountPositionsOfObstructions = 0;

    for(int y = 0; y < convertedMap.Count; y ++){
        for(int x = 0; x < convertedMap[0].Length; x ++){
            if(convertedMap[y][x] == '#') continue;
            convertedMap[y][x] = 'O';
            if(SimulateMovementGuardAndDoesItLoop(convertedMap, guardPositionYx.Item1, guardPositionYx.Item2)){
                amountPositionsOfObstructions++;
            }
            convertedMap[y][x] = '.';
        }
    }  

    return amountPositionsOfObstructions;
}

List<char[]> ConvertMap(List<string> map){

    List<char[]> convertedMap = new List<char[]>();
    foreach(var line in map){
        convertedMap.Add(line.ToArray());
    }
    return convertedMap;
}

bool SimulateMovementGuardAndDoesItLoop(List<char[]> map, int posY, int posX){
    
    (int,int) movementVectorYX = (-1,0); //starts with going up
    //keeps track of which obstacles are hit and where they are hit
    Dictionary<(int,int), ObstacleHitReg> ObstaclesHits = new();

    while (true){
        map[posY][posX] = 'X';
        posY += movementVectorYX.Item1;
        posX += movementVectorYX.Item2;

        if(posY < 0 || posY >= map.Count || posX < 0 || posX >= map[0].Length)
            break;
        if(map[posY][posX] == '#' || map[posY][posX] == 'O'){
            if(ObstaclesHits.ContainsKey((posY, posX))){
                if(ObstaclesHits[(posY,posX)].HasFlag(ConvertToObstacleHitReg(movementVectorYX))){
                    return true;
                }
                ObstaclesHits[(posY,posX)] |= ConvertToObstacleHitReg(movementVectorYX);
            }
            else {
                ObstaclesHits.Add((posY, posX), ConvertToObstacleHitReg(movementVectorYX));
            }
            posY -= movementVectorYX.Item1;
            posX -= movementVectorYX.Item2;
            movementVectorYX = NextMovementVector(movementVectorYX);
        }
    }
    return false;
}

ObstacleHitReg ConvertToObstacleHitReg((int,int) movementVectorYX){
    
    switch (movementVectorYX)
    {
        case (-1, _):
            return ObstacleHitReg.Bottom;
        case (_, 1):
            return ObstacleHitReg.Left;
        case (1, _):
            return ObstacleHitReg.Top;
        case (_, -1):
            return ObstacleHitReg.Right;
        default:
            return ObstacleHitReg.None;
    }
    // you can use switch expressions
    // return movementVectorYX switch
    // {
    //     (-1, _) => ObstacleHitReg.Bottom,
    //     (_, 1) => ObstacleHitReg.Left,
    //     (1, _) => ObstacleHitReg.Top,
    //     (_, -1) => ObstacleHitReg.Right,
    //     _ => ObstacleHitReg.None,
    // };
}

(int, int) NextMovementVector((int, int) movementVectorYX){

    switch (movementVectorYX)
    {
        case (-1, _):
            return (0, 1);
        case (_, 1):
            return (1, 0);
        case (1, _):
            return (0, -1);
        case (_, -1):
            return (-1, 0);
        default:
            return (0, 0);
    }
    //you can also use switch expressions
    // return movementVectorYX switch
    // {
    //     (-1, _) => (0, 1),
    //     (_, 1) => (1, 0),
    //     (1, _) => (0, -1),
    //     (_, -1) => (-1, 0),
    //     _ => (0, 0),
    // };
}

int CountDistinctPositions(List<char[]> map){
    int count = 0;

    foreach(char[] row in map){
        count += row.Count(x => x == 'X');
    }
    return count;
    //using Linq this can be shorter
    //return map.Sum(row => row.Count(x => x == 'X'));
}

(int,int) FindGuard(List<string> map){
    (int,int) positionYX = (-1,-1);

    for(int y = 0; y < map.Count; y++){
        int x = map[y].IndexOf('^');
        if(x != -1){
            positionYX.Item1 = y;
            positionYX.Item2 = x;
            return positionYX;
        }
    }

    return positionYX;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

[Flags]
enum ObstacleHitReg{
    None = 0,
    Bottom = 1 << 0,
    Left = 1 << 1,
    Top = 1 << 2,
    Right = 1 << 3,
}