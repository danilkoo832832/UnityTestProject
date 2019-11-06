using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMeRight : MonoBehaviour
{
    public float rotating;

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(3,3,3);
        //float F = transform.position.x;
        transform.Rotate(rotating,rotating,rotating);
    }
}
