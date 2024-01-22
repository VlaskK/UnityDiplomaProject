using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

class MapGenerator
{
    private static Random rand = new Random();

    public static void GenerateMap(int countC, int countM, int countG, string filePath, int width = 20, int height = 28)
    {
        char[,] map = new char[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                {
                    map[i, j] = '#';
                }
                else
                {
                    map[i, j] = '.';
                }
            }
        }
        
        // Расстановка G, M, D и S по исходному шаблону или случайно
        
        // Заполнение карты буквами C и M
        PlaceRandomLetters(map, 'C', countC, null);
        PlaceRandomLetters(map, 'M', countM, null);
        PlaceRandomLetters(map, 'G', countG, null);
        PlaceRandomLetters(map, 'S', 1, 'M');

        // Создание строки для сохранения в файл
        StringBuilder output = new StringBuilder();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                output.Append(map[i, j]);
            }
            if (i < height - 1) output.AppendLine();
        }

        // Запись строки в файл
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
            }
            while (map[y, x] != '.' || (avoidLetter.HasValue && IsInProximity(map, x, y, avoidLetter.Value)));
            // Проверяем, чтобы позиция была свободна
            
            map[y, x] = letter;
        }
    }
    
    private static bool IsInProximity(char[,] map, int x, int y, char letter, int radius = 5) // можно изменить радиус
    {
        int width = map.GetLength(1);
        int height = map.GetLength(0);

        // Начальные и конечные индексы проверки с учетом границ карты и радиуса
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

}