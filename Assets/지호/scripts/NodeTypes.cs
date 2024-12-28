using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class NodeTypes : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public NodeType type;
    public Paths[] paths;
    public string name;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!GameManager.Instance.Turn.turnReady) return;
        if (!GameManager.Instance.Skill.all) return;

        if (GameManager.Instance.Skill.type == type)
        {
            GameManager.Instance.Skill.UseSkill(transform.GetComponentInChildren<Node>());
        }
        else if (GameManager.Instance.Skill.type == NodeType.Trigger)
        {
            if (name == "Iron" || name == "Spike")
            {
                GameManager.Instance.Skill.UseSkill(transform.GetComponentInChildren<Node>());
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GameManager.Instance.Turn.turnReady) return;
        if (!GameManager.Instance.Skill.all) return;

        if (GameManager.Instance.Skill.type == type)
        {
            this.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
        else if(GameManager.Instance.Skill.type == NodeType.Trigger)
        {
            if (name == "Iron" || name == "Spike")
            {
                this.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!GameManager.Instance.Skill.all) return;
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        
    }
}
