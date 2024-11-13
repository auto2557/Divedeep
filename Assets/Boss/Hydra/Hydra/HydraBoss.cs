using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class HydraBoss : enemyHP
{
    public Transform player; 
    public player playerscript;
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
    public GameObject skill7;
    public GameObject skill8;
    public GameObject Halo;

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
    public AudioSource hydraSong2;

    void Start()
{

        SoundManager.instance.PlaySFX("boss", 4, 1);

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

        skill7.SetActive(false);
        skill8.SetActive(false);

        ULTboss.SetActive(false);

    Dictionary<int, int> bgmSelections = new Dictionary<int, int>
    {
        { 0, 1 },
        { 1, 2 }
    };

    SoundManager.instance.PlayMultipleBGM(bgmSelections);

    speed = 1.2f;

  
    /*GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
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
    }*/

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
            SoundManager.instance.StopAllBGM();

            if (hydraSong2 != null)
            {
                hydraSong2.Play();
            }
            LightPhase2.SetActive(true);
            Halo.SetActive(true);
    phase1 = false;
    phase2 = true;


            maxHP = 4000;
    hp = maxHP;
    healthSlider.maxValue = maxHP;
    healthSlider.value = maxHP;
            spawnMissile[1].SetActive(true);
        }
        else if (hp <= 0 && phase2 == true)
        {
            StartCoroutine(dead());
        }
    }

  public void UpdateHealthSlider()
{
    healthSlider.value = Mathf.Clamp(hp, 0, maxHP);
}

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("hitblock") || collision.CompareTag("slash")) && !isDead)
        {
          

            int damageAmount = Playerscript.damage;
            hp -= damageAmount;

            ShowDamagePopup(damageAmount);

            UpdateHealthSlider();
          
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
        redzone[3].SetActive(false);
        redzone[4].SetActive(false);
        redzone[5].SetActive(false);
        redzone[6].SetActive(false);
        hitblock[0].SetActive(false);
        hitblock[1].SetActive(false);
        hitblock[2].SetActive(false);
        hitblock[3].SetActive(false);
        hitblock[4].SetActive(false);
        hitblock[5].SetActive(false);
        hitblock[6].SetActive(false);

        part[0].SetBool("ult",false);
         part[1].SetBool("ult",false);
          part[2].SetBool("ult",false);
           part[3].SetBool("ult",false);
            part[4].SetBool("ult",false);

        skill7.SetActive(false);
        skill8.SetActive(false);

       


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

        Novabeam scriptToRemove4 = hydra[5].GetComponent<Novabeam>();
        if (scriptToRemove4 != null)
        {
            Destroy(scriptToRemove4);
        }

        Novabeam2 scriptToRemove5 = hydra[6].GetComponent<Novabeam2>();
        if (scriptToRemove5 != null)
        {
            Destroy(scriptToRemove5);
        }

        Novabeam scriptToRemove6 = hydra[7].GetComponent<Novabeam>();
        if (scriptToRemove6 != null)
        {
            Destroy(scriptToRemove6);
        }

        Novabeam2 scriptToRemove7 = hydra[8].GetComponent<Novabeam2>();
        if (scriptToRemove7 != null)
        {
            Destroy(scriptToRemove7);
        }

        LaserSpawner scriptToRemove8 = gameObject.GetComponent<LaserSpawner>();
        if (scriptToRemove8 != null)
        {
            Destroy(scriptToRemove8);
        }

        StartCoroutine(coolDownskill());
    }

    IEnumerator coolDownskill()
    {
        yield return new WaitForSeconds(4f);

        if (phase1)
        {

            if (hp >= 3000 && head1 && head2 && head3)
            {
                int patternSkill = Random.Range(1, 5);
                skillNumber = patternSkill;
            }
            else if (hp <= 3000 && hp > 2000 && head1 && head2 && head3)
            {
                int patternSkill = Random.Range(3, 5);
                skillNumber = patternSkill;
            }
            else if (hp <= 2000 && hp > 1000 && head1 == false && head2 == true && head3 == true)
            {
                int patternSkill = Random.Range(2, 6);
                skillNumber = patternSkill;
            }
            else if (hp < 1000 && head1 == false && head2 == false && head3 == true)
            {
                int patternSkill = Random.Range(4, 6);
                skillNumber = patternSkill;
            }
        }

        else if (phase2)
        {

            if (hp <= 4000 && head1 == false && head2 == false)
            {
                int patternSkill = Random.Range(4, 9);
                skillNumber = patternSkill;
                speed = 2f;
                waitTimes = 8f;
            }
            else if (hp <= 2700 && head1 == false && head2 == false)
            {
                int patternSkill = Random.Range(7, 9);
                skillNumber = patternSkill;
                speed = 2f;
                waitTimes = 8f;
            }
            else if (hp <= 2000 && head1 == false && head2 == false)
            {
                int patternSkill = Random.Range(5, 9);
                skillNumber = patternSkill;
                speed = 2f;
                waitTimes = 8f;
            }
            else if (hp <= 1000 && head1 == false && head2 == false)
            {
                int patternSkill = Random.Range(6, 9);
                skillNumber = patternSkill;
                speed = 2f;
                waitTimes = 8f;
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
            isMoving = true;
            if (head1)
            {
                redzone[0].SetActive(true);
                hitblock[0].SetActive(true);
                hydra[0].AddComponent<Head1atk>();
                Debug.Log("1");
            }
                waitTimes = 4f;
                break;

        case 2:
            isMoving = true;
            if (head2)
            {
                redzone[1].SetActive(true);
                hitblock[1].SetActive(true);
                hydra[1].AddComponent<Head2atk>();
                Debug.Log("2");
            }
                waitTimes = 4f;
                break;

        case 4:
            isMoving = true;
            if (head3)
            {
                redzone[2].SetActive(true);
                hitblock[2].SetActive(true);
                hydra[2].AddComponent<Head3atk>();
                Debug.Log("3");
            }
                waitTimes = 4f;
                break;
              

            case 3:
                isMoving = false;
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
                waitTimes = 6f;
                break;

        case 5:
            if (head1 || head2 || head3)
            {
                isMoving = true;
                    Camera.main.orthographicSize = 9;
                    StartCoroutine(laserspawn());
            }
            break;

        case 6:
            if (head1 || head2 || head3)
            {
                    SoundManager.instance.PlaySFX("boss", 0, 1);
                    Dimension.SetActive(true);
                waitTimes = 10f;
                isMoving = false;
                ULTboss.SetActive(true);
                for (int i = 0; i < part.Length; i++)
                {
                    part[i].SetBool("ult", true);
                }
            }
            break;
            case 7:
                if(head1 || head2 || head3)
                {
                    Camera.main.orthographicSize = 4f;
                    skill7.SetActive(true);
                    redzone[3].SetActive(true);
                    redzone[4].SetActive(true);
                    hitblock[3].SetActive(true);
                    hitblock[4].SetActive(true);

                    hydra[5].AddComponent<Novabeam>();
                    hydra[6].AddComponent<Novabeam2>();

                    waitTimes = 4f;
                    isMoving = false;

                   
                }
                break;
            case 8:
                if (head1 || head2 || head3)
                {
                    Camera.main.orthographicSize = 4f;
                 
                    skill8.SetActive(true);
                    redzone[5].SetActive(true);
                    redzone[6].SetActive(true);
                    hitblock[5].SetActive(true);
                    hitblock[6].SetActive(true);

                    hydra[7].AddComponent<Novabeam>();
                    hydra[8].AddComponent<Novabeam2>();
                    waitTimes = 4f;
                    isMoving = false;

                  
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

    IEnumerator laserspawn()
    {
        yield return new WaitForSeconds(1f);
        gameObject.AddComponent<LaserSpawner>();
    }

    public void TakeDamage(int damageAmount)
{
    hp -= damageAmount;
    UpdateHealthSlider(); 
}

}
