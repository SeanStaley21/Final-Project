using UnityEngine;
public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private bool isDead = false; // ADD THIS
    public AudioSource deathSound;

    void Start() => currentHealth = maxHealth;

    public void TakeDamage(float amount)
    {
        if (isDead) return; // ADD THIS
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        GameManager.Instance.EnemyKilled();
        if (deathSound != null)
        {
            deathSound.transform.parent = null; // detach so it doesn't get destroyed
            deathSound.Play();
            Destroy(deathSound.gameObject, deathSound.clip.length);
        }
        Destroy(gameObject);
    }
}