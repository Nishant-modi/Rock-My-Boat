using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] GameObject sqPrefab;
    [SerializeField] GameObject water;
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] GameObject parent;
    //[SerializeField] GameObject gameManager;
    //public instantiateScript sn;
    private void Update()
    {
        //instantiateScript iS = new instantiateScript();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("button clicked");


            Vector3 pos = sqPrefab.transform.position;
            Debug.Log("" + pos);
            
            //sn.Updatescript(pos);

            HingeJoint2D joint = sqPrefab.GetComponent<HingeJoint2D>();
            joint.enabled = false;
            rb.freezeRotation = false;
        }


    }
}
