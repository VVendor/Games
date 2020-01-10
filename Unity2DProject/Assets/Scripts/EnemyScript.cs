using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private bool hasSpawn;
    private MoveScript moveScript;
    private WeaponScript[] weapons;

    void Awake()
    {
        // получить оружие только один раз
        weapons = GetComponentsInChildren<WeaponScript>();

        moveScript = GetComponent<MoveScript>();
    }

    void Start()
    {
        hasSpawn = false;
        GetComponent<Collider2D>().enabled = false;
        moveScript.enabled = false;
        foreach(WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSpawn == false)
        {
            if (GetComponent<Renderer>().isVisible)
            {
                Spawn();
            }
        }
        else
        {
            foreach (WeaponScript weapon in weapons)
            {
                // автоматическая стрельба
                if (weapon != null && weapon.CanAttack)
                {
                    weapon.Attack(true);
                    SoundsEffectHelper.instance.makeEnemyShotSound();
                }
            }

            if(GetComponent<Renderer>().isVisible == false)
            {
                Debug.Log("Hiy");
                Destroy(gameObject);
            }

        }
    }

    void Spawn()
    {
        hasSpawn = true;
        GetComponent<Collider2D>().enabled = true;
        moveScript.enabled = true;
        foreach(WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }
    }
}
