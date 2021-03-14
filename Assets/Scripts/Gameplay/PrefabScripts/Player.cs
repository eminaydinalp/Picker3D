using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Player : MonoBehaviour
{
    Rigidbody _myRigidbody;
    [SerializeField]
    GameController _gameController;
    [SerializeField]
    float _speed = 1f;
    Vector3 _moveVector;
    Vector2 _moveVectorForAndroid;
    [SerializeField]
    Collider _levelEndLine;
    GameData _gameData;
    Transform _gameOverPosition;
    public Transform GameOverPosition { get => _gameOverPosition; }
    private void Awake()
    {
        _gameController = GameController.Instance;
        _gameData = GameData.Instance;
        _moveVector = new Vector3(0,0,-1);
        _myRigidbody = GetComponent<Rigidbody>();
    }
    void Forward()
    {
        if (_gameData.IsStart)
            _myRigidbody.velocity = _moveVector * _speed;
    }
    public void SetKinematic(bool isKinematic)
    {
        _myRigidbody.isKinematic = isKinematic;
    }
    private void FixedUpdate()
    {
        Forward();
        Move();
    }
    public void SetLocation()
    {
        gameObject.transform.position = _gameController.Checkouts[_gameData.CurrentLevel].position;
    }
   
    public void SetLocationForInfinity(int cache)
    {
        gameObject.transform.position = new Vector3(0,1.1f, _gameOverPosition.position.z+60);
    }
    void Move()
    {
        if (_gameData.IsStart)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x >= Screen.width / 2)
                {
                    transform.position = new Vector3(transform.position.x + -0.1f, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
                }
            }
        }
#if UNITY_EDITOR
        if (_gameData.IsStart)
        {
            if (Input.GetMouseButton(0))
            {
                transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            }
            if (Input.GetMouseButton(1))
            {
                transform.position = new Vector3(transform.position.x + -0.1f, transform.position.y, transform.position.z);
            }
        }
#endif
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            _gameOverPosition = collision.transform.parent.transform;
        }
    }
}
