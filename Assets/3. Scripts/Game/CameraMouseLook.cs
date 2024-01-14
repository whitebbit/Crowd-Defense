using System;
using _3._Scripts.Game.Main;
using UnityEngine;

namespace _3._Scripts.Game
{
    public class CameraMouseLook : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 5.0f;
        [SerializeField] private Vector2 xClamp;
        [SerializeField] private Vector2 yClamp;

        private float _yRotation;
        private float _xRotation;

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

            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
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