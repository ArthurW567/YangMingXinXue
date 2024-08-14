using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    static public float health = 50f;
    static public float maxHealth = 100f;

    private int combo=0;
    static public float mistakeTime=0;
    private Animator animator;
    public int a = 0;
    public int judge1 = 0;
    public int judge2 = 0;
    private int judgeShake = 0;
    public float x;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = 75f - 5f * LevelManager.level;
    }
    bool mistake = false;
    bool regen = false;
    void Breath()
    {
        x = animator.GetCurrentAnimatorStateInfo(0).normalizedTime - (int)animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        mistake = false;
        regen = false;
        if(x>=0.06f && x<= 0.44f)
		{
            if (Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.X))
                regen = true;
            else
                mistake = true;
		}

        if (x >= 0.56f && x <= 0.94f)
        {
            if (Input.GetKey(KeyCode.X) && !Input.GetKey(KeyCode.Z))
                regen = true;
            else
                mistake = true;
        }

        if (regen)
		{
            health += LevelManager.curParams.breathRegen * Time.deltaTime;
            mistakeTime -= 0.5f * Time.deltaTime;
        }
        if (mistake)
        {
            health -= LevelManager.curParams.breathDamage * Time.deltaTime;
            mistakeTime += Time.deltaTime;
        }
        if(mistakeTime >= 3f)
        {
            CameraShake.Instance.Shake();
            mistakeTime = 0f;
        }
        if (health > 100)
            health = 100;
        if(health <= 0f)
            GameManager.Instance.UpdateGameState(GameState.GameOver);
    }
    // Update is called once per frame
    void Update()
    {
        Breath();
        GameObject.Find("HPText").GetComponent<Text>().text = "HP = " + health
            + "\n X = " + x;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (health <= 1f)
            {
                health = 0;
                GameManager.Instance.UpdateGameState(GameState.GameOver);
            }
            else
            {
                int dmg = other.gameObject.GetComponent<DisturbanceAI>().GetDisturbance().damage;
                health -= dmg;
                health = Mathf.Max(1f, health);
            }
        }
    }
}
