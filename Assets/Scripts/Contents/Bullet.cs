using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField]
  int damage = 0;


  void OnTriggerEnter(Collider other)
  {

    if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
    {
      GameObject.Destroy(gameObject);
    }
    else if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
    {
      MonsterController monster = other.gameObject.GetComponent<MonsterController>();
      monster.OnDamaged(damage);
      GameObject.Destroy(gameObject);
    }
    else
    {
      GameObject.Destroy(gameObject, 3);
    }
  }
}
