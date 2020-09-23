using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{

  public Action KeyAction = null;
  public Action<Define.MouseEvent> MouseAction = null;

  bool _pressed = false;
  float _pressedTime = 0;

  public void OnUpdate()
  {

    if (EventSystem.current.IsPointerOverGameObject())
      return; //UI 클릭하면 리턴

    if (Input.anyKey && KeyAction != null)
      KeyAction.Invoke();

    if (MouseAction != null)
    {
      //GetMouseButton - 누르고만 있어도 뜬다.
      //GetMouseButtonDown - 처음에 누를때만 뜬다.
      if (Input.GetMouseButton(0)) //누르면 true 떼면 false를 리턴
      {
        if (!_pressed)
        {
          MouseAction.Invoke(Define.MouseEvent.PointerDown);
          _pressedTime = Time.time;
        }
        MouseAction.Invoke(Define.MouseEvent.Press);
        _pressed = true;
      }
      else
      {
        if (_pressed)
        {
          if (Time.time < _pressedTime + 0.2f)
          {
            MouseAction.Invoke(Define.MouseEvent.click);
          }
          MouseAction.Invoke(Define.MouseEvent.PointerUp);
        }
        _pressed = false;
        _pressedTime = 0;
      }
    }
  }

  public void Clear()
  {
    KeyAction = null;
    MouseAction = null;
  }


}
