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

    public InventorySlot.SlotID slottedIn;

    Image image;

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialise(Sprite sprite)
    {
        image = GetComponent<Image>();

        image.sprite = sprite;
    }
}
