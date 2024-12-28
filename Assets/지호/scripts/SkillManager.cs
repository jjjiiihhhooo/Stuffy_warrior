using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    //고기, 장애물 정지, 

    public NodeType type = NodeType.Normal;

    public int useCount;
    public int meatCount;

    public bool use = true;
    public bool[] skill;

    public bool all;

    public GameObject stun;
    public GameObject smoke;
    public GameObject meat;

    public void Init()
    {
        for(int i = 0; i < skill.Length; i++)
        {
            if (skill[i]) all = true;
        }
        SelectSkill(0);
    }

    public void CheckUse()
    {
        if (useCount <= 0)
            use = false;
    }

    public void SelectSkill(int i)
    {
        if (i == 0) type = NodeType.Normal;
        else if (i == 1) type = NodeType.Trigger;
        else if (i == 2) type = NodeType.Enemy;

        GameManager.Instance.UI.SelectUI(i);
    }

    public void UseSkill(Node n)
    {
        if (!use && !GameManager.Instance.Turn.turnReady) return;

        if (type == NodeType.Normal) Meat(n);
        else if (type == NodeType.Trigger) ObjectPause(n);
        else if (type == NodeType.Enemy) EnemyPause(n);
    }

    public void Meat(Node n)
    {
        if (meatCount > 0) return;
        useCount--;
        meatCount++;

        n.meat = GameObject.Instantiate(meat, n.transform);
        n.meat.transform.localPosition = new Vector3(0, 1f, -0.5f);
        n.meat.GetComponent<MeatTrigger>().pos = n.NodePos;
        n.type = NodeType.Item;
    }

    public void ObjectPause(Node n)
    {
        n.pause = true;
        useCount--;

        n.stun = GameObject.Instantiate(smoke, n.transform);
        n.stun.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void EnemyPause(Node n)
    {
        n.transform.parent.GetComponentInChildren<EnemyTrigger>().pause = true;
        useCount--;

        n.stun = GameObject.Instantiate(stun, n.transform);
        n.stun.transform.localPosition = new Vector3(0, 1f, 0);
    }

}
