using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Mouse;

public class MouseInput : MonoBehaviour, IMouseActionsActions
{

    Mouse controls;
    private void Awake()
    {
        controls = new Mouse();
       

        controls.MouseActions.SetCallbacks(this);
    }
    public void OnEnable()
    {
       
        controls.Enable();
    }
    public void OnDisable()
    {
        controls.Disable();
    }
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("clicked");
                break;

        }
    }

    public void OnLeftHold(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("held");
                break;

        }
    }

    public void OnZoomInOut(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Debug.Log( context.ReadValue<Vector2>());
                Debug.Log("wheels");
                break;

        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("soace");
        throw new System.NotImplementedException();
    }
}
