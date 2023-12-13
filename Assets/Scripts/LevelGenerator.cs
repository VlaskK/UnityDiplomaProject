using UnityEngine;

using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class LevelGenerator : MonoBehaviour
{
    public string levelFileName = "Assets/Scenes/level_1.txt";
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public GameObject doorPrefab;
    private GameObject player;
    
    public enum TileType
    {
        Wall,
        Floor,
        Door
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GenerateLevel();
    }

    void GenerateLevel()
    {
        string[] lines = File.ReadAllLines(levelFileName);

        for (int y = 0; y < lines.Length; y++)
        {
            char[] chars = lines[y].ToCharArray();

            for (int x = 0; x < chars.Length; x++)
            {
                Vector2 tilePosition = new Vector2(x, -y);

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
                    // Другие символы могут быть добавлены в зависимости от ваших требований
                }
            }
        }
    }

    void InstantiateTile(GameObject prefab, Vector2 position, TileType type)
    {
        GameObject tile = Instantiate(prefab, position, Quaternion.identity);
        Tile tileData = new Tile();
        tileData.type = type;
        tileData.position = position;
        tile.GetComponent<Tile>().type = tileData.type;
        tile.GetComponent<Tile>().position = tileData.position;
    }
}