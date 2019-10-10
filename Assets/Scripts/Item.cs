using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        Armour
    }

    public enum ArmourType
    {
        Light,
        Medium,
        Heavy
    }

    public enum WeaponType
    {
        Sword,
        Staff,
        Bow
    }

    public enum EquipmentSlot
    {
        Head,
        Shoulders,
        Chest,
        Legs,
        Feet,
        Hands,
        Weapon,
        Ring1,
        Ring2,
        Neck
    }

    [Header("Item Types")]
    public ItemType itemType;
    public ArmourType armourType;
    public WeaponType weaponType;
    public EquipmentSlot equipmentSlot;

    [Header("Stat Bonuses")]
    public bool applyRandomStats;
    public int strength;
    public int agility;
    public int constitution;
    public int intellect;
    public int physicalArmour;
    public int magicalArmour;

    [Header("Image")]
    public Sprite inventorySprite;

    [Header("Inventory Space")]
    public int[][] inventorySpace;
}
