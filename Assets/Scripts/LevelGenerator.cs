using UnityEngine;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using Random = System.Random;


public class LevelGenerator : DifficultySettings
{
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

    private List<GameObject> generatedObjects;
    
    
    public static event Action OnNewLevel;


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
        generatedObjects = new List<GameObject>();
        GenerateLevel(MapGenerator.GenerateMap(coinsWinCondition * 2, fragsWinCondition, 3));
    }
    
    
    private void OnEnable() {
        ScoreCounter.OnEndLevel += HandleGenerateNextLvl;
    }

    private void OnDisable() {
        ScoreCounter.OnEndLevel -= HandleGenerateNextLvl;
    }

    void GenerateLevel(char[,] mapArray)
    {
        for (int y = 0; y < mapArray.GetLength(1); y++)
        {
            for (int x = 0; x < mapArray.GetLength(0); x++)
            {
                Vector3 tilePosition = new Vector3(x, -y, 0);
                switch (mapArray[x, y])
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
                }
            }
        }
    }

    void InstantiateTile(GameObject prefab, Vector3 position, TileType type)
    {
        GameObject tile = Instantiate(prefab, position, Quaternion.identity);
        generatedObjects.Add(tile);
        Tile tileData = new Tile();
        tileData.type = type;
        tileData.position = position;
        tile.GetComponent<Tile>().type = tileData.type;
        tile.GetComponent<Tile>().position = tileData.position;
    }

    void ClearLevel()
    {
        foreach (GameObject obj in generatedObjects)
        {
            Destroy(obj);
        }
        generatedObjects.Clear();
    }

    void HandleGenerateNextLvl(float scoreTime, int coinCount, int fragCount)
    {
        OnNewLevel.Invoke();
        float coinMultiplier = 1.5f;
        float enemyMultiplier = 1.5f;
        
        float coinDecrease = 0.75f;
        float enemyDecrease = 0.75f;

        float coinAmount = coinsWinCondition * 2;
        float enemyAmount = fragsWinCondition * 1.2f;
         
        if (coinCount == coinsWinCondition)
        {
            coinAmount = coinAmount * coinDecrease > coinsWinCondition ? coinAmount * coinDecrease : coinsWinCondition;
            enemyAmount *= enemyMultiplier;
        }

        if (fragCount == fragsWinCondition)
        {
            coinAmount *= coinMultiplier;
            enemyAmount = enemyAmount * enemyDecrease > fragsWinCondition ? enemyAmount * enemyDecrease : fragsWinCondition;
        }
        
        Debug.Log("level complete time: " + scoreTime);
        Debug.Log("Enemies in next level: " + enemyAmount);
        Debug.Log("Coins in next level: " + coinAmount);
        ClearLevel();
        GenerateMapFile((int)coinAmount, (int)enemyAmount, 3);
    }

    void GenerateMapFile(int coinAmount, int enemyAmount, int gunAmount)
    {
        float missingHealth = PlayerStartingHealth - player.GetComponent<PlayerHealth>().healthAmount;
        player.GetComponent<PlayerHealth>().Heal(missingHealth);
        
        GenerateLevel(MapGenerator.GenerateMap(coinAmount, enemyAmount, gunAmount));
    }

}