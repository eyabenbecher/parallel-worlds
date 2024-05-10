using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupDrop : MonoBehaviour
{
    [SerializeField]private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask
        ;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            float pickUpDistance = 2f;
            RaycastHit raycastHit;
           if( Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out raycastHit, pickUpDistance, pickUpLayerMask))
            {
                Debug.Log(raycastHit.transform);
            }
        }
    }
}
