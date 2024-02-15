using System;
using _3._Scripts.Game.Main;
using UnityEngine;
using YG;

namespace _3._Scripts.Game
{
    public class CameraMouseLook : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 5.0f;
        [SerializeField] private float rotationSmoothness  = 5.0f;
        //[SerializeField] private float direction = -1f;
        [SerializeField] private Vector2 xClamp;
        [SerializeField] private Vector2 yClamp;

        private float _yRotation;
        private float _xRotation;
        private Vector3 _originalRotation;
        private Touch _initialTouch = new();
        
        private void Start()
        {
            rotationSpeed = YandexGame.EnvironmentData.isMobile ? rotationSpeed * 0.25f : rotationSpeed;

            if(!YandexGame.EnvironmentData.isMobile) return;
            
            _originalRotation = transform.eulerAngles;
            _xRotation = _originalRotation.x;
            _yRotation = _originalRotation.y;
        }

        private void Update()
        {
            if (!LevelManager.Instance.CurrentLevel.LevelInProgress) return;
            Rotate();
        }

        private void Rotate()
        {
            if (!Input.GetMouseButton(0)) return;
            
            var mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            _yRotation += mouseX;
            _xRotation -= mouseY;

            _yRotation = Mathf.Clamp(_yRotation, yClamp.x, yClamp.y);
            _xRotation = Mathf.Clamp(_xRotation, xClamp.x, xClamp.y);

            var targetRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
        }

        private void MobileRotation()
        {
            foreach (var touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _initialTouch = touch;
                        break;
                    case TouchPhase.Moved:
                    {
                        var deltaX = _initialTouch.position.x - touch.position.x;
                        var deltaY = _initialTouch.position.y - touch.position.y;

                        //_xRotation -= deltaY * Time.deltaTime * rotationSpeed * direction;
                        //_yRotation += deltaX * Time.deltaTime * rotationSpeed * direction;

                        _xRotation = Mathf.Clamp(_xRotation, xClamp.x, xClamp.y);
                        _yRotation = Mathf.Clamp(_yRotation, yClamp.x, yClamp.y);

                        transform.eulerAngles = new Vector3(_xRotation, _yRotation, 0);
                        break;
                    }
                    case TouchPhase.Ended:
                        _initialTouch = new Touch();
                        break;
                }
            }
        }
        
        
        private static void UnlockCursor()
        {
            if (Input.GetMouseButtonUp(0))
                Cursor.lockState = CursorLockMode.None;
        }

        private static void LockCursor()
        {
            if (Input.GetMouseButtonDown(0))
                Cursor.lockState = CursorLockMode.Locked;
        }
    }
}