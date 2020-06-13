using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [Tooltip("Is loot chosen randomly? If not, all loot in this list will drop.")]
    public bool isRandom = true;
    [Tooltip("The percent chance that this item will drop nothing.")]
    [Range(0.0f, 100.0f)]
    public float emptyChance = 0.0f;

    public GameObject[] lootList;

    private void Start()
    {
        if(lootList.Length == 0)
        {
            Debug.LogError("No loot listed for loot table on object " + gameObject.name);
        }
    }

    public void DropLoot()
    {

    }
}