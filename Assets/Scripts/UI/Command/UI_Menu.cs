using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Menu : MonoBehaviour
{
  //삭제 예정
  public void Inventory()
  {
    // Managers.UI.SetCanvas(i.gameObject);
    Managers.UI.ShowPopupUI<UI_Button>();
  }

}
