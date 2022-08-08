using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public FloatVal playerStamina;
    Slider _slider;
    Canvas _canvas;

    float _barFilledTime = 0;
    bool _barFilled;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = 1;
        _canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = playerStamina.Val;

        if(playerStamina.Val == 1 && !_barFilled)
        {
            _barFilled = true;
            _barFilledTime = Time.time;
        }
        else if(_canvas.enabled && playerStamina.Val == 1 && (Time.time - _barFilledTime) > 0.5f)
        {
            _canvas.enabled = false;
        }
        else if(playerStamina.Val < 1)
        {
            _barFilled = false;
            _canvas.enabled = true;
        }

    }
}
