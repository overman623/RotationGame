using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : BaseScene
{


  public Image loadingFill;
  public GameObject LoginScreen;
  private bool isReady = false;

  protected override void Init()
  {
    base.Init();
    SceneType = Define.Scene.Intro;

    loadingFill.fillAmount = 0f;
    Invoke("Delay", 0.2f);
  }
  public override void Clear()
  {
    Debug.Log("Login Scene Clear");
  }

  void Delay()
  {
    StartCoroutine(loading());
  }
  IEnumerator loading()
  {
    while (loadingFill.fillAmount < 1)
    {
      loadingFill.fillAmount += (0.01f / 2f);
      yield return null;
    }

    StartGame();
    isReady = true;
    yield return 0;
  }

  void StartGame()
  { // 로그인창은 나중에 작성함.
    // LoginScreen.SetActive(true);
    // this.gameObject.SetActive(false);
  }

  private void Update()
  {

    //마우스 클릭하면 다음 신으로 넘어가게 한다.
    if (Input.GetMouseButtonDown(0) && isReady)
    {
      Managers.Scene.LoadScene(Define.Scene.Lobby);
      // Managers.Scene.LoadScene(Define.Scene.Play);
    }

  }

}
