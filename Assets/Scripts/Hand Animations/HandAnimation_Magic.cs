//Script to controll the player's hand animations via input reference and animation blend trees

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation_Magic : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] Animator animator;

    //Private Variables
    private const string _animParameter_Grip = "Grip";
    private const string _animParameter_Spellpose = "Spellpose";

    void Update()
    {
        //Grabbing the user input reference from the User Input Manager script, if the grab input (R) is greater than 0...
        if (UserInput.Instance.inputGrip_R.action.ReadValue<float>() > 0)
        {
            //Then set the animator paramenter, which controls the hand animation blendtree gradient, to the same value, as the input value (how much the button is being pressed down on the controller)
            animator.SetFloat(_animParameter_Grip, UserInput.Instance.inputGrip_R.action.ReadValue<float>());
        }
        else
        {
            animator.SetFloat(_animParameter_Grip, 0.0f);
        }

        if (UserInput.Instance.inputSpellpose_R.action.ReadValue<float>() > 0)
        {
            animator.SetFloat(_animParameter_Spellpose, UserInput.Instance.inputSpellpose_R.action.ReadValue<float>());
        }
        else
        {
            animator.SetFloat(_animParameter_Spellpose, 0.0f);
        }
    }
}

#region Pre User Input Manager Script
//Pre User Input Manager Script:

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;
//
//public class HandAnimation_Magic : MonoBehaviour
//{
//    //Serialized Variables
//    [SerializeField] Animator animator;
//    [SerializeField] InputActionReference inputGrip;
//    [SerializeField] InputActionReference inputSpellpose;
//
//    //Private Variables
//    private const string _animParameter_Grip = "Grip";
//    private const string _animParameter_Spellpose = "Spellpose";
//
//    void Update()
//    {
//        if (inputGrip.action.ReadValue<float>() > 0)
//        {
//            animator.SetFloat(_animParameter_Grip, inputGrip.action.ReadValue<float>());
//        }
//        else
//        {
//            animator.SetFloat(_animParameter_Grip, 0.0f);
//        }
//
//        if (inputSpellpose.action.ReadValue<float>() > 0)
//        {
//            animator.SetFloat(_animParameter_Spellpose, inputSpellpose.action.ReadValue<float>());
//        }
//        else
//        {
//            animator.SetFloat(_animParameter_Spellpose, 0.0f);
//        }
//    }
//}
#endregion