using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface ILoader<Key, Value>
{
  Dictionary<Key, Value> MakeDict();
}

public class DataManager
{

  public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

  public Dictionary<int, List<Item>> itemInfos = new Dictionary<int, List<Item>>();

  public void Init()
  {
    StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
  }

  Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
  {
    //확장자는 필요없다. //파일 스트링 그대로 부른다.
    TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
    return JsonUtility.FromJson<Loader>(textAsset.text);
  }

  public void setItemData(Item item)
  {
    List<Item> items;
    itemInfos.TryGetValue(item.getItemNum(), out items);
    if (items == null)
    {
      items = new List<Item>();
    }
    items.Add(item);
    itemInfos[item.getItemNum()] = items;
    Debug.Log(itemInfos.Count);
  }

  public Dictionary<int, List<Item>> getItemData()
  {
    return itemInfos;
  }

  public void removeItemData(int num)
  {

    List<Item> items;
    itemInfos.TryGetValue(num, out items);
    if (items != null)
    {
      items.RemoveAt(0);
      if (items.Count == 0)
      {
        itemInfos.Remove(num);
        return;
      }
    }

  }

}
