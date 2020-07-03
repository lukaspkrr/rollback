using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyBoss : MonoBehaviour
{
    public float flySpeed = 2f;
    public float frequency = 5f;
    public float magnitude = 0.5f;

    public float initX = 0, initY = 0;
    public float rangedLimitFly = 10f;

    bool faceDirectionRight = true;

    Vector3 pos;

    Vector3 toRight = new Vector3(-3, 3, 3);
    Vector3 toLeft = new Vector3(3, 3, 3);


    void Start()
    {
        pos = new Vector3(initX, initY, 0);
        transform.localScale = toRight;

    }


    void Update()
    {

        verifyDirection();

        if (faceDirectionRight)
        {
            moveRight();
        }
        else
        {
            moveLeft();
        }

    }

    void verifyDirection()
    {
        if (pos.x > (initX + (rangedLimitFly / 2)))
        {
            faceDirectionRight = false;
            transform.localScale = toLeft;
        }
        else if (pos.x < (initX - (rangedLimitFly / 2)))
        {
            faceDirectionRight = true;
            transform.localScale = toRight;
        }


    }


    void moveRight()
    {
        pos += transform.right * Time.deltaTime * flySpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }
    void moveLeft()
    {
        pos -= transform.right * Time.deltaTime * flySpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

}
