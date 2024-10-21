using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public VolumeProfile profile;
    public UnityEngine.Rendering.Universal.Vignette vignette;
    public UnityEngine.Rendering.Universal.Bloom bloom;
    public UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments;

    static public float health = 50f;
    static public float maxHealth = 100f;

    private int combo=0;
    static public float mistakeTime=0;
    private Animator animator;
    public int a = 0;
    public int judge1 = 0;
    public int judge2 = 0;
    private int judgeShake = 0;
    public float x, fx;
    float baseHealth;
    GameObject debugText;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = 75f - 5f * LevelManager.level;
        baseHealth = health;
        profile = FindObjectOfType<Volume>().profile;
        profile.TryGet(out vignette);
        profile.TryGet(out bloom);
        profile.TryGet(out colorAdjustments);
        debugText = GameObject.Find("HPText");
        mistakeTime = 0f;
    }
    bool mistake = false;
    bool regen = false;
    public float dmgScore, bloomScore;
    void Breath()
    {
        x = animator.GetCurrentAnimatorStateInfo(0).normalizedTime - (int)animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        fx = (Mathf.Cos((x - 0.25f) * 2f * Mathf.PI) + 1f) / 2f;
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

        // Update VFX
        dmgScore = 0f;
        bloomScore = 0f;
        if (mistakeTime < -1f)
            bloomScore = (-1f - mistakeTime) * 0.2f * fx;
        if(health <= baseHealth * 0.5f)
            dmgScore = 2f * (0.5f - health / baseHealth) * (0.6f * fx + 0.4f);
        dmgScore = Mathf.Clamp01(dmgScore);
        bloomScore = Mathf.Clamp01(bloomScore);

        vignette.intensity.Override(dmgScore * 0.7f);
        bloom.intensity.Override(bloomScore * 1.2f);
        colorAdjustments.saturation.Override(bloomScore * -0.4f);

    }
    // Update is called once per frame
    void Update()
    {
        Breath();
        if(debugText)
           debugText.GetComponent<Text>().text = "HP = " + health
            + "\n X = " + x + " fx = " + fx
            +"\n mt = " + mistakeTime;
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
