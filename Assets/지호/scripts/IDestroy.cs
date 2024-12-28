using UnityEngine;

public class IDestroy : MonoBehaviour
{
    public float time;

    private float t;

    private void OnEnable()
    {
        t = time;
    }

    private void Update()
    {
        if (t > 0)
            t -= Time.deltaTime;
        else this.gameObject.SetActive(false);
    }
}
