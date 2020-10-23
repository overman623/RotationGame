using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : BaseScene
{

  // public GameObject SplashScreen;
  // public GameObject loginScreen;
  public GameObject mainMenuScreen;
  public GameObject CharaterSelectPopup;
  public GameObject InventoryPopup;
  public GameObject SettingsScreen;
  public GameObject LeaderBoardScreen;
  public GameObject LevelScreen;
  public GameObject ShopScreen;
  public GameObject StatsScreen;
  public GameObject SucessDialog;
  // public GameObject GameplayScreen;

  GameObject currentScreen;


  protected override void Init()
  {
    base.Init();
    SceneType = Define.Scene.Lobby;
  }
  public override void Clear()
  {
    Debug.Log("Lobby Scene Clear");
  }

  private void DisableAll()
  {
    // SplashScreen.SetActive(false);
    // loginScreen.SetActive(false);
    mainMenuScreen.SetActive(false);
    CharaterSelectPopup.SetActive(false);
    InventoryPopup.SetActive(false);
    SettingsScreen.SetActive(false);
    LeaderBoardScreen.SetActive(false);
    LevelScreen.SetActive(false);
    ShopScreen.SetActive(false);
    StatsScreen.SetActive(false);
    SucessDialog.SetActive(false);
    // GameplayScreen.SetActive(false);
  }

  private void DisableAllPopup()
  {
    CharaterSelectPopup.SetActive(false);
    InventoryPopup.SetActive(false);
  }

  // public void Show_SplashScreen()
  // {
  //   DisableAll();
  //   SplashScreen.SetActive(true);
  //   currentScreen = SplashScreen;
  // }

  // public void Show_LoginScreen()
  // {
  //   DisableAll();
  //   loginScreen.SetActive(true);
  //   currentScreen = loginScreen;
  // }

  public void Show_mainMenuScreen()
  {
    DisableAll();
    mainMenuScreen.SetActive(true);
    currentScreen = mainMenuScreen;
  }
  public void Show_CharaterSelectPopup()
  {
    DisableAllPopup();
    CharaterSelectPopup.SetActive(false);
    CharaterSelectPopup.SetActive(true);
    currentScreen = CharaterSelectPopup;
  }
  public void Show_InventoryPopup()
  {
    DisableAllPopup();
    InventoryPopup.SetActive(false);
    InventoryPopup.SetActive(true);
    currentScreen = InventoryPopup;
  }
  public void Show_SettingsScreen()
  {
    DisableAll();
    SettingsScreen.SetActive(true);
    currentScreen = SettingsScreen;
  }
  public void Show_LeaderBoardScreen()
  {
    DisableAll();
    LeaderBoardScreen.SetActive(true);
    currentScreen = LeaderBoardScreen;
  }
  public void Show_LevelScreen()
  {
    DisableAll();
    LevelScreen.SetActive(true);
    currentScreen = LevelScreen;
  }
  public void Show_ShopScreen()
  {
    DisableAll();
    ShopScreen.SetActive(true);
    currentScreen = ShopScreen;
  }
  public void Show_StatsScreen()
  {
    DisableAll();
    StatsScreen.SetActive(true);
    currentScreen = StatsScreen;
  }
  public void Show_SucessDialog()
  {
    DisableAll();
    SucessDialog.SetActive(true);
    currentScreen = SucessDialog;
  }
  public void Show_GameplayScreen()
  {
    DisableAll();
    // GameplayScreen.SetActive(true);
    // currentScreen = GameplayScreen;
    // Managers.Scene.Clear();
    Managers.Scene.LoadScene(Define.Scene.Play, false);
  }

  public void Exit_Popup()
  {
    DisableAllPopup();
  }



}
