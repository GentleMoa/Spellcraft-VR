using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation_Magic : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] Animator animator;
    [SerializeField] InputActionReference inputGrip;
    [SerializeField] InputActionReference inputSpellpose;

    //Private Variables
    private const string _animParameter_Grip = "Grip";
    private const string _animParameter_Spellpose = "Spellpose";

    void Update()
    {
        if (inputGrip.action.ReadValue<float>() > 0)
        {
            animator.SetFloat(_animParameter_Grip, inputGrip.action.ReadValue<float>());
        }
        else
        {
            animator.SetFloat(_animParameter_Grip, 0.0f);
        }

        if (inputSpellpose.action.ReadValue<float>() > 0)
        {
            animator.SetFloat(_animParameter_Spellpose, inputSpellpose.action.ReadValue<float>());
        }
        else
        {
            animator.SetFloat(_animParameter_Spellpose, 0.0f);
        }
    }
}
