using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    TimeBody _timeBody;
    [SerializeField] Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _timeBody = GetComponent<TimeBody>();
        _slider.maxValue = Mathf.Round(5f / Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = _timeBody.pointsInTime.Count;
    }
}
