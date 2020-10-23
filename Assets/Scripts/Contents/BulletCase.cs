using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCase : MonoBehaviour
{

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
    {
      GameObject.Destroy(gameObject);
    }
  }

}
