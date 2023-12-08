using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : UI_Scene
{
    enum GameObjects
    {
        GridPanel,
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        for(int i = 0; i < 8; i++)
        {
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inventory_Item");
            item.transform.SetParent(gridPanel.transform);

            UI_Inventory_Item invenItem = Util.GetOrAddComponent<UI_Inventory_Item>(item);
            invenItem.SetInfo($"knife {i}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
