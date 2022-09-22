using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadRobot : MonoBehaviour
{
    private bool _playAnimation = false;

    public float rotationSpeed = 5f;
    
    private float _wobbleAmount = 3.5f;
    private float _wobbleTimer = .4f;
    private int _wobbleSwitch = 0;

    private void OnEnable()
    {
        _playAnimation = true;
    }

    private void Update()
    {
        if (_playAnimation)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + .5f * Time.deltaTime, transform.position.z);

            _wobbleTimer -= Time.deltaTime;

            if (_wobbleTimer <= 0)
            {
                if (_wobbleSwitch == 0)
                {
                    _wobbleSwitch = 1;
                }

                else if (_wobbleSwitch == 1)
                {
                    _wobbleSwitch = 0;
                }

                switch (_wobbleSwitch)
                {
                    case 0:
                        _wobbleTimer = .4f;
                        _wobbleAmount = 3.5f;
                        break;

                    case 1:
                        _wobbleTimer = .4f;
                        _wobbleAmount = -3.5f;
                        break;
                }
            }
            
            Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, transform.position.y, _wobbleAmount);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
