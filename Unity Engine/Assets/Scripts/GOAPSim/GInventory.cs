using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
  private List<GameObject> _items = new List<GameObject>();

  public void AddItem(GameObject item)
  {
    _items.Add(item);
  }

  public void RemoveGameObject(GameObject item)
  {
    int indexToRemove = -1;

    foreach (GameObject g in _items)
    {
      indexToRemove++;
      
      if (g == item)
      {
        break;
      }

      if (indexToRemove >= -1)
      {
        _items.RemoveAt(indexToRemove);
      }
    }
  }

  public GameObject FindItemWithTag(string tag)
  {
    foreach (GameObject item in _items)
    {
      if (item.tag == tag)
      {
        return item;
      }
    }

    return null;
  }
}
