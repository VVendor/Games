using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour
{
    public static SpecialEffectsHelper instance;
    public ParticleSystem fireEffect;
    public ParticleSystem smokeEffect;

    void Awake()
    {
      if(instance != null)
        {
            Debug.Log("too much objects");
        }
        instance = this;  
    }


    // создание взрыва в данной точке
    public void Explosion(Vector3 place)
    {

        // создаем дым
        instantiate(smokeEffect, place);
        // создаем огонь
        instantiate(fireEffect, place);
    }

    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 place)
    {
        ParticleSystem system = Instantiate(prefab, place, Quaternion.identity) as ParticleSystem;
        // эффект уничтожен
        Destroy(system.gameObject, system.startLifetime);
        return system;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
