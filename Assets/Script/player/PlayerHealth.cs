using UnityEngine;
using UnityEngine.UI;  
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] private int currentHealth;
    public Slider healthSlider; 

    void Start()
    {
        maxHealth = 100;
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
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Player died!");
    }
}
