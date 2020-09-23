﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
  // int <-> GameObject --서버에서
  // Dictionary<int, GameObject> _monsters = new Dictionary<int, GameObject>();
  // Dictionary<int, GameObject> _players = new Dictionary<int, GameObject>();

  GameObject _player;
  HashSet<GameObject> _monsters = new HashSet<GameObject>();

  public Action<int> OnSpawnEvent;

  public GameObject GetPlayer()
  {
    return _player;
  }

  public GameObject Spawn(Define.WroldObject type, string path, Transform parent = null)
  {
    GameObject go = Managers.Resource.Instantiate(path, parent);

    switch (type)
    {
      case Define.WroldObject.Monster:
        _monsters.Add(go);
        if (OnSpawnEvent != null)
          OnSpawnEvent.Invoke(1);
        break;
      case Define.WroldObject.Player:
        _player = go;
        break;
    }

    return go;
  }

  public Define.WroldObject GetWroldObjectType(GameObject go)
  {
    BaseController bc = go.GetComponent<BaseController>();
    if (bc == null)
      return Define.WroldObject.Unknown;
    return bc.WroldObjectType;
  }

  public void Despawn(GameObject go)
  {
    Define.WroldObject type = GetWroldObjectType(go);
    switch (type)
    {
      case Define.WroldObject.Monster:
        if (_monsters.Contains(go))
        {
          _monsters.Remove(go);
          if (OnSpawnEvent != null)
            OnSpawnEvent.Invoke(-1);
        }
        break;
      case Define.WroldObject.Player:
        if (_player == go)
          _player = null;
        break;
    }

    Managers.Resource.Destroy(go);

  }


}
