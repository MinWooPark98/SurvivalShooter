using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    private Vector3 targetDistance = new Vector3(1f, 15f, -22f);

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position + targetDistance;
            transform.LookAt(target.transform);
        }
    }
}
