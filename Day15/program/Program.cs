﻿using System.Data;
using System.Runtime.CompilerServices;

string inputFilePath = "./example.txt";
// string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

(List<char[]> map, List<char[]> movement) converted = ConvertToCharMap(fileLines);
List<char[]> secondMap = ConvertToSecondMap(converted.map);

Console.WriteLine($"Result of Task 1 is {Task1(converted.map, converted.movement)}");
Console.WriteLine($"Result of Task 2 is {Task2(secondMap, converted.movement)}");

int Task1(List<char[]> map, List<char[]> movement){
    (int y, int x) robotPos = FindRobot(map);
    map[robotPos.y][robotPos.x] = '.';
    foreach(char[] moves in movement){
        foreach(char move in moves){
            robotPos = SimulateMovePart1(map, robotPos, move);
        }
    }
    return CalculateGPSPart1(map);
}

int Task2(List<char[]> map, List<char[]> movement){
    (int y, int x) robotPos = FindRobot(map);
    map[robotPos.y][robotPos.x] = '.';
    PrintMap(map, robotPos);
    return 0;
}

(int y, int x) SimulateMovePart2(List<char[]> map, (int y, int x) robotPos, char move){
    (int y, int x) vector = ConvertMoveToVector(move);
    (int y, int x) futurePos = (robotPos.y + vector.y, robotPos.x + vector.x);
    char mapFuturePos = map[futurePos.y][futurePos.x];

    if(mapFuturePos == '.') return futurePos;
    if(mapFuturePos == '#') return robotPos;
    
    if(move == '<' || move == '>'){
        return HorizontalMovement(map, futurePos, vector);
    }
    return VerticalMovement(map, futurePos, vector);
}

(int y, int x) HorizontalMovement(List<char[]> map, (int y, int x) robotPos, (int y, int x) vector){
    (int y, int x) currentPos = (robotPos.y + vector.y, robotPos.x + vector.x);
    while(true){
        char mapChar = map[currentPos.y][currentPos.x];
        if(mapChar == '#'){
            return (-1,-1);
        }
        else if(mapChar == '.'){
            
        }
    }
}

(int y, int x) VerticalMovement(List<char[]> map, (int y, int x) robotPos, (int y, int x) vector){
    return (-1,-1);
}

List<char[]> ConvertToSecondMap(List<char[]> map){
    List<char[]> convertedMap = new();
    int sizeRow = map[0].Length;
    foreach(char[] row in map){
        char[] tmp = new char[sizeRow * 2];
        for(int i = 0; i < row.Length; i++){
            char c = row[i];
            if(c == '.' || c == '#'){
                tmp[i * 2] = c;
                tmp[i * 2+ 1] = c;
            }
            else if(c == 'O'){
                tmp[i * 2] = '[';
                tmp[i * 2 + 1] = ']';
            }
            else if(c == '@'){
                tmp[i * 2] = '@';
                tmp[i * 2 + 1] = '.';
            }
        }
        convertedMap.Add(tmp);
    }
    return convertedMap;
}

int CalculateGPSPart1(List<char[]> map){
    int sumGPS = 0;
    for(int y = 0; y < map.Count; y++){
        for(int x = 0; x < map[0].Length; x++){
            if(map[y][x] == 'O')
                sumGPS += 100 * y + x;
        }
    }
    return sumGPS;
}


(int, int) SimulateMovePart1(List<char[]> map, (int y, int x) robotPos, char move){
    (int y, int x) vector = ConvertMoveToVector(move);
    (int y, int x) futurePos = (robotPos.y + vector.y, robotPos.x + vector.x);
    char mapFuturePos = map[futurePos.y][futurePos.x];

    if(mapFuturePos == '.') return futurePos;
    if(mapFuturePos == '#') return robotPos;
    else{
        (int y, int x) newPos = LookForEmptySpace(map, futurePos, vector);
        if (newPos == (-1,-1)) return robotPos;
        map[futurePos.y][futurePos.x] = '.';
        map[newPos.y][newPos.x] = 'O';
    }
    return futurePos;
}

(int, int) LookForEmptySpace(List<char[]> map, (int y, int x) startingPos, (int y, int x) vector){
    
    (int y, int x) currentPos = (startingPos.y + vector.y, startingPos.x + vector.x);
    while(true){
        char mapFuturePos = map[currentPos.y][currentPos.x];
        if(mapFuturePos == '.') return currentPos;
        else if  (mapFuturePos == '#') return (-1,-1);
        currentPos = (currentPos.y + vector.y, currentPos.x + vector.x);
    }
}


(int,int) ConvertMoveToVector(char move){
    return move switch{
        '<' => (0, -1),
        '^' => (-1, 0),
        '>' => (0, 1),
        'v' => (1, 0),
        _ => (0, 0)
    };
}

(int, int) FindRobot(List<char[]> map){
    for(int y = 0; y < map.Count; y++){
        for(int x = 0; x < map[0].Length; x++){
            if(map[y][x] == '@')
                return (y,x);
        }
    }
    return (-1,-1);
}

(List<char[]>, List<char[]> movement) ConvertToCharMap(List<string> fileLines){
    List<char[]> map = new();
    List<char[]> movement = new();
    foreach(string line in fileLines){
        if(line == string.Empty) continue;
        if(line[0] == '#')
            map.Add(line.ToArray());
        else if(line[0] != '\n')
            movement.Add(line.ToArray());
    }
    return (map, movement);
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

#pragma warning disable CS8321
void PrintMap(List<char[]> map, (int y, int x) robotPos){
    map[robotPos.y][robotPos.x] = '@';
    foreach(char[] line in map){
        foreach(char c in line)
            Console.Write(c);
        Console.WriteLine();
    }
    map[robotPos.y][robotPos.x] = '.';
}

void PrintMovement(List<char[]> movement){
    Console.WriteLine("Movement");
    foreach(char[] line in movement){
        foreach(char c in line)
            Console.Write(c);
        Console.WriteLine();
    }
}
#pragma warning restore CS8321
