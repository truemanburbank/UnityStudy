using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0; // ���� ������ ��
    int _reserveCount = 0; // ��ȯ ����� ���� �� 

    [SerializeField]
    int _keepMonsterCount = 0; // �����ؾ� �ϴ� ���� �� 

    [SerializeField]
    Vector3 _spawnPos; // �����Ǵ� ��ġ

    [SerializeField]
    float _spawnRadius = 15.0f; // �����Ǵ� ��ġ�� ����

    [SerializeField]
    float _spawnTime = 5.0f; // ���� ��Ÿ�� 

    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }
    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    // ���� ���� ī��Ʈ�� ŵ ���� ī��Ʈ���� �۴ٸ� ���͸� ����� �ش�. 
    void Update()
    {
        // ���� �ڷ�ƾ�� 5�� �ִٰ� ������ �ȴٸ�
        // �� ���̿� ���� �������� ���Ƽ� ���Ͱ� �� ���� ������ ���� �ֱ� ������
        // reserveCount�� �� �ش�.
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

            // �� �� �ִ� ���ΰ�?
            NavMeshPath path = new NavMeshPath();
            if(nma.CalculatePath(randPos, path))
                break;
        }

        obj.transform.position = randPos;
        _reserveCount--;
    }
}
