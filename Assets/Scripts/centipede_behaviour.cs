using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_behaviour : MonoBehaviour {

    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance; //Distancia minima para o ataque
    public float moveSpeed;
    public float timer; //Cooldown entre os ataques
    public Transform leftLimit;
    public Transform rightLimit;

    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance; //Distancia entre inimigo e player
    private float intTimer;
    private bool attackMode;
    private bool inRange; //Verifica se o player esta no range do ataque
    private bool cooling; //Verifica se o inimigo esta dando uma pausa antes do ataque

    void Awake() {
        SelectTarget();
        intTimer = timer; //Guarda o valor inicial do timer
        anim = GetComponent<Animator>();
    }
    
    void Update() {
        if (!attackMode && !anim.GetCurrentAnimatorStateInfo(0).IsName("centipedeDie")) {
            Move();
        }

        if (!insideOfLimits() && !inRange 
        && (
            !anim.GetCurrentAnimatorStateInfo(0).IsName("centipedeAttack2")
            || !anim.GetCurrentAnimatorStateInfo(0).IsName("centipedeAttack3")
            || !anim.GetCurrentAnimatorStateInfo(0).IsName("centipedeAttack4")
        )) {
            SelectTarget();
        }

        if (inRange) {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, rayCastMask);
            RaycastDebugger();
        }

        //Se o player for detectado
        if (hit.collider != null) {
            EnemyLogic();
        } else if (hit.collider == null) {
            inRange = false;
        }

        if (inRange == false) {
            StopAttack();
        }
    }

    void OnTriggerEnter2D(Collider2D trig) {
        if (trig.gameObject.tag == "Player") {
            target = trig.transform;
            inRange = true;
            Flip();
        }
    }

    void EnemyLogic() {
        distance = Vector2.Distance(transform.position, target.position);
        
        if (distance > attackDistance) {
            StopAttack();
        } else if (attackDistance >= distance && cooling == false) {
            Attack();
        }

        if (cooling) {
            Cooldown();
            anim.SetBool("attack", false);
        }
    }

    void Move() {
        anim.SetBool("walk", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("centipedeAttack2")
            || !anim.GetCurrentAnimatorStateInfo(0).IsName("centipedeAttack3")
            || !anim.GetCurrentAnimatorStateInfo(0).IsName("centipedeAttack4")
        ) {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack() {
        timer = intTimer; //Reseta o timer quando o jogador entra no range do ataque
        attackMode = true; //Verifica se o inimigo esta atacando ou nao

        anim.SetBool("walk", false);
        anim.SetBool("attack", true);
    }

    void Cooldown() {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode) {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack() {
        cooling = false;
        attackMode = false;
        anim.SetBool("attack", false);
    }

    void RaycastDebugger() {
        if (distance > attackDistance) {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        } else if (attackDistance > distance) {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    public void TriggerCooling() {
        cooling = true;
    }

    private bool insideOfLimits() {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget() {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight) {
            target = leftLimit;
        } else {
            target = rightLimit;
        }

        Flip();
    }

    private void Flip() {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x) {
            rotation.y = 0f;
        } else {
            rotation.y = 180f;
        }

        transform.eulerAngles = rotation;
    }

}
