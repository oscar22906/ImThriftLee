using UnityEngine;
using System.Collections;

public class PORSpin : MonoBehaviour
{
    public Vector3 speed = new Vector3(0, 0, 5);
    public Transform transform;

    private void FixedUpdate()
    {
        transform.Rotate(speed);
    }



}
