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
    //private float timeToNextDisturbance = 0;
    //private float timeToWave = 0;
    //private float TotalTime = 0;
    //private int Boss = 1;
    public GameObject[] gos;
    static GameObject dPrefab;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartGenerate()
	{
        StartCoroutine(GenerateCoroutine());
        StartCoroutine(WaveAndBossCoroutine());
	}

    public void Reset()
    {
        gos= GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in gos)
        {
            Destroy(go);
        }
    }
    bool inWave = false;

    float IncreStrengthMultiplier()
	{
        return 1f +
            0.4f * (LevelManager.Instance.curLevelTime / LevelManager.curParams.maxTime);
	}

    float WaveStrengthMultiplier()
	{
        return inWave ? 1f + LevelManager.curParams.waveStrength : 1f;
    }

    IEnumerator GenerateCoroutine()
	{
        while(LevelManager.Instance.curLevelTime < LevelManager.curParams.maxTime - 10f)
		{
            GenerateDisturbance();
            yield return new WaitForSeconds(LevelManager.curParams.genCD
                / WaveStrengthMultiplier() / IncreStrengthMultiplier());
		}
	}

    IEnumerator WaveAndBossCoroutine()
    {
        float mt = LevelManager.curParams.maxTime - 10f;
        yield return new WaitForSeconds(0.48f * mt);
        if(LevelManager.curParams.waveCnt >= 2)
        {
            inWave = true;
            yield return new WaitForSeconds(0.06f * mt);
            inWave = false;
        }
        else
            yield return new WaitForSeconds(0.06f * mt);
        yield return new WaitForSeconds(0.32f * mt);
        if (LevelManager.curParams.waveCnt >= 1)
        {
            inWave = true;
            yield return new WaitForSeconds(0.06f * mt);
            inWave = false;
        }
        else
            yield return new WaitForSeconds(0.06f * mt);
        if (LevelManager.curParams.hasBoss)
            GenerateDisturbance(true);
    }

    DisturbanceParam GetDisturbanceParam()
    {
        DisturbanceParam res = new DisturbanceParam();
        float spd = Random.value;
        if(spd < LevelManager.curParams.highSpdMaxRatio)
		{ // high speed
            res.tracespeed = 2.4f;
            res.health = Mathf.CeilToInt(Random.Range(0.01f, LevelManager.curParams.highSpdMaxHealth));
            }
        else if(spd < LevelManager.curParams.highSpdMaxRatio + LevelManager.curParams.midSpdMaxRatio)
        { // mid speed
            res.tracespeed = 1.2f;
            res.health = Mathf.CeilToInt(Random.Range(0.01f, LevelManager.curParams.midSpdMaxHealth));
            }
        else
        { // low speed
            res.tracespeed = 0.6f;
            res.health = Mathf.CeilToInt(Random.Range(0.01f, LevelManager.curParams.lowSpdMaxHealth));
        }
        res.damage = res.health;


        return res;
    }
    void GenerateDisturbance(bool isBoss = false)
    {

        if (!dPrefab)
        {
            dPrefab = Resources.Load<GameObject>("Prefabs/Disturbance");
            Debug.Log("D");
        }
        GenerationDirection dir = (GenerationDirection)Random.Range(0, 4);
        GameObject disturbance = null;
        switch (dir)
        {
            case GenerationDirection.Up:
                disturbance = Instantiate(dPrefab, new Vector3(Random.Range(-9, 9), 6), Quaternion.identity);
                break;
            case GenerationDirection.Down:
                disturbance = Instantiate(dPrefab, new Vector3(Random.Range(-9, 9), -6), Quaternion.identity);
                break;
            case GenerationDirection.Left:
                disturbance = Instantiate(dPrefab, new Vector3(-10, Random.Range(-6, 6)), Quaternion.identity);
                break;
            case GenerationDirection.Right:
                disturbance = Instantiate(dPrefab, new Vector3(10, Random.Range(-6, 6)), Quaternion.identity);
                break;
        }
        var param = GetDisturbanceParam();
        if (isBoss)
        {
            param.health = 9999;
            param.damage = 9999;
            param.tracespeed = 0.4f;
        }
        disturbance.GetComponent<DisturbanceAI>().SetDisturbance(param);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
