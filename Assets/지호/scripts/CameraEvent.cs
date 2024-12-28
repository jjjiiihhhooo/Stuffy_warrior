using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Camera mainCamera; // 메인 카메라
    public float checkDistance = 10f; // 체크할 거리
    public LayerMask layer;
    public GameObject current; // 현재 가로막고 있는 오브젝트

    private void Update()
    {
        RayTest();
    }

    private void RayTest()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, checkDistance, layer))
        {
            if(current != hit.transform.gameObject)
            {
                if(current != null)
                    current.GetComponent<MeshRenderer>().enabled = true;
                current = hit.transform.gameObject;
                current.GetComponent<MeshRenderer>().enabled = false;
            }

        }
        else
        {
            if (current != null)
            {
                current.GetComponent<MeshRenderer>().enabled = true;
                current = null;
            }
        }
    }


}
