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
    public bool isMouseItem = false;

    public bool isEquipped = false;
    public EquipmentSlot usedEquipSlot;

    Image image;

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Initialise(GameObject parentObj, Item insertedItem, Vector3 invPosition, Vector3 invScale, bool mouseItem = false)
    {
        slotsUsed = new List<InventorySlot>();
        rect = GetComponent<RectTransform>();
        rect.SetParent(parentObj.transform);
        image = GetComponent<Image>();

        SetItem(insertedItem);

        rect.position = invPosition;
        rect.localScale = invScale;

        isMouseItem = mouseItem;

        return true;
    }

    public void Initialise(GameObject parentObj, Item insertedItem, Vector3 invPosition, Vector3 invScale, Vector2 invPivot, bool mouseItem = false)
    {
        slotsUsed = new List<InventorySlot>();
        rect = GetComponent<RectTransform>();
        rect.SetParent(parentObj.transform);
        image = GetComponent<Image>();

        SetItem(insertedItem);

        rect.position = invPosition;
        rect.localScale = invScale;
        rect.pivot = invPivot;

        isMouseItem = mouseItem;
    }

    public void SetWidth(float width)
    {
        Vector2 updatedWidth = new Vector2(width, rect.sizeDelta.y);
        rect.sizeDelta = updatedWidth;
    }
    public void SetHeight(float height)
    {
        Vector2 updatedHeight = new Vector2(rect.sizeDelta.x, height);
        rect.sizeDelta = updatedHeight;
    }

    public void SetItem(Item givenItem)
    {
        item = givenItem;

        if (item != null)
        {
            image.sprite = item.inventorySprite;

            width = InventorySlot.width * item.inventorySpaceX;
            height = InventorySlot.height * item.inventorySpaceY;

            rect.sizeDelta = new Vector2(width, height);
        }
    }

    public void SetItem(InventoryItem givenItem, bool isEquipping = false)
    {
        item = givenItem.item;

        if (item != null)
        {
            if (isEquipping)
            {
                image.sprite = item.equippedSprite;

                width = EquipmentSlot.width * 0.9f;
                height = EquipmentSlot.height * 0.9f;
            }
            else
            {
                image.sprite = item.inventorySprite;

                width = InventorySlot.width * item.inventorySpaceX;
                height = InventorySlot.height * item.inventorySpaceY;
            }

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
