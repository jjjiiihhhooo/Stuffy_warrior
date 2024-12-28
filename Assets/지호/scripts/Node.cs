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

    public GameObject stun;
    public GameObject meat;

    public Vector2 NodePos;

    public bool meatArea;

    public int turnCount;

    public bool isfake;
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
        if (type == NodeType.Enemy)
        {
            transform.parent.GetComponentInChildren<EnemyTrigger>().Action(this);
        }
        else if (type == NodeType.Item)
        {
            transform.GetComponentInChildren<MeatTrigger>().Setting();
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
