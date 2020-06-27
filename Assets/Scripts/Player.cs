using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {
    // Start is called before the first frame update
    public SpriteRenderer rendererRef;
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Move();
        
    }

    void Move() {
        Vector3 pos = transform.position;
        if(Input.GetKey(KeyCode.RightArrow)){
            pos.x += 0.1f;
            rendererRef.flipX = false;
        } else if (Input.GetKey(KeyCode.LeftArrow)){
            pos.x += -0.1f;
            rendererRef.flipX = true;
        }
        transform.position = pos;
    }
}
