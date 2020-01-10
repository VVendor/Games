using UnityEngine;

public class MoveScript : MonoBehaviour
{
    // скорость врага
    public Vector2 speed = new Vector2(10, 10);


    // направление движения 
    private Vector2 movement;

    public Vector2 direction = new Vector2(-1, 0);


    // Update is called once per frame
    void Update()
    {
        //перемещение объекта
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
    }

    void FixedUpdate()
    {
        // расположение компонента Rigidbody2D
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}
