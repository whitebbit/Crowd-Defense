using UnityEngine;

public class TCP2_Demo_AutoRotate : MonoBehaviour
{
    public Vector3 axis = Vector3.up;
    public float Speed = -50f;

    private void Update()
    {
        transform.Rotate(axis, Time.deltaTime * Speed);
    }
}