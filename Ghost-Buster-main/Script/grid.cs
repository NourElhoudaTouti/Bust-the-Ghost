using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Game.initiate_game;


public class grid : MonoBehaviour
{
    private Dictionary<Vector2, Tile> tiles;
    [SerializeField] public int w, h;
    [SerializeField] private Tile tile;
    [SerializeField] private Transform camera;


    void Start() {
        
        GenerateGrid();
    }
 
    void GenerateGrid() {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < w; x++) {
            for (int y = 0; y < h; y++) {
                var spawnedTile = Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
 
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
                
                tiles[new Vector2(x, y)] = spawnedTile;
                init(tiles, w, h);
            }
        }
        camera.transform.position = new Vector3((float)w/2 -0.5f, (float)h / 2 - 0.5f,-10);
    }
 
    
}
