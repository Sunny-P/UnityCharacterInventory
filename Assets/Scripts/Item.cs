using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Item Types")]
    public ItemType itemType;
    public ArmourType armourType;

    [Header("Stat Bonuses")]
    public bool applyRandomStats;
    public int strength;
    public int agility;
    public int constitution;
    public int intellect;
    public int physicalArmour;
    public int magicalArmour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
