using UnityEngine;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using Random = System.Random;


public class LevelGenerator : DifficultySettings
{
    public string levelFileName = "Assets/Scenes/level_1.txt";
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject doorPrefab;
    public GameObject mob1Prefab;
    public GameObject mob2Prefab;
    public GameObject gun1Prefab;
    public GameObject gun2Prefab;
    public GameObject coinPrefab;
    private GameObject player;
    private Random rnd = new Random();
    private string[] mapLines;


    public enum TileType
    {
        Wall,
        Floor,
        Door,
        Mob1,
        Mob2,
        Gun1,
        Gun2,
        Coin
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mapLines = File.ReadAllLines(levelFileName);
        GenerateLevel();
    }
    
    
    private void OnEnable() {
        ScoreCounter.OnEndLevel += HandleGenerateNextLvl;
    }

    private void OnDisable() {
        ScoreCounter.OnEndLevel -= HandleGenerateNextLvl;
    }

    void GenerateLevel()
    {
        Debug.Log(mapLines);
        for (int y = 0; y < mapLines.Length; y++)
        {
            char[] chars = mapLines[y].ToCharArray();

            for (int x = 0; x < chars.Length; x++)
            {
                Vector3 tilePosition = new Vector3(x, -y, 0);

                switch (chars[x])
                {
                    case '#':
                        InstantiateTile(wallPrefab, tilePosition, TileType.Wall);
                        break;

                    case '.':
                        InstantiateTile(floorPrefab, tilePosition, TileType.Floor);
                        break;

                    case 'D':
                        InstantiateTile(doorPrefab, tilePosition, TileType.Door);
                        break;
                    
                    case 'S':
                        player.transform.position = new Vector3(x, -y, 0);
                        InstantiateTile(floorPrefab, tilePosition, TileType.Floor);
                        break;

                    case 'M':
                        InstantiateTile(floorPrefab, tilePosition, TileType.Floor);
                        if (rnd.Next(1, 3) == 1)
                        {
                            InstantiateTile(mob1Prefab, tilePosition, TileType.Mob1);
                        }
                        else
                        {
                            InstantiateTile(mob2Prefab, tilePosition, TileType.Mob2);
                        }
                        break;
                    
                    case 'G':
                        InstantiateTile(floorPrefab, tilePosition, TileType.Floor);
                        if (rnd.Next(1, 3) == 1)
                        {
                            InstantiateTile(gun1Prefab, tilePosition, TileType.Gun1);
                        }
                        else
                        {
                            InstantiateTile(gun2Prefab, tilePosition, TileType.Gun2);
                        }
                        break;
                    case 'C':
                        player.transform.position = new Vector3(x, -y, 0);
                        InstantiateTile(coinPrefab, tilePosition, TileType.Coin);
                        break;
                    // Другие символы могут быть добавлены в зависимости от ваших требований
                }
            }
        }
    }

    void InstantiateTile(GameObject prefab, Vector3 position, TileType type)
    {
        GameObject tile = Instantiate(prefab, position, Quaternion.identity);
        Tile tileData = new Tile();
        tileData.type = type;
        tileData.position = position;
        tile.GetComponent<Tile>().type = tileData.type;
        tile.GetComponent<Tile>().position = tileData.position;
    }

    void HandleGenerateNextLvl(int v)
    {
        Debug.Log("HERE");
        MapGenerator.GenerateMap(5, 15, 15, "Assets/Scenes/level_2.txt");
        mapLines = File.ReadAllLines("Assets/Scenes/level_2.txt");
        GenerateLevel();
    }

}