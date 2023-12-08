using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{ 
    public T Load<T>(string path) where T : Object // 조건: Where
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        GameObject go = Object.Instantiate(prefab, parent);
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index);

        // 이 스크립트에 있는 같은 함수를 부르지 않기 위해 Object를 붙임. 
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }

}
