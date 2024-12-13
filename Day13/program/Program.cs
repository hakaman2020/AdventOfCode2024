using System.Text.RegularExpressions;

// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
List<Equations> equationsTask1 = ProcessList(fileLines,0);
List<Equations> equations2Task2 = ProcessList(fileLines,10000000000000);

Console.WriteLine($"Result of Task 1 is {GeneralTask(equationsTask1)}");
Console.WriteLine($"Result of Task 2 is {GeneralTask(equations2Task2)}");

long GeneralTask(List<Equations> equations){
    long totalTokens = 0;
    foreach(var equation in equations){
        (long A,long B) solution = SolveEquations(equation);
        if(solution == (-1,-1)) continue;
        totalTokens += solution.A * 3 + solution.B;
    }
    
    return totalTokens;
}

(long, long) SolveEquations(Equations eqs){
    long[,] A = {{eqs.Eq1A,eqs.Eq1B}, {eqs.Eq2A,eqs.Eq2B}};
    long[,] B = {{eqs.Eq1X},{eqs.Eq2Y}};
    //determine the determinant
    long determinant = A[0,0] * A[1,1] - A[0,1] * A[1,0];
    //calculate the inverse
    long[,] AInv = {{A[1,1],-A[0,1]},{-A[1,0],A[0,0]}};
    
    long[,] product = {{AInv[0,0] * B[0,0] + AInv[0,1] * B[1,0]}, {AInv[1,0] * B[0,0] + AInv[1,1] * B[1,0]}};
    
    //X and Y have to integers so if there are remains than the result X or/and Y is not a integer 
    long remainsX = product[0,0] % determinant;
    long remainsY = product[1,0] % determinant;

    if(remainsX != 0|| remainsY != 0)
        return (-1,-1);
 
    long X = product[0,0] / determinant;
    long Y = product[1,0] / determinant;

    return (X, Y);
}

List<Equations> ProcessList(List<string> fileLines, long increasePrice){
    List<Equations> equations = new();
    Regex regex = new Regex(@"\d+");

    for(int i = 0; i < fileLines.Count; i+=4){
        Equations eqs = new(); 
  
        var matches = regex.Matches(fileLines[i]);
        eqs.Eq1A = long.Parse(matches[0].Value);
        eqs.Eq2A = long.Parse(matches[1].Value);

        matches = regex.Matches(fileLines[i+1]);
        eqs.Eq1B = long.Parse(matches[0].Value);
        eqs.Eq2B = long.Parse(matches[1].Value);

        matches = regex.Matches(fileLines[i+2]);
        eqs.Eq1X = long.Parse(matches[0].Value) + increasePrice;
        eqs.Eq2Y = long.Parse(matches[1].Value) + increasePrice; 

        equations.Add(eqs);
    }
    return equations;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

public class Equations{
    public long Eq1A {get; set;}
    public long Eq1B {get; set;}
    public long Eq1X {get; set;}
    public long Eq2A {get; set;}
    public long Eq2B {get; set;}
    public long Eq2Y {get; set;}
}