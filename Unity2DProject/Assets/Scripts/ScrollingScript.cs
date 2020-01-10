using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// прокрутка слоя
public class ScrollingScript : MonoBehaviour
{

    // скорость прокрутки
    public Vector2 speed = new Vector2(2, 2);

    // направление движения
    public Vector2 direction = new Vector2(-1, 0);

    // движения должны быть применимы к камере
    public bool isLinkedCamera = false;

    // бесконечный фон
    public bool isLooping = false;

    // список детей с рендером
    private List<Transform> backGroundPart;
    private Vector2 repeatableSize;
    int change = 0;

    // Start is called before the first frame update
    void Start()
    {

        // получаем детей (только для бесконечного цикла)
        if(isLooping)
        {
            backGroundPart = new List<Transform>();
            for(int i = 0; i < transform.childCount; i++)
            {
                // получаем ребенка и добавляем его в список, если он видим
                Transform child = transform.GetChild(i);
                if(child.GetComponent<Renderer>() != null)
                {
                    backGroundPart.Add(child);
                }

            }
            // сортируем по позициям (слева направо)
            backGroundPart = backGroundPart.OrderBy(obj => obj.position.x * (-1 * direction.x)).
                ThenBy(t => t.position.y * (-1 * direction.y)).ToList();

            var first = backGroundPart.First();
            var last = backGroundPart.Last();

            repeatableSize = new Vector2(
                Mathf.Abs(last.position.x - first.position.x),
                Mathf.Abs(last.position.y - first.position.y));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // перемещение
        Vector3 movement = new Vector3(speed.x * direction.x,
            speed.y * direction.y, 0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        if(isLinkedCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        // организация бесконечного цикла
        if(isLooping)
        {
            change += 1;

            // получаем позицию камеры
            var dist = (transform.position - Camera.main.transform.position).z;
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
            float width = Mathf.Abs(rightBorder - leftBorder);
            float height = Mathf.Abs(bottomBorder - topBorder);

            Vector3 exitBorder = Vector3.zero;
            Vector3 entryBorder = Vector3.zero;
            
            if(direction.x < 0)
            {
                exitBorder.x = leftBorder;
                entryBorder.x = rightBorder;
            } 

            else if (direction.x > 0)
            {
                exitBorder.x = rightBorder;
                entryBorder.x = leftBorder;
            }

            if(direction.y < 0)
            {
                exitBorder.y = bottomBorder;
                entryBorder.y = topBorder;
            }

            else if (direction.y > 0)
            {
                exitBorder.y = topBorder;
                entryBorder.y = bottomBorder;
            }

            // достаем первого ребенка
            Transform firstChild = backGroundPart.FirstOrDefault();
            if(firstChild != null)
            {
                bool checkVisible = false;

                if(direction.x != 0)
                {
                    if((direction.x < 0 && (firstChild.position.x < exitBorder.x)) ||
                        (direction.x > 0 && (firstChild.position.x > exitBorder.x)))
                    {
                        checkVisible = true;
                    }
                }

                if(direction.y != 0)
                {
                    if((direction.y < 0 && (firstChild.position.y < exitBorder.y)) ||
                        (direction.y > 0 && (firstChild.position.y > exitBorder.y)))
                        {
                        checkVisible = true;
                    }
                }


                if (checkVisible)
                {
                        // камера не видит ребенка
                        if (firstChild.GetComponent<Renderer>().isVisible == false && change > 2)
                        {
                            
                           firstChild.position = new Vector3(firstChild.position.x + ((repeatableSize.x + firstChild.GetComponent<Renderer>().
                               bounds.size.x) * (-1) * direction.x), firstChild.position.y + ((repeatableSize.y + 
                               firstChild.GetComponent<Renderer>().bounds.size.y) * (-1) * direction.y), firstChild.position.z);

                            backGroundPart.Remove(firstChild);
                            backGroundPart.Add(firstChild);
                        }

                    }
                }

            }
        }

    }
