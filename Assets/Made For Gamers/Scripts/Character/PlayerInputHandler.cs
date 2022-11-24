using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    /// <summary>
    /// Player input and interaction system
    /// </summary>
    private InputSystem input;  
    [SerializeField] private GameObject uiHammerspace;

    private void OnEnable()
    {
        //Init Input
        input = new InputSystem();
        input.UI.Enable();

        //Input Events
        input.UI.Quit.started += Quit;
        input.UI.RPMeUrlUI.started += RPMeUrlUI;
        input.UI.Hammersapce.started += Hammerspace;
    }  

    private void OnDisable()
    {
        //CleanUp events
        input.UI.Quit.started -= Quit;
        input.UI.RPMeUrlUI.started -= RPMeUrlUI;
        input.UI.Hammersapce.started -= Hammerspace;
    }
   
    private void Hammerspace(InputAction.CallbackContext ctx)
    {
        if (uiHammerspace.activeSelf == true)
        {
            uiHammerspace.SetActive(false);
        }
        else
        {
            uiHammerspace.SetActive(true);
        }
    }

    //QUIT
    private void Quit(InputAction.CallbackContext ctx)
    {
        Application.Quit();
    }

    //RPMe url Input
    private void RPMeUrlUI(InputAction.CallbackContext ctx)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("RPMe Link UI").transform.GetChild(0).gameObject.SetActive(true);
    }
}

