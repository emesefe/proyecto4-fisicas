using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    [SerializeField] private float speed = 30f;
    private float forwardInput;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        
        // forwardInput > -0.01f && forwardInput < 0.01f
        if (Mathf.Abs(forwardInput) < 0.01f)
        {
            playerRigidbody.velocity = Vector3.zero;
        }
        else
        {
            playerRigidbody.AddForce(Vector3.forward * 
                                     speed * forwardInput);
        }
    }
}
