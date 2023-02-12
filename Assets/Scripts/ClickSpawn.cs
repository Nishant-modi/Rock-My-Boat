using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawn : MonoBehaviour
{
    [SerializeField] GameObject sqPrefab;
    private void Update()
    {
        if( Input.GetMouseButtonDown(0) )
        {
            Vector3 pos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
            GameObject g = Instantiate(sqPrefab,(Vector2)worldPos,Quaternion.identity);
            
        }
    }
}
