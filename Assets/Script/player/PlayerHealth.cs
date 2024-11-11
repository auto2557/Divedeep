using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Slider healthSlider;
    public GameObject lowHP;
    public GameObject healEffect;

    private bool isRegenerating = false; // ตัวแปรสถานะสำหรับเช็คว่ากำลังฟื้นฟูอยู่หรือไม่

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        if (currentHealth <= 20)
        {
            lowHP.SetActive(true);
        }
        else
        {
            lowHP.SetActive(false);
        }

        if (currentHealth < maxHealth && !isRegenerating)
        {
            StartCoroutine(regen());
        }
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("savePoint") && currentHealth < maxHealth)
        {
            Instantiate(healEffect, transform.position, transform.rotation);
            currentHealth = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Player died!");
    }

    IEnumerator regen()
    {
        isRegenerating = true;
        yield return new WaitForSeconds(20f);

        if (currentHealth < maxHealth)
        {
            currentHealth += 3;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
            healthSlider.value = currentHealth;
            Instantiate(healEffect, transform.position, transform.rotation);
        }

        isRegenerating = false; 
    }
}
