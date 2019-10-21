using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [Header("The type of slot this is")]
    public Item.EquipmentSlot equipmentSlot;
    [Header("If the slot is currently in use")]
    public bool isUsed;
    [Header("The currently equipped item in this slot")]
    public Item equippedItem;

    public static float width;
    public static float height;

    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialise(GameObject parent, float _width, float _height, float offset, Item.EquipmentSlot equipSlot)
    {
        width = _width;
        height = _height;
    }
}
