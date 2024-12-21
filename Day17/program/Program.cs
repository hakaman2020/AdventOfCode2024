
// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
(List<long> registers, List<int> instructions) = procesFile(fileLines);

Console.Write($"Result of Task 1 is ");
Task1(registers, instructions);
Console.WriteLine($"Result of Task 2 is {Task2()}");

long Task1(List<long> registers, List<int> instructions)
{
    Func<List<long>, int,int, int>[] instructionsList =
        [ADVInstruction, BXLInstruction, BSTInstruction, JNZInstruction
            , BXCInstruction, OUTInstruction, BDVInstruction, CDVInstruction];

    int count =0;
    bool firstOut = true;
    for(int ip = 0; ip < instructions.Count; ip +=2)
    {
        if(instructions[ip] == 5)
        {
            if(!firstOut)
            {
                Console.Write(",");
            }
            firstOut = false;
        } 
        ip = instructionsList[instructions[ip]](registers, instructions[ip + 1], ip);
        count++;
    }
    Console.WriteLine();
    return 0;
}

long Task2()
{
    return 0;
}

int ADVInstruction(List<long> registers, int operand,int ip)
{
    registers[(int)Register.A] = registers[(int)Register.A] / (long) Math.Pow(2, (double) GetValueComboOperand(registers,operand));
    return ip;
}

int BXLInstruction(List<long> registers, int operand, int ip)
{
    registers[(int) Register.B] = registers[(int) Register.B] ^ operand;
    return ip;
}

int BSTInstruction(List<long> registers, int operand, int ip)
{
    registers[(int) Register.B] = GetValueComboOperand(registers, operand) % 8;
    return ip; 
}

int JNZInstruction(List<long> registers, int operand, int ip)
{
    if(registers[(int) Register.A] != 0) return operand - 2;
    return ip;
}

int BXCInstruction(List<long> registers, int operand, int ip)
{
    registers[(int) Register.B] = registers[(int) Register.B] ^ registers[(int) Register.C];
    return ip;
}

int OUTInstruction(List<long> registers, int operand, int ip)
{
    Console.Write(GetValueComboOperand(registers,operand) % 8);
    return ip;
}

int BDVInstruction(List<long> registers, int operand, int ip)
{
    registers[(int)Register.B] = registers[(int)Register.A] / (long) Math.Pow(2, (double) GetValueComboOperand(registers,operand));
    return ip;
}

int CDVInstruction(List<long> registers, int operand, int ip)
{
    registers[(int)Register.C] = registers[(int)Register.A] / (long) Math.Pow(2, (double) GetValueComboOperand(registers,operand));
    return ip;
}

long GetValueComboOperand(List<long> registers, int operand)
{
    return operand switch{
        >= 0 and <= 3 => operand,
        4 => registers[(int)Register.A],
        5 => registers[(int)Register.B],
        6 => registers[(int)Register.C],
        _ => -1
    };
}


(List<long> registers,List<int>) procesFile(List<string> fileLines){
    List<long> registers = new();
    List<int> instructions = new();

    for(int i = 0; i < 3; i++){
        registers.Add(long.Parse(fileLines[i].Split(": ")[1].Trim()));
    }
    instructions = fileLines[4].Split(": ")[1].Split(',').Select(n => int.Parse(n)).ToList();
    return (registers, instructions);
}


List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

enum Register
{
    A,
    B,
    C
}