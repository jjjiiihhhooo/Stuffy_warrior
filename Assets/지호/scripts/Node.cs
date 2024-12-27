using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum NodeType
{
    Start, End, Trigger, Item, Normal, Wall, Enemy
}

[System.Serializable]
public class Node : MonoBehaviour
{
    public NodeType type;
    public Animator anim;
    public UnityEvent _event;

    public Vector2 NodePos;

    public int turnCount;

    public bool pause;

    private void Awake()
    {        
        Init();
    }

    public virtual void Init()
    {

    }

    //public void CountCheck()
    //{
    //    if(GameManager.Instance.Turn.TurnCount % turnCount == 0)
    //    {
    //        Action();
    //    }
    //}

    public virtual void Action()
    {
        if(type == NodeType.Enemy)
        {
            transform.parent.GetComponentInChildren<EnemyTrigger>().Action(this);
        }
    }

    public virtual void Damage()
    {
        if (type == NodeType.Enemy)
        {
            GameManager.Instance.GameOver();
        }
    }
}
