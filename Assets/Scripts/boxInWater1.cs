using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

//using Unity.UI;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class boxInWater1 : MonoBehaviour
{
    [SerializeField] GameObject penaltyText;
    [SerializeField] BuoyancyEffector2D water;
    [SerializeField] int flowMag;
    [SerializeField] GameObject boat;
    [SerializeField] GameObject lostLevel;
    [SerializeField] int reduceSize;
    //[SerializeField] GameObject leftFeedback;
    //[SerializeField] GameObject rightFeedback;
    [SerializeField] GameObject splash;
    public int droppedCount = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Box")
        {
            droppedCount++;

            //if(droppedCount >= 4)
            //lostLevel.SetActive(true);
            StartCoroutine(boxDropped(collision));

        }
    }

    IEnumerator boxDropped(Collider2D col)
    {
        int right;
        Vector3 dropPos = col.gameObject.transform.position;
        //GameObject g = Instantiate(penaltyText, (Vector2)dropPos, Quaternion.identity);
        Vector3 currBoat = boat.transform.localScale;
        dropPos.y = -2.65f;
        GameObject g = Instantiate(splash, (Vector2)dropPos, Quaternion.identity,this.transform);
        //yield return new WaitForSeconds(0.5f);
        
        if (dropPos.x > 0)
        {
            right = 1;
            Coroutine move = StartCoroutine(MoveBoat(right));
            //GameObject g = Instantiate(splash, (Vector2)dropPos, Quaternion.identity, this.transform);
            //rightFeedback.SetActive(true);
            //yield return new WaitForSeconds(0.1f);
            //rightFeedback.SetActive(false);
            //StopCoroutine(move);
            right = 0;
            dropPos.x = 2.1f;
            dropPos.y = 0f;
        }
        else
        {
            right = 2;
            Coroutine move = StartCoroutine(MoveBoat(right));
            //leftFeedback.SetActive(true);
            //yield return new WaitForSeconds(0.1f);
            //eftFeedback.SetActive(false);
            //StopCoroutine(move);
            right = 0;
            dropPos.x = -2.1f;
            dropPos.y = -0f;
        }
        //g.transform.SetParent(water, false);
        //g.transform.position = dropPos;

        //Destroy(g);
        yield return new WaitForSeconds(1f);
        Destroy(g);
        Destroy(col.gameObject);
    }

    IEnumerator MoveBoat(int right)
    {
        if(right == 1)
        {
            water.flowMagnitude = -flowMag;
            yield return new WaitForSeconds(1f);
            water.flowMagnitude = 0;
        }
        else if(right == 2)
        {
            water.flowMagnitude = flowMag;
            yield return new WaitForSeconds(1f);
            water.flowMagnitude = 0;
        }
        else
        {
            water.flowMagnitude = 0;
        }
        yield return null;
    }
}
