using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour {
    // Start is called before the first frame update
    public SpriteRenderer rendererRef;
    public float jumpForce ;
    bool isGrounded = false;
    public Transform isGroundedChecker; 
    public float checkGroundRadius; 
    public LayerMask groundLayer;

    private Animator anim;

    private int attackSequence;
    public Combo[] combos;

    bool isRunning = false;
        void Start() {
        anim =  GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Move();
        Jump();
        CheckIfGrounded();
        anim.SetBool("run", isRunning && isGrounded);
        anim.SetBool("jump", !isGrounded);
        CheckInputs();

        
    }

    void Move() {
        Vector3 pos = transform.position;
        if(Input.GetKey(KeyCode.RightArrow)){
            pos.x += 0.1f;
            rendererRef.flipX = false;
            isRunning = true;
        } else if (Input.GetKey(KeyCode.LeftArrow)){
            pos.x += -0.1f;
            rendererRef.flipX = true;
            isRunning = true;
        } else {
            isRunning = false;
        }
        transform.position = pos;
    }

    void Jump() {
        Vector3 pos = transform.position;
        if(Input.GetKey(KeyCode.UpArrow ) && isGrounded){
            pos.y += 0.1f * jumpForce;
            rendererRef.flipX = false;
        } 
        transform.position = pos;
    }

    void CheckIfGrounded() { 
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer); 
        if (collider != null) { 
            isGrounded = true; 
        } else { 
            isGrounded = false; 
        } 
    }

    void CheckInputs() {
        for (int i = 0; i < combos.Length; i++){
            if (Input.GetButtonDown(combos[i].hits[0].inputButton)){
                PlayerHit(combos[i].hits[0]);
                break;
            }
        }
    }

    void PlayerHit(Hit hit) {
        anim.Play(hit.animation);

    }

    void ResetCombo() {

    }
}
