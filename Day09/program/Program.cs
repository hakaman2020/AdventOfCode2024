
// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

List<int> diskMap = ConvertToDiskMap(fileLines[0]);

Console.WriteLine($"Result of Task 1 is {Task1(new List<int>(diskMap))}");
Console.WriteLine($"Result of Task 2 is {Task2()}");

long Task1(List<int> diskMap){

    long checksum = 0;

    int blockIndex = 0;
    
    int leftIndex = 0;
    int rightIndex = diskMap.Count - 1;
    bool freespace = false;

    int leftID = 0;
    int rightID = (diskMap.Count / 2) + diskMap.Count % 2 -1;

    while (leftIndex <= rightIndex){
        if(!freespace){
            checksum += blockIndex * leftID;
            diskMap[leftIndex]--;
            if(diskMap[leftIndex] == 0){
                leftIndex++;
                freespace = true;
                leftID++;
            }
        }
        else{
            if(diskMap[leftIndex] == 0){
                leftIndex++;
                freespace = false;
                continue;       
            }
            diskMap[rightIndex]--;
            checksum += blockIndex * rightID;
            if(diskMap[rightIndex] == 0){
                rightID--;
                rightIndex -= 2;
            }
            diskMap[leftIndex]--;
            if(diskMap[leftIndex] == 0){
                leftIndex++;
                freespace = false;
            }
        }
        blockIndex++;
    }
    return checksum;
}

int Task2(List<int> diskMap){
    List<BlockGroup> blockGroupings = CreateBlockGroupings(diskMap);

    return 0;
}

List<BlockGroup> CreateBlockGroupings(List<int> diskMap){
    List<BlockGroup> blockGroupings = new();
    bool isFile = true;
    int currentFileId = 0;

    foreach(int number in diskMap){
        BlockGroup blockGroup = new BlockGroup(){isFile = isFile, FileID = currentFileId, size = number };
        blockGroupings.Add(blockGroup);
        if(isFile){
            currentFileId++;
        }
        isFile = !isFile;
    }
    return blockGroupings;
}

List<int> ConvertToDiskMap(string inputline){
    List<int> diskMap = new();

    foreach(char c in inputline){
        diskMap.Add(Convert.ToInt32(c + ""));
    }
    return diskMap;
}


List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

public class BlockGroup{
    public bool isFile {get; set;}
    public int FileID {get; set;}
    public int size{get;set;}
}