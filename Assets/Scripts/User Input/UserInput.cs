using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    //User Input references
    public InputActionReference inputGrip_L;
    public InputActionReference inputGrip_R;
    public InputActionReference inputSpellpose_R;

    //Singleton
    public static UserInput Instance { set; get; }

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
}
