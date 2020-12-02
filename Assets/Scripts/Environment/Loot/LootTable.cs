using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [Tooltip("Is loot chosen randomly? If not, all loot in this list will drop.")]
    public bool isRandom = false;
    [Tooltip("The percent chance that this item will drop nothing.")]
    [Range(0, 100)]
    public int chanceForNothing = 0;
    public int minItems = 0;
    public int maxItems = 1;

    public GameObject[] lootList;

    private void Start()
    {
        if (lootList.Length == 0)
        {
            Debug.LogError("No loot listed for loot table on object " + gameObject.name);
        }
    }

    public void DropLoot()
    {
        if (isRandom)
        {
            if (Random.Range(0, 100) >= chanceForNothing)
            {
                int items = Random.Range(minItems, maxItems + 1);

                for (int index = 0; index < items; ++index)
                {
                    Instantiate(lootList[Random.Range(0, lootList.Length)]);
                }
            }
            // Else, chance for nothing proc'd and spawn nothing
        }
        else
        {
            foreach (GameObject lootDrop in lootList)
            {
                Instantiate(lootDrop, transform.position + Vector3.up, Quaternion.identity);
            }
        }
    }

    private void OnValidate()
    {
        // Make sure minimum items can't be 0
        if (minItems <= 1)
            minItems = 1;
        if(minItems > maxItems)
        {
            maxItems = minItems;
        }
        // If chance for nothing is 0, loot table always has to drop loot. Min items can't be 0
        if (chanceForNothing == 0 && minItems == 0)
        {
            minItems = 1;
        }
    }
}