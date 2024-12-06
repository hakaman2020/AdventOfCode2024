enum GuardFacing{
    Left = 0,
    Right = 1,
    Up = 2,
    Down = 3
}

string inputFilePath = "./example.txt";
// string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Console.WriteLine($"Result of Task 1 is {Task1(fileLines)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

int Task1(List<string> map){
    (int, int) positionYX = FindGuard(map);
    if (positionYX.Item1 == -1){
        Console.WriteLine("No guard found");
        return 0;
    }
    List<char[]> convertedMap = ConvertMap(map);
    SimulateMovementGuard(convertedMap, positionYX.Item1, positionYX.Item2);
    int total = CountDistinctPositions(convertedMap);
    return total;
}

int Task2(){
    return 0;
}

List<char[]> ConvertMap(List<string> map){
    List<char[]> convertedMap = new List<char[]>();
    foreach(var line in map){
        convertedMap.Add(line.ToArray());
    }
    return convertedMap;
}


void SimulateMovementGuard(List<char[]> map, int posY, int posX){
    (int,int) movementVector = 
    while(posY >= 0  && posY < map.Count
            && posX >= 0 && posX < map[0].Length){
                (posY, posX,guardFacing) = MoveGuard(map, posY,posX, GuardFacing.Up);
            }
}


int CountDistinctPositions(List<char[]> map){
    return 0;
}


(int,int) FindGuard(List<string> map){
    (int,int) positionYX = (-1,-1);
    for(int y = 0; y < map.Count; y++){
        int x = map[y].IndexOf('^');
        if(x != -1){
            Console.WriteLine($"Guard position is {y} , {x}");
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