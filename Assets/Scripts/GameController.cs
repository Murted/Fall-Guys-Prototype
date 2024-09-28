using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private CharacterMove player;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private float amountOfDamage;    
    [SerializeField] private HealthBar healthBar;    
    [SerializeField] private Text defeatText;
    [SerializeField] private Victory victory;
    [SerializeField] private Text victoryText;
    [SerializeField] private Button restart;
    [SerializeField] private float yThreshold;
    [SerializeField] private StartTimer timer;

    private HitPlatform[] hitPlatform;
    private ExplosionPlatform[] explosionPlatforms;
    private WindPlatform[] windPlatform;
    private bool dealDamage = true;

    public void Start()
    {
        Time.timeScale = 1.0f;
        timer.timeScore.text = timer.timer.ToString();
        hitPlatform = FindObjectsOfType<HitPlatform>();
        explosionPlatforms = FindObjectsOfType<ExplosionPlatform>();
        windPlatform = FindObjectsOfType<WindPlatform>();
        foreach (var wind in windPlatform) 
        {
            StartCoroutine(wind.WindDirection());
        }
    }

    public void FixedUpdate()
    {
        player.MovePlayer();

        foreach (var platform in explosionPlatforms)
        {
            if (platform.IsTakenDamage && dealDamage)
            {
                healthBar.TakeDamage(amountOfDamage);
                dealDamage = false;
                StartCoroutine(TakeDamageReload());
                if(healthBar.CurrentHealth <= 0)
                {
                    Defeat();
                }
            }
        }

        if(player.gameObject.transform.position.y <= yThreshold)
        {
            Defeat();
        }
    }

    public void Update()
    {
        player.KeyboardInput();
        player.UpdateAnimationState();
        player.CheckFallCondition();

        cameraMove.Move();

        foreach (var hit in hitPlatform)
        {
            hit.PlatformMove();
        }

        if(victory.IsVictory)
        {
            Victory();
        }
    }

    public IEnumerator TakeDamageReload()
    {
        yield return new WaitForSeconds(explosionPlatforms[0].ReloadTime);
        dealDamage = true;
    }

    private void Defeat()
    {
        Time.timeScale = 0f;
        defeatText.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        StopAllCoroutines();
    }

    private void Victory()
    {
        Time.timeScale = 0f;
        victoryText.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        timer.TimerVictoryPosition();
        StopAllCoroutines();
    }
}