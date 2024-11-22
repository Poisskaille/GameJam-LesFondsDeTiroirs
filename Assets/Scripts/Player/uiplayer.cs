using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class uiplayer : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] Image[] image;
    [SerializeField] float inactiveAlpha = 0.3f; // Transparence quand inactif
    [SerializeField] float activeAlpha = 1f; // Transparence quand actif
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(joystick.Horizontal) > 0.01f || Mathf.Abs(joystick.Vertical) > 0.01f)
        {
            SetAlpha(activeAlpha); // Rendre le joystick opaque
        }
        else
        {
            SetAlpha(inactiveAlpha); // Rendre le joystick transparent
        }

    }
    void SetAlpha(float alpha)
    {

        foreach (Image image in image)
        {
            if (image != null) // Vérifie que l'image existe
            {
                Color color = image.color;
                color.a = alpha;
                image.color = color;
            }
        }
    }
}
