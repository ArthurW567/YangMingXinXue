using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbanceAI : MonoBehaviour
{
    private GameObject player;
    private Disturbance disturbance;

    // Start is called before the first frame update
    public void SetDisturbance(Disturbance x) 
    {
        this.disturbance = x;
    }
    public Disturbance GetDisturbance()
    {
        return this.disturbance;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (disturbance.health >= 1&&disturbance.health<=6) { gameObject.transform.localScale = new Vector3(0.14f + disturbance.health * 0.02f, 0.14f + disturbance.health * 0.02f, 1); }
        if (disturbance.health > 10) { gameObject.transform.localScale = new Vector3(1f, 1f, 1); }
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
        if (disturbance.health >= 0 && disturbance.health <= 10) { gameObject.transform.localScale = new Vector3(0.14f + disturbance.health * 0.02f, 0.14f + disturbance.health * 0.02f, 1); }
        if(disturbance.health>10){ gameObject.transform.localScale = new Vector3(1f, 1f, 1); }
        if (disturbance.health<=0)
        {
            StartCoroutine("Fade");
            DisturbanceGenerator.Instance.AddDeath();
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine("Fade");
            DisturbanceGenerator.Instance.AddDeath();
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
