﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
  protected override void Init()
  {
    base.Init();
    SceneType = Define.Scene.Login;


  }
  public override void Clear()
  {
    Debug.Log("Login Scene Clear");
  }
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Q))
    {
      Managers.Scene.LoadScene(Define.Scene.Game);
      //유니티에서 제공받는 메소드.
    }
  }

}