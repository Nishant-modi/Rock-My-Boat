using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody2D rb;
    public float depthBeforeSub = 1f;
    public float dispAmount = 3f;

    private void FixedUpdate()
    {
        if(transform.position.y < -2)
        {
            float dispMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSub) * dispAmount;
            rb.AddForce(new Vector2(0f, Mathf.Abs(Physics2D.gravity.y) * dispMultiplier * 5), ForceMode2D.Force);
        }
    }

}
