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
    struct SplitInfo
    {
        public SplitInfo(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public int x;
        public int y;
        public int width;
        public int height;
        public void Print()
        {
            Debug.Log(" x: " + x + " y: " + y + " width: " + width + " height: " + height);
        }
    }
    static void Swap<T>(ref T a, ref T b)
    {
        T c = a;
        a = b;
        b = c;
    }

    public static void GenerateWalls(ref CellTypes[,] map, int w, int h, System.Random randomizer)
    {

        //Generate Walls
        for (int x = 0; x < w; x++)
        {
            map[x, 0] = CellTypes.Wall;
            map[x, h - 1] = CellTypes.Wall;
        }
        for (int y = 0; y < h; y++)
        {
            map[w - 1, y] = CellTypes.Wall;
            map[0, y] = CellTypes.Wall;
        }
        var splitArray = new List<SplitInfo>();
        splitArray.Add(new SplitInfo(0, 0, w, h));
        var splitArrayTmp = new List<SplitInfo>();

        int numsteps = 3;// w * h / 10; //original algorithm does that untill there is no area larger than 1 square but i'm ok with openings

        for(int i=0; i< numsteps; i++)
        {
            foreach(var split in splitArray)
            {
                if (split.width > 2 && split.height > 2)
                {
                    split.Print();
                    int splitx = randomizer.Next(split.width - 3) + 2;
                    int splity = randomizer.Next(split.height - 3) + 2;
                    Debug.Log("splitx: "+splitx + " splity: "+splity);
                    //put walls
                    for (int x = 0; x < split.width; x++)
                    {
                        map[split.x + x, splity] = CellTypes.Wall;
                    }
                    for (int y = 0; y < split.height; y++)
                    {
                        map[splitx, split.y + y] = CellTypes.Wall;
                    }
                    
                    //create 3 "doors"
                    int side = randomizer.Next(4);
                    if (side != 0)
                    {
                        int maxVal = split.height - splity - 1;
                        int rnd = randomizer.Next(maxVal);
                        Debug.Log("maxVal: " + maxVal + " rnd: " + rnd);
                        map[splitx+split.x, split.y + splity + rnd +1] = CellTypes.Empty;
                    }
                    if (side != 1)
                    {

                        map[splitx + split.x, split.y + randomizer.Next(splity - 1) +1] = CellTypes.Empty;
                    }
                    if (side != 2)
                    {
                        map[split.x + splitx + randomizer.Next(split.width - splitx - 1) +1, splity+split.y] = CellTypes.Empty;
                    }
                    if (side != 3)
                    {
                        map[split.x + randomizer.Next(splitx - 1) +1, splity + split.y] = CellTypes.Empty;
                    }
                    
                    //add 4 new areas
                    splitArrayTmp.Add(new SplitInfo(split.x, split.y, splitx, splity));
                    splitArrayTmp.Add(new SplitInfo(split.x + splitx, split.y, split.width - splitx, splity));
                    splitArrayTmp.Add(new SplitInfo(split.x, split.y+splity, splitx, split.height - splity));
                    splitArrayTmp.Add(new SplitInfo(split.x + splitx, split.y + splity, split.width - splitx, split.height - splity));

                }
                
            }

            splitArray = new List<SplitInfo>();
            Swap<List<SplitInfo>>(ref splitArray, ref splitArrayTmp);
        }
        

        //split in two leaving passage

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

        GenerateWalls(ref map, w, h, randomizer);

        //place items and monsters
        if (false)
        {
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
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


}
