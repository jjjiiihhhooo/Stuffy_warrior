using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    //고기, 장애물 정지, 

    public NodeType type = NodeType.Normal;
    
    public void SelectSkill(int i)
    {
        if (i == 0) Meat();
        else if (i == 1) ObjectPause();
        else if (i == 2) EnemyPause();

        GameManager.Instance.UI.SelectUI(i);
    }

    public void Meat()
    {
        type = NodeType.Normal;
    }

    public void ObjectPause()
    {
        type = NodeType.Trigger;
    }

    public void EnemyPause()
    {
        type = NodeType.Enemy;
    }

}
