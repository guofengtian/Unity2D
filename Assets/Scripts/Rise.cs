using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rise : MonoBehaviour
{
    private float moveSpeed = 5;
    private string m;
    void Start()
    {
        transform.position = new Vector2(32, -9);
        RiseMethod(m);
    }
    void Update()
    {
        if (m.Equals("true"))
        {
            if (transform.position.y < 30)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(32, -9);
            }
        }
    }
    void RiseMethod(string rise)
    {
        m = rise;
    }
 
}
