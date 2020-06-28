using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ComboManager instance;
    public Text comboText;
    private Animator comboTextAnimator;
    private int totalCombo;
    public float resetTime = 2f;

    private void Awake(){
        instance = this;
    }
    void Start(){
        comboTextAnimator = comboText.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SetCombo(){
        totalCombo++;
        comboText.text = "x" +totalCombo;
        comboTextAnimator.SetTrigger("Hit");
        CancelInvoke();
        Invoke("ResetCombo", 2f);
    }

     public void ResetCombo(){
        totalCombo=0;
    }
}
