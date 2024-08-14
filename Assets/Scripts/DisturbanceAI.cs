using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbanceAI : MonoBehaviour
{
    private GameObject player;
    private DisturbanceParam disturbance;

    // Start is called before the first frame update
    public void SetDisturbance(DisturbanceParam x) 
    {
        this.disturbance = x;
    }
    public DisturbanceParam GetDisturbance()
    {
        return this.disturbance;
    }

    void CalcScale()
    {
        float scale = 0.12f + Mathf.Pow(disturbance.health, 0.6f) * 0.02f;
        scale *= Mathf.Pow(0.95f, LevelManager.level - 1f);
        scale = Mathf.Clamp(scale, 0.10f, 0.24f);
        gameObject.transform.localScale = new Vector3(scale, scale, 1f);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        CalcScale();
    }

    IEnumerator Fade()
    {
        
        SpriteRenderer[] children= GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer child in children)
        {
            Color color = child.color;
            for (float alpha = 1f; alpha >= 0; alpha -= 0.02f)
            {
                color.a = alpha;
                child.color = color;
                yield return new WaitForSeconds(0.01f);
            }
            Destroy(gameObject);

        }
        
    }

    private void OnMouseDown()
    {
        disturbance.health--;
        CalcScale();        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine("Fade");
            //DisturbanceGenerator.Instance.AddDeath();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            transform.position += direction * Time.deltaTime * disturbance.tracespeed;
        }

    }
}
