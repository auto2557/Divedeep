using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSystem : MonoBehaviour
{
   public openA A;
   public openB B;
   public GameObject quest;
   public Elevator[] ele;

   void Update()
   {
    if(A.GenA == true && B.GenB == true)
    {
        ele[0].enabled = true;
        ele[1].enabled = true;
        Destroy(quest);
    }
   }
}
