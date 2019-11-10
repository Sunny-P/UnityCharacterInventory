using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool isUsed;
    public Item storedItem;

    public static float width;
    public static float height;

    public struct SlotID
    {
        public int x;
        public int y;
    }
    public SlotID slotID;

    RectTransform slotRect;

    InventoryBase invBase;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialise(GameObject slotParent, float _width, float _height, float offset, SlotID id)
    {
        invBase = slotParent.GetComponent<InventoryBase>();
        slotID.x = id.x;
        slotID.y = id.y;

        slotRect = GetComponent<RectTransform>();
        slotRect.SetParent(slotParent.transform);
        slotRect.anchorMin = new Vector2(0, 1.0f);
        slotRect.anchorMax = new Vector2(0, 1.0f);

        width = _width;
        height = _height;

        slotRect.sizeDelta = new Vector2(width, height);

        Vector2 slotPosition = new Vector2(offset, -offset);
        slotPosition.x += (width * id.x);
        slotPosition.y -= (height * id.y);

        slotRect.anchoredPosition = slotPosition;
        slotRect.localScale = new Vector2(1, 1);
    }
}
