using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{

  public enum SkillType
  {
    None,
    Gun,
    Sword
  }

  GameObject _skillParent;
  SkillType _nowSkillType = SkillType.None;

  internal GameObject GetSkillObject()
  {
    if (_skillParent == null)
    {
      _skillParent = new GameObject { name = "@Skill" };
    }

    return _skillParent;
  }


  public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
  {
    GameObject go = Managers.Resource.Instantiate(path, parent);

    return go;
  }

  internal void SetSkillType(SkillType type)
  {
    _nowSkillType = type;
  }
}
