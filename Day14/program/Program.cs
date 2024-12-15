using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
int maxWidth = inputFilePath == "./example.txt" ? 11 : 101;
int maxHeight = inputFilePath == "./example.txt" ? 7 : 103;

Console.WriteLine($"Result of Task 1 is {Task1(fileLines, maxWidth, maxHeight)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

int Task1(List<string> fileLines, int maxWidth, int maxHeight){

    List<Robot> robots = ProcesFileLines(fileLines);

    SimulateMovement(robots, 10000, maxWidth, maxHeight);

    int safetyFactor = CalculateSafetyFactor(robots, maxWidth, maxHeight);


    return safetyFactor;
}



int Task2(){
    return 0;
}

void PrintMap(List<Robot> robots, int maxWidth, int maxHeight){
    int[,] map = new int[maxHeight, maxWidth];
    foreach(Robot robot in robots){
        map[robot.PosY, robot.PosX]++;
    }
    for(int y = 0; y < maxHeight; y++){
        for(int x = 0; x < maxWidth; x++){
            if(map[y,x] == 0)
                Console.Write(".");
            else
                Console.Write(map[y,x]);
        }
        Console.WriteLine();
    }
}


void SimulateMovement(List<Robot> robots, int seconds, int maxWidth, int maxHeight){
    int currentLowestSafetyFactor = int.MaxValue;
    int frame = 0;
    for(int i = 0; i < seconds; i++){
        foreach(Robot robot in robots){
            robot.PosX = (robot.PosX + robot.VX) % maxWidth;
            if(robot.PosX < 0) robot.PosX = maxWidth + robot.PosX;
            robot.PosY = (robot.PosY + robot.VY) % maxHeight;
            if(robot.PosY < 0) robot.PosY = maxHeight + robot.PosY;
        }
        
        int safetyFactor = CalculateSafetyFactor(robots, maxWidth, maxHeight);
        if(safetyFactor < currentLowestSafetyFactor){
            currentLowestSafetyFactor = safetyFactor;
            frame = i;
        }
        if(i == 6474){
            PrintMap(robots, maxWidth, maxHeight);
        }
    }
    Console.WriteLine($"frameSecond : {frame}");
}

int CalculateSafetyFactor(List<Robot> robots, int maxWidth, int maxHeight){
    int centerWidth = maxWidth / 2;
    int centerHeight = maxHeight / 2;

    int[] amountRobots = new int[4];

    foreach(Robot robot in robots){
        int quadrantNumber = 
            ReturnQuadrant(robot.PosX, robot.PosY, maxWidth, maxHeight, centerWidth, centerHeight);
       if(quadrantNumber!= 0) amountRobots[quadrantNumber - 1]++;
    }

    return amountRobots.Aggregate((total,next) => total * next);
}

//Quadrants layout
//  1|2
//  ---
//  4|3

int ReturnQuadrant(int x, int y, int maxWidth, int maxHeight, int centerWidth, int centerHeight){
    if(x < centerWidth && y < centerHeight) return 1;
    if(x < centerWidth && y > centerHeight) return 4;
    if(x > centerWidth && y < centerHeight) return 2;
    if(x > centerWidth && y > centerHeight) return 3;
    return 0;
}
List<Robot> ProcesFileLines(List<string> fileLines){
    List<Robot> robots = new();
    foreach (string line in fileLines){
        var numbers = Regex.Matches(line, @"-?\d+");
        robots.Add(new Robot(){
            PosX = int.Parse(numbers[0].Value),
            PosY = int.Parse(numbers[1].Value),
            VX = int.Parse(numbers[2].Value),
            VY = int.Parse(numbers[3].Value),
        });
    }
    return robots;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

public class Robot{
    public int PosX {get; set;}
    public int PosY {get; set;}
    public int VX {get; set;}
    public int VY {get; set;}
}