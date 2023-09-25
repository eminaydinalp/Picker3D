using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        private bool _isTouching;

        private float _currentVelocity;
        public float3 moveVector;
        private Vector2? _mousePosition;

        public float HorizontalInputSpeed = 1.2f;
        public float ClampSpeed = 0.07f;


        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _isTouching = true;

                _mousePosition = Input.mousePosition;
            }

            if (_isTouching)
            {
                Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
                if (mouseDeltaPos.x > HorizontalInputSpeed)
                    moveVector.x = HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                else if (mouseDeltaPos.x < -HorizontalInputSpeed)
                    moveVector.x = -HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                else
                    moveVector.x = Mathf.SmoothDamp(moveVector.x, 0f, ref _currentVelocity,
                        ClampSpeed);

                //moveVector.x = mouseDeltaPos.x;

                _mousePosition = Input.mousePosition;
            }
        }
    }
}