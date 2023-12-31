//Script on each spellnode. Enables identification of spellnodes via enums/IDs. Also enables visual feedback reaction to being touched.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Spellnodes : MonoBehaviour
{
    //Enum
    public enum SpellnodeID {ID1, ID2, ID3, ID4, ID5, ID6, ID7, ID8, ID9, ID10, ID11, ID12, ID13, ID14, ID15, ID16, ID17, ID18, ID19, ID20, ID21, ID22, ID23, ID24, ID25, ID26, ID27};

    //Public Variables
    public SpellnodeID spellnodeID;

    //Private Variables
    private Vector3 defaultSize;
    private Vector3 hoverSize = new Vector3(0.0175f, 0.0175f, 0.0175f);

    private AudioSource audioSource;


    void Start()
    {
        //Save the default Size
        defaultSize = transform.localScale;

        //Saving reference to the audio source
        audioSource = GetComponent<AudioSource>();
    }

    //REMEMBER ONTRIGGERENTER/EXIT ONLY WORKS IF ONE OF THE OBJECTS HAS A RIGIDBODY COMPONENT
    private void OnTriggerEnter(Collider other)
    {
        //Debugging
        //Debug.Log(other.gameObject.name);

        //Check if the entering collider is the spellpose collider
        if (other.gameObject.tag == "Spellpose Collider")
        {
            //Add this spellnode to the RuneCastingDetection's spellnodeSequence List for rune recognition once the 3-part sequence is completed
            RuneCastingDetection.Instance.AddSpellnodeToSequence(this.gameObject);

            //Checking if the line Renderer is currently eligible for drawing/adding new points to it
            if (RuneCastingDetection.Instance.lrDrawable == true)
            {
                //Call the UpdateLineRenderer function to visualize the rune, which is being drawn
                //Invoke("UpdateLineRenderer", 0.05f);
                UpdateLineRenderer();
            }

            //Call sizing lerp coroutine to grow the object
            StartCoroutine(LerpSize(defaultSize, hoverSize, 0.1f));

            //Play audio cue
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check if the exiting collider is the spellpose collider
        if (other.gameObject.tag == "Spellpose Collider")
        {
            //Call sizing lerp coroutine to shrink the object
            StartCoroutine(LerpSize(hoverSize, defaultSize, 0.1f));
        }
    }

    //Coroutine for lerping the size of an object
    IEnumerator LerpSize(Vector3 startSize, Vector3 endSize, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startSize, endSize, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endSize;
    }

    private void UpdateLineRenderer()
    {
        //Debugging
        //Debug.Log("Spellnode Sequence Index: " + RuneCastingDetection.Instance.spellnodeSequence.IndexOf(gameObject));
        //Debug.Log("Line Renderer Position Count: " + RuneCastingDetection.Instance.lineRenderer.positionCount);

        //Sets the "positionCount" or the length of the position Array of the line renderer to the Index at which this spellnode is placed within the spellnodeSequence List
        RuneCastingDetection.Instance.lineRenderer.positionCount = RuneCastingDetection.Instance.spellnodeSequence.IndexOf(gameObject) + 1;

        //Sets the position of the respective line Renderer Vertex to the local (?!) position of this spellnode
        RuneCastingDetection.Instance.lineRenderer.SetPosition(RuneCastingDetection.Instance.spellnodeSequence.IndexOf(gameObject), gameObject.transform.localPosition);
    }
}
