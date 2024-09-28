using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPlatform : MonoBehaviour
{
    [SerializeField] private float windPower;
    [SerializeField] private List<Vector3> windDirections;
    [SerializeField] private float activeWindTime;

    private Vector3 currentWindDirection;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            collision.rigidbody.AddForce(currentWindDirection.normalized * windPower, ForceMode.Force);
        }
    }

    public IEnumerator WindDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(activeWindTime);
            int randomIndex = Random.Range(0, windDirections.Count);
            currentWindDirection = windDirections[randomIndex];
        }
    }
}