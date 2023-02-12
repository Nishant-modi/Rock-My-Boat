using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

//using Unity.UI;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class boxInWater : MonoBehaviour
{
    [SerializeField] GameObject penaltyText;
    [SerializeField] Transform water;
    [SerializeField] GameObject boat;
    [SerializeField] GameObject lostLevel;
    [SerializeField] GameObject life1;
    [SerializeField] GameObject life2;
    [SerializeField] GameObject life3;
    [SerializeField] GameObject life4;
    [SerializeField] int reduceSize;
    public int droppedCount = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Box")
        {
            droppedCount++;
            if (droppedCount == 1)
                life1.SetActive(false);
            if (droppedCount == 2)
                life2.SetActive(false);
            if (droppedCount == 3)
                life3.SetActive(false);
            if (droppedCount >= 4)
            {
                life4.SetActive(false);
                lostLevel.SetActive(true);
            }
            
            StartCoroutine(boxDropped(collision));

        }
    }

    IEnumerator boxDropped(Collider2D col)
    {
        Vector3 dropPos = col.gameObject.transform.position;
        //Debug.Log("box dropped at : " + dropPos);
        GameObject g = Instantiate(penaltyText, (Vector2)dropPos, Quaternion.identity);
        Vector3 currBoat = boat.transform.localScale;
        //boat.transform.localScale = new Vector3(currBoat.x - reduceSize,currBoat.y,currBoat.z);
        //Debug.Log("boat dimension : " + currBoat);
        if (dropPos.x > 0)
        {
            dropPos.x = 2.1f;
            dropPos.y = 0f;
        }
        else
        {
            dropPos.x = -2.1f;
            dropPos.y = -0f;
        }
        g.transform.SetParent(water, false);
        g.transform.position = dropPos;
        yield return new WaitForSeconds(1.5f);
        Destroy(g);
        Destroy(col.gameObject);
    }
}


