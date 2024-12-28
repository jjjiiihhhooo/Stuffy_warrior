using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (GameManager.Instance.Skill.skill[0])
                GameManager.Instance.Skill.SelectSkill(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (GameManager.Instance.Skill.skill[1])
                GameManager.Instance.Skill.SelectSkill(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (GameManager.Instance.Skill.skill[2])
                GameManager.Instance.Skill.SelectSkill(2);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.Instance.Turn.turnReady)
                GameManager.Instance.TurnStart();
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.Instance.UI.TurnReady();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
