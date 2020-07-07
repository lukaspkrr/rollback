using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyBoss : MonoBehaviour
{

    public float speed = 2f;
    public float frequency = 5f;
    public float magnitude = 0.5f;
    public float rangedLimit = 10f;
    public float initX = 0, initY = 0;
    public float gravityScale = 10f;

    // privates
    private bool faceDirectionRight = true;

    private Vector3 pos;

    void Start()
    {
        initialPositionBoss();
    }

    private  void initialPositionBoss()
    {
        Vector3 lTemp = transform.localScale;
        if (lTemp.x > 0)
        {
            lTemp.x = lTemp.x * -1;
            transform.localScale = lTemp;
        }
    }

    void Update()
    {
        verifyDirection();

        if (faceDirectionRight)
        {
            flyRight();
        }
        else
        {
            flyLeft();
        }

    }

    void verifyDirection()
    {
        if (pos.x > (initX + (rangedLimit / 2)))
        {
            
            faceDirectionRight = false;
            invert(faceDirectionRight);
            
        }
        else if (pos.x < (initX - (rangedLimit / 2)))
        {
            faceDirectionRight = true;
            invert(faceDirectionRight);
        } else {
            invert(faceDirectionRight);
        }

    }

    // alter direction object example: (3, 3, 3) to (-3, 3, 3)
    private void invert(bool isRight)
    {

         Vector3 lTemp = transform.localScale;
         if(isRight && lTemp.x > 0){
             lTemp.x = lTemp.x * -1;
         }else if(!isRight && lTemp.x < 0) {
             lTemp.x = lTemp.x * -1;
         } else{
             transform.localScale = lTemp;
         }   

        transform.localScale = lTemp;
    }

    void flyRight()
    {
        pos += transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    void flyLeft()
    {
        pos -= transform.right * Time.deltaTime * speed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }


}
