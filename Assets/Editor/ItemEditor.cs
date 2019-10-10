using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
[CanEditMultipleObjects]
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

    // Start is called before the first frame update
    private void OnEnable()
    {
        itemScript = target as Item;

        //armourFieldConditions = new List<FieldCondition>();
       // ShowOnEnum("itemType", "Armour", "armourType");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        //serializedObject.Update();

        //IterateEnumFields();
        EditorGUILayout.LabelField("== Types ==", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Item Type");
        itemScript.itemType = (Item.ItemType)EditorGUILayout.EnumPopup(itemScript.itemType);

        if (itemScript.itemType == Item.ItemType.Armour)
        {
            EditorGUILayout.LabelField("Armour Type");
            itemScript.armourType = (Item.ArmourType)EditorGUILayout.EnumPopup(itemScript.armourType);
        }

        EditorGUILayout.LabelField("Bonus Stats", EditorStyles.boldLabel);

        itemScript.applyRandomStats = GUILayout.Toggle(itemScript.applyRandomStats, "Apply Random Item Stats?");
        
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
            CreateLabelWithRange(ref itemScript.strength, "Strength", statLabelWidth, ref strengthRange.min, ref strengthRange.max);
            CreateLabelWithRange(ref itemScript.agility, "Agility", statLabelWidth, ref agilityRange.min, ref agilityRange.max);
            CreateLabelWithRange(ref itemScript.constitution, "Constitution", statLabelWidth, ref constitutionRange.min, ref constitutionRange.max);
            CreateLabelWithRange(ref itemScript.intellect, "Intellect", statLabelWidth, ref intellectRange.min, ref intellectRange.max);
            CreateLabelWithRange(ref itemScript.physicalArmour, "Physical Armour", statLabelWidth, ref physicalArmourRange.min, ref physicalArmourRange.max);
            CreateLabelWithRange(ref itemScript.magicalArmour, "Magical Armour", statLabelWidth, ref magicalArmourRange.min, ref magicalArmourRange.max);
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

    void CreateLabelWithRange(ref int stat, string statName, float nameLabelWidth, ref int rangeMin, ref int rangeMax)
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
}
