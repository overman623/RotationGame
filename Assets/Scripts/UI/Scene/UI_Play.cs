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

  private GameObject player = null;

  public override void Init()
  {
    base.Init();
    player = Managers.Game.GetPlayer();

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
    TestController test = player.transform.GetComponent<TestController>();
    test.Attack();
  }

  public void OnSwordButtonClicked(PointerEventData data)
  {
    TestController test = player.transform.GetComponent<TestController>();
    test.Attack(true);
  }


}
