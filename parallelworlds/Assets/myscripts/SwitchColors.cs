using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColors : MonoBehaviour
{
    public LayerMask clickableLayer;
    [SerializeField] private List<Transform> selectedObjects = new List<Transform>();
    public List<GameObject> targetObjects = new List<GameObject>(); 
    public AudioClip correctOrderSound;
    private AudioSource audioSource;
    private bool allObjectsInPosition = false;
    private Transform firstSelectedObject;
    private Transform secondSelectedObject;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

                        SwitchObjectsPosition();
                        CheckObjectPositions();
                    }
                }
            }
        }
    }

    void CheckObjectPositions()
    {
        for (int i = 0; i < selectedObjects.Count; i++)
        {
     
            float distance = Vector3.Distance(selectedObjects[i].position, targetObjects[i].transform.position);
            Debug.Log($"Object {i + 1} position: {selectedObjects[i].position}, Target position: {targetObjects[i].transform.position}, Distance to target position: {distance}");

           
            if (distance > 0.01f)
            {
                return; 
            }
        }

       
        if (!audioSource.isPlaying && !allObjectsInPosition)
        {
            Debug.Log("All objects in correct position");
            audioSource.PlayOneShot(correctOrderSound);
            allObjectsInPosition = true; 
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

            firstSelectedObject = null;
            secondSelectedObject = null;
        }
    }
}
