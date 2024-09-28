using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform healthBarFill;
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth { get; private set; }

    private float initialWidth;

    void Start()
    {
        CurrentHealth = maxHealth;
        initialWidth = healthBarFill.sizeDelta.x;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float width = (CurrentHealth / maxHealth) * initialWidth;
        healthBarFill.sizeDelta = new Vector2(width, healthBarFill.sizeDelta.y);
    }
}