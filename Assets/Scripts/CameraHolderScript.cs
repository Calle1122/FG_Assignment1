using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolderScript : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;

    [SerializeField] private ActivePlayerController currentPlayer;
    
    void Start()
    {
        playerCameraTransform = currentPlayer.GetActiveUnit().transform;
    }

    void Update()
    {
        transform.position = playerCameraTransform.position;
    }
}
