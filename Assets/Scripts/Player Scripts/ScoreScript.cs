using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private Text coinTextScore;
    private AudioSource audioManager;
    private int scoreCount = 0;

    private void Awake()
    {
        audioManager = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        coinTextScore = GameObject.Find("CoinText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D target) 
    {
        // detect collision with coins
        if (target.tag == MyTags.COIN_TAG)
        {
            target.gameObject.SetActive(false);
            scoreCount++;
            coinTextScore.text = "x" + scoreCount;

            audioManager.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == MyTags.BONUSBOX_TAG)
        {
            scoreCount++;
            coinTextScore.text = "x" + scoreCount;

            audioManager.Play();
        }
    }
}
