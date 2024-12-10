
string inputFilePath = "./example.txt";
// string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

List<int> diskMap = ConvertToDiskMap(fileLines[0]);

Console.WriteLine($"Result of Task 1 is {Task1(new List<int>(diskMap))}");
Console.WriteLine($"Result of Task 2 is {Task2(new List<int>(diskMap))}");

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

long Task2(List<int> diskMap){
    List<BlockGroup> blockGroups = CreateBlockGroupings(diskMap);

    // foreach(BlockGroup blockGroup in blockGroups){
    //     if(blockGroup.IsFile)
    //         Console.WriteLine($"File {blockGroup.FileID} {blockGroup.Size}");
    //     else
    //         Console.WriteLine($"Free {blockGroup.Size}");
    // }
PrintBlockGroups(blockGroups);
    for(int i = blockGroups.Count - 1; i>=0; i--){
        BlockGroup currentBlockGroup = blockGroups[i];
        if(currentBlockGroup.IsFile){
            int freespaceBlockIndex = FindFreeSpace(blockGroups,currentBlockGroup.Size, i);
            
            if(freespaceBlockIndex == -1) 
                continue;
            
            int sizeDifference = blockGroups[freespaceBlockIndex].Size - currentBlockGroup.Size;
            int fileID = currentBlockGroup.FileID;
            int fileSize = currentBlockGroup.Size;
            if(sizeDifference != 0 ){
                blockGroups.Insert(freespaceBlockIndex, new BlockGroup(){IsFile = false, Size = sizeDifference});
                blockGroups.Insert(freespaceBlockIndex, new BlockGroup(){IsFile = true, FileID = fileID, Size = fileSize});
                blockGroups.RemoveAt(i + 2);
                currentBlockGroup.IsFile = false;
                i++;
            }
            else{
                blockGroups.Insert(freespaceBlockIndex, new BlockGroup(){IsFile = true, FileID = fileID, Size = fileSize});
                blockGroups.RemoveAt(i + 1);
                currentBlockGroup.IsFile = false;
            }
        }
        PrintBlockGroups(blockGroups);
    }

//    PrintBlockGroups(blockGroups);
    long checksum = CalculateChecksum(blockGroups);
    return checksum;
}

void PrintBlockGroups(List<BlockGroup> blockGroups){
    foreach(BlockGroup bg in blockGroups){
        for(int i = 0; i < bg.Size; i++){
            if(bg.IsFile)
                Console.Write(bg.FileID);
            else
                Console.Write(".");
        }
    } 
    Console.WriteLine();   
}
long CalculateChecksum(List<BlockGroup> blockGroups){
    int currentBlockIndex = 0;
    long checksum = 0;
    foreach(BlockGroup bg in blockGroups){
        if(!bg.IsFile) continue;
        for(int i = 0; i < bg.Size; i++){
            checksum += currentBlockIndex * bg.FileID;
            currentBlockIndex++;
        }
    }
    return checksum;
}



int FindFreeSpace(List<BlockGroup> blockGroups, int fileBlocks, int fileIndex){
    for(int i = 0; i < fileIndex;i++){
        if(!blockGroups[i].IsFile && blockGroups[i].Size >= fileBlocks)
            return i;
    }
    return -1;
}


List<BlockGroup> CreateBlockGroupings(List<int> diskMap){
    List<BlockGroup> blockGroupings = new();
    bool isFile = true;
    int currentFileId = 0;

    foreach(int number in diskMap){
        if(number == 0) {
            isFile = !isFile;
            continue;
        }
        BlockGroup blockGroup = new BlockGroup(){IsFile = isFile, FileID = currentFileId, Size = number };
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
    public bool IsFile {get; set;}
    public int FileID {get; set;}
    public int Size{get;set;}
    public bool Evaluated{get; set;} = false;
}