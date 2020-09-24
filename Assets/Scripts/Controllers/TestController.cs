using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : BaseController
{

  [SerializeField] private UI_JoyStick Joystick;//Joystick reference for assign in inspector
  [SerializeField] private float Speed = 5;
  public override void Init()
  {
    Managers.Input.JoyStickAction -= DirectionChange;
    Managers.Input.JoyStickAction += DirectionChange;
  }

  void Start()
  {

  }


  void Update()
  {

    if (!Joystick.isPress) return;

    //Step #2
    //Change Input.GetAxis (or the input that you using) to Joystick.Vertical or Joystick.Horizontal
    float v = Joystick.Vertical; //get the vertical value of joystick
    float h = Joystick.Horizontal;//get the horizontal value of joystick

    //in case you using keys instead of axis (due keys are bool and not float) you can do this:
    //bool isKeyPressed = (Joystick.Horizontal > 0) ? true : false;

    //ready!, you not need more.
    Vector3 translate = (new Vector3(h, 0, v) * Time.deltaTime) * Speed;
    transform.Translate(translate, Space.World);

    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(translate.normalized), 10 * Time.deltaTime);
  }

  void DirectionChange(Define.JoyStickEvent evt)
  {

  }
}
