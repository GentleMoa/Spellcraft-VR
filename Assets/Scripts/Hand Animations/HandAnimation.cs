using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] Animator animator;
    [SerializeField] InputActionReference inputGrip;

    //Private Variables
    private const string _animParameter_Grip = "Grip";

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
    }
}
