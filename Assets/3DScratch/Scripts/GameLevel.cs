using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class GameLevel : MonoBehaviour
{
    private MapGenerator map = null;
    private Cell[,] cells = null;

    // Start is called before the first frame update
    private Cell start = null;
    private Cell exit = null;
    void Awake()
    {
        //Generate map
        map = new MapGenerator(5, 5);

        cells = new Cell[map.Width, map.Height];


        //Create entiites
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                MapGenerator.CellTypes c = map.map[x, y];

                GameObject o = Instantiate(Resources.Load<GameObject>("cell")) as GameObject;
                o.transform.position = new Vector3(x - map.Width / 2, 0.0f, y - map.Height / 2);
                var e = o.GetComponent<Cell>();

                e.SetType(c);
                cells[x, y] = e;
            }
        }

        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                Cell e = cells[x, y];
                e.InitView();

                if (e.Type == MapGenerator.CellTypes.Start)
                {
                    e.SetCleared();
                    start = e;
                }
                else if (e.Type == MapGenerator.CellTypes.Exit)
                {
                    e.Open();
                    exit = e;
                }
                else if (e.Type == MapGenerator.CellTypes.Wall)
                {
                    e.Open();
                }
                e.Open();
            }
        }
        UpdateWalkable();
    }

    public void UpdateWalkable()
    {
        //rebuild the whole map accessibility
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                Cell e = cells[x, y];
                if (e.Cleared)
                {
                    if (x > 0)
                    {
                        Cell eAdj = cells[x - 1, y];
                        if (eAdj.Type != MapGenerator.CellTypes.Wall && !eAdj.Cleared)
                            eAdj.SetWalkable();
                    }
                    if (x < map.Width)
                    {
                        Cell eAdj = cells[x + 1, y];
                        if (eAdj.Type != MapGenerator.CellTypes.Wall && !eAdj.Cleared)
                            eAdj.SetWalkable();
                    }
                    if (y > 0)
                    {
                        Cell eAdj = cells[x, y-1];
                        if (eAdj.Type != MapGenerator.CellTypes.Wall && !eAdj.Cleared)
                            eAdj.SetWalkable();
                    }
                    if (y < map.Height)
                    {
                        Cell eAdj = cells[x, y+1];
                        if (eAdj.Type != MapGenerator.CellTypes.Wall && !eAdj.Cleared)
                            eAdj.SetWalkable();
                    }
                }

            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Do something with the object that was hit by the raycast.
                {
                    Transform obj = hit.transform.parent;
                    Cell e = obj.GetComponentInParent<Cell>();
                    e.SetCleared();
                }
                UpdateWalkable();
            }
        }
    }
}
