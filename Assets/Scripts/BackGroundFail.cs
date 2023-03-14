using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFail : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Background") && !gameManager.isGameOver)
        {
            gameManager.ClickFail();
        }

    }
}
