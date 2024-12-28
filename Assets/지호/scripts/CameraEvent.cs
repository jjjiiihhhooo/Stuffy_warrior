using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public Camera mainCamera; // ���� ī�޶�
    public float checkDistance = 10f; // üũ�� �Ÿ�
    public LayerMask layer;
    public GameObject current; // ���� ���θ��� �ִ� ������Ʈ

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
