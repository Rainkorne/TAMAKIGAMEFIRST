using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class HealthInterface : MonoBehaviour
{ 
    public Image HealthBar; // Ссылка на Image для отображения полосы здоровья
    public float HP;
    public float MaxHP;
    public float DamageAmount;
    public float RestoreAmount;
    public float decreaseSpeed = 1f; // Скорость уменьшения и восстановления полоски здоровья
    private Coroutine damageCoroutine; // Ссылка на корутину уменьшения здоровья
    private Coroutine restoreCoroutine; // Ссылка на корутину восстановления здоровья
    private bool isTakingDamage = false; // Флаг, указывающий, получает ли объект урон в данный момент
    private bool isRestoringHealth = false; // Флаг, указывающий, восстанавливается ли здоровье в данный момент
    private float timeSinceLastDamage = 0f; // Время с последнего урона

    void Start()
    {
        UpdateHealthBar();
    }

    void Update()
    {
        // Если прошло более 3 секунд с последнего урона и здоровье не максимальное, начинаем восстанавливать здоровье
        if (!isTakingDamage && HP < MaxHP && !isRestoringHealth && timeSinceLastDamage >= 3f)
        {
            StartRestoreHealth();
        }

        // Обновляем время с момента последнего урона
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

        // Если корутина уменьшения здоровья уже запущена, останавливаем её
        if (damageCoroutine != null)
            StopCoroutine(damageCoroutine);

        // Запускаем корутину уменьшения здоровья
        damageCoroutine = StartCoroutine(DecreaseHealthSmoothly(damage));
    }

    void StartRestoreHealth()
    {
        // Если корутина восстановления здоровья уже запущена, останавливаем её
        if (restoreCoroutine != null)
            StopCoroutine(restoreCoroutine);

        // Запускаем корутину восстановления здоровья
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

        // Гарантируем, что здоровье точно установлено на целевое значение
        HP = targetHealth;
        if (HP < 0)
            HP = 0;

        UpdateHealthBar();

        // Обнуляем ссылку на корутину
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

        // Гарантируем, что здоровье точно установлено на целевое значение
        HP = targetHealth;
        if (HP > MaxHP)
            HP = MaxHP;

        UpdateHealthBar();

        // Обнуляем ссылку на корутину
        restoreCoroutine = null;
        isRestoringHealth = false;
    }

    void UpdateHealthBar()
    {
        HealthBar.fillAmount = HP / MaxHP;
    }
}