using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Play : UI_Scene
{


  enum Buttons
  {
    GunButton,
    SwordButton,
    InventoryButton
  }

  enum GameObjects
  {
    JoyStick,
  }

  GameObject player = null;

  private void SetPlayer()
  {
    if (player == null)
    {
      player = Managers.Game.GetPlayer();
    }
  }

  public override void Init()
  {
    base.Init();

    Bind<Button>(typeof(Buttons));
    Bind<GameObject>(typeof(GameObjects));

    GetButton((int)Buttons.InventoryButton).gameObject.AddUIEvent(OnInventoryButtonClicked);
    GetButton((int)Buttons.SwordButton).gameObject.AddUIEvent(OnSwordButtonClicked);
    GetButton((int)Buttons.GunButton).gameObject.AddUIEvent(OnGunButtonClicked);

    GameObject joyStick = Get<GameObject>((int)GameObjects.JoyStick);
    // foreach (Transform child in joyStick.transform)
    // Managers.Resource.Destroy(child.gameObject);
  }

  public void OnInventoryButtonClicked(PointerEventData data)
  {
    Managers.UI.ShowPopupUI<UI_Inven>();
  }

  public void OnGunButtonClicked(PointerEventData data)
  {
    SetPlayer();
    TestController test = player.transform.GetComponent<TestController>();
    test.Attack();
  }

  public void OnSwordButtonClicked(PointerEventData data)
  {
    SetPlayer();
    TestController test = player.transform.GetComponent<TestController>();
    test.Attack(true);
  }


}
