using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColors : MonoBehaviour
{
    public LayerMask clickableLayer;
    [SerializeField] private List<Transform> selectedObjects = new List<Transform>();

    private Transform firstSelectedObject;
    private Transform secondSelectedObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayer))
            {
                Transform hitObject = hit.collider.transform;

                if (selectedObjects.Contains(hitObject))
                {
                    if (firstSelectedObject == null)
                    {
                        firstSelectedObject = hitObject;
                        Debug.Log("First object selected");
                    }
                    else if (secondSelectedObject == null && hitObject != firstSelectedObject)
                    {
                        secondSelectedObject = hitObject;
                        Debug.Log("Second object selected");

                        // Switch positions if both objects are selected
                        SwitchObjectsPosition();
                    }
                }
            }
        }
    }

    void SwitchObjectsPosition()
    {
        if (firstSelectedObject != null && secondSelectedObject != null)
        {
            Vector3 tempPos = firstSelectedObject.position;
            firstSelectedObject.position = secondSelectedObject.position;
            secondSelectedObject.position = tempPos;

            Debug.Log("Objects swapped: " + firstSelectedObject.name + " and " + secondSelectedObject.name);

            // Reset selected objects
            firstSelectedObject = null;
            secondSelectedObject = null;
        }
    }
}
