using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWall : MonoBehaviour
{
    public bool isCheck;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player") && !isCheck)
        {
            isCheck = true;
            Call();
        }
    }

    public void Call()
    {
        Destroy(this.transform.GetChild(0).gameObject);
    }
}
