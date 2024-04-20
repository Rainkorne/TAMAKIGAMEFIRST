using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPDAMAGEEN : MonoBehaviour
{ 
    public Image HealthBar; // Reference to the Image for displaying the health bar
    public float HP;
    public float MaxHP;
    public float DamageAmount;
    public float RestoreAmount;
    public float decreaseSpeed = 1f; // Speed of health bar decrease and restoration
    private Coroutine damageCoroutine; // Reference to the damage coroutine
    private Coroutine restoreCoroutine; // Reference to the restore coroutine
    private bool isTakingDamage = false; // Flag indicating whether the object is taking damage
    private bool isRestoringHealth = false; // Flag indicating whether health is being restored
    private float timeSinceLastDamage = 0f; // Time since last damage

    void Start()
    {
        UpdateHealthBar();
    }

    void Update()
    {
        // If more than 3 seconds have passed since the last damage and health is not maximum, start restoring health
        if (!isTakingDamage && HP < MaxHP && !isRestoringHealth && timeSinceLastDamage >= 3f)
        {
            StartRestoreHealth();
        }

        // Update time since last damage
        if (!isTakingDamage)
        {
            timeSinceLastDamage += Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FIRESHOOT"))
        {
            TakeDamage(DamageAmount);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("FIRESHOOT"))
        {
            TakeDamage(DamageAmount);
        }
    }

    void TakeDamage(float damage)
    {
        isTakingDamage = true;
        timeSinceLastDamage = 0f;

        // If damage coroutine is already running, stop it
        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);

        // Start damage coroutine
        damageCoroutine = StartCoroutine(DecreaseHealthSmoothly(damage));
    }

    void StartRestoreHealth()
    {
        // If restore coroutine is already running, stop it
        if (restoreCoroutine != null)
            StopCoroutine(restoreCoroutine);

        // Start restore coroutine
        restoreCoroutine = StartCoroutine(RestoreHealthSmoothly());
    }

    IEnumerator DecreaseHealthSmoothly(float damage)
    {
        float targetHealth = HP - damage;
        float initialHealth = HP;
        float elapsedTime = 0f;

        while (elapsedTime < decreaseSpeed)
        {
            HP = Mathf.Lerp(initialHealth, targetHealth, elapsedTime / decreaseSpeed);
            UpdateHealthBar();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure health is set to the target value
        HP = targetHealth;
        if (HP < 0)
            HP = 0;

        UpdateHealthBar();

        // Reset coroutine reference
        damageCoroutine = null;
        isTakingDamage = false;
    }

    IEnumerator RestoreHealthSmoothly()
    {
        isRestoringHealth = true;
        float targetHealth = HP + RestoreAmount;
        float initialHealth = HP;
        float elapsedTime = 0f;

        while (elapsedTime < decreaseSpeed)
        {
            HP = Mathf.Lerp(initialHealth, targetHealth, elapsedTime / decreaseSpeed);
            UpdateHealthBar();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure health is set to the target value
        HP = targetHealth;
        if (HP > MaxHP)
            HP = MaxHP;

        UpdateHealthBar();

        // Reset coroutine reference
        restoreCoroutine = null;
        isRestoringHealth = false;
    }

    void UpdateHealthBar()
    {
        HealthBar.fillAmount = HP / MaxHP;
    }
}