using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour
{
    /// <summary>
    /// Player input and interaction system
    /// </summary>
    private InputSystem input;
    [Header("Interaction RayCast")]
    [SerializeField] private float distance; //raycast distance
    [SerializeField] private LayerMask layer = 1 << 6; //raycast layer
    [Header("Game Manager")]   
    private Vector2 moveInput;
    public float xInput;
    public float yInput;
    public bool run;
    public bool jump;   
    private Camera avatarCamera;
   

    private void OnEnable()
    {
        //Init Input
        input = new InputSystem();
        input.Misc.Enable();
        input.Player.Enable();

        //Input Events
        input.Misc.Quit.started += Quit;
        input.Player.Interaction.started += Interact;
        input.Player.Run.performed += Run;
        input.Player.Jump.performed += Jump;
        input.Misc.RPMeUrlUI.started += RPMeUrlUI;
        input.Player.Move.performed += i => moveInput = i.ReadValue<Vector2>();
    }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        avatarCamera = Camera.main;
    }

    private void OnDisable()
    {
        //CleanUp events
        input.Misc.Quit.started -= Quit;
        input.Player.Interaction.started -= Interact;
        input.Player.Run.performed -= Run;
        input.Player.Jump.performed -= Jump;
        input.Misc.RPMeUrlUI.started -= RPMeUrlUI;
    }

    //Called by player controller
    public void UpdateInputs()
    {
        MoveInput();
    }

    //RUN
    public void Run(InputAction.CallbackContext obj)
    {
        if (obj.ReadValueAsButton() == true)
        {
            run = true;
        }
        else
        {
            run = false;
        }
    }

    //JUMP
    private void Jump(InputAction.CallbackContext obj)
    {
        if (!jump)
        {
            jump = true;
        }
    }

    //MOVE
    private void MoveInput()
    {
        xInput = moveInput.x;
        yInput = moveInput.y;
    }

    //QUIT
    private void Quit(InputAction.CallbackContext obj)
    {
        Application.Quit();
    }

    //RPMe url Input
    private void RPMeUrlUI(InputAction.CallbackContext obj)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("RPMe Link UI").transform.GetChild(0).gameObject.SetActive(true);
    }

    //INTERACT
    private void Interact(InputAction.CallbackContext obj)
    {
        RayCast();
    }

    //Player Interactions
    void RayCast()
    {
        Ray ray = new Ray(avatarCamera.transform.position, avatarCamera.transform.forward);
        RaycastHit hitData;
        if (Physics.Raycast(ray, out hitData, distance, layer))
        {
            print("Raycast -> " + hitData.transform.name);

            //perform action bt the name of the clicked object, must be on the Interact layer and within raycast distance
            switch (hitData.transform.name)
            {
                #region === World Walker Portals ===

                case "Portal Test Door":
                   hitData.transform.GetChild(0).gameObject.SetActive(true); // Display portal UI confirmation box
                    return;
               

                    #endregion
            }
        }
    }

}

