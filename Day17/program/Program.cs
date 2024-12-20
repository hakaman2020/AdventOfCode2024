
string inputFilePath = "./example.txt";
//string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);
(List<long> registers, List<(int opcode, int operand)> instructions) = procesFile(fileLines);

Console.WriteLine($"Result of Task 1 is {Task1(registers, instructions)}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

long Task1(List<long> registers, List<(int opcode, int operand)> instructions)
{
    foreach(var instruction in instructions)
    {
        PerformInstruction(registers, instruction);
    }    
    return 0;
}

void PerformInstruction(List<long> registers, (int opcode, int operand) instruction)
{
    Action<List<long>, int>[] instructions =
        [ADVInstruction, BXLInstruction, BSTInstruction, JNZInstruction
            , BXCInstruction, OUTInstruction, BDVInstruction, CDVInstruction];
    instructions[instruction.opcode](registers, instruction.operand);
}

long Task2(){
    return 0;
}

void ADVInstruction(List<long> registers, int operand)
{
    Console.WriteLine("ADV instruction");
    registers[(int)Register.A] = registers[(int)Register.A] / (long) Math.Pow(2, (double) GetValueComboOperand(registers,operand));
}

void BXLInstruction(List<long> registers, int operand)
{
    Console.WriteLine("BXL instruction");
    registers[(int) Register.B] = registers[(int) Register.B] ^ operand;
}

void BSTInstruction(List<long> registers, int operand)
{
    Console.WriteLine("BST instruction");
    registers[(int) Register.B] = 
}

void JNZInstruction(List<long> registers, int operand)
{
    Console.WriteLine("JNZ instruction");
}

void BXCInstruction(List<long> registers, int operand)
{
    Console.WriteLine("BXC instruction");
}

void OUTInstruction(List<long> registers, int operand)
{
    Console.WriteLine("OUT instruction");
}
void BDVInstruction(List<long> registers, int operand)
{
    Console.WriteLine("BDV instruction");
}

void CDVInstruction(List<long> registers, int operand)
{
    Console.WriteLine("CDV instruction");
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


(List<long> registers,List<(int, int)>) procesFile(List<string> fileLines){
    List<long> registers = new();
    List<(int, int)> instructions = new();

    for(int i = 0; i < 3; i++){
        registers.Add(long.Parse(fileLines[i].Split(": ")[1].Trim()));
    }

    var instructionsline = fileLines[4].Split(": ")[1].Split(',');
    for(int i = 0; i < instructionsline.Count(); i += 2){
        instructions.Add((int.Parse(instructionsline[i]), int.Parse(instructionsline[i + 1])));
    }

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