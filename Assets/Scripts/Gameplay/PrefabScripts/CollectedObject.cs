using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollectedObject : MonoBehaviour
{
    Rigidbody _myRigidbody;
    GameData _gameData;
    private bool _isCollected;
    Vector3 _firstLocation;
    public bool IsCollected
    {
        get { return _isCollected; }
        set { _isCollected = value; }
    }

    private void Awake()
    {
        _firstLocation = gameObject.transform.localPosition;
        _gameData = GameData.Instance;
        _myRigidbody = GetComponent<Rigidbody>();
    }
    public void SetDefaultLocation()
    {
        if (!_gameData.isActiveAndEnabled)
        {
            transform.localPosition = _firstLocation;
            gameObject.SetActive(true);
        }
        else
        {
            transform.localPosition = _firstLocation;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "LevelEndObject")
        {
            _gameData.CollectedObjectCount++;
            transform.localPosition = _firstLocation; 
        }
    }
}
