using UnityEngine;
using UnityEngine.UI;  

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public Slider healthSlider; 

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth; 
        healthSlider.value = currentHealth; 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player hit! Current health: " + currentHealth);

        healthSlider.value = currentHealth; 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
    }
}
