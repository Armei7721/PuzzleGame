using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intansSkelleton : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject skeletonPrefab;
    int randomX;
    Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            randomX = Random.Range(-5, 5);
            offset = new Vector3(randomX, 0, 0);
            GameObject skeleton = Instantiate(skeletonPrefab);

            // 플레이어의 Y축 위치를 가져와서 스켈레톤의 Y축 위치를 설정
           
            skeleton.transform.position = new Vector3(playerObj.transform.position.x + offset.x, -2.5f, playerObj.transform.position.z);
        }
    }
}
