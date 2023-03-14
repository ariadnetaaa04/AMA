using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Script : MonoBehaviour
{
    public float minx = -5;
    public float maxX = 5;

    public float miny = -5;
    public float maxy = 5;

    public float minz = -5;
    public float maxz = 5;

    public bool isGameOver;
    private Material mat;
    public int points = 0;
    public bool hasBeenClick;
    public int lives = 3;

    public AudioSource sound;
    public AudioClip crash;
    public AudioClip point;

    public TextMeshProUGUI text;


    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        hasBeenClick = false;
        lives = 3;
        text.text = $"Lives: {lives} ";

        StartCoroutine(GenerateNextRandomPos());
        mat=GetComponent<MeshRenderer>().material;
       // sound = GetComponent<AudioSource>();
      
    }


    private Vector3 GenerateRandomPos()
    {
        Vector3 pos = new Vector3(Random.Range(minx, maxX), Random.Range(miny, maxy), Random.Range(minz, maxz));

        return pos;
    }

    private IEnumerator GenerateNextRandomPos()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(2);
            if (!hasBeenClick)
            {
                lives--;
                text.text = $"Lives: {lives} ";
                //sound.PlayOneShot(crash, 1.0f);
                if (lives == 0)
                {
                    isGameOver = true;
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
        if (!hasBeenClick)
        {
            mat.color = Color.green;
            points++;
           // sound.PlayOneShot(point, 1.0f);
            hasBeenClick = true;
        }
        
    }

}
