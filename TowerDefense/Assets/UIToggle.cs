using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIToggle : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public Image displayImage;
    public Color minColor, maxColor;
    public Slider thisSlider;

    public void DisplayNumber(float value)
    {
        textBox.text = value.ToString();

        float proportion = value / thisSlider.maxValue;

        Color mixedColor = Color.Lerp(minColor, maxColor, proportion);

        displayImage.color = mixedColor;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePressed()
    {
        Debug.Log("TogglePressed");
    }

    public void TogglePressedBool(bool value)
    {
        if(value == true)
        {
            Debug.Log("TogglePressed = True");
        }
        else
        {
            Debug.Log("TogglePressed = False");
        }
    }

}
