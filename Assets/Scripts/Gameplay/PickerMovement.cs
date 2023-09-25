using System;
using UnityEngine;
using DG.Tweening;
using Gameplay;
using Lean.Touch;
using UnityEngine.Serialization;

public class PickerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float horiztontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    public bool canMove = false;
    public bool canRun = false;
    private float horizontal;
    private Vector3 mousePosition;

    public static event Action movedToNextStartEvent;
    
    private LeanFinger myFinger;

    [SerializeField] private float direction;
    
    [Header("Horizontal Movement Information \n")] 
    [SerializeField] private Vector3 localMoverTarget;
    
    [SerializeField] private float lerpSpeed;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FingerDown(LeanFinger finger)
    {
        if (finger.IsOverGui)
            return;

        if (myFinger == null)
        {
            myFinger = finger;
        }
    }

    private void FingerUpdate(LeanFinger finger)
    {
        if (finger == myFinger)
        {
            if (canMove)
            {
                direction = finger.ScaledDelta.x;
                
                if (Input.GetMouseButton(0))
                {
                    //Move(direction, horiztontalSpeed);
                
                }
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    if (touch.phase is TouchPhase.Moved or TouchPhase.Stationary)
                    {
                        //Move(direction , horiztontalSpeed);
                    }
                }
            }
        }
    }

    private void FingerUp(LeanFinger finger)
    {
        if (myFinger == finger)
        {
            _rigidbody.velocity = Vector3.zero;
            localMoverTarget = Vector3.zero;
            direction = 0;
            myFinger = null;
        }
    }
    
    private void MovePlayer()
    {
        var velocity = _rigidbody.velocity;
        velocity = new Vector3(InputManager.Instance.moveVector.x * horiztontalSpeed, velocity.y, velocity.z);
        _rigidbody.velocity = velocity;
    }

    private void Move()
    {
        if(Mathf.Abs(direction) < 0.1f) return;
        var velocity = _rigidbody.velocity;
        velocity = new Vector3( direction * horiztontalSpeed, velocity.y, velocity.z);
        _rigidbody.velocity = velocity;
    }
    
    // public void FollowLocalMoverTarget()
    // {
    //     //Vector3 nextPos = new Vector3(localMoverTarget.x, 0, 0); ;
    //     //_rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, nextPos, lerpSpeed * Time.deltaTime);
    //     //_rigidbody.velocity = nextPos;
    //     _rigidbody.MovePosition(_rigidbody.position + localMoverTarget);
    // }
    //
    // public void Move(float direction, float speed)
    // {
    //     localMoverTarget += Vector3.right * (direction * speed * Time.deltaTime);
    // }

    private void VerticalMove()
    {
        var velocity = _rigidbody.velocity;
        velocity = new Vector3( velocity.x, velocity.y, verticalSpeed);
        _rigidbody.velocity = velocity;
    }

    private void ClampRigid()
    {
        var position1 = _rigidbody.position;
        var position = new Vector3(Mathf.Clamp(position1.x, -1.3f, 1.3f),
            position1.y, position1.z);
        _rigidbody.position = position;
    }
    private void Update()
    {
        if (canRun)
        {
            ClampRigid();
            //FollowLocalMoverTarget();
        }
    }
    private void FixedUpdate()
    {
        if (canRun)
        {
            //Move();
            VerticalMove();
            //FollowLocalMoverTarget();
            
            MovePlayer();
        }
    }
    private void EnableMovement()
    {
        canMove = true;
        canRun = true;
    }
    private void DisableMovement()
    {
        canMove = false;
        canRun = false;
        direction = 0;
        _rigidbody.velocity = Vector3.zero;
    }
    private void DisableVerticalMovement()
    {
        canRun = false;
        direction = 0;
        _rigidbody.velocity = Vector3.zero;
    }
    private void EnableVerticalMovement()
    {
        canRun = true;
    }
    private void MoveToNextLevelStartPos()
    {
        DisableMovement();
        float curLevellength = LevelManager.Instance.GetCurrentLevelLength(LevelManager.Instance.GetCurrentLevel());
        Vector3 targetPos = new Vector3(0, transform.position.y, curLevellength-10f);
        transform.DOMove(targetPos, 2f).OnComplete(() =>
        {
            movedToNextStartEvent?.Invoke();
        });
    }
    private void OnEnable()
    {
        GameManager.gameStartedEvent += EnableMovement;
        GameManager.gameFinishedEvent += DisableMovement;
        PickerPhysicsCallbacks.hittedBallCollecterEvent += DisableVerticalMovement;
        PickerPhysicsCallbacks.hittedLevelEndEvent += MoveToNextLevelStartPos;
        BallCollecterPlatform.collecterSuccessEvent += EnableVerticalMovement;
        
        LeanTouch.OnFingerUpdate += FingerUpdate;
        LeanTouch.OnFingerDown += FingerDown;
        LeanTouch.OnFingerUp += FingerUp;
    }
    private void OnDisable()
    {
        GameManager.gameStartedEvent -= EnableMovement;
        GameManager.gameFinishedEvent -= DisableMovement;
        PickerPhysicsCallbacks.hittedBallCollecterEvent -= DisableVerticalMovement;
        PickerPhysicsCallbacks.hittedLevelEndEvent -= MoveToNextLevelStartPos;
        BallCollecterPlatform.collecterSuccessEvent -= EnableVerticalMovement;
        
        LeanTouch.OnFingerUpdate -= FingerUpdate;
        LeanTouch.OnFingerDown -= FingerDown;
        LeanTouch.OnFingerUp -= FingerUp;
    }
}
