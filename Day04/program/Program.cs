﻿
// string inputFilePath = "./example.txt";
string inputFilePath = "./input.txt";

List<string> fileLines = ReadFileLines(inputFilePath);

Console.WriteLine($"Result of Task 1 is {Task1(fileLines)}");
Console.WriteLine($"Result of Task 2 is {Task2(fileLines)}");

int Task1(List<string> puzzle){

    int height = puzzle.Count;
    int width = puzzle[0].Length;
    int totalcount = 0;
    
    for(int row = 0; row < height; row++){
        for(int column = 0; column < width; column++){
            if(puzzle[row][column] == 'X')
                totalcount += countXmas(row, column, height, width, puzzle);    
        }
    }

    return totalcount;
}

int Task2(List<string> puzzle){
    
    int height = puzzle.Count;
    int width = puzzle[0].Length;
    int totalcount = 0;

     for(int row = 0; row < height - 2; row++){
        for(int column = 0; column < width - 2; column++){
            totalcount += countXDashMAS(row, column, height, width, puzzle);
        }
    }
    return totalcount;
}

List<string> ReadFileLines(string inputFile){
    return File.ReadLines(inputFile).ToList();
}

int countXmas(int row, int column, int height, int width, List<string> puzzle){
    
    int count = 0;
    //left
    if(column >= 3 
        && puzzle[row][column - 1] == 'M' 
        && puzzle[row][column - 2] == 'A'
        && puzzle[row][column - 3] == 'S'){
        count++;
    }
    //right
    if(column < width - 3 
        && puzzle[row][column + 1] == 'M' 
        && puzzle[row][column + 2] == 'A'
        && puzzle[row][column + 3] == 'S'){
        count++;
    }
    //up
    if(row >= 3
        && puzzle[row - 1][column] == 'M' 
        && puzzle[row - 2][column] == 'A'
        && puzzle[row - 3][column] == 'S'){
        count++;
    }
    //down
    if(row < height - 3
        && puzzle[row + 1][column] == 'M' 
        && puzzle[row + 2][column] == 'A'
        && puzzle[row + 3][column] == 'S'){
        count++;
    }
    //upleft
    if(column >= 3 && row >=3 
        && puzzle[row - 1][column - 1] == 'M' 
        && puzzle[row - 2][column - 2] == 'A'
        && puzzle[row - 3][column - 3] == 'S'){
        count++;
    }
    //upright
    if(column < width -3 && row >=3
        && puzzle[row - 1][column + 1] == 'M' 
        && puzzle[row - 2][column + 2] == 'A'
        && puzzle[row - 3][column + 3] == 'S'){
        count++;
    }
    //downleft
    if(column >= 3 && row < height - 3 
        && puzzle[row + 1][column - 1] == 'M' 
        && puzzle[row + 2][column - 2] == 'A'
        && puzzle[row + 3][column - 3] == 'S'){
        count++;
    }
    //downright
    if(column < width - 3 && row < height -3 
        && puzzle[row + 1][column + 1] == 'M' 
        && puzzle[row + 2][column + 2] == 'A'
        && puzzle[row + 3][column + 3] == 'S'){
        count++;
    }
    
    return count;
}

int countXDashMAS(int row, int column, int width, int height, List<string> puzzle){

    int count = 0;

    if(     puzzle[row][column] == 'M' && puzzle[row][column + 2] == 'M'
                    && puzzle[row + 1][column + 1] == 'A'
        &&  puzzle[row + 2][column] == 'S' && puzzle[row + 2][column + 2] == 'S'){
            count++;
    }
    if(     puzzle[row][column] == 'M' && puzzle[row][column + 2] == 'S'
                    && puzzle[row + 1][column + 1] == 'A'
        &&  puzzle[row + 2][column] == 'M' && puzzle[row + 2][column + 2] == 'S'){
            count++;
    }
    if(     puzzle[row][column] == 'S' && puzzle[row][column + 2] == 'M'
                    && puzzle[row + 1][column + 1] == 'A'
        &&  puzzle[row + 2][column] == 'S' && puzzle[row + 2][column + 2] == 'M'){
            count++;
    }
    if(     puzzle[row][column] == 'S' && puzzle[row][column + 2] == 'S'
                    && puzzle[row + 1][column + 1] == 'A'
        &&  puzzle[row + 2][column] == 'M' && puzzle[row + 2][column + 2] == 'M'){
            count++;
    }
   
    return count;
}