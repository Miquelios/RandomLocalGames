using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    public LayerMask grassLayer;

    public event Action OnEncountered;

    private bool isMoving;
    private Vector2 input; // input es una variable de 2dimensions

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    

    public void HandleUpdate ()
    {
        if (!isMoving)
        {
            //diem que el input.x sigui l'eix horitzontal i el input.y el vertical. Amb el GetAxisRaw fem que agafi valors de +1 o -1, així es desplaça 1 casella a la dreta o una a l'esquerra.
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");


            // amb aquestes dues linies fem que no es pugui anar en diagonal
            if (input.x != 0) input.y = 0;
            if (input.y != 0) input.x = 0;


            // aqui diem que si la variable input té un valor diferent de zero, la posició del personatge serà la que té més el input
            if (input != Vector2.zero)
            {

                //aquí cridem el animator i fem que canvii el sprite left/right/up/down depenent dels inputs
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;


                //al introduir les colisions, la co-rutina de Move només la cridarem si "isWalkable" = true
                if (isWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
                
            }
        }

        animator.SetBool("isMoving", isMoving);
    }


    //aixo no sé massa què és
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();

    }

    private bool isWalkable(Vector3 targetPos)
    {
        //hem de checkejar si hi ha un objecte solid a la casella de posició utilitzant lu següent:
        //aquí estem checkejant la target position i el radi del overlap el fem de 0.3f. La tercera component es la layer del solid object.
        //si no és null, vol dir que hi ha un solid object en el nostre target position, per tant retornarem false (no és walkable)
        //otherwise true
        if (Physics2D.OverlapCircle(targetPos, 0.02f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }

    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                animator.SetBool("isMoving", false);
                OnEncountered();                
            }
        }
    }
}
