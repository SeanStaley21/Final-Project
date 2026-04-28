using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private TextMeshProUGUI ammoText;
    private TextMeshProUGUI healthText;

    private PlayerShoot playerShoot;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerShoot = FindFirstObjectByType<PlayerShoot>();
        playerHealth = FindFirstObjectByType<PlayerHealth>();

        ammoText = GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>();
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (ammoText != null && playerShoot != null)
            ammoText.text = $"Ammo: {playerShoot.currentAmmo}/{playerShoot.maxAmmo}";

        if (healthText != null && playerHealth != null)
            healthText.text = $"HP: {playerHealth.currentHealth}";
    }

    public void Button_Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}