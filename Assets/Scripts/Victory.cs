using UnityEngine;

public class Victory : MonoBehaviour
{
    public bool IsVictory { get; private set; } = false;
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            IsVictory = true;
        }
    }
}