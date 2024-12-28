using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation readyDot;
    [SerializeField] private TextMeshProUGUI overturnCount;
    [SerializeField] private TextMeshProUGUI clearturnCount;
    [SerializeField] private TextMeshProUGUI curTurn;
    [SerializeField] private GameObject over_obj;
    [SerializeField] private GameObject clear_obj;
    [SerializeField] private GameObject[] select;

    public void Init()
    {

    }

    public void SelectUI(int idx)
    {
        for (int i = 0; i < select.Length; i++)
            select[i].SetActive(false);

        select[idx].SetActive(true);
    }

    public void OverUI()
    {
        overturnCount.text = "SCORE : " + GameManager.Instance.Turn.TurnCount + " turn";
        over_obj.SetActive(true);
    }

    public void ClearUI()
    {
        clearturnCount.text = "SCORE : " + GameManager.Instance.Turn.TurnCount + " turn";
        clear_obj.SetActive(true);
    }

    public void TurnReady()
    {
        readyDot.DORestartById("Ready");
        curTurn.text = "Turn : " + GameManager.Instance.Turn.TurnCount;
    }

    public void TurnReadyOut()
    {
        GameManager.Instance.Turn.turnReady = true;
    }
}
