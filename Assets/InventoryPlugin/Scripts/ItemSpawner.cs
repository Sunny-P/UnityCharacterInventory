using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Basic LootTable with Rarity")]
    public List<Item> commonItems;
    public List<Item> uncommonItems;
    public List<Item> rareItems;

    [Header("Rarity Ratios")]
    public int commonRatio;
    public int uncommonRatio;
    public int rareRatio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item GetSpawnedItem()
    {
        int rarityTotal = commonRatio + uncommonRatio + rareRatio;

        int randomNumber = Random.Range(0, (rarityTotal + 1));

        if (randomNumber <= rareRatio) //randomNumber hit the rare loot
        {
            Item spawnedItem = rareItems[Random.Range(0, rareItems.Count)];
            return spawnedItem;
        }
        else if (randomNumber <= uncommonRatio) //randomNumber hit the uncommon loot
        {
            Item spawnedItem = uncommonItems[Random.Range(0, uncommonItems.Count)];
            return spawnedItem;
        }
        else   // randomNumber hit the common loot
        {
            Item spawnedItem = commonItems[Random.Range(0, commonItems.Count)];
            return spawnedItem;
        }
    }
}
