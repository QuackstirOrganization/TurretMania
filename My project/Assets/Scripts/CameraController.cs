using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public float Speed;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Target.position.x, Target.position.y, -10), Time.deltaTime * Speed);
    }
}
