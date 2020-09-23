using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
  public enum WroldObject
  {
    Unknown,
    Player,
    Monster
  }
  public enum State
  {
    Die,
    Moving,
    Idle,
    Skill
  }
  public enum Layer
  {
    Monster = 8,
    Ground = 9,
    Block = 10,
  }
  public enum Scene
  {
    Unknown,
    Login,
    Lobby,
    Game,
    Main
  }

  public enum Sound
  {
    Bgm,
    Effect,
    MaxCount
  }
  public enum UIEvent
  {
    Click,
    Drag,

  }
  public enum MouseEvent
  {
    Press,
    PointerDown, //처음 누르자마자 긴 시간 유지
    PointerUp, //PointerDown이후에 마우스를 떼어냄.
    click,
  }

  public enum CameraMode
  {
    QuarterView
  }
}
