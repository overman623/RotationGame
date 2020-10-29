using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Attack : MonoBehaviour
{

  GameObject player = null;

  private void SetPlayer()
  {
    if (player == null)
    {
      player = Managers.Game.GetPlayer();
    }
  }

  public void AttackGun()
  {
    // SetPlayer();
    // TestController test = player.transform.GetComponent<TestController>();
    // test.Attack();
  }

  public void AttackSword()
  {
    // SetPlayer();
    // TestController test = player.transform.GetComponent<TestController>();
    // test.Attack(true);
  }
}
