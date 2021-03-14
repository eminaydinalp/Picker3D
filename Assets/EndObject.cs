using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class EndObject : MonoBehaviour
{
    Animator _myAnimator;

    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }
    public void Open()
    {
        _myAnimator.SetTrigger("open");
    }
}
