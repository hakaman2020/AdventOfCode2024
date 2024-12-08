// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Dictionary<char, List<(int,int)>> antennas = FindAntennas(fileLines);
int maxHeight = fileLines.Count;
int maxWidth = fileLines[0].Length;

Console.WriteLine($"Result of Task 1 is {Task1(antennas, maxHeight, maxWidth)}");
Console.WriteLine($"Result of Task 2 is {Task2(antennas, maxHeight, maxWidth)}");

int Task1(Dictionary<char,List<(int,int)>> antennas, int maxHeight, int maxWidth){

    HashSet<(int,int)> antiNodesPositions = new();

    foreach(var antennaList in antennas){
        var antennaPositions = antennaList.Value;
        for(int i = 0; i < antennaPositions.Count; i++){
            for(int j = i + 1; j < antennaPositions.Count;j++){
                ((int,int),(int,int)) antiNodes = CalculateAntiNodes(antennaPositions[i],antennaPositions[j]);
                if((antiNodes.Item1.Item1 >= 0 && antiNodes.Item1.Item1 < maxHeight) 
                    && (antiNodes.Item1.Item2 >= 0 && antiNodes.Item1.Item2 < maxWidth)){
                    antiNodesPositions.Add(antiNodes.Item1);
                }
                if((antiNodes.Item2.Item1 >= 0 && antiNodes.Item2.Item1 < maxHeight) 
                    && (antiNodes.Item2.Item2 >= 0 && antiNodes.Item2.Item2 < maxWidth)){
                    antiNodesPositions.Add(antiNodes.Item2);
                }
            }
        }
    }
    
    return antiNodesPositions.Count;
}

int Task2(Dictionary<char,List<(int,int)>> antennas, int maxHeight, int maxWidth){

    HashSet<(int,int)> antiNodesPositions = new();

    foreach(var antennaList in antennas){
        var antennaPositions = antennaList.Value;
        for(int i = 0; i < antennaPositions.Count; i++){
            for(int j = i + 1; j < antennaPositions.Count;j++){
                List<(int,int)> antiNodesPositionsList = CalculateAntiNodesExtended(antennaPositions[i], antennaPositions[j], maxHeight, maxWidth);
                foreach(var antiNodePosition in antiNodesPositionsList){
                   antiNodesPositions.Add(antiNodePosition); 
                }
            }
        }
    }

    return antiNodesPositions.Count;
}

((int,int),(int,int)) CalculateAntiNodes((int,int) antenna1, (int,int) antenna2){

    (int,int) differenceVector = CalculateVector(antenna1, antenna2, '-');
    (int,int) antiNode1 = CalculateVector(antenna1, differenceVector, '+');
    (int,int) antiNode2 = CalculateVector(antenna2, differenceVector,'-');
 
    return (antiNode1, antiNode2);   
}

(int,int) CalculateVector((int,int) coord1, (int,int) coord2, char op){

    return op switch{
        '+' => (coord1.Item1 + coord2.Item1, coord1.Item2 + coord2.Item2),
        '-' => (coord1.Item1 - coord2.Item1, coord1.Item2 - coord2.Item2),
        _ =>(int.MaxValue,int.MaxValue)
    };
}

List<(int,int)> CalculateAntiNodesExtended((int,int) antenna1, (int,int) antenna2, int maxHeight, int maxWidth){

    List<(int,int)> antiNodesList = new();
    (int,int) differenceVector = CalculateVector(antenna1, antenna2, '-');
    
    antiNodesList = CalculateAntiNodesPartial(antenna1, differenceVector, maxHeight, maxWidth, '+');
    List<(int,int)> antiNodeListPartial = CalculateAntiNodesPartial(antenna1, differenceVector, maxHeight, maxWidth,'-');
    antiNodesList = antiNodesList.Concat(antiNodeListPartial).ToList();
    return antiNodesList;
}

List<(int,int)> CalculateAntiNodesPartial((int,int) antenna, (int,int) differenceVector, int maxHeight, int maxWidth, char op){
    
    List<(int,int)> antiNodePartialList = new(){antenna};

    bool done = false;
    for(int i = 1; !done; i++){
        (int,int) antiNode = CalculateVector(antenna, (differenceVector.Item1 * i, differenceVector.Item2 * i), op);
        if((antiNode.Item1 >= 0 && antiNode.Item1 < maxHeight) 
            && (antiNode.Item2 >= 0 && antiNode.Item2 < maxWidth)){
            antiNodePartialList.Add(antiNode);
        }
        else done = true;
    }

    return antiNodePartialList;
}

Dictionary<char, List<(int,int)>> FindAntennas(List<string> map){

    Dictionary<char,List<(int,int)>> antennas = new();

    for(int y = 0; y < map.Count; y++){
        for(int x = 0; x < map[y].Length; x++){
            char antenna = map[y][x];
            if(antenna != '.'){
                if(!antennas.ContainsKey(antenna)){
                    antennas.Add(antenna, new List<(int,int)>{(y,x)});
                    continue;
                }
                antennas[antenna].Add((y,x));
            }
        }
    }

    return antennas;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}