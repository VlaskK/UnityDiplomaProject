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
    
    
    public static event Action dropGunNewLevel;


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
        
        if (coinsWinCondition == 0 || fragsWinCondition == 0)
        {
            coinsWinCondition = 3;
            fragsWinCondition = 5;
        }
        
        GenerateLevel(MapGenerator.GenerateMap(coinsWinCondition * 2, fragsWinCondition, fragsWinCondition / 2));
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
        player.GetComponent<PlayerHealth>().resetHealth();
        dropGunNewLevel.Invoke();
        float coinMultiplier = 1.25f;
        float enemyMultiplier = 3f;
        
        float coinDecrease = 0.45f;
        float enemyDecrease = 0.75f;

        float coinAmount = coinsWinCondition * 2;
        float enemyAmount = fragsWinCondition * 1.2f;
         
        if (coinCount == coinsWinCondition)
        {
            enemyAmount = fragCount > fragsWinCondition / 2
                ? enemyAmount * enemyMultiplier * 2
                : enemyAmount * enemyDecrease;

            coinAmount = coinAmount * coinDecrease > coinsWinCondition 
                ? coinAmount * coinDecrease 
                : coinsWinCondition;
        }

        if (fragCount == fragsWinCondition)
        {
            coinAmount = coinCount > coinsWinCondition / 3
                ? coinAmount * coinMultiplier
                : coinAmount * coinDecrease;
            
            enemyAmount = playerDamageTaken > PlayerStartingHealth / 2
                ? enemyAmount * enemyMultiplier
                : enemyAmount * enemyDecrease;
        }

        coinsWinCondition = (int)coinAmount;
        fragsWinCondition = (int)enemyAmount;

        playerDamageTaken = 0;
        
        Debug.Log("level complete time: " + scoreTime);
        Debug.Log("Enemies in next level: " + enemyAmount);
        Debug.Log("Coins in next level: " + coinAmount);
        ClearLevel();
        GenerateMapFile(coinsWinCondition, fragsWinCondition, fragsWinCondition / 2);
    }

    void GenerateMapFile(int coinAmount, int enemyAmount, int gunAmount)
    {
        GenerateLevel(MapGenerator.GenerateMap(coinAmount, enemyAmount, gunAmount));
    }

}