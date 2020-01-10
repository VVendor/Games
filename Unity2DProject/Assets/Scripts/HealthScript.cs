using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    // здоровье персонажа
    public int hp = 1;

    // игрок или враг
    public bool isEnemy = true;

    // наносим урон и проверка на жизнь персонажа

    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if(hp <= 0)
        {
            SpecialEffectsHelper.instance.Explosion(transform.position);
            SoundsEffectHelper.instance.makeExplosionSound();
            Destroy(gameObject);
        } 
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // это был выстрел?
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if(shot != null)
        {
            // стреляем сами в себя?
            if(shot.isEnemyShot != isEnemy)
            {
                Damage(shot.damage);
                // уничтожить выстрел
                Destroy(shot.gameObject); // нужно целится в игровой объект
            }
        }
    }
}
