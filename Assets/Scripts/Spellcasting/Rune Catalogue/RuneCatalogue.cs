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

    //Private Variables
    private Color32 lrColorRed = new Color32 (255, 0, 0, 255);
    private Color32 lrColorGreen = new Color32(0, 255, 0, 255);

    //Rune Catalogue
    //Rune 1 "Push" (ID5, ID14, ID23)
    public Spellnodes.SpellnodeID[] rune_1_Push;

    //Serialized Variables
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip SFX_rune_1;
    [SerializeField] private AudioClip SFX_invalidRune;

    //Function to check if a passed spellnodeSequence resembles any valid rune
    public bool CheckForRune(List<GameObject> list)
    {
        //Checking if the passed spellnodeSequence has the same length/count as the rune it is being compared to
        if (rune_1_Push.Length != list.Count)
        {
            Debug.Log("Can't compare spellnodeSequence List with rune Arrays, they are not of the same Length/Count!");

            //Clear the spellnodeSequence List after a failed rune recognition
            Invoke("ClearSpellnodeSequence", 0.1f);

            //Temporarily locking the spellnodes, so no new ones can be added to the sequence. Lasts for 2 secs (the same duration as the lineRenderer lingers after rune recognition)
            RuneCastingDetection.Instance.spellnodesLocked = true;
            Invoke("DelayedSpellnodesUnlock", 2.0f);

            //Color the Line Renderer red!
            RuneCastingDetection.Instance.lineRenderer.startColor = lrColorRed;
            RuneCastingDetection.Instance.lineRenderer.endColor = lrColorRed;

            //Add auditory feedback for invalid rune
            audioSource.clip = SFX_invalidRune;
            audioSource.Play();

            return false;
        }
        else
        {
            for (int i = 0; i < rune_1_Push.Length; i++)
            {
                //Checking via a foor loop if each spellnode stored in the checked spellnodeSequence is different from the compared counterpart in the rune
                if (rune_1_Push[i] != list[i].GetComponent<Spellnodes>().spellnodeID)
                {
                    //If so, rune recognition finishes unsuccessful with return false
                    Debug.Log("Rune 1 is not a match for this sequence!");

                    //Clear the spellnodeSequence List after a failed rune recognition
                    Invoke("ClearSpellnodeSequence", 0.1f);

                    //Temporarily locking the spellnodes, so no new ones can be added to the sequence. Lasts for 2 secs (the same duration as the lineRenderer lingers after rune recognition)
                    RuneCastingDetection.Instance.spellnodesLocked = true;
                    Invoke("DelayedSpellnodesUnlock", 2.0f);

                    //Color the Line Renderer red!
                    RuneCastingDetection.Instance.lineRenderer.startColor = lrColorRed;
                    RuneCastingDetection.Instance.lineRenderer.endColor = lrColorRed;

                    //Add auditory feedback for invalid rune
                    audioSource.clip = SFX_invalidRune;
                    audioSource.Play();

                    return false;
                }
            }

            //However, if no differences are detected, this means that the spellnodes of the tested spellnodeSequence and the rune are identical. Rune recognition finishes successfully with return true
            //Add this rune array to the RuneCastingDetection's runeSequence List
            RuneCastingDetection.Instance.runeSequence.Add(rune_1_Push);

            //Clear the spellnodeSequence List after a successful rune recognition
            Invoke("ClearSpellnodeSequence", 0.1f);

            //Temporarily locking the spellnodes, so no new ones can be added to the sequence. Lasts for 2 secs (the same duration as the lineRenderer lingers after rune recognition)
            RuneCastingDetection.Instance.spellnodesLocked = true;
            Invoke("DelayedSpellnodesUnlock", 2.0f);

            //Color the Line Renderer green!
            RuneCastingDetection.Instance.lineRenderer.startColor = lrColorGreen;
            RuneCastingDetection.Instance.lineRenderer.endColor = lrColorGreen;

            //Add auditory feedback for valid rune
            audioSource.clip = SFX_rune_1;
            audioSource.Play();

            Debug.Log("Rune 1 IS A MATCH!!");
            return true;
        }
    }

    private void ClearSpellnodeSequence()
    {
        RuneCastingDetection.Instance.spellnodeSequence.Clear();
    }

    private void DelayedSpellnodesUnlock()
    {
        RuneCastingDetection.Instance.spellnodesLocked = false;
    }
}
