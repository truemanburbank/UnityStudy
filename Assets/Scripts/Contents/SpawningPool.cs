using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0; // 현재 몬스터의 수
    int _reserveCount = 0; // 소환 예약된 몬스터 수 

    [SerializeField]
    int _keepMonsterCount = 0; // 유지해야 하는 몬스터 수 

    [SerializeField]
    Vector3 _spawnPos; // 스폰되는 위치

    [SerializeField]
    float _spawnRadius = 15.0f; // 스폰되는 위치의 범위

    [SerializeField]
    float _spawnTime = 5.0f; // 스폰 쿨타임 

    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }
    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    // 만약 몬스터 카운트가 킵 몬스터 카운트보다 작다면 몬스터를 만들어 준다. 
    void Update()
    {
        // 만약 코루틴이 5초 있다가 실행이 된다면
        // 그 사이에 많은 프레임이 돌아서 몬스터가 더 많이 생성될 수도 있기 때문에
        // reserveCount를 써 준다.
        while (_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, _spawnTime));
        GameObject obj = Managers.Game.Spawn(Define.WorlObject.Monster, "Monster");
        NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;

        while(true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
            randDir.y = 0;
            randPos = _spawnPos + randDir;

            // 갈 수 있는 곳인가?
            NavMeshPath path = new NavMeshPath();
            if(nma.CalculatePath(randPos, path))
                break;
        }

        obj.transform.position = randPos;
        _reserveCount--;
    }
}
