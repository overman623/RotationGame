using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven : UI_Popup
{
  enum Buttons
  {
    CloseButton
  }

  // enum Texts
  // {
  //   ScoreText
  // }

  enum GameObjects
  {
    // TestObject,
    GridPanel
  }

  // enum Images
  // {
  //   ItemIcon,
  // }

  public override void Init()
  {
    base.Init();

    Bind<Button>(typeof(Buttons));
    Bind<GameObject>(typeof(GameObjects));

    // GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnPointButtonClicked);
    GetButton((int)Buttons.CloseButton).gameObject.AddUIEvent(OnCloseButtonClicked);

    // GameObject go = GetImage((int)Images.ItemIcon).gameObject;
    // BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

    GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
    foreach (Transform child in gridPanel.transform)
      Managers.Resource.Destroy(child.gameObject);

    Dictionary<int, List<Item>> itemInfos = Managers.Data.getItemData();

    foreach (KeyValuePair<int, List<Item>> each in itemInfos)
    {
      int itemNum = each.Key;
      List<Item> items = each.Value;
      GameObject itemObject = Managers.UI.MakeSubItem<UI_Inven_Item>(parent: gridPanel.transform).gameObject;
      UI_Inven_Item inven_Item = itemObject.GetOrAddComponent<UI_Inven_Item>();
      inven_Item.SetInfo(itemNum, items.Count);
    }

  }

  // int _score = 0;

  // public void OnPointButtonClicked(PointerEventData data)
  // {
  //   _score++;
  //   GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
  // }

  public void OnCloseButtonClicked(PointerEventData data)
  {
    ClosePopupUI();
  }

}
