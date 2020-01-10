using UnityEngine;

public class ShotScript : MonoBehaviour
{
    // причиненный вред
    public int damage = 1;

    // снаряд наносит удар игроку или врагам?
    public bool isEnemyShot = false;

    // Start is called before the first frame update
    void Start()
    {
        // ограниченное время жизни, чтобы избежать утечек
        Destroy(gameObject, 20);        
    }

}
