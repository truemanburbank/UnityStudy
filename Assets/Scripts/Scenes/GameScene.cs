using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inventory>();

        Coroutine co = StartCoroutine("ExplodeAfterSeconds", 4.0f);

        StopCoroutine(co);
    }

    IEnumerator ExplodeAfterSeconds(float seconds)
    { 
        yield return new WaitForSeconds(seconds);
    }

    public override void Clear()
    {
       
    }
}
