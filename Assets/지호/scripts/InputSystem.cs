using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{

    public void Init()
    {

    }

    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.Skill.SelectSkill(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Instance.Skill.SelectSkill(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.Instance.Skill.SelectSkill(2);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if(GameManager.Instance.Turn.turnReady)
                GameManager.Instance.TurnStart();
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.Instance.UI.TurnReady();
        }
    }
}
