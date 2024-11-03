using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for using UI elements like Slider

public class HydraBoss : enemyHP
{
    public Transform player; 
    public float speed;
    private Animator animator;
    private int skillNumber = 0;
    
  
    public Slider healthSlider1;
    public Slider healthSlider2;
    public Slider healthSlider3;

    public GameObject[] hydra;
    public GameObject[] redzone;
    public GameObject[] hitblock;


    private const int maxHPPerSlider = 1000; 
    
    void Start()
    {
        redzone[0].SetActive(false);
         redzone[1].SetActive(false);
          redzone[2].SetActive(false);
        hitblock[0].SetActive(false);
        hitblock[1].SetActive(false);
        hitblock[2].SetActive(false);

        Dictionary<int, int> bgmSelections = new Dictionary<int, int>
        {
            { 0, 0 },  
            { 1, 1 }   
        };


        SoundManager.instance.PlayMultipleBGM(bgmSelections);

        speed = 0.8f;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        hp = 3000; 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameObject Player = GameObject.FindWithTag("Player");
        Playerscript = Player.GetComponent<player>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();

        healthSlider1.maxValue = maxHPPerSlider;
        healthSlider2.maxValue = maxHPPerSlider;
        healthSlider3.maxValue = maxHPPerSlider;

        healthSlider1.value = maxHPPerSlider;
        healthSlider2.value = maxHPPerSlider;
        healthSlider3.value = maxHPPerSlider;

          StartCoroutine(waitTime());
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }

        
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("hitblock") || collision.CompareTag("slash")) && !isDead && !isHit)
        {
            isHit = true; 

            int damageAmount = Playerscript.damage;  
            hp -= damageAmount;

            ShowDamagePopup(damageAmount);

            UpdateHealthSliders();

            if (hp <= 0)
            {
                isDead = true;
                StartCoroutine(Die());
            }

            StartCoroutine(ResetHit()); 
        }
    }

    private void UpdateHealthSliders()
    {
        int remainingHP = hp;

        healthSlider1.value = Mathf.Clamp(remainingHP, 0, maxHPPerSlider);
        remainingHP -= maxHPPerSlider;

        healthSlider2.value = Mathf.Clamp(remainingHP, 0, maxHPPerSlider);
        remainingHP -= maxHPPerSlider;

        healthSlider3.value = Mathf.Clamp(remainingHP, 0, maxHPPerSlider);
    }


IEnumerator waitTime()
{
    yield return new WaitForSeconds(15f);

      redzone[0].SetActive(false);
         redzone[1].SetActive(false);
          redzone[2].SetActive(false);
        hitblock[0].SetActive(false);
        hitblock[1].SetActive(false);
        hitblock[2].SetActive(false);

    
        Camera.main.orthographicSize = 2.409138f;


            Head1atk scriptToRemove = hydra[0].GetComponent<Head1atk>();
        if (scriptToRemove != null)
        {
            Destroy(scriptToRemove);
        }

             Head2atk scriptToRemove2 = hydra[1].GetComponent<Head2atk>();
        if (scriptToRemove2 != null)
        {
            Destroy(scriptToRemove2);
        }

              Head3atk scriptToRemove3 = hydra[2].GetComponent<Head3atk>();
        if (scriptToRemove3 != null)
        {
            Destroy(scriptToRemove3);
        }

         LaserSpawner scriptToRemove4 = gameObject.GetComponent<LaserSpawner>();
        if (scriptToRemove4 != null)
        {
            Destroy(scriptToRemove4);
        }

    StartCoroutine(coolDownskill());
}

    IEnumerator coolDownskill()
    {
        yield return new WaitForSeconds(5f);
       
        int patternSkill = Random.Range(1, 6);
        skillNumber = patternSkill;
        patternBoss();
        StartCoroutine(waitTime());

    }

     private void patternBoss()
    {
 
    
        switch(skillNumber)
        {
            case 1: 
            redzone[0].SetActive(true);
            hitblock[0].SetActive(true);
            hydra[0].AddComponent<Head1atk>();

            Debug.Log("1");
            break;

            case 2: 
            redzone[1].SetActive(true);
            hitblock[1].SetActive(true);
          hydra[1].AddComponent<Head2atk>();
          
             Debug.Log("2");
            break;

            case 3: 
            redzone[2].SetActive(true);
            hitblock[2].SetActive(true);
           hydra[2].AddComponent<Head3atk>();
             Debug.Log("3");
            break;

            case 4:
             redzone[0].SetActive(true);
            hitblock[0].SetActive(true);

             redzone[1].SetActive(true);
            hitblock[1].SetActive(true);

            redzone[2].SetActive(true);
            hitblock[2].SetActive(true);

            hydra[0].AddComponent<Head1atk>();
            hydra[1].AddComponent<Head2atk>();
             hydra[2].AddComponent<Head3atk>();
             Debug.Log("4");
            break;

            case 5:
            gameObject.AddComponent<LaserSpawner>();
            break;
        }
    }
}
