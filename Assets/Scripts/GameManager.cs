using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float minx = -5;
    public float maxX = 5;

    public float miny = -5;
    public float maxy = 5;

    public float minz = -5;
    public float maxz = 5;

    public bool isGameOver;
    private Material mat;
    public int score = 0;
    public bool hasBeenClick;
    public int lives = 3;

    private AudioSource sound;
    public AudioClip fail;
    public AudioClip plusPoint;
    public AudioClip gameOverSound;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI pointsText;
    public GameObject gameOverPanel;
    public GameObject startGamePanel;

    public GameObject explosionParticle;


    // Start is called before the first frame update
    void Start()
    {
        startGamePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        hasBeenClick = false;
        score = 0;
        lives = 3;
        livesText.text = $"Lives: {lives} ";
        pointsText.text = $"Points: {score} ";

        StartCoroutine(GenerateNextRandomPos());
        mat =GetComponent<MeshRenderer>().material;
        sound = GetComponent<AudioSource>();
      
    }


    private Vector3 GenerateRandomPos()
    {
        Vector3 pos = new Vector3(Random.Range(minx, maxX), Random.Range(miny, maxy), Random.Range(minz, maxz));

        return pos;
    }

    private float RandomSecond()
    {
        float randomSec = Random.Range(1f, 2.5f);
        return randomSec;
    }
        

    private IEnumerator GenerateNextRandomPos()
    {
        yield return new WaitForSeconds(2);
        startGamePanel.SetActive(false);
        while (!isGameOver)
        {
            yield return new WaitForSeconds(RandomSecond());
            if (!hasBeenClick)
            {
                lives--;
                livesText.text = $"Lives: {lives} ";
                sound.PlayOneShot(fail, 1.0f);
                if (lives == 0)
                {
                    isGameOver = true;
                    mat.color = Color.red;
                    GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
                    sound.PlayOneShot(gameOverSound, 1.0f);
                    gameOverPanel.SetActive(true);
                    break;
                }
            }

            transform.position =  GenerateRandomPos();
            mat.color = Color.blue;
            hasBeenClick = false;
        }
        
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Player") && !hasBeenClick && !isGameOver)
        {
            mat.color = Color.green;
            score++;
            pointsText.text = $"Points: {score} ";
            sound.PlayOneShot(plusPoint, 1.0f);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            hasBeenClick = true;
        }
        
    }

    public void ClickFail()
    {
        if (!hasBeenClick)
        {
            lives--;
            livesText.text = $"Lives: {lives} ";
            sound.PlayOneShot(fail, 1.0f);
            if (lives == 0)
            {
                isGameOver = true;
                mat.color = Color.red;
                GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
                sound.PlayOneShot(gameOverSound, 1.0f);
                gameOverPanel.SetActive(true);
                
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.
         GetActiveScene().name);
        startGamePanel.SetActive(true);
    }

}
