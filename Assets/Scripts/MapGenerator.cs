using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class MapGenerator
{
    public enum CellTypes
    {
        Empty,
        Wall,
        Enemy,
        Gold,
        Start,
        Exit,
    }

    public CellTypes[,] map = null;
    private int width = 0;
    private int height = 0;
    public int Width
    {
        get { return width; }
    }
    public int Height
    {
        get { return height; }
    }

    // Start is called before the first frame update
    public MapGenerator(int w, int h)
    {
        width = w;
        height = h;

        System.Random randomizer = new System.Random();

        //Generate map
        map = new CellTypes[w,h];


        //Init
        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                map[x, y] = CellTypes.Empty;

        //place start and exit (top left and bottom right respectively)
        {
            int x = randomizer.Next((int)w / 3) + 1;
            int y = randomizer.Next((int)h / 3) + 1;
            map[x, y] = CellTypes.Start;
        }
        {
            int x = w - randomizer.Next((int)w / 3) - 2;
            int y = h - randomizer.Next((int)h / 3) - 2;
            map[x, y] = CellTypes.Exit;
            Debug.Log(x);
            Debug.Log(y);

        }


        //Generate Walls
        for (int x = 0; x < w; x++)
        {
            map[x, 0] = CellTypes.Wall;
            map[x, h-1] = CellTypes.Wall;
        }
        for (int y = 0; y < h; y++)
        {
            map[w-1, y] = CellTypes.Wall;
            map[0, y] = CellTypes.Wall;
        }

        //place items and monsters
        for (int x = 0; x< w; x++)
            for(int y=0; y<h;y++)
            {
                CellTypes c = map[x, y];
                if (c == CellTypes.Empty) //to keep previous layout intact
                {

                    int goldChance = randomizer.Next(10);
                    int enemyChance = randomizer.Next(10);

                    if (goldChance < 5)
                    {
                        c = CellTypes.Gold;
                    }
                    if (enemyChance < 1)
                    {
                        c = CellTypes.Enemy;
                    }
                }

                map[x, y] = c;
            }


    }


}
