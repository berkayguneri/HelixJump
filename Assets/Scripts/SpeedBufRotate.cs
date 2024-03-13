using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBufRotate : MonoBehaviour
{
    public float rotationSpeed;
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * 5  * Time.deltaTime);
    }
}
