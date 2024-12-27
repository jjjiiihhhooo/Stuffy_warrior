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

    public void Action(Node n)
    {
        Debug.Log("Action");
        if (!isMove) return;
        isMove = false;
        int x = (int)curPos.x + paths[count].x;
        int y = (int)curPos.y + paths[count].y;

        Node t = GameManager.Instance.NodeManager.Nodess[x].node[y];

        n.type = NodeType.Normal;
        //t.type = NodeType.Enemy;

        transform.parent = t.transform.parent;

        StopCoroutine(EnemyMoveCor());
        StartCoroutine(EnemyMoveCor());

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
        isMove = true;
    }

    private IEnumerator EnemyMoveCor()
    {
        while (Vector3.Distance(transform.localPosition, Vector3.zero) > 0.3f)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, Vector3.zero, 0.05f);
            //transform.position = Vector3.MoveTowards(transform.position, target, 0.8f);
            yield return new WaitForFixedUpdate();
        }

        transform.localPosition = Vector3.zero;
        SetNodeType();
        Debug.Log("Cor");
    }
}
