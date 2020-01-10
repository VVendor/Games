using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // скорость движения корабля
    public Vector2 speed = new Vector2(50, 50);

    // направление движения корабля
    private Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        // извлечение координат корабля
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // задаем направление корабля
        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");

        if(shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if(weapon != null)
            {
                weapon.Attack(false);
                SoundsEffectHelper.instance.makePlayerShotSound();
            }
        }

        // игрок не должен выходить за рамки игры
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z);

    }

    void FixedUpdate()
    {
        // перемещение корабля
        GetComponent<Rigidbody2D>().velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        // столкновение с врагом
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();

        if (enemy != null)
        {
            // убиваем врага
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null)
            {
                enemyHealth.Damage(enemyHealth.hp);
            }
            damagePlayer = true;
        }

        BossScript boss = collision.gameObject.GetComponent<BossScript>();
        if (boss != null) damagePlayer = true;

        // повреждения у игрока
        if (damagePlayer)
        {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null)
            {
                playerHealth.Damage(1);
            }
        }
    }

    void OnDestroy()
    {
        transform.parent.gameObject.AddComponent<GameOverScript>(); 
    }

}
