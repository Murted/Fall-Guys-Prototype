using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    [SerializeField] public Text timeScore;
    [SerializeField] private Vector2 victoryPosition; 
    public int timer { get; private set; } = 0;

    private Vector2 initialPosition;
    private bool isStarted = false;

    public void Start()
    {
        initialPosition = timeScore.rectTransform.anchoredPosition;
    }

    public IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timer++;
            timeScore.text = timer.ToString();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isStarted)
        {
            isStarted = true;
            StartCoroutine(Timer());
        }
    }

    public void TimerVictoryPosition()
    {
        timeScore.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        timeScore.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        timeScore.rectTransform.anchoredPosition = victoryPosition;
    }

    public void TimerInitialPosition()
    {
        timeScore.rectTransform.anchoredPosition = initialPosition;
    }
}