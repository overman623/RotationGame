using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{

  enum GameObjects
  {
    ItemIcon,
    ItemNameText,
    ItemCountText,
  }

  string _name;

  int _num = 0;
  int _count = 0;

  public override void Init()
  {
    Bind<GameObject>(typeof(GameObjects));
    Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;
    Get<GameObject>((int)GameObjects.ItemCountText).GetComponent<Text>().text = $"{_count}";

    Get<GameObject>((int)GameObjects.ItemIcon).AddUIEvent((PointerEventData) =>
    {
      if (--_count <= 0)
      {
        Debug.Log($"아이템 없어짐! {_name}");
        Destroy(gameObject);

      }
      else
      {
        Debug.Log($"아이템 수량 감소! {_name}");
        Get<GameObject>((int)GameObjects.ItemCountText).GetComponent<Text>().text = $"{_count}";
      }
      Managers.Data.removeItemData(_num);
      //나중에 아이템<번호, 개수> 의 방식으로 데이터를 저장하는게 좋을것 같음.
    });
  }

  public void SetInfo(string name)
  {
    _name = name;
  }

  public void SetInfo(int num, int count)
  {
    _name = $"아이템 {num}번";
    _num = num;
    _count = count;
    // Debug.Log(_name);
  }

}
