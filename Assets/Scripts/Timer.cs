using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField] private Image timerImage;
    public int duration;
    private int remDuration;
    [SerializeField] GameObject timeEndPanel;
    [SerializeField] TextMeshProUGUI timerText;
    public Coroutine time;

    public void Start()
    {
        Begin(duration);
    }

    public void Begin(int Sec)
    {
        remDuration = Sec;
        time = StartCoroutine(updateTimer());
    }

    IEnumerator updateTimer()
    {
        while(remDuration >= 0)
        {
            //timerImage.fillAmount = Mathf.InverseLerp(0, duration, remDuration);
            timerText.text = remDuration.ToString();
            remDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
        yield return null;
    }

    private void OnEnd()
    {
        timeEndPanel.SetActive(true);
        StopAllCoroutines();
        StopCoroutine(time);

    }
}
