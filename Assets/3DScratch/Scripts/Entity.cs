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

    private void HideEntities()
    {
        //hide all child
        this.transform.Find("Troll").gameObject.SetActive(false);
        this.transform.Find("Start").gameObject.SetActive(false);
        this.transform.Find("Exit").gameObject.SetActive(false);
        this.transform.Find("Gold").gameObject.SetActive(false);
        this.transform.Find("Wall").gameObject.SetActive(false);

    }
    public void SetType(MapGenerator.CellTypes t)
    {
        type = t;
    }
    public MapGenerator.CellTypes Type
    {
        get { return type; }
    }

    public void Show()
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
            case MapGenerator.CellTypes.Wall:
                this.transform.Find("Wall").gameObject.SetActive(true);
                break;
        }

    }
    public void Hide()
    {
        HideEntities();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
