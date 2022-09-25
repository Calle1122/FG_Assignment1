using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPickup : MonoBehaviour
{
    [SerializeField] private float time;

    private ActivePlayerController _activePlayerCon;

    private void Start()
    {
        _activePlayerCon = GameObject.Find("ActivePlayerController").GetComponent<ActivePlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _activePlayerCon.currentTurnTimer += time;
            
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, 100f * Time.deltaTime);
    }
}