using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Inventory_Item : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    string _name;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<TextMeshProUGUI>().text = _name;

        Get<GameObject>((int)GameObjects.ItemIcon).AddUIEvent((PointerEventData) => { Debug.Log($"아이템 클릭! {_name}"); }); ;
    }

    void Update()
    {
        
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
