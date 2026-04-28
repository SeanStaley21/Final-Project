using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public AudioSource deathSound;


    void Start() => currentHealth = maxHealth;

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Player HP: {currentHealth}");
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        Debug.Log("Player died! Game over.");
        if (deathSound != null) deathSound.Play();
        GameManager.Instance.PlayerDied();
    }
}