using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;
using TMPro;
//using UnityEditor.PackageManager;


public class weightCheck : MonoBehaviour
{
    [SerializeField] GameObject boat;
    [SerializeField] BuoyancyEffector2D waterBuo;
    [SerializeField] GameObject water;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI boxText;
    [SerializeField] GameObject nextLevel;
    [SerializeField] float waterlevel;
    [SerializeField] float sinkAngle;
    [SerializeField] instantiateScript instantiated;
    //[SerializeField] boxInWater1 boxInWater1;
    [SerializeField] boxInWater boxInWater;
    [SerializeField] int endCount;
    [SerializeField] GameObject gameOver;
    IEnumerator temp;
    float currTime = 0f;
    float startTime = 3f;
    
    //bool coroutineReady = true;

    // Update is called once per frame

    private void Start()
    {
        temp = winTimer();
        StartCoroutine(temp);
        StartCoroutine(angleCheck());
    }

    IEnumerator winTimer()
    {
        //StartCoroutine(angleCheck());
        while (true)
        {
            //Debug.Log("wait over");
            int finalCount = instantiated.countBox - boxInWater.droppedCount;
            boxText.text = finalCount.ToString() + "/" + endCount.ToString();
            Debug.Log("Boxes on boat: " + finalCount);
            //Vector3 boatLevel = boat.transform.position;
            float boatAngle = boat.transform.rotation.eulerAngles.z;
            Vector3 waterPos = water.transform.position;
            Vector3 currWaterLevel = new Vector3(waterPos.x, waterPos.y + waterlevel, waterPos.z);
           // Debug.Log("boat angle: " + boatAngle);
            timeText.text = "";

            if (finalCount>=endCount)
            {
                //Debug.Log("waiting");
                if((boatAngle>=0&&boatAngle<=3)||(boatAngle>=357&&boatAngle<360))
                {
                    timeText.fontSize = 100;
                    int ct = 6;
                    for (ct = 6; ct >= 0; ct--)
                    {
                        finalCount = instantiated.countBox - boxInWater.droppedCount;
                        boxText.text = finalCount.ToString() + "/" + endCount.ToString();
                        if (finalCount >= endCount)
                        {
                            boatAngle = boat.transform.rotation.eulerAngles.z;
                            if ((boatAngle >= 0 && boatAngle <= 3) || (boatAngle >= 357 && boatAngle < 360))
                            {
                                if (ct == 6)
                                    timeText.text = null;
                                else
                                    timeText.text = ct.ToString();
                            }
                            else
                            {
                                yield return new WaitForSeconds(1f);
                                timeText.fontSize = 80;
                                timeText.text = "Stabilize the boat!";
                                ct = 6;
                            }
                        }
                        else
                        {
                            timeText.text = null;
                            break;
                        }
                        yield return new WaitForSeconds(1f);
                    }
                    if (ct == -1)
                    {
                        nextLevel.SetActive(true);
                    }
                }
                else
                {
                    timeText.fontSize = 80;
                    timeText.text = "Stabilize the boat!";
                }

            }

            yield return null;  
        }

    }

    IEnumerator angleCheck()
    {
        while(true)
        {
            int boatAngle = (int)boat.transform.rotation.eulerAngles.z;
            int angle = (int)sinkAngle;
            if ((boatAngle>=0 && boatAngle <= angle) || ((boatAngle >= 360-angle) && boatAngle<360))
            {
                yield return null;
            }
            else
            {
                waterBuo.density = 0;
                StopCoroutine(temp);
                yield return new WaitForSeconds(3f);
                gameOver.SetActive(true);
                Debug.Log("boat sink");
            }
            yield return null;
        }
        //yield return null;


    }

    void timerStart()
    {
        currTime = startTime;
        int ct = (int)currTime;
        while (ct>0)
        {
            ct -= 1 * (int)Time.deltaTime;
            timeText.text = ct.ToString();
        }
        
    }
}
