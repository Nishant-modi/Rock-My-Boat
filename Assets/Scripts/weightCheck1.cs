using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.PackageManager;


public class weightCheck1 : MonoBehaviour
{
    [SerializeField] GameObject boat;
    [SerializeField] BuoyancyEffector2D waterBuo;
    [SerializeField] GameObject water;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI boxText;
    [SerializeField] GameObject nextLevel;
    [SerializeField] float waterlevel;
    [SerializeField] float sinkAngle;
    [SerializeField] int stableAngle;
    [SerializeField] instantiateScript instantiated;
    //[SerializeField] boxInWater1 boxInWater1;
    [SerializeField] boxInWater1 boxInWater;
    [SerializeField] int endCount;
    [SerializeField] GameObject gameOver;
    [SerializeField] Image bar;
    [SerializeField] GameObject barImage;
    [SerializeField] Timer timer;
    [SerializeField] Animator boatAnim;
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
        //barImage.transform.position = new Vector3(1.855f, 3.48f, -1f);
        //StartCoroutine(angleCheck());
        while (true)
        {
            //Debug.Log("wait over");
            
            int finalCount = instantiated.countBox - boxInWater.droppedCount;
            Vector3 initialbarPos = new Vector3(0.6675f, 3.927f, -1f);
            Vector3 finalbarPos = new Vector3(1.989f,3.927f,-1f);
            float barLength = finalbarPos.x - initialbarPos.x;
            int x;
           
            barImage.transform.position = new Vector3(initialbarPos.x + (finalCount*(barLength/endCount)),initialbarPos.y,initialbarPos.z);
            //Debug.Log(finalCount + ":" + endCount);
            bar.fillAmount = (float) finalCount / endCount;
            // Debug.Log("Boxes on boat: " + finalCount);
            //Vector3 boatLevel = boat.transform.position;
            float boatAngle = boat.transform.rotation.eulerAngles.z;
            Vector3 waterPos = water.transform.position;
            Vector3 currWaterLevel = new Vector3(waterPos.x, waterPos.y + waterlevel, waterPos.z);
           // Debug.Log("boat angle: " + boatAngle);
            timeText.text = "";
            boatAnim.SetInteger("StableDegree", 0);

            if (finalCount>=endCount)
            {
                //Debug.Log("waiting");
                if((boatAngle>=0&&boatAngle<=stableAngle)||((boatAngle>=(360-stableAngle))&&(boatAngle<360)))
                {
                    timeText.fontSize = 100;
                    int ct = 4;
                    for (ct = 4; ct >= 0; ct--)
                    {
                        finalCount = instantiated.countBox - boxInWater.droppedCount;
                        if (finalCount > endCount)
                            finalCount = endCount;
                        //boxText.text = finalCount.ToString() + "/" + endCount.ToString();
                        bar.fillAmount = (float)finalCount / endCount;
                        if (finalCount >= endCount)
                        {
                            boatAngle = boat.transform.rotation.eulerAngles.z;
                            if ((boatAngle >= 0 && boatAngle <= 3) || (boatAngle >= 357 && boatAngle < 360))
                            {
                                if (ct == 4)
                                {
                                    timeText.text = "";
                                    boatAnim.SetInteger("StableDegree", 0);
                                    
                                }
                                    
                                else
                                {
                                    boatAnim.SetInteger("StableDegree", 1);
                                    timeText.text = ct.ToString();
                                }
                                    
                            }
                            else
                            {
                                yield return new WaitForSeconds(0.2f);
                                //timeText.fontSize = 80;
                                ct = 4;
                                boatAnim.SetInteger("StableDegree", 2);
                                timeText.text = "";
                                
                            }
                        }
                        else
                        {
                            timeText.text = "";
                            boatAnim.SetInteger("StableDegree", 0);
                            break;
                        }
                        yield return new WaitForSeconds(1f);
                    }
                    if (ct == -1)
                    {
                        StopCoroutine(timer.time);
                        nextLevel.SetActive(true);
                    }
                }
                else
                {
                    timeText.fontSize = 80;
                    boatAnim.SetInteger("StableDegree", 2);
                    timeText.text = "";
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
                StopCoroutine(timer.time);
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
