using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private float smoothTime = 0.1f;
    public Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    private void LateUpdate()
    {

        //transform.position = target.position +velocity;
       ///* if (target == null) { return; }
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
