using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCamera : MonoBehaviour
{
    public Transform target;
    // [SerializeField] 시간 오래걸림.
    public float speed;


    void Start()
    {

    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
    }
}
