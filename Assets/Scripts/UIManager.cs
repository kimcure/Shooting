using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerHealthText;
    public Text waveText;
    public Text enemyCountText;
    public Text bossWarningText;
    public Text scoreText;
    public Text stageClearText;
    public Slider bossHealthBar;

    public PlayerHealth playerHealth; 
    
    public EnemySpawner spawner;

    public float warningDuration = 2f;
    public float clearDisplayDuration = 2f;

    private void Start()
    {
        playerHealthText.fontSize = 32;
        playerHealthText.color = Color.white;

        enemyCountText.fontSize = 32;
        enemyCountText.color = Color.white;
    }

    void Update()
    {
        playerHealthText.text = $"HP : {playerHealth.CurrentHealth}/{playerHealth.MaxHealth}";
        enemyCountText.text = $"Enemies: {spawner.ActiveEnemyCount()}";

     
    }

    public void ShowWave(int wave)
    {
        waveText.text = $"Wave {wave}";
    }

    public void ShowBossWarning() 
    {
        StartCoroutine(BossWarningCoroutine());
    }

    public void ShowStageClear()
    {
        StartCoroutine(StageClearCoroutine());
    }

    IEnumerator StageClearCoroutine()
    {
        stageClearText.text = "STAGE CLEAR!";
        stageClearText.color = new Color(1, 1, 0, 0);
        stageClearText.gameObject.SetActive(true);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            stageClearText.color = new Color(1, 1, 0, t);
            yield return null;
        }

        yield return new WaitForSeconds(clearDisplayDuration);

        t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime * 2f;
            stageClearText.color = new Color(1, 1, 0, t);
            yield return null;
        }

        stageClearText.gameObject.SetActive(false);
    }

    IEnumerator BossWarningCoroutine()
    {
        bossWarningText.text = "!!!BOSS INCOMING!!!";
        bossWarningText.color = new Color(1, 0, 0, 0);
        float t = 0f;


        //보스 텍스트 페이드 인/아웃 효과
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            bossWarningText.color = new Color(1, 0, 0, t);
            yield return null;
        }

        yield return new WaitForSeconds(warningDuration);

        t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime * 2f;
            bossWarningText.color = new Color(1, 0, 0, t);
            yield return null;
        }

        bossWarningText.text = "";
    }

    //보스 체력바 설정
    public void ShowBossHealthBar()
    {
        bossHealthBar.gameObject.SetActive(true);
    }

    public void HideBossHealthBar()
    {
        bossHealthBar.gameObject.SetActive(false);
    }

    public void updateBossHealth(float normalizadHealth)
    {
        bossHealthBar.value = normalizadHealth;
    }

    //점수 시스템
    public void UpdateScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }
}
