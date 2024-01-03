//Default Hand Animation script, including Grab and Idle pose

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] Animator animator;

    //Private Variables
    private const string _animParameter_Grip = "Grip";

    void Update()
    {
        if (UserInput.Instance.inputGrip_L.action.ReadValue<float>() > 0)
        {
            animator.SetFloat(_animParameter_Grip, UserInput.Instance.inputGrip_L.action.ReadValue<float>());
        }
        else
        {
            animator.SetFloat(_animParameter_Grip, 0.0f);
        }
    }
}
