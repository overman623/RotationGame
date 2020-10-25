using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
  [SerializeField]
  GameObject VisiblePlayerObject;
  [SerializeField]
  GameObject MainMenuObject;
  private void Start()
  {
    Debug.Log("Start");
    PositionLerpAnimation positionLerpAnimation = MainMenuObject.GetComponent<PositionLerpAnimation>();
    positionLerpAnimation.appearPlayer -= appearPlayer;
    positionLerpAnimation.appearPlayer += appearPlayer;
  }

  private void appearPlayer()
  {
    VisiblePlayerObject.SetActive(true);
    // VisiblePlayerObject.transform.position = new Vector3(7000, 7000, 7000);
  }

}