using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

class MapGenerator
{
    private static Random rand = new Random();

    public static void GenerateMap(
        int coinAmount,
        int enemyAmount,
        int gunAmount,
        string filePath,
        int width = 60,
        int height = 28
    )
    {
        char[,] levelMap = new char[height, width];

        MakeWalls(levelMap, height, width);
        MakeWay(levelMap, height, width);

        PlaceRandomLetters(levelMap, 'C', coinAmount, null);
        PlaceRandomLetters(levelMap, 'M', enemyAmount, null);
        PlaceRandomLetters(levelMap, 'G', gunAmount, null);
        PlaceRandomLetters(levelMap, 'S', 1, 'M');


        GenerateFile(levelMap, height, width, filePath);
        // GenerateFile(GenerateMazeByEller(width, height), height, width, filePath);
    }

    private static void MakeWalls(char[,] levelMap, int height, int width)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                levelMap[i, j] = '#';
            }
        }

        int currentX = 1;
        while (currentX < width - 1)
        {
            int passageWidth = rand.Next(5, 11); // случайная ширина прохода от 4 до 10
            for (int y = 0; y < height; y++)
            {
                for (int x = currentX; x < width && x < currentX + passageWidth; x++)
                {
                    if (!isInBorder(x, y, height, width))
                    {
                        levelMap[y, x] = '.';
                    }
                }
            }

            currentX += passageWidth + 1;
        }
    }

    private static void MakeWay(char[,] levelMap, int height, int width)
    {
        bool hasAWay = false;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!isInBorder(x, y, height, width) && !hasAWay && (rand.Next(1, 10) == 5))
                {
                    levelMap[y, x] = '.';
                    levelMap[y + 1, x] = '.';
                    hasAWay = true;
                }
            }

            hasAWay = false;
        }
    }

    private static bool isInBorder(int x, int y, int height, int width)
    {
        return y == 0 || y == height - 1 || x == 0 || x == width - 1;
    }

    private static void GenerateFile(char[,] levelMap, int height, int width, string filePath)
    {
        StringBuilder output = new StringBuilder();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                output.Append(levelMap[i, j]);
            }

            if (i < height - 1) output.AppendLine();
        }

        File.WriteAllText(filePath, output.ToString());
    }

    private static void PlaceRandomLetters(char[,] map, char letter, int count, char? avoidLetter)
    {
        int width = map.GetLength(1);
        int height = map.GetLength(0);
        for (int i = 0; i < count; i++)
        {
            int x, y;
            do
            {
                x = rand.Next(1, width - 1);
                y = rand.Next(1, height - 1);
            } while (map[y, x] != '.' || (avoidLetter.HasValue && IsInProximity(map, x, y, avoidLetter.Value)));

            map[y, x] = letter;
        } //TODO ландшафт карт подумать 
    }

    private static bool IsInProximity(char[,] map, int x, int y, char letter, int radius = 7)
    {
        int width = map.GetLength(1);
        int height = map.GetLength(0);

        int startX = Math.Max(1, x - radius);
        int endX = Math.Min(width - 2, x + radius);
        int startY = Math.Max(1, y - radius);
        int endY = Math.Min(height - 2, y + radius);

        for (int i = startY; i <= endY; i++)
        {
            for (int j = startX; j <= endX; j++)
            {
                // Если нашли указанный символ в радиусе, возвращаем true
                if (map[i, j] == letter)
                {
                    return true;
                }
            }
        }

        return false;
    }


    public static char[,] GenerateMazeByEller(int width, int height)
    {
        //char[,] maze = new char[height, width];

        char[,] maze = new char[2 * height + 1, 2 * width + 1];

        // Заполнение лабиринта стенами
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (i % 2 == 0 || j % 2 == 0)
                {
                    maze[i, j] = '#';
                }
                else
                {
                    maze[i, j] = '.';
                }
            }
        }

        Random random = new Random();
        for (int i = 1; i < height; i++)
        {
            for (int j = 1; j < width; j++)
            {
                int passageWidth = random.Next(5, 11); // Ширина прохода от 5 до 10 точек

                if (i % 2 == 0 && j % passageWidth == 0)
                {
                    for (int k = 1; k < passageWidth; k++)
                    {
                        maze[2 * i, 2 * j - k] = '.'; // Создать проход шириной passageWidth
                    }
                }
                else if (i % 2 != 0 && j < width - 1 && random.Next(2) == 0)
                {
                    maze[2 * i - 1, 2 * j] = '.'; // Удалить стенку
                }
            }
        }

        return maze;
    }
}