using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipPanelSprites : MonoBehaviour
{
    [SerializeField]
    Sprite headSlot;
    [SerializeField]
    Sprite shouldersSlot;
    [SerializeField]
    Sprite chestSlot;
    [SerializeField]
    Sprite legsSlot;
    [SerializeField]
    Sprite feetSlot;
    [SerializeField]
    Sprite handsSlot;
    [SerializeField]
    Sprite weaponSlot;
    [SerializeField]
    Sprite ring1Slot;
    [SerializeField]
    Sprite ring2Slot;
    [SerializeField]
    Sprite neckSlot;

    static public Sprite head;
    static public Sprite shoulders;
    static public Sprite chest;
    static public Sprite legs;
    static public Sprite feet;
    static public Sprite hands;
    static public Sprite weapon;
    static public Sprite ring1;
    static public Sprite ring2;
    static public Sprite neck;

    private void Start()
    {
        head = headSlot;
        shoulders = shouldersSlot;
        chest = chestSlot;
        legs = legsSlot;
        feet = feetSlot;
        hands = handsSlot;
        weapon = weaponSlot;
        ring1 = ring1Slot;
        ring2 = ring2Slot;
        neck = neckSlot;
    }
}
