using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    [SerializeField] private float speed = 30f;
    private float forwardInput;

    [SerializeField] private GameObject focalPointGameObject;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        
        playerRigidbody.AddForce(focalPointGameObject.transform.forward * 
                                 speed * forwardInput);
        
        // SI QUEREMOS QUE EL PLAYER FRENE CUANDO NO PULSAMOS VERTICAL INPUT
        // // forwardInput > -0.01f && forwardInput < 0.01f
        // if (Mathf.Abs(forwardInput) < 0.01f)
        // {
        //     playerRigidbody.velocity = Vector3.zero;
        // }
        // else
        // {
        //     playerRigidbody.AddForce(focalPointGameObject.transform.forward * 
        //                              speed * forwardInput);
        // }
    }
}
