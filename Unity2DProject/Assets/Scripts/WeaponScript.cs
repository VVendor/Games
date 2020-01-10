using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // префаб для стрельбы
    public Transform shotPrefab;

    // перезарядка в секундах
    public float shootingRate = 0.25f;



    // непосредственно перезарядка
    private float shootCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        shootCoolDown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(shootCoolDown > 0)
        {
            shootCoolDown -= Time.deltaTime;
        }
    }

    public void Attack(bool isEnemy)
    {
        if(CanAttack)
        {
            shootCoolDown = shootingRate;

            // создание нового выстрела
            var shotTransform = Instantiate(shotPrefab) as Transform;

            //определение положения
            shotTransform.position = transform.position;

            //свойство врага
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if(shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            // направляем выстрел на врага
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if(move != null)
            {
                move.direction = this.transform.right;
            }
        }
    }

    // готово ли оружие выпустить снаряд

    public bool CanAttack
    {
        get
        {
            return shootCoolDown <= 0f;
        }
    }

}
