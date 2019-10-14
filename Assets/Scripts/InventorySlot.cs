using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool isUsed;
    public Item storedItem;

    public float width;
    public float height;

    public struct SlotID
    {
        public int x;
        public int y;
    }
    int xID;
    int yID;

    RectTransform slotRect;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialise(GameObject slotParent, float width, float height, float offset, SlotID id)
    {
        xID = id.x;
        yID = id.y;

        slotRect = GetComponent<RectTransform>();
        slotRect.SetParent(slotParent.transform);
        slotRect.anchorMin = new Vector2(0, 1);
        slotRect.anchorMax = new Vector2(0, 1);

        this.width = width;
        this.height = height;

        slotRect.sizeDelta = new Vector2(width, height);

        Vector2 slotPosition = new Vector2(offset, -offset);
        slotPosition.x += (width * id.x);
        slotPosition.y -= (height * id.y);

        slotRect.anchoredPosition = slotPosition;
    }
}
