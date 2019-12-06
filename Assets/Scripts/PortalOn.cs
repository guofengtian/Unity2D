using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOn : MonoBehaviour
{
    private string m;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m.Equals("true"))
        {
            this.transform.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            this.transform.GetComponent<SpriteRenderer>().sortingOrder = 3;
        }

    }
    void ShowMethod(string show)
    {
        m = show;
    }
}
