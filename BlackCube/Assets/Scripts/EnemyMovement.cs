using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed;
    public float range;

    private float t = 0;
    private bool isMovingRight = true;
    private Vector2 initialPos;



    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float finalX = initialPos.x + range;
        if (isMovingRight)
        { 
        transform.position = new Vector2(Mathf.SmoothStep(initialPos.x, finalX, t), transform.position.y);
            t += speed * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector2(Mathf.SmoothStep(finalX, initialPos.x, t), transform.position.y);
            t += speed * Time.deltaTime;
        }

        if (transform.position.x > 0.99 * finalX || transform.position.x < 1.01 * initialPos.x)
        {
            isMovingRight = !isMovingRight;
            t = 0;
        }
    }
}
