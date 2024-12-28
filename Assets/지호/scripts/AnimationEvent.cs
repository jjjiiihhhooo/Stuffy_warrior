using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void ReadyExit()
    {
        if (Player.Instance.isDestroyMeat)
        {
            Player.Instance.isDestroyMeat = false;
            FindObjectOfType<MeatTrigger>().DestroyCall();
        }

        Player.Instance.Move();
    }

    public void CollisionExit()
    {
        if (Player.Instance.isfake)
        {
            Player.Instance.isfake = false;
            Player.Instance.fakeWall.GetComponent<wallTrigger>().Change();

            GameManager.Instance.TurnEnd();
        }
        else
        {
            GameManager.Instance.PlayerEnd();
        }
    }

    public void MeatExit()
    {
        Player p = Player.Instance;
        MeatTrigger m = FindObjectOfType<MeatTrigger>();

        if(p.dir == Dir.Left)
        {
            if (p.playerPos.x > m.pos.x)
            {
                p.dir = Dir.Front;
                p.ReRotate();
            }
            else
            {
                GameManager.Instance.PlayerEnd();
            }
        }
        else if(p.dir == Dir.Front)
        {
            if (p.playerPos.y < m.pos.y)
            {
                p.dir = Dir.Right;
                p.ReRotate();
            }
            else
            {
                GameManager.Instance.PlayerEnd();
            }
        }
        else if (p.dir == Dir.Right)
        {
            if (p.playerPos.x < m.pos.x)
            {
                p.dir = Dir.Back;
                p.ReRotate();
            }
            else
            {
                GameManager.Instance.PlayerEnd();
            }
        }
        else if (p.dir == Dir.Back)
        {
            if (p.playerPos.y > m.pos.y)
            {
                p.dir = Dir.Left;
                p.ReRotate();
            }
            else
            {
                GameManager.Instance.PlayerEnd();
            }
        }

    }




    public void EnemyReadyExit()
    {
        transform.GetComponentInParent<EnemyTrigger>().EnemyMove();
    }

}
