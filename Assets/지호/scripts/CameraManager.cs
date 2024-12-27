using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private CinemachineVirtualCamera[] cams;

    public CinemachineVirtualCamera[] Cams { get => cams; set => cams = value; }

    public bool CamMove;

    public void Init()
    {

    }

    private void RemovePriority()
    {
        for(int i = 0; i < cams.Length; i++)
        {
            cams[i].Priority = 10;
        }
    }

    public void SetPrioiry(int i)
    {
        RemovePriority();
        cams[i].Priority = 11;
    }
}
