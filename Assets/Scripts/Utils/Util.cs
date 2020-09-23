using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
  public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
  { //컴포넌트에서 UI_EventHandler 클래스 유무를 확인하고 없다면 새로 추가해서 가져온다.
    T component = go.GetComponent<T>();
    if (component == null)
      component = go.AddComponent<T>();
    return component;
  }
  public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
  {
    Transform transform = FindChild<Transform>(go, name, recursive);
    if (transform == null)
      return null;
    return transform.gameObject;
  }
  public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
  {
    if (go == null)
      return null;

    if (recursive == false)
    {
      for (int i = 0; i < go.transform.childCount; i++)
      {
        Transform transform = go.transform.GetChild(i);
        if (string.IsNullOrEmpty(name) || transform.name == name)
        {
          T component = transform.GetComponent<T>();
          if (component != null)
            return component;
        }
      }
    }
    else
    {
      foreach (T component in go.GetComponentsInChildren<T>()) //자식 컴포넌트까지 원하는 타입을 찾는다.
      {
        if (string.IsNullOrEmpty(name) || component.name == name) //이름없이 타입만 찾거나, 컴포넌트에 이름이 없는경우
        {
          return component;
        }
      }
    }
    return null;
  }
}
