using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Xstand : MonoBehaviour
{
    public float length = 0.95f;
    public float desiredHeight = 0.7f;
    public float minBuffer = 0.05f;
    
    public Transform leg1, leg2;
    public Transform foot1, foot2;
    public Transform foot1b, foot2b;
    public Transform top;

    public bool onlyInEditor = true;
    
    void Update()
    {
        if (onlyInEditor)
        {
            if (Application.isPlaying)
                return;
        }

        // use top shelf pos as desired height
        var topPos = top.position;
        desiredHeight = topPos.y;
        if (transform.position.y < 0)
            transform.position += (Vector3.up * transform.position.y);

        // clamp height to physical possible values
        desiredHeight = Mathf.Clamp(desiredHeight, minBuffer, length - minBuffer);
        topPos.y = desiredHeight;
        top.position = topPos;
        var tempPos = top.position;
        top.localPosition = Vector3.zero;
        transform.position += top.position - tempPos;
        
        var halfPos = topPos;
        halfPos.y = topPos.y / 2f;
        // position legs at middle
        leg1.position = leg2.position = halfPos;

        // pythagoras ;)
        var a = length;
        var b = desiredHeight;
        var c = Mathf.Sqrt(a * a - b * b);

        var lookPos = top.position + top.right * c/2;
        leg1.LookAt(lookPos);
        foot1.position = lookPos;
        foot1b.position = lookPos + Vector3.down * lookPos.y;
        lookPos = top.position - top.right * c/2;
        leg2.LookAt(lookPos);
        foot2.position = lookPos;
        foot2b.position = lookPos + Vector3.down * lookPos.y;

    }
}
