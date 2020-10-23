using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

  [SerializeField] int num = 1;
  private GameObject item;

  void Start()
  {
    StartCoroutine("AppearItem");
  }

  void OnTriggerEnter(Collider other)
  {

    if (other.gameObject.tag == "Player") //other는 플레이어 일테니까.
    {
      // Debug.Log(other.gameObject.name);
      StartCoroutine("DisappearItem");
    }

  }

  IEnumerator DisappearItem()
  {
    //아이템 획득
    TestController player = Managers.Game.GetPlayer().GetComponent<TestController>();
    player.SetItem(this);
    Managers.Resource.Destroy(item);
    item = null;
    yield return new WaitForSeconds(5); // 0~5 사이 숫자 반환 //시간동안 기다린후 다음줄 실행
    StartCoroutine("AppearItem");
  }

  IEnumerator AppearItem()
  {
    item = Managers.Resource.Instantiate($"Item/Item_{num}");
    item.transform.localPosition = gameObject.transform.position;
    item.transform.SetParent(gameObject.transform);
    yield return null; // 0~5 사이 숫자 반환 //시간동안 기다린후 다음줄 실행
  }

  public int getItemNum()
  {
    return num;
  }
}
