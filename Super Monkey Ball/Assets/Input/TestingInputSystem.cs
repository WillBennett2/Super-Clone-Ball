using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class TestingInputSystem : MonoBehaviour
{
    //public static bool m_Paused = false;
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
    }
    //public void Pause(InputAction.CallbackContext context)
    //{
    //    Debug.Log("Pause2");
    //    //Debug.Log("Pause"+ context.phase); //shows the different phases of getting a button input

    //}
    public void Pause()
    {
        Debug.Log("Pause1");
        //Debug.Log("Pause"+ context.phase); //shows the different phases of getting a button input
        
    }

    public void ControllerDisconnect()
    {
        Debug.LogError("Controller Disconnected.");
    }
}
