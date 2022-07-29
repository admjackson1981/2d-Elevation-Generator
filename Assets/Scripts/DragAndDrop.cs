using System;
using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField]

    private InputAction mouseClick;

    [SerializeField]
    private InputAction scrollZoom;

    [SerializeField]
    private float mouseDragSpeed = 0.0f;
    private Vector3 velocity = Vector3.zero;

    private Camera mainCamera;

    private float CamerProj;

    private void Awake()
    {
        mainCamera = Camera.main; 
    }


    private void OnEnable()
    {
        CamerProj = mainCamera.orthographicSize;

        mouseClick.Enable();
        mouseClick.performed += MousePressed;

        scrollZoom.Enable();
        scrollZoom.performed += ScroolZoomed;
    }

   
    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        scrollZoom.performed -= ScroolZoomed;
    }
    private void MousePressed(InputAction.CallbackContext obj)
    {
        Debug.Log("pressed");
     Ray ray=   mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider != null)
            {
                Debug.Log("hit" + hit.collider.name);
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }

        RaycastHit2D hit2d = Physics2D.GetRayIntersection(ray);
       
            if (hit2d.collider != null)
            {
                Debug.Log("hit" + hit2d.collider.name);
                StartCoroutine(DragUpdate(hit2d.collider.gameObject));
            }
        


    }

    IEnumerator DragUpdate(GameObject clickedObject)
    {
        float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);


        while(mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);   

            yield return null;
        }
    }

    private void ScroolZoomed(InputAction.CallbackContext context)
    {
        
     
        if(context.ReadValue<Vector2>() == new Vector2(0,120f) )
        {
            CamerProj += 0.2f;
        }
        else
        {
            CamerProj -= 0.2f;
        }
      
        mainCamera.orthographicSize = CamerProj;
    }
 
}
