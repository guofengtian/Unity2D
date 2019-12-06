using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoMove : MonoBehaviour
{
    private float moveSpeed = 5;
    void Start()
    {
        transform.position = new Vector2(-30, 30);
    }
    void Update()
    {
        if (transform.position.x > -30)
        {
            transform.Translate(Vector2.right * -moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(-6, 30);
        }
    }
}
