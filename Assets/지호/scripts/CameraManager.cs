using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private CinemachineVirtualCamera[] cams;

    CinemachineBasicMultiChannelPerlin noise;

    public CinemachineVirtualCamera[] Cams { get => cams; set => cams = value; }

    public bool CamMove;

    public void Init()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9)) Shake(0.5f);
    }

    public void Shake(float time)
    {
        StartCoroutine(CamCor(time));
    }


    private IEnumerator CamCor(float time)
    {
        if(noise == null) noise = cams[1].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 5f;
        noise.m_FrequencyGain = 1f;
        yield return new WaitForSeconds(time);
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;

    }
    private void RemovePriority()
    {
        for (int i = 0; i < cams.Length; i++)
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
