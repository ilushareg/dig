using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private MapGenerator.CellTypes type = MapGenerator.CellTypes.Empty;
    private Entity entity = null;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void InitView()
    {
        entity = this.GetComponent<Entity>();
        entity.Hide();

        this.transform.Find("Base").gameObject.SetActive(true);
        this.transform.Find("BaseCleared").gameObject.SetActive(false);
        this.transform.Find("BaseCanWalk").gameObject.SetActive(false);

    }
   
    public void SetType(MapGenerator.CellTypes t)
    {
        InitView();
        type = t;
        entity.SetType(t);
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
        
        Open();
    }
    public bool Cleared
    {
        get { return isCleared; }
    }

    public void Open()
    {
        entity.Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
