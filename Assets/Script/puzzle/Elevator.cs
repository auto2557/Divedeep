using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform startPos; 
    public Transform endPos;   
    public float speed = 0.5f;   
    public float waitTime = 10f; 

    private bool playerOnElevator = false;  
    private bool movingToEnd = true; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnElevator = true; 
            StopAllCoroutines(); 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnElevator = false; 
            if (!movingToEnd)
            {
                StartCoroutine(ReturnToStartPos()); 
            }
        }
    }

    private void Update()
    {
        if (playerOnElevator || movingToEnd)
        {
            MoveElevator();
        }
    }

    private void MoveElevator()
    {
        Vector2 targetPos = movingToEnd ? endPos.position : startPos.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

       
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            if (movingToEnd && !playerOnElevator) 
            {
                movingToEnd = false;
                StartCoroutine(ReturnToStartPos()); 
            }
            else if (!movingToEnd) 
            {
                movingToEnd = true; 
            }
        }
    }

    private IEnumerator ReturnToStartPos()
    {
        yield return new WaitForSeconds(waitTime); 
        if (!playerOnElevator) 
        {
            movingToEnd = false; 
        }
    }
}
