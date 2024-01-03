//Script to detect what runes the player is casting and combining

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCastingDetection : MonoBehaviour
{
    //Singleton
    public static RuneCastingDetection Instance { set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //Public Variables
    public List<GameObject> spellnodeSequence = new List<GameObject>();
    public List<Spellnodes.SpellnodeID[]> runeSequence = new List<Spellnodes.SpellnodeID[]>();

    public void AddSpellnodeToSequence(GameObject spellnode)
    {
        if (spellnodeSequence.Count < 3)
        {
            spellnodeSequence.Add(spellnode);

            //Check if the spellnode combination resembles a valid rune from the catalogue
            if (spellnodeSequence.Count == 3)
            {
                RuneCatalogue.Instance.CheckForRune(spellnodeSequence);
            }
        }
        else if (spellnodeSequence.Count == 3 || spellnodeSequence.Count > 3)
        {
            //Do nothings
        }
    }

}
