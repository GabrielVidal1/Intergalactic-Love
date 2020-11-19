using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventoryList inventoryList;

    public void Open()
    {
        inventoryList.Open();
    }

}
