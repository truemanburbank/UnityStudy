using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    // 플레이어가 여러 명인 경우
    // Dictionary<int, GameObject> _players = new Dictionary<int, GameObject>();

    GameObject _player;
    HashSet<GameObject> _monsters = new HashSet<GameObject>();

    public Action<int> OnSpawnEvent;

    public GameObject GetPlayer() { return _player; }

    public GameObject Spawn(Define.WorlObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch(type)
        {
            case Define.WorlObject.Monster:
                _monsters.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorlObject.Player:
                _player = go;
                break;
        }

        return go;
    }

    public Define.WorlObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorlObject.Unknown;

        return bc.WorlObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorlObject type = GetWorldObjectType(go);

        switch(type)
        {
            case Define.WorlObject.Monster:
                {
                    if(_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.WorlObject.Player:
                {
                    if (_player == go)
                    {
                        _player = null;
                    }
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }
}
