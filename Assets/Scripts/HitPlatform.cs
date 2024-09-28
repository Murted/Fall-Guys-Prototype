using System.Collections;
using UnityEngine;

public class HitPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 targetPlatform;
    [SerializeField] private float platformSpeed = 2f;
    [SerializeField] private float waitTime = 1f;

    private Vector3 startPosition;
    private bool movingToTarget = true;
    private bool waiting = false;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    public void PlatformMove()
    {
        if (!waiting)
        {
            if (movingToTarget)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPlatform, platformSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.localPosition, targetPlatform) < 0.01f)
                {
                    StartCoroutine(WaitBeforeReturn());
                    movingToTarget = false;
                }
            }
            else
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPosition, platformSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.localPosition, startPosition) < 0.01f)
                {
                    StartCoroutine(WaitBeforeReturn());
                    movingToTarget = true;
                }
            }
        }
    }

    private IEnumerator WaitBeforeReturn()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        waiting = false;
    }
}