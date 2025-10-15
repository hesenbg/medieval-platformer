using UnityEngine;

/// <summary>
/// Base class for all enemy types with common functionality like health, damage, and death
/// </summary>
public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float damagePushVelocity = 1f;
    
    [Header("References")]
    protected Rigidbody2D rb;
    protected SoundManager soundManager;
    
    // Events
    public System.Action<float> OnHealthChanged;
    public System.Action OnDeath;
    public System.Action<float> OnDamageTaken;
    
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
    
    protected virtual void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SM")?.GetComponent<SoundManager>();
    }
    
    /// <summary>
    /// Apply damage to the enemy
    /// </summary>
    /// <param name="damage">Amount of damage to apply</param>
    /// <param name="damageSource">Source of the damage (for push direction)</param>
    public virtual void TakeDamage(float damage, Transform damageSource = null)
    {
        if (currentHealth <= 0) return;
        
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
        OnDamageTaken?.Invoke(damage);
        
        // Apply push force if damage source is provided
        if (damageSource != null && rb != null)
        {
            float pushDirection = Mathf.Sign(damageSource.position.x - transform.position.x);
            rb.linearVelocityX = damagePushVelocity * pushDirection;
        }
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    /// <summary>
    /// Heal the enemy
    /// </summary>
    /// <param name="healAmount">Amount to heal</param>
    public virtual void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(maxHealth, currentHealth);
        OnHealthChanged?.Invoke(currentHealth / maxHealth);
    }
    
    /// <summary>
    /// Handle enemy death
    /// </summary>
    protected virtual void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Check if enemy is alive
    /// </summary>
    public bool IsAlive => currentHealth > 0;
    
    /// <summary>
    /// Get current health percentage (0-1)
    /// </summary>
    public float HealthPercentage => currentHealth / maxHealth;
    
    /// <summary>
    /// Get current health
    /// </summary>
    public float CurrentHealth => currentHealth;
    
    /// <summary>
    /// Get max health
    /// </summary>
    public float MaxHealth => maxHealth;
}
