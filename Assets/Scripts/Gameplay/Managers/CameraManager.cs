using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineBrain _cinemachineBrain;

        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCameraTunel;

        private void Start()
        {
            _cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
        }

        private void OnEnable()
        {
            PickerPhysicsCallbacks.hittedLevelEndEvent += ChangeToSmartUpdate;
            PickerPhysicsCallbacks.hittedLevelEndEvent += ChangeTunnelCam;
        }
        
        private void OnDisable()
        {
            PickerPhysicsCallbacks.hittedLevelEndEvent += ChangeToSmartUpdate;
            PickerPhysicsCallbacks.hittedLevelEndEvent -= ChangeTunnelCam;
        }

        private void ChangeToSmartUpdate()
        {
            _cinemachineBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.SmartUpdate;
        }

        private void ChangeTunnelCam()
        {
            _cinemachineVirtualCameraTunel.Priority = 20;
            DOVirtual.DelayedCall(1.5f, ChangeFollowCam);
        }
        
        private void ChangeFollowCam()
        {
            _cinemachineVirtualCameraTunel.Priority = 2;
        }
    }
}