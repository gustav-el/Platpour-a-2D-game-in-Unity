using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer: MonoBehaviour
{

    public Transform followTransform;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(followTransform.position.x +5, followTransform.position.y, this.transform.position.z);
        
        
    }
}