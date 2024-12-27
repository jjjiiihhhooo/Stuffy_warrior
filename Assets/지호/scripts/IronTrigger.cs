using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronTrigger : Node
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
        if (Player.Instance.playerPos == NodePos) return;

        if (pause)
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
            type = NodeType.Wall;
        }

        isUp = !isUp;
    }

}
