using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
  [SerializeField]
  int _monsterCount = 0;
  int _reserveCount = 0;

  [SerializeField]
  int _keepMonsterCount = 0;

  [SerializeField]
  Vector3 _spawnPos = new Vector3(0, 0, 5.0f);

  float _spawnRadius = 1; //_spawnPos에서 소환될 반경을 랜덤으로 정함.

  float _spawnTime = 5.0f;

  public void AddMonsterCount(int value) { _monsterCount += value; }
  public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }

  void Start()
  {
    Managers.Game.OnSpawnEvent -= AddMonsterCount;
    Managers.Game.OnSpawnEvent += AddMonsterCount;
  }

  void Update()
  {
    while (_reserveCount + _monsterCount < _keepMonsterCount)
    {
      StartCoroutine("ReserveSpawn");
    }
  }

  IEnumerator ReserveSpawn()
  {
    _reserveCount++;
    yield return new WaitForSeconds(Random.Range(0, _spawnTime)); // 0~5 사이 숫자 반환 //시간동안 기다린후 다음줄 실행

    GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");

    NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

    Vector3 randPos;

    while (true)
    {
      Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius); // 랜덤한 방향벡터 획득
      randDir.y = 0;
      randPos = _spawnPos + randDir;

      //갈수 있나.
      NavMeshPath path = new NavMeshPath();
      if (nma.CalculatePath(randPos, path))
        break;
    }

    obj.transform.position = randPos;
    _reserveCount--;
  }
}
