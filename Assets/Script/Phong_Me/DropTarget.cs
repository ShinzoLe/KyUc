using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTarget : MonoBehaviour
{
    public bool CanDrop(GameObject item)
    {
        if (item.CompareTag("ItemDrop"))
            return true;
        return false;
    }
    public void OnDropped(GameObject item)
    {
        if (CanDrop(item))
        {
        item.transform.SetParent(transform, true);
        int i= item.gameObject.GetComponent<Items>().idItem;
        MiniGameManager.instance.CheckIngre(i);
        Destroy(item.gameObject);
        }
    }
}
