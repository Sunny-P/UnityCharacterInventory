using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    // The item this inventoryItem is representing
    public Item item;

    float width;
    float height;

    RectTransform rect;

    public List<InventorySlot> slotsUsed;
    public bool isMouseItem;

    Image image;

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Initialise(GameObject parentObj, Item insertedItem, Vector3 invPosition, Vector3 invScale)
    {
        slotsUsed = new List<InventorySlot>();
        rect = GetComponent<RectTransform>();
        rect.SetParent(parentObj.transform);
        image = GetComponent<Image>();

        SetItem(insertedItem);

        rect.position = invPosition;
        rect.localScale = invScale;

        return true;
    }

    public void Initialise(GameObject parentObj, Item insertedItem, Vector3 invPosition, Vector3 invScale, Vector2 invPivot)
    {
        slotsUsed = new List<InventorySlot>();
        rect = GetComponent<RectTransform>();
        rect.SetParent(parentObj.transform);
        image = GetComponent<Image>();

        SetItem(insertedItem);

        rect.position = invPosition;
        rect.localScale = invScale;
        rect.pivot = invPivot;
    }

    public void SetItem(Item givenItem)
    {
        item = givenItem;

        if (item != null)
        {
            image.sprite = item.inventorySprite;

            width = InventoryBase.slotWidth * item.inventorySpaceX;
            height = InventoryBase.slotHeight * item.inventorySpaceY;

            rect.sizeDelta = new Vector2(width, height);
        }
    }

    public void SetItem(InventoryItem givenItem)
    {
        item = givenItem.item;

        if (item != null)
        {
            image.sprite = item.inventorySprite;

            width = InventoryBase.slotWidth * item.inventorySpaceX;
            height = InventoryBase.slotHeight * item.inventorySpaceY;

            rect.sizeDelta = new Vector2(width, height);

            slotsUsed.Clear();
            slotsUsed.AddRange(givenItem.slotsUsed);
        }
        else
        {
            item = null;
            slotsUsed.Clear();
        }
    }

    public void ClearItem()
    {
        item = null;

        image.sprite = null;

        width = 0;
        height = 0;
        rect.sizeDelta = new Vector2(width, height);

        slotsUsed.Clear();
    }
}
