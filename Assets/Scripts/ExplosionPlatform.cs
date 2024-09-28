using System.Collections;
using UnityEngine;

public class ExplosionPlatform : MonoBehaviour
{
    [SerializeField] private Material triggerMaterial;
    [SerializeField] private Material explosionMaterial;
    [SerializeField] private float explosionTime;
    [SerializeField] private float prepareExplosionTime;
    [SerializeField] private float reloadTime;

    public float ReloadTime { get { return reloadTime; } }
    public bool IsTakenDamage { get; private set; } = false;

    private Renderer changeColor;
    private Material initialMaterial;
    private bool isReloaded = true;
    private bool dealDamage = false;
    
    void Start()
    {
        changeColor = gameObject.GetComponent<Renderer>();
        initialMaterial = changeColor.material;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && dealDamage && !IsTakenDamage)
        {
            IsTakenDamage = true;
        }
        else if (collision.gameObject.tag == "Player" && isReloaded)
        {
            changeColor.material = triggerMaterial;
            StartCoroutine(Explosion());
        }
    }

    public IEnumerator Explosion()
    {
        isReloaded = false;
        yield return new WaitForSeconds(prepareExplosionTime);
        changeColor.material = explosionMaterial;
        dealDamage = true;
        yield return new WaitForSeconds(explosionTime);
        changeColor.material = triggerMaterial;
        dealDamage = false;
        StartCoroutine(Reload());

    }

    public IEnumerator Reload()
    {
        IsTakenDamage = false;
        yield return new WaitForSeconds(reloadTime);
        changeColor.material = initialMaterial;
        isReloaded = true;        
    }
}