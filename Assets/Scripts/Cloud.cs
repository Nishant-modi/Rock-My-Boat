using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject cloud;
    void Start()
    {
        StartCoroutine(UpdateCloud());
    }

    // Update is called once per frame
    IEnumerator UpdateCloud()
    {
        bool up = false;
        while (true)
        {
            Vector3 pos = cloud.transform.position;
            if(up == true)
            {
                cloud.transform.position = new Vector3(pos.x, cloud.transform.position.y + 0.01f, pos.z);
                yield return new WaitForSeconds(1f);
                up = false;
            }
            else
            {
                cloud.transform.position = new Vector3(pos.x, cloud.transform.position.y - 0.01f, pos.z);
                yield return new WaitForSeconds(1f);
                up = true;
            }
            //pos = cloud.transform.position;
            //cloud.transform.position = new Vector3(pos.x, cloud.transform.position.y - 0.5f, pos.z);
        }
        yield return null;
    }
}
