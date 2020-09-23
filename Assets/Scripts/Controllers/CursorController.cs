﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

  Texture2D _attackIcon;
  Texture2D _handIcon;

  public enum CursorType
  {
    None,
    Attack,
    Hand,
  }

  CursorType _cursorType = CursorType.None;
  void Start()
  {
    _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
    _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
  }

  int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);// mask bit flag 로 계산
  void Update()
  {
    if (Input.GetMouseButton(0))
      return;

    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100.0f, _mask))
    {
      if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
      {
        if (_cursorType != CursorType.Attack)
        {
          Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);//(텍스쳐, 마우스클릭점의 기준, 그냥 진행함.)
          _cursorType = CursorType.Attack;
        }
      }
      else
      {
        if (_cursorType != CursorType.Hand)
        {
          Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
          _cursorType = CursorType.Hand;
        }
      }
    }
  }
}
