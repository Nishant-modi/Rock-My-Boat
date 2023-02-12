using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateScript : MonoBehaviour
{
    [SerializeField] GameObject newsq;
    [SerializeField] GameObject[] sq;
    [SerializeField] Rigidbody2D crane;
    [SerializeField] Rigidbody2D hook;
    [SerializeField] int maxCountGreen;
    [SerializeField] int maxCountPink;
    public int countBox = 0;

    public void Start()
    {
        StartCoroutine(tapCheck());
    }
    IEnumerator tapCheck()
    {
        int counterGreen = 0;
        int counterPink = 0;
        maxCountGreen++;
        maxCountPink++;
        //int bigsq = 0;
        while (true)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                int index = Random.Range(0, sq.Length);
                Debug.Log("random index = " + index);

                if (index == 1)
                {
                    counterGreen++;
                    Debug.Log("green box = "+counterGreen);
                    if (counterGreen >= maxCountGreen)
                    {
                        index = 0;
                    }
                }
                else if (index == 2)
                {
                    counterPink++;
                    Debug.Log("pink box = " + counterPink);
                    if (counterPink >= maxCountPink)
                    {
                        index = 0;
                        Debug.Log("pink to orange");
                    }
                }

                

               

                Debug.Log("random index after if = " + index);

                yield return new WaitForSeconds(0.5f);
                Vector3 oldsqPos = newsq.transform.position;
                Vector3 hookPos = hook.transform.position;
                
                GameObject g = Instantiate(sq[index], (Vector2)hookPos, Quaternion.identity, this.transform);
                HingeJoint2D newjoint = g.GetComponent<HingeJoint2D>();
                newjoint.connectedBody = hook;
                yield return new WaitForSeconds(0.6f);
                countBox++;
                //Debug.Log("box count = " + countBox);
            }
            yield return null;
        }
            
    }

}
