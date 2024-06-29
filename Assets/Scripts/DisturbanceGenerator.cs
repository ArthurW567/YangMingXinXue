using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GenerationDirection
{
    Up,
    Down,
    Left,
    Right
}

public class DisturbanceGenerator : MonoBehaviour
{
    public static DisturbanceGenerator Instance;
    private float timeToNextDisturbance = 0;
    private float timeToWave = 0;
    private float TotalTime = 0;
    private int Death = 0;
    private int Boss = 1;
    public GameObject[] gos;
    List<string> DisturbanceCategory = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Instance = this;
    }
    public void AddDeath()
    {
        Death++;
    }
    public void Reset()
    {
        timeToNextDisturbance = 0;
        timeToWave = 0;
        TotalTime = 0;
        Boss = 1;
        Death = 0;
        gos= GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in gos)
        {
            Destroy(go);
        } 
    }
    int GetRandom()
    {
        int random;
        random = Random.Range(-50, 50);
        random += LevelManager.Instance.GetLevel() * Random.Range(8, 12)+(int)TotalTime*Random.Range(1,2);
        return random;
    }
    Disturbance GetRelatedDisturbance(string Category)
    {
        Disturbance res = null;
        switch (Category) 
        {
            case "A"://0
                res= new Disturbance(1, 1, 0.5f); 
                switch (LevelManager.Instance.GetLevel())//又臭又长，不知道能不能优化（没想好合适的函数）
                {
                    case 2:
                        if (GetRandom() >= 70)
                        {
                            res.health += 1;
                        }
                        if (GetRandom() >= 70)
                        {
                            res.damage += 1;
                        }
                        break;
                    case 3:
                        if (GetRandom() <= 100)
                        {
                            res.health += 0;
                        }
                        else if (GetRandom() >= 150)
                        {
                            res.health += 2;
                        }
                        else
                        {
                            res.health += Random.Range(1, 3);
                        }
                        if (GetRandom() >= 150)
                        {
                            res.damage += 1;
                        }
                        break;
                    case 4:
                        if (GetRandom() <= 150)
                        {
                            res.health += 1;
                        }
                        else if (GetRandom() >= 200)
                        {
                            res.health += 2;
                        }
                        else
                        {
                            res.health += Random.Range(1, 3);
                        }
                        if (GetRandom() >= 200)
                        {
                            res.damage += 1;
                        }
                        break;
                    case 5:
                        if (GetRandom() <= 170)
                        {
                            res.health += Random.Range(0,2);
                        }
                        else if (GetRandom() >= 300)
                        {
                            res.health += 2;
                        }
                        else
                        {
                            res.health += Random.Range(1, 3);
                        }
                        if (GetRandom() >= 250)
                        {
                            res.damage += 1;
                        }
                        break;
                    case 6:
                        if (GetRandom() <= 200)
                        {
                            res.health += Random.Range(0, 2);
                        }
                        else if (GetRandom() >= 350)
                        {
                            res.health += 21;
                        }
                        else
                        {
                            res.health += Random.Range(1, 3);
                        }
                        if (GetRandom() >= 250)
                        {
                            res.damage += 1;
                        }
                        break;
                    case 7:
                        if (GetRandom() <= 250)
                        {
                            res.health += 1;
                        }
                        else if (GetRandom() >= 350)
                        {
                            res.health += 3;
                        }
                        else
                        {
                            res.health += Random.Range(1, 3);
                        }
                        if (GetRandom() >= 350)
                        {
                            res.damage += 1;
                        }
                        break;
                }
                break;
               
            case "B"://1
                res = new Disturbance(3, 2, 0.5f);
                switch (LevelManager.Instance.GetLevel())
                {
                    case 4:
                        if (GetRandom() > 200)
                        {
                            res.health += Random.Range(0, 2);
                            res.damage += Random.Range(0, 1);
                        }
                        break;
                    case 5:
                        if (GetRandom() > 250)
                        {
                            res.health += Random.Range(0, 2);
                            res.damage += Random.Range(0, 2);
                        }
                        break;
                    case 6:
                        if (GetRandom() > 300)
                        {
                            res.health += Random.Range(0, 3);
                            res.damage += Random.Range(0, 2);
                        }
                        break;
                    case 7:
                        if (GetRandom() > 350)
                        {
                            res.health += Random.Range(0, 3);
                            res.damage += Random.Range(0, 3);
                        }
                        break;
                }
                break;
            case "C"://2
                res = new Disturbance(1, 1, 1f);
                switch (LevelManager.Instance.GetLevel())
                {
                    case 2:
                        res.health += Random.Range(0, 2);
                        res.damage += Random.Range(0, 2);
                        break;
                    case 3:
                        res.health += Random.Range(0, 2);
                        res.damage += Random.Range(0, 2);
                        break;
                    case 4:
                        res.health += Random.Range(0, 2);
                        res.damage += Random.Range(0, 2);
                        break;
                    case 5:
                        res.health += Random.Range(1, 3);
                        res.damage += Random.Range(0, 3);
                        break;
                    case 6:
                        res.health += Random.Range(1, 3);
                        res.damage += Random.Range(0, 2);
                        break;
                    case 7:
                        res.health += 2;
                        res.damage += Random.Range(0, 2);
                        break;
                }
                        break;
            case "D"://3
                res = new Disturbance(3, 2, 1f);
                if (LevelManager.Instance.GetLevel() * GetRandom() > 400)
                {
                    res.health += Random.Range(1, 3);
                    res.damage += Random.Range(1, 3);
                }
                break;
            case "E"://4
                res = new Disturbance(1, 1, 3f);
                if (LevelManager.Instance.GetLevel()>3)
                {
                    if (GetRandom() > 350)
                    {
                        res.health += Random.Range(0, 0);
                    }
                }
                break;
            case "F"://5
                res = new Disturbance(1, 1, 6f);
                break;
            case "Boss":
                res = new Disturbance(999, 1, 0.1f);
                break;
        }
        return res;
    }
    void GenerateDisturbance(string Category)
    {
        GenerationDirection dir = (GenerationDirection)Random.Range(0, 4);
        GameObject disturbance = null;
        switch (dir)
        {
            case GenerationDirection.Up:
                disturbance = Instantiate(Resources.Load("Prefabs/Disturbance"), new Vector3(Random.Range(-9, 9), 6, 0), Quaternion.identity) as GameObject;
                break;
            case GenerationDirection.Down:
                disturbance = Instantiate(Resources.Load("Prefabs/Disturbance"), new Vector3(Random.Range(-9, 9), -6, 0), Quaternion.identity) as GameObject;
                break;
            case GenerationDirection.Left:
                disturbance = Instantiate(Resources.Load("Prefabs/Disturbance"), new Vector3(-10, Random.Range(-6, 6), 0), Quaternion.identity) as GameObject;
                break;
            case GenerationDirection.Right:
                disturbance = Instantiate(Resources.Load("Prefabs/Disturbance"), new Vector3(10, Random.Range(-6, 6), 0), Quaternion.identity) as GameObject;
                break;
        }
        disturbance.GetComponent<DisturbanceAI>().SetDisturbance(GetRelatedDisturbance(Category));
    }

    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
        if (timeToWave >= LevelManager.Instance.Wave())
        {
           for(int i = 1; i< Random.Range(20,25); i++)
            { 
                GenerateDisturbance(LevelManager.Instance.GetName()); 
            }
            timeToWave = 0;
        }
        else
        {
            timeToWave += Time.deltaTime;
            if (timeToNextDisturbance <= 0)
            {   
                timeToNextDisturbance = Random.Range(1, 5);
                for (int i = 0; i < Random.Range(1,3); i++)
                {
                    GenerateDisturbance(LevelManager.Instance.GetName());
                }
            }
            else
            {
                timeToNextDisturbance -= Time.deltaTime;
            }
        }
        TotalTime += Time.deltaTime;
        if (TotalTime >= 120&&LevelManager.Instance.GetLevel()==5&&Boss==1)
        {
            GenerateDisturbance("Boss");
            Boss=0;
        }
        if (TotalTime >= LevelManager.Instance.LevelTime()+20)
        {
            GameManager.Instance.UpdateGameState(GameState.Victory);
        }
        if (Player.health <= 0)
        {
            GameManager.Instance.UpdateGameState(GameState.GameOver);
        }
    }
}
