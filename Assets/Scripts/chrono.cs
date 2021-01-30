using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chrono : MonoBehaviour
{
    public float time_start;
    public Text text_box;
    public int text_size;
    public Color text_color;
    public bool is_bold;

    bool timer_active = false;

    void Start()
    {
        timer_button();
        text_box.text = time_start.ToString("F2");
        text_box.fontSize = text_size;
        text_box.color = text_color;
        if (is_bold) text_box.fontStyle = UnityEngine.FontStyle.Bold;
    }

    // Update is called once per frame
    void Update()
    {
    	if (timer_active)
    	{
        	time_start += Time.deltaTime;
        	text_box.text = time_start.ToString("F2");
    	}
    }

    public void timer_button()
    {
    	timer_active = !timer_active;
    }
}
