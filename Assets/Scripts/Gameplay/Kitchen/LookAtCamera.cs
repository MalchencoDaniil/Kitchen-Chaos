using UnityEngine;

namespace KitchenChaos.Kitchen
{
    public class LookAtCamera : MonoBehaviour
    {
        private enum Mode
        {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted
        }

        [SerializeField] private Mode _lookAtMode = Mode.CameraForward;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = FindAnyObjectByType<Camera>();
        }

        private void LateUpdate()
        {
            switch (_lookAtMode)
            {
                case Mode.LookAt:
                    transform.LookAt(_mainCamera.transform.position);
                    break;
                case Mode.LookAtInverted:
                    Vector3 _directionFromCamera = transform.position - _mainCamera.transform.position;
                    transform.LookAt(transform.position + _directionFromCamera);
                    break;
                case Mode.CameraForward:
                    transform.forward = _mainCamera.transform.forward;
                    break;
                case Mode.CameraForwardInverted:
                    transform.forward = -_mainCamera.transform.forward;
                    break;
            }
        }
    }
}