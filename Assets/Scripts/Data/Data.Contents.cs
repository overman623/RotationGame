using System;
using System.Collections.Generic;
//파일명에 . 이 붙어도 유니티에는 이상이 있어 보이지만 실제로는 이상이 없다.
namespace Data
{
  #region Stat
  [Serializable]
  public class Stat
  {
    public int level;
    public int maxHp;
    public int attack;
    public int totalExp;
  }

  [Serializable]
  public class StatData : ILoader<int, Stat>
  {
    public List<Stat> stats = new List<Stat>();

    public Dictionary<int, Stat> MakeDict()
    {
      Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
      foreach (Stat stat in stats)
      {
        dict.Add(stat.level, stat); //레벨을 키로 대신해서 입력한다.
      }
      return dict;
    }
  }
  #endregion
}