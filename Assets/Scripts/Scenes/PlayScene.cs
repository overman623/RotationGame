﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : BaseScene
{
  [SerializeField]
  List<GameObject> itemTransfrom = new List<GameObject>();

  protected override void Init()
  {
    base.Init();
    SceneType = Define.Scene.Play;

    Managers.UI.ShowSceneUI<UI_Play>(); //인벤토리 잠시 주석
    // Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

    // Transform rootTransform = Managers.UI.Root.transform;

    // GameObject goJoyStick = Managers.Resource.Instantiate("UI/JoyStick/UI_JoyStick");
    // GameObject goCommand = Managers.Resource.Instantiate("UI/Button/UI_Command");

    // goJoyStick.transform.SetParent(rootTransform);
    // goCommand.transform.SetParent(rootTransform);

    // rootTransform.SetParent(transform);

    GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
    Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

    //Managers.Game.Spawn(Define.WroldObject.Monster, "Knight");

    GameObject goSpawningPool = new GameObject { name = "SpawningPool" };
    SpawningPool pool = goSpawningPool.GetOrAddComponent<SpawningPool>();
    pool.SetKeepMonsterCount(1);

  }

  public override void Clear()
  {
  }


}