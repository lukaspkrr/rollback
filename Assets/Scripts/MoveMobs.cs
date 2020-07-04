using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMobs : MonoBehaviour
{

    public float speed = 2f;
    public SpriteRenderer sr;
    private float durationRun = 2f;
    private bool toRight = true;
    private float lastTime = 0;
    private float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= durationRun)
        {
            timer = 0f;
            toRight = !toRight;
        }

        if (toRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            sr.flipX = true;
        }

        if (!toRight)
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            sr.flipX = false;
        }

    }


}
