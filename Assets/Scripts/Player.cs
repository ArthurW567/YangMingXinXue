using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    static public int health = 100;
    private int combo=0;
    static public int falseTime=0;
    private Animator animator;
    public int a = 0;
    public int judge1 = 0;
    public int judge2 = 0;
    private int judgeShake = 0;
    public int health1 = 0;
    public float x;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (LevelManager.Instance.GetLevel() > 4)
        {
            health = 100 - 10 * LevelManager.Instance.GetLevel();
        }
    }
    void breath()
    {
        health1 = health;
        x = animator.GetCurrentAnimatorStateInfo(0).normalizedTime - (int)animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (x <= 0.15 || x >= 0.85)
        {
            judge1 = 1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                a = 1;
            }
        }
        if (health < 30)
        {
            if (judgeShake == 0)
            { 
                CameraShake.Instance.Shake();
                judgeShake = 1;
            }
        }
        if (x>= 0.35 && x <= 0.65)
        {
            judge2 = 1;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (a == 1)
                { 
                    combo += 1;
                    a = 2;
                    falseTime = 0;
                    if (combo >= 2)
                    {
                        health += 1;
                        if (health > 100) { health = 100; }
                    }
                    if (combo >= 3&&combo<7)
                    {
                        Time.timeScale = 1 - 0.02f * combo;
                    }
                    if (combo >= 5)
                    {
                        health += 3;
                        if (health > 100) { health = 100; }
                    }
                    if (combo >= 7)
                    {
                        Time.timeScale = 0.85f;
                    }
                }
            }
        }
        if(x > 0.55 && judge1 == 1 && judge2 == 1)
        {
            judge1 = 0;
            judge2 = 0;
            judgeShake = 0;
            if (a != 2)
            {
                health -= 1;
                combo = 0;
                falseTime += 1;
                Time.timeScale = 1;
            }
            a = 0;
            if (falseTime >= 2)
            {
                CameraShake.Instance.Shake();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        breath();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health -= other.gameObject.GetComponent<DisturbanceAI>().GetDisturbance().damage;
        }
        if (health <= 0)
        {
            GameManager.Instance.UpdateGameState(GameState.GameOver);
        }
    }
}
