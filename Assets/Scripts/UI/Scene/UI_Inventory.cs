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
            GameObject item = Managers.UI.MakeSubItem<UI_Inventory_Item>(parent: gridPanel.transform).gameObject;
            UI_Inventory_Item invenItem = item.GetOrAddComponent<UI_Inventory_Item>();
            invenItem.SetInfo($"knife {i}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
