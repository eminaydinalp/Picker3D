using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingController : MonoBehaviour
{
    [SerializeField] private GameObject rightWing;
    [SerializeField] private GameObject leftWing;
    
    private void OnEnable()
    {
        PickerPhysicsCallbacks.hittedBallCollecterEvent += DisableWings;
    }
    private void OnDisable()
    {
        PickerPhysicsCallbacks.hittedBallCollecterEvent -= DisableWings;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WingUpgrade"))
        {
            Debug.Log("WingUpgrade");
            other.gameObject.SetActive(false);
            EnableWings();
        }
    }

    private void EnableWings()
    {
        leftWing.SetActive(true);
        rightWing.SetActive(true);
    }

    private void DisableWings()
    {
        leftWing.SetActive(false);
        rightWing.SetActive(false);
    }
}
