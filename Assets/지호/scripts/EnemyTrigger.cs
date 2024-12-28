using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyTrigger : MonoBehaviour
{
    public Paths[] paths;
    public Vector2 curPos;
    public Node node;
    public DOTweenAnimation dot;

    public int count = 0;
    public bool isMove;
    public bool pause;
    public Animator anim;

    public void Action(Node n)
    {
        Debug.Log("Action");
        if(pause) 
        {
            pause = false;
            Destroy(n.stun);
            return; 
        }
        if (!isMove) return;
        isMove = false;
        int x = (int)curPos.x + paths[count].x;
        int y = (int)curPos.y + paths[count].y;

        Node t = GameManager.Instance.NodeManager.Nodess[x].node[y];

        n.type = NodeType.Normal;
        n.GetComponentInParent<NodeTypes>().type = NodeType.Normal;
        //t.type = NodeType.Enemy;

        transform.parent = t.transform.parent;

        transform.LookAt(t.transform);

        Vector3 dir = transform.eulerAngles;
        dir.y = 0;
        transform.eulerAngles = dir;

        anim.Play("Ready");
        

        //dot.DORestartById("move");

        count++;
        curPos.x = x;
        curPos.y = y;
        if (count >= paths.Length)
            count = 0;
    }

    public void SetNodeType()
    {
        transform.parent.GetComponentInChildren<Node>().type = NodeType.Enemy;
        transform.parent.GetComponent<NodeTypes>().type = NodeType.Enemy;
        isMove = true;
    }

    public void EnemyMove()
    {
        anim.Play("Move");
        StopCoroutine(EnemyMoveCor());
        StartCoroutine(EnemyMoveCor());
    }

    private IEnumerator EnemyMoveCor()
    {
        while (Vector3.Distance(transform.localPosition, Vector3.zero) > 0.3f)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, Vector3.zero, 0.05f);
            //transform.position = Vector3.MoveTowards(transform.position, target, 0.8f);
            yield return new WaitForFixedUpdate();
        }
        anim.Play("Stop");

        transform.localPosition = Vector3.zero;

        SetNodeType();

        Debug.Log("Cor");
    }
}
