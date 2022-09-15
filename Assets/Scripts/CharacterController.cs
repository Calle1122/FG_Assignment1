using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float speed = 10f;
    public float jumpForce = 10f;

    private Rigidbody _playerRb;
    
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        
    }
}
