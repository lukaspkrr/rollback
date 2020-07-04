using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Player : MonoBehaviour {
    // Start is called before the first frame update
    public SpriteRenderer rendererRef;
    public float jumpForce  = 7f;
    bool isGrounded = false;
    public Transform isGroundedChecker; 
    public float checkGroundRadius; 
    public LayerMask groundLayer;
    public bool directionFoward = true;
    private Animator anim;

    private int attackSequence;
    public Combo[] combos;

    public Attack attack;
    public bool attack1;
    public bool attack2;
    public bool attack3;
    private bool startCombo;

    public Shoot shootArrow;

    public List<string> currentCombo;
    private float comboTimmer;
    private Hit currentHit, nextHit;
    private bool canHit = true;
    private bool resetCombo ;
    private Rigidbody2D rb;
    public Rigidbody2D shot;

    bool isRunning = false;
        void Start() {
        anim =  GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
            directionFoward = true;
        } else if (Input.GetKey(KeyCode.LeftArrow)){
            pos.x += -0.1f;
            rendererRef.flipX = true;
            isRunning = true;
            directionFoward = false;
        } else {
            isRunning = false;
        }
        transform.position = pos;
    }

    void Jump() {
        if(Input.GetKey(KeyCode.UpArrow ) && isGrounded){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        } 
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

            if(combos[i].hits.Length > currentCombo.Count){
                if (Input.GetButtonDown(combos[i].hits[currentCombo.Count].inputButton)){

                    if(currentCombo.Count == 0){
                        Debug.Log("Primeiro hit adicionado");
                        PlayerHit(combos[i].hits[currentCombo.Count], currentCombo.Count);
                        break;
                    }else {
                        bool  comboMatch = false;
                        for (int j = 0; j < currentCombo.Count; j++)
                        {
                            if ( currentCombo[j] != combos[i].hits[j].inputButton  ){
                                comboMatch = false;
                                Debug.Log("botao errado");
                                break;
                            } else {
                                comboMatch = true;
                            }
                        }

                        if(comboMatch && canHit){
                            Debug.Log("Hit adicionao a lista");
                            canHit = false;
                            nextHit = combos[i].hits[currentCombo.Count];
                            break;
                        }
                    }
                    
                }
            } 
        }
        if(startCombo) {
            comboTimmer += Time.deltaTime;
            if(comboTimmer >= currentHit.animationTime && !canHit){
                PlayerHit(nextHit, currentCombo.Count);
            }

            if(comboTimmer >= currentHit.resetTime){
                ResetCombo();
            }
        }
    }

    async void PlayerHit(Hit hit, int count) {
        comboTimmer = 0;
        attack.SetAttack(hit);
        anim.SetBool(hit.animation, true);
        startCombo = true;
        currentCombo.Add(hit.inputButton);
        currentHit= hit;
        canHit=true;
        if(hit.inputButton == "Fire2" && count > 1 ){
            Rigidbody2D newShot = Instantiate(shot, transform.position, Quaternion.identity);
               int shotDirection = 1;

        if(directionFoward){
            shotDirection = 1;
        }else{
           shotDirection = -1; 
        }
        newShot.velocity = Vector2.right * 15 * shotDirection;
            
        }
    }

    void ResetCombo() {
        startCombo= false;
        comboTimmer= 0;
        currentCombo.Clear();
        anim.Rebind();
        canHit=true;

    }
}
