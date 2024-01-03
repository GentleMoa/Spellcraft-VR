//Script to store all the possible rune combinations / spells available to the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneCatalogue : MonoBehaviour
{
    //Singleton
    public static RuneCatalogue Instance { set; get; }

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

    //Rune Catalogue

    //Rune 1 "Push" (ID5, ID14, ID23)
    public Spellnodes.SpellnodeID[] rune_1_Push;

    public bool CheckForRune(List<GameObject> list)
    {
        if (rune_1_Push.Length != list.Count)
        {
            Debug.Log("Can't compare spellnodeSequence List with rune Arrays, they are not of the same Length/Count!");
            RuneCastingDetection.Instance.spellnodeSequence.Clear();
            return false;
        }
        else
        {
            for (int i = 0; i < rune_1_Push.Length; i++)
            {
                if (rune_1_Push[i] != list[i].GetComponent<Spellnodes>().spellnodeID)
                {
                    Debug.Log("Rune 1 is not a match for this sequence!");
                    RuneCastingDetection.Instance.spellnodeSequence.Clear();
                    return false;
                }
            }

            //Add this rune array to the RuneCastingDetection's runeSequence List
            RuneCastingDetection.Instance.runeSequence.Add(rune_1_Push);
            RuneCastingDetection.Instance.spellnodeSequence.Clear();

            Debug.Log("Rune 1 IS A MATCH!!");
            return true;
        }
    }
}
