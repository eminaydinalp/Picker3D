using System;
using Cinemachine;
using UnityEngine;

namespace Gameplay
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineBrain _cinemachineBrain;
        
        private void OnEnable()
        {
            PickerPhysicsCallbacks.hittedLevelEndEvent += ChangeToSmartUpdate;
        }
        
        private void OnDisable()
        {
            PickerPhysicsCallbacks.hittedLevelEndEvent += ChangeToSmartUpdate;
        }

        private void ChangeToSmartUpdate()
        {
            _cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
        }
    }
}