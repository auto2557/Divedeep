using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class HydraBoss : enemyHP
{
    public Transform player; 
    private player playerscript;
    public float speed;
    private Animator animator;
    public Animator[] part;
    private int skillNumber = 0;
    
    public Slider healthSlider;

    public GameObject Dimension;
    public GameObject[] head;
    public GameObject[] hydra;
    public GameObject[] redzone;
    public GameObject[] hitblock;
    public GameObject ULTboss;
    public GameObject[] spawnMissile;

    public bool head1 = true;
    public bool head2 = true;
    private bool head3 = true;

    private  int maxHP = 4000;

    public float waitTimes;
     private bool isMoving = false;
      private bool hasSwitchedBGM = false;

      private bool phase1;
      private bool phase2;
      public GameObject LightPhase2;

    void Start()
{
    phase1 = true;
    phase2 = false;

    isMoving = false;
    StartCoroutine(startFight());

    Dimension.SetActive(false);
    waitTimes = 10f;

    redzone[0].SetActive(false);
    redzone[1].SetActive(false);
    redzone[2].SetActive(false);
    hitblock[0].SetActive(false);
    hitblock[1].SetActive(false);
    hitblock[2].SetActive(false);

    ULTboss.SetActive(false);

    Dictionary<int, int> bgmSelections = new Dictionary<int, int>
    {
        { 0, 1 },
        { 1, 2 }
    };

    SoundManager.instance.PlayMultipleBGM(bgmSelections);

    speed = 1.2f;

  
    GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
    if (playerObject != null)
    {
        player = playerObject.transform;
        playerscript = playerObject.GetComponent<player>();  
    }

   
    GameObject dashModeObject = GameObject.FindGameObjectWithTag("dashMode");
    if (dashModeObject != null)
    {
        player = dashModeObject.transform;  
        playerscript = playerObject.GetComponent<player>(); 
    }

    hp = maxHP;
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();

    GameObject hydraslider = GameObject.FindWithTag("hydra");
    healthSlider = hydraslider.GetComponent<Slider>();

    healthSlider.maxValue = maxHP;
    healthSlider.value = maxHP;

    StartCoroutine(waitTime());
}


    void Update()
    {
        if (isMoving && player != null) 
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }

       if (hp <= 2000 && hp>1000 && phase1 == true)
        {
      
            part[0].SetTrigger("die");
            head[0].SetActive(false);
            head1 = false;

            StartCoroutine(head1Die());
            spawnMissile[0].SetActive(true);
        
       
        }
        else if(hp <= 1000 && hp > 500 && phase1 == true)
        {
            head[1].SetActive(false);
            part[1].SetTrigger("die");

            head2 = false;
           StartCoroutine(head2Die());


        }
        else if ((hp <= 0 && phase1 == true) && !hasSwitchedBGM)
{
    LightPhase2.SetActive(true);
    phase1 = false;
    phase2 = true;
    maxHP = 1500;
    hp = maxHP;
    healthSlider.maxValue = maxHP;
    healthSlider.value = maxHP;
} 
    }

  public void UpdateHealthSlider()
{
    healthSlider.value = Mathf.Clamp(hp, 0, maxHP);
}

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("hitblock") || collision.CompareTag("slash")) && !isDead && !isHit)
        {
            isHit = true;

            int damageAmount = Playerscript.damage;
            hp -= damageAmount;

            ShowDamagePopup(damageAmount);

            UpdateHealthSlider();
            StartCoroutine(ResetHit());
        }
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(waitTimes);
       
        waitTimes = 10f;

        Dimension.SetActive(false);
        isMoving = true;

        redzone[0].SetActive(false);
        redzone[1].SetActive(false);
        redzone[2].SetActive(false);
        hitblock[0].SetActive(false);
        hitblock[1].SetActive(false);
        hitblock[2].SetActive(false);

        part[0].SetBool("ult",false);
         part[1].SetBool("ult",false);
          part[2].SetBool("ult",false);
           part[3].SetBool("ult",false);
            part[4].SetBool("ult",false);

         ULTboss.SetActive(false);

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

         if (phase1)
    {
      
        if (hp > 3000 && head1 && head2 && head3)
        {
               int patternSkill = Random.Range(1, 5);
            skillNumber = patternSkill;
        }
        else if (hp <= 3000 && hp > 2000 && head1 && head2 && head3)
        {
               int patternSkill = Random.Range(1, 6);
            skillNumber = patternSkill;
        }
        else if(hp<= 2000 && hp > 1000 && head1 == false && head2 == true && head3 == true)
        {
                int patternSkill = Random.Range(2, 7);
             skillNumber = patternSkill;
        }       
        else if(hp < 1000 && head1 == false && head2 == false && head3 == true)
        {
               int patternSkill = Random.Range(4, 7);
            skillNumber = patternSkill;
        }

          else if (phase2)
    {
       
        if (hp <= 1500 && head1 == false && head2 == false)
        {
               int patternSkill = Random.Range(4, 7); 
            skillNumber = patternSkill;
            speed = 15f;
            waitTimes = 6f;
        }
        
    }
    }
        
        patternBoss();
        StartCoroutine(waitTime());
    }

    private void patternBoss()
{
    switch (skillNumber)
    {
        case 1:
            isMoving = false;
            if (head1)
            {
                redzone[0].SetActive(true);
                hitblock[0].SetActive(true);
                hydra[0].AddComponent<Head1atk>();
                Debug.Log("1");
            }
            break;

        case 2:
            isMoving = false;
            if (head2)
            {
                redzone[1].SetActive(true);
                hitblock[1].SetActive(true);
                hydra[1].AddComponent<Head2atk>();
                Debug.Log("2");
            }
            break;

        case 3:
            isMoving = true;
            if (head3)
            {
                redzone[2].SetActive(true);
                hitblock[2].SetActive(true);
                hydra[2].AddComponent<Head3atk>();
                Debug.Log("3");
            }
            break;

        case 4:
            isMoving = true;
            if (head1)
            {
                redzone[0].SetActive(true);
                hitblock[0].SetActive(true);
                hydra[0].AddComponent<Head1atk>();
            }
            if (head2)
            {
                redzone[1].SetActive(true);
                hitblock[1].SetActive(true);
                hydra[1].AddComponent<Head2atk>();
            }
            if (head3)
            {
                redzone[2].SetActive(true);
                hitblock[2].SetActive(true);
                hydra[2].AddComponent<Head3atk>();
            }
            Debug.Log("4");
            break;

        case 5:
            if (head1 || head2 || head3)
            {
                isMoving = true;
                gameObject.AddComponent<LaserSpawner>();
            }
            break;

        case 6:
            if (head1 || head2 || head3)
            {
                Dimension.SetActive(true);
                waitTimes = 18f;
                isMoving = false;
                ULTboss.SetActive(true);
                for (int i = 0; i < part.Length; i++)
                {
                    part[i].SetBool("ult", true);
                }
            }
            break;
    }
}



    IEnumerator startFight()
    {
        yield return new WaitForSeconds(10f);
        isMoving = true;
    }

    IEnumerator head1Die()
    {
        yield return new WaitForSeconds(3f);

        hydra[0].SetActive(false);
    }

    IEnumerator head2Die()
    {
        yield return new WaitForSeconds(3f);

        hydra[1].SetActive(false);
    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("EndCutSCENE"); 
    }

    public void TakeDamage(int damageAmount)
{
    hp -= damageAmount;
    UpdateHealthSlider(); 
}

}
