using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : Node
{
    public bool isUp;

    public override void Init()
    {
        isUp = false;
        anim.Play("Action2");
        type = NodeType.Normal;
    }

    public override void Action()
    {
        if(pause)
        {
            pause = false;
            return;
        }

        if (isUp)
        {
            anim.Play("Action2");
            type = NodeType.Normal;
        }
        else
        {
            anim.Play("Action");
            type = NodeType.Trigger;
            if (NodePos == Player.Instance.playerPos) Damage();
        }

        isUp = !isUp;
    }

    public override void Damage()
    {
        GameManager.Instance.GameOver();
    }

}
