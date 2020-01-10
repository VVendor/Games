using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private bool hasSpawn;

    // параметры босса
    private MoveScript movescript;
    private WeaponScript[] weapons;
    private Animator animator;
    private SpriteRenderer[] renderers;

    // поведение босса
    public float minAttackCoolDown = 0.5f;
    public float maxAttackCoolDown = 2f;

    private float aiCoolDown;
    private bool isAttacking;
    private Vector2 positionTarget;

    void Awake()
    {
        weapons = GetComponentsInChildren<WeaponScript>();

        movescript = GetComponent<MoveScript>();

        animator = GetComponent<Animator>();

        renderers = GetComponentsInChildren<SpriteRenderer>();  
    }

    // Start is called before the first frame update
    void Start()
    {
        // отключаем все компоненты

        hasSpawn = false;

        GetComponent<Collider2D>().enabled = false;

        movescript.enabled = false;

        foreach(WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }

        isAttacking = false;
        aiCoolDown = maxAttackCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasSpawn == false)
        {
            if(renderers[0].isVisible)
            {
                Spawn();
            }
        }
        else
        {

            aiCoolDown -= Time.deltaTime;

            if(aiCoolDown <= 0f)
            {
                isAttacking = !isAttacking;
                aiCoolDown = Random.Range(minAttackCoolDown, maxAttackCoolDown);
                positionTarget = Vector2.zero;

                animator.SetBool("Attack", isAttacking);
            }

            if(isAttacking)
            {
                movescript.direction = Vector2.zero;

                foreach(WeaponScript weapon in weapons)
                {
                    if(weapon != null && weapon.enabled && weapon.CanAttack)
                    {
                        weapon.Attack(true);
                        SoundsEffectHelper.instance.makeEnemyShotSound();
                    }
                }
            }

            else
            {
                if(positionTarget == Vector2.zero)
                {
                    Vector2 randomPoint = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
                    positionTarget = Camera.main.ViewportToWorldPoint(randomPoint);

                }

                if(GetComponent<Collider2D>().OverlapPoint(positionTarget))
                {
                    positionTarget = Vector2.zero;
                }

                Vector3 direction = ((Vector3)positionTarget - this.transform.position);
                movescript.direction = Vector3.Normalize(direction);

            }
        }
    }

    void Spawn()
    {
        hasSpawn = true;

        GetComponent<Collider2D>().enabled = true;
        movescript.enabled = true;
        foreach(WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }

        foreach(ScrollingScript scrolling in FindObjectsOfType<ScrollingScript>())
        {
            if(scrolling.isLinkedCamera)
            {
                scrolling.speed = Vector2.zero;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ShotScript shot = collision.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            if(shot.isEnemyShot == false)
            {
                aiCoolDown = Random.Range(minAttackCoolDown, maxAttackCoolDown);
                isAttacking = false;

                animator.SetTrigger("Hit");
            }
        }
    }

    void OnDrawGizmos()
    {
      if(hasSpawn && isAttacking == false)
        {
            Gizmos.DrawSphere(positionTarget, 0.25f);
        }  
    }

    void OnDestroy()
    {
        transform.parent.gameObject.AddComponent<GameOverScript>();
         
    }
}
