using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spellpose : MonoBehaviour
{
    //Public Variables
    public bool spellpose = false;

    //Serialized Variables
    [SerializeField] private GameObject spellposeColliderGo;
    [SerializeField] private GameObject spellposeTrailGo;
    [SerializeField] private GameObject handRModel;

    [SerializeField] private Material matHandDefault;
    [SerializeField] private Material matHandSpellpose;

    [SerializeField] private AudioClip enterSpellposeSFX;
    [SerializeField] private AudioClip exitSpellposeSFX;

    //Private Variables
    private bool spellposeCues = false;

    private float trailRendererDefaultTime;
    private float trailRendererDefaultStartWidth;
    private float trailRendererDefaultEndWidth;

    private AudioSource audioSource;

    void Start()
    {
        //Trail Renderer default and initial settings 
        trailRendererDefaultTime = spellposeTrailGo.GetComponent<TrailRenderer>().time;
        trailRendererDefaultStartWidth = spellposeTrailGo.GetComponent<TrailRenderer>().startWidth;
        trailRendererDefaultEndWidth = spellposeTrailGo.GetComponent<TrailRenderer>().endWidth;

        //Initial "Disabling" the Trail Renderer
        spellposeTrailGo.GetComponent<TrailRenderer>().time = 0.0f;
        spellposeTrailGo.GetComponent<TrailRenderer>().startWidth = 0.0f;
        spellposeTrailGo.GetComponent<TrailRenderer>().endWidth = 0.0f;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Grabbing the user input reference from the User Input Manager script, if the spellpose input (R) is greater than 0...
        if (UserInput.Instance.inputSpellpose_R.action.ReadValue<float>() > 0)
        {
            //enable spellpose bool
            spellpose = true;
        }
        else
        {
            //disable spellpose bool
            if (spellpose == true && spellposeCues == true)
            {
                spellpose = false;

                //Bool to avoid calling this each frame6a
                spellposeCues = false;

                //Disable spellposeColliderGo & spellposeTrailGo
                spellposeColliderGo.SetActive(false);

                //"Disabling" the Trail Renderer
                spellposeTrailGo.GetComponent<TrailRenderer>().startWidth = 0.0f;
                spellposeTrailGo.GetComponent<TrailRenderer>().endWidth = 0.0f;

                //Trying to hide the old trail, when entering spellmode again
                spellposeTrailGo.GetComponent<TrailRenderer>().time = 0.0f;

                //Execute all visual & auditory feedback for turning spellmode off (will need a dedicated bool to avoid being called each frame)
                handRModel.GetComponent<Renderer>().material = matHandDefault;
                audioSource.clip = exitSpellposeSFX;
                audioSource.Play();
            }
        }

        //If spellmode is active...
        if (spellpose == true && spellposeCues == false)
        {
            //Bool to avoid calling this each frame
            spellposeCues = true;

            //Enable spellposeColliderGo & spellposeTrailGo
            spellposeColliderGo.SetActive(true);

            //"Enabling" the Trail Renderer
            spellposeTrailGo.GetComponent<TrailRenderer>().startWidth = trailRendererDefaultStartWidth;
            spellposeTrailGo.GetComponent<TrailRenderer>().endWidth = trailRendererDefaultEndWidth;

            //Trying to hide the old trail, when entering spellmode again
            spellposeTrailGo.GetComponent<TrailRenderer>().time = trailRendererDefaultTime;

            //Execute all visual & auditory feedback for turning spellmode on (will need a dedicated bool to avoid being called each frame)
            handRModel.GetComponent<Renderer>().material = matHandSpellpose;
            audioSource.clip = enterSpellposeSFX;
            audioSource.Play();
        }
    }
}
