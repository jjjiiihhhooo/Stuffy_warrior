using DG.Tweening;
using System.Collections;
using UnityEngine;

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
    Node t;

    int x;
    int y;
    public void Action(Node n)
    {
        Debug.Log("Action");
        if (pause)
        {
            pause = false;
            Destroy(n.stun);
            return;
        }
        if (!isMove) return;
        isMove = false;
        x = (int)curPos.x + paths[count].x;
        y = (int)curPos.y + paths[count].y;

        t = GameManager.Instance.NodeManager.Nodess[x].node[y];

        n.type = NodeType.Normal;
        n.GetComponentInParent<NodeTypes>().type = NodeType.Normal;
        //t.type = NodeType.Enemy;


        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
        transform.forward = t.transform.position - transform.position;
        transform.parent = t.transform.parent;

        

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
