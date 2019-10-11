using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(Item))]
//[CanEditMultipleObjects]
public class ItemEditor : Editor
{
    Item itemScript;

    struct IntRange
    {
        public int min;
        public int max;
    }
    IntRange strengthRange;
    IntRange agilityRange;
    IntRange constitutionRange;
    IntRange intellectRange;
    IntRange physicalArmourRange;
    IntRange magicalArmourRange;

    delegate void ItemDelegate();
    ItemDelegate methodToCall;

    //bool foldoutImageProperties = false;
    bool useCustomImage;

    SerializedProperty itemImage;

    // Start is called before the first frame update
    private void OnEnable()
    {
        itemScript = target as Item;
        itemImage = serializedObject.FindProperty("inventorySprite");
        if (itemScript.inventorySprite != null)
        {
            useCustomImage = true;
        }
        else
        {
            useCustomImage = false;
        }

        SetStatRangeValues(ref strengthRange, itemScript.strength);
        SetStatRangeValues(ref agilityRange, itemScript.agility);
        SetStatRangeValues(ref constitutionRange, itemScript.constitution);
        SetStatRangeValues(ref intellectRange, itemScript.intellect);
        SetStatRangeValues(ref physicalArmourRange, itemScript.physicalArmour);
        SetStatRangeValues(ref magicalArmourRange, itemScript.magicalArmour);
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Types", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Item Type");
        itemScript.itemType = (Item.ItemType)EditorGUILayout.EnumPopup(itemScript.itemType);

        if (itemScript.itemType == Item.ItemType.Armour)
        {
            EditorGUILayout.LabelField("Armour Type");
            itemScript.armourType = (Item.ArmourType)EditorGUILayout.EnumPopup(itemScript.armourType);
        }
        else
        {
            EditorGUILayout.LabelField("Weapon Type");
            itemScript.weaponType = (Item.WeaponType)EditorGUILayout.EnumPopup(itemScript.weaponType);
        }

        EditorGUILayout.LabelField("Equipment Slot");
        itemScript.equipmentSlot = (Item.EquipmentSlot)EditorGUILayout.EnumPopup(itemScript.equipmentSlot);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Stat Bonuses", EditorStyles.boldLabel);

        itemScript.applyRandomStats = GUILayout.Toggle(itemScript.applyRandomStats, "Use Random Item Stats?");

        if (!itemScript.applyRandomStats)
        {
            float statLabelWidth = 95.0f;
            CreateLabelIntSliderField(ref itemScript.strength, "Strength", statLabelWidth, 0, 100);
            CreateLabelIntSliderField(ref itemScript.agility, "Agility", statLabelWidth, 0, 100);
            CreateLabelIntSliderField(ref itemScript.constitution, "Constitution", statLabelWidth, 0, 100);
            CreateLabelIntSliderField(ref itemScript.intellect, "Intellect", statLabelWidth, 0, 100);
            CreateLabelIntSliderField(ref itemScript.physicalArmour, "Physical Armour", statLabelWidth, 0, 100);
            CreateLabelIntSliderField(ref itemScript.magicalArmour, "Magical Armour", statLabelWidth, 0, 100);
        }
        else
        {
            float statLabelWidth = 95.0f;
            CreateLabelWithRange("Strength", statLabelWidth, ref strengthRange.min, ref strengthRange.max);
            CreateLabelWithRange("Agility", statLabelWidth, ref agilityRange.min, ref agilityRange.max);
            CreateLabelWithRange("Constitution", statLabelWidth, ref constitutionRange.min, ref constitutionRange.max);
            CreateLabelWithRange("Intellect", statLabelWidth, ref intellectRange.min, ref intellectRange.max);
            CreateLabelWithRange("Physical Armour", statLabelWidth, ref physicalArmourRange.min, ref physicalArmourRange.max);
            CreateLabelWithRange("Magical Armour", statLabelWidth, ref magicalArmourRange.min, ref magicalArmourRange.max);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Str: " + itemScript.strength, EditorStyles.label, GUILayout.Width(90));
            EditorGUILayout.LabelField("Agi: " + itemScript.agility, EditorStyles.label, GUILayout.Width(90));
            EditorGUILayout.LabelField("Con: " + itemScript.constitution, EditorStyles.label, GUILayout.Width(90));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Int: " + itemScript.intellect, EditorStyles.label, GUILayout.Width(70));
            EditorGUILayout.LabelField("Phys Arm: " + itemScript.physicalArmour, EditorStyles.label, GUILayout.Width(90));
            EditorGUILayout.LabelField("Magi Arm: " + itemScript.magicalArmour, EditorStyles.label, GUILayout.Width(90));
            EditorGUILayout.EndHorizontal();

            //methodToCall = RandomiseStatValues;
            CreateButton("Randomise Stat Values", methodToCall = RandomiseStatValues, methodToCall = ResetAllStatRangeValues);

            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("Note: that the Max range value is exclusive. \nWill return a randomised value from Min to (Max-1)", MessageType.Info, true);
        }
        EditorGUILayout.Space();

        // Create methods for editor to determine how much inventory space item will take up

        EditorGUILayout.Space();

        useCustomImage = GUILayout.Toggle(useCustomImage, "Use Custom Inventory Icon?");
        if (useCustomImage)
        {
            Sprite invImage = itemScript.inventorySprite;
            EditorGUILayout.PropertyField(itemImage, new GUIContent("Inventory Icon"));
            serializedObject.ApplyModifiedProperties();
        }

        EditorUtility.SetDirty(itemScript);
    }

    void CreateLabelIntSliderField(ref int stat, string statName, float nameLabelWidth, int rangeMin, int rangeMax)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(statName, GUILayout.Width(nameLabelWidth));
        stat = EditorGUILayout.IntSlider(stat, rangeMin, rangeMax);
        EditorGUILayout.EndHorizontal();
    }

    void CreateLabelWithRange(string statName, float nameLabelWidth, ref int rangeMin, ref int rangeMax)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(statName, GUILayout.Width(nameLabelWidth));
        EditorGUILayout.LabelField("| Range:    Min", GUILayout.Width(80));
        rangeMin = EditorGUILayout.IntField(rangeMin, GUILayout.Width(30));
        EditorGUILayout.LabelField("    Max: ", GUILayout.Width(50));
        rangeMax = EditorGUILayout.IntField(rangeMax, GUILayout.Width(30));
        EditorGUILayout.EndHorizontal();

        if (rangeMin > rangeMax)
        {
            rangeMax = rangeMin;
        }
    }

    void CreateButton(string buttonText, ItemDelegate method)
    {
        if (GUILayout.Button(buttonText))
        {
            method();
        }
    }

    void CreateButton(string buttonText, ItemDelegate method, ItemDelegate methodTwo)
    {
        if (GUILayout.Button(buttonText))
        {
            method();
            methodTwo();
        }
    }

    void SetStatRangeValues(ref IntRange range, int realStatValue)
    {
        if (range.min > 5)
        {
            range.min = realStatValue - 5;
        }
        else
        {
            range.min = 0;
        }
        if (range.max == realStatValue)
        {
            
        }
        else
        {
            range.max = realStatValue + 6;
        }
    }

    void RandomiseStatValues()
    {
        if (itemScript.applyRandomStats)
        {
            itemScript.strength = Random.Range(strengthRange.min, strengthRange.max);
            itemScript.agility = Random.Range(agilityRange.min, agilityRange.max);
            itemScript.constitution = Random.Range(constitutionRange.min, constitutionRange.max);
            itemScript.intellect = Random.Range(intellectRange.min, intellectRange.max);
            itemScript.physicalArmour = Random.Range(physicalArmourRange.min, physicalArmourRange.max);
            itemScript.magicalArmour = Random.Range(magicalArmourRange.min, magicalArmourRange.max);
        }
    }

    void ResetAllStatRangeValues()
    {
        SetStatRangeValues(ref strengthRange, itemScript.strength);
        SetStatRangeValues(ref agilityRange, itemScript.agility);
        SetStatRangeValues(ref constitutionRange, itemScript.constitution);
        SetStatRangeValues(ref intellectRange, itemScript.intellect);
        SetStatRangeValues(ref physicalArmourRange, itemScript.physicalArmour);
        SetStatRangeValues(ref magicalArmourRange, itemScript.magicalArmour);
    }
}
