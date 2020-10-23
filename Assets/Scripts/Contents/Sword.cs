using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
  [SerializeField]
  int damage = 0;

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
    {
      MonsterController monster = other.gameObject.GetComponent<MonsterController>();
      monster.OnDamaged(damage);
    }
  }
}
