using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawImageCornerCurver : MonoBehaviour
{
    public RawImage rawImage;
    Texture baseTexture;

    // Start is called before the first frame update
    void Start()
    {
        baseTexture = rawImage.texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
