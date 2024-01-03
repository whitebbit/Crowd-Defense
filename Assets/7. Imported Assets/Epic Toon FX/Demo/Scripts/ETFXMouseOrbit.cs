﻿using UnityEngine;

namespace EpicToonFX
{
    public class ETFXMouseOrbit : MonoBehaviour
    {
        public Transform target;
        public float distance = 5.0f;
        public float xSpeed = 120.0f;
        public float ySpeed = 120.0f;
        public float yMinLimit = -20f;
        public float yMaxLimit = 80f;
        public float distanceMin = .5f;
        public float distanceMax = 15f;
        public float smoothTime = 2f;
        private float rotationXAxis;
        private float rotationYAxis;
        private float velocityX;

        private float velocityY;

        // Use this for initialization
        private void Start()
        {
            var angles = transform.eulerAngles;
            rotationYAxis = angles.y;
            rotationXAxis = angles.x;
            // Make the rigid body not change rotation
            if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().freezeRotation = true;
        }

        private void LateUpdate()
        {
            if (target)
            {
                if (Input.GetMouseButton(1))
                {
                    velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
                    velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
                }

                rotationYAxis += velocityX;
                rotationXAxis -= velocityY;
                rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
                //Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
                var toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
                var rotation = toRotation;

                distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
                RaycastHit hit;
                if (Physics.Linecast(target.position, transform.position, out hit)) distance -= hit.distance;
                var negDistance = new Vector3(0.0f, 0.0f, -distance);
                var position = rotation * negDistance + target.position;

                transform.rotation = rotation;
                transform.position = position;
                velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
                velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
            }
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}