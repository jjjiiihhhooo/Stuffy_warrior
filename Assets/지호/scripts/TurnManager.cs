using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private int turnCount;

    public bool turnReady = false;
    public int TurnCount { get => turnCount; }


    public void Init()
    {

    }

    public void AddCount()
    {
        turnCount++;
    }

    private void TurnAction()
    {

    }

}
