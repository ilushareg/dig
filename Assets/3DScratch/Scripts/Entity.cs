using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private MapGenerator.CellTypes type = MapGenerator.CellTypes.Empty;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitView()
    {
        HideEntities();
        this.transform.Find("Base").gameObject.SetActive(true);
        this.transform.Find("BaseCleared").gameObject.SetActive(false);
        this.transform.Find("BaseCanWalk").gameObject.SetActive(false);

    }
    private void HideEntities()
    {
        //hide all child
        this.transform.Find("Troll").gameObject.SetActive(false);
        this.transform.Find("Start").gameObject.SetActive(false);
        this.transform.Find("Exit").gameObject.SetActive(false);
        this.transform.Find("Gold").gameObject.SetActive(false);

    }
    public void SetType(MapGenerator.CellTypes t)
    {
        type = t;
    }
    public MapGenerator.CellTypes Type
    {
        get { return type; }
    }

    private bool isWalkable = false;
    public void SetWalkable()
    {
        GameObject oBase = this.transform.Find("Base").gameObject;
        GameObject oBaseCleared = this.transform.Find("BaseCleared").gameObject;
        GameObject oBaseCanWalk = this.transform.Find("BaseCanWalk").gameObject;
        oBase.SetActive(false);
        oBaseCleared.SetActive(false);
        oBaseCanWalk.SetActive(true);
        isWalkable = true;

    }
    public bool Walkable
    {
        get { return isWalkable; }
    }
    private bool isCleared = false;
    public void SetCleared()
    {
        GameObject oBase = this.transform.Find("Base").gameObject;
        GameObject oBaseCleared = this.transform.Find("BaseCleared").gameObject;
        GameObject oBaseCanWalk = this.transform.Find("BaseCanWalk").gameObject;
        oBase.SetActive(false);
        oBaseCleared.SetActive(true);
        oBaseCanWalk.SetActive(false);
        isCleared = true;
        
        Show();
    }
    public void Open()
    {
        Show();
    }
    public bool Cleared
    {
        get { return isCleared; }
    }

    private void Show()
    {
        HideEntities();

        switch (type)
        {
            case MapGenerator.CellTypes.Enemy:
                this.transform.Find("Troll").gameObject.SetActive(true);
                break;
            case MapGenerator.CellTypes.Exit:
                this.transform.Find("Exit").gameObject.SetActive(true);
                break;
            case MapGenerator.CellTypes.Start:
                this.transform.Find("Start").gameObject.SetActive(true);
                break;
            case MapGenerator.CellTypes.Gold:
                this.transform.Find("Gold").gameObject.SetActive(true);
                break;
        }

    }
    private void Hide()
    {
        HideEntities();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
