using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{

  public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
  { //컴포넌트에서 UI_EventHandler 클래스 유무를 확인하고 없다면 새로 추가해서 가져온다.

    return Util.GetOrAddComponent<T>(go);
  }

  public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
  {
    UI_Base.BindEvent(go, action, type);
  }

  public static bool IsValid(this GameObject go)
  {
    return go || go.activeSelf;
  }

  public static Vector3 LocalToGlobalTransVector(this Transform transform, Vector3 v)
  {
    transform.localPosition = v;
    return transform.position;
  }

  public static Vector3 GlobalToLocalTransVector(this Transform transform, Vector3 v)
  {
    transform.position = v;
    return transform.localPosition;
  }

}
