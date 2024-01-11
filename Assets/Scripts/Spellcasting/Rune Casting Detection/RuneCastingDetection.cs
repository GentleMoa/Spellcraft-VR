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
    public LineRenderer lineRenderer;
    public bool spellnodesLocked = false;
    public List<GameObject> spellnodeSequence = new List<GameObject>();
    public List<Spellnodes.SpellnodeID[]> runeSequence = new List<Spellnodes.SpellnodeID[]>();

    [HideInInspector] public bool lrDrawable = true;

    //Private Variables
    private bool lineRendererActive = false;
    private Color32 lrColorWhite = new Color32(255, 255, 255, 255);

    //Function to add spellnodes touched during spellmode to the spellnodeSequence List (called in "Spellnodes" Script)
    public void AddSpellnodeToSequence(GameObject spellnode)
    {
        if (spellnodesLocked == false)
        {
            if (spellnodeSequence.Count < 3)
            {
                //Add the passed spellnode to the spellnodeSequence List
                spellnodeSequence.Add(spellnode);

                //Enable the line renderer to be drawn, or more specifically, to have new points vertecies added to it
                lrDrawable = true;

                if (spellnodeSequence.Count == 3)
                {
                    //Check if the spellnode combination resembles a valid rune from the catalogue
                    RuneCatalogue.Instance.CheckForRune(spellnodeSequence);

                    //Disable the line renderer to be drawn, or more specifically, to have new points vertecies added to it
                    Invoke("DelayedLRDrawableToggle", 0.01f);
                }
            }
            else if (spellnodeSequence.Count == 3 || spellnodeSequence.Count > 3)
            {
                //Do nothings

                //Disable the line renderer to be drawn, or more specifically, to have new points vertecies added to it
                lrDrawable = false;
            }
        }
    }

    private void Start()
    {
        //Disable the lineRenderer gameobject in the beginning
        lineRenderer.gameObject.SetActive(false);
    }

    private void Update()
    {
        //If spellnodeSequence contains any spellnodes & flag is false, ...
        if (spellnodeSequence.Count > 0 && lineRendererActive == false)
        {
            //Enable the lineRenderer gameobject
            lineRenderer.gameObject.SetActive(true);

            //Toggle the flag to true
            lineRendererActive = true;
        }
        //If spellnodeSequence contains no spellnodes & flag is true, ...
        else if (spellnodeSequence.Count == 0 && lineRendererActive == true)
        {
            //Disable the lineRenderer gameobject
            Invoke("DelayedLRDisable", 2.0f);

            //Toggle the flag to false
            lineRendererActive = false;
        }
    }

    private void DelayedLRDisable()
    {
        //Disable the lineRenderer gameobject
        lineRenderer.gameObject.SetActive(false);

        //Clear the lineRenderer's Position Count Array
        lineRenderer.positionCount = 0;

        //Reset the lineRenderer's color to white
        RuneCastingDetection.Instance.lineRenderer.startColor = lrColorWhite;
        RuneCastingDetection.Instance.lineRenderer.endColor = lrColorWhite;
    }

    private void DelayedLRDrawableToggle()
    {
        lrDrawable = false;
    }

}
