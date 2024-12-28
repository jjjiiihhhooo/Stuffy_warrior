using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatTrigger : MonoBehaviour
{
    public Vector2 pos;
    private void Start()
    {
        Setting();
    }

    public void Setting()
    {
        Debug.Log("Setting");

        int x = (int)pos.x;
        int y = (int)pos.y;
        
        for(int i = 0; i < 2; i++)
        {
            x--;
            y--;

            if (x >= 0) 
            { 
                GameManager.Instance.NodeManager.Nodess[x].node[(int)pos.y].meatArea = true; 
                Debug.Log("x = " + x + " y = " + (int)pos.y); 
            }
            if(y >= 0)
            {
                GameManager.Instance.NodeManager.Nodess[(int)pos.x].node[y].meatArea = true; 
                Debug.Log("x = " + (int)pos.x + " y = " + y);
            }
        }

        x = (int)pos.x;
        y = (int)pos.y;

        for (int i = 0; i < 2; i++)
        {
            x++;
            y++;

            if (x < 6) 
            { 
                GameManager.Instance.NodeManager.Nodess[x].node[(int)pos.y].meatArea = true;
                Debug.Log("x = " + x + " y = " + (int)pos.y);
            }
            if (y < 6) 
            { 
                GameManager.Instance.NodeManager.Nodess[(int)pos.x].node[y].meatArea = true;
                Debug.Log("x = " + (int)pos.x + " y = " + y);
            }
        }
    }

    public void DeSetting()
    {
        GameManager.Instance.Skill.meatCount--;
        int x = (int)pos.x;
        int y = (int)pos.y;

        for (int i = 0; i < 2; i++)
        {
            x--;
            y--;

            if (x >= 0)
            {
                GameManager.Instance.NodeManager.Nodess[x].node[(int)pos.y].meatArea = false;
                Debug.Log("x = " + x + " y = " + (int)pos.y);
            }
            if (y >= 0)
            {
                GameManager.Instance.NodeManager.Nodess[(int)pos.x].node[y].meatArea = false;
                Debug.Log("x = " + (int)pos.x + " y = " + y);
            }
        }

        x = (int)pos.x;
        y = (int)pos.y;

        for (int i = 0; i < 2; i++)
        {
            x++;
            y++;

            if (x < 6)
            {
                GameManager.Instance.NodeManager.Nodess[x].node[(int)pos.y].meatArea = false;
                Debug.Log("x = " + x + " y = " + (int)pos.y);
            }
            if (y < 6)
            {
                GameManager.Instance.NodeManager.Nodess[(int)pos.x].node[y].meatArea = false;
                Debug.Log("x = " + (int)pos.x + " y = " + y);
            }
        }

    }

    public void DestroyCall()
    {
        GameManager.Instance.Skill.meatCount--;
        DeSetting();
        StartCoroutine(DestroyCor());
    }

    private IEnumerator DestroyCor()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
