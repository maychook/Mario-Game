using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector3(1, 1, 1);

        // calculate the width and the higth
        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        // the unity's world height equals to the size of the camera multiplied by 2
        // in unity -  32 pixels is 1 world unit
        float worldHeight = Camera.main.orthographicSize * 2f; // in Unity's world space
        float worldWidth = worldHeight / Screen.height * Screen.width;

        Vector3 tempScale = transform.localScale;
        tempScale.x = worldWidth / width + 0.1f;
        tempScale.y = worldHeight / height + 0.1f;

        transform.localScale = tempScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
