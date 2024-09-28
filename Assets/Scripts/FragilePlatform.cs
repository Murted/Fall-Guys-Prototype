using System.Collections;
using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    [SerializeField] private Material triggeredMaterial;

    private Renderer changeColor;

    private void Start()
    {
        changeColor = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody != null)
        {
            changeColor.material = triggeredMaterial;
            StartCoroutine(Vanish());
        }
    }

    public IEnumerator Vanish()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}