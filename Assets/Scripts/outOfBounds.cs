using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outOfBounds : MonoBehaviour
{
    [SerializeField] Rigidbody2D boat;
    public float ofbImpulse;
    //[SerializeField] GameObject bounds;
    void FixedUpdate()
    {
        Vector3 boatPos = boat.transform.position;

        if(boatPos.x >0.8)
        {
            boat.AddForce((new Vector2(-boatPos.x, 0) * ofbImpulse), ForceMode2D.Force);
            //Debug.Log("out of bounds right");
        }

        if (boatPos.x < -0.8)
        {
            boat.AddForce(new Vector2(boatPos.x,0) * -ofbImpulse, ForceMode2D.Force);
            //Debug.Log("out of bounds left");
        }
    }
}
