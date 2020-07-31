using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTimer : MonoBehaviour
{
    public float timeStart = 0;
    public Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = timeStart.ToString();   
    }

    // Update is called once per frame
    void Update()
    {
        timeStart += Time.deltaTime;
        var sec = Mathf.Round(Mathf.Round(timeStart) % 60);
        var min = (int)(Mathf.Round(timeStart) / 60);
        
        string secText = sec < 9 ? "0" + sec.ToString() : sec.ToString();
        string minText = min < 9 ? "0" + min.ToString() : min.ToString();
        textBox.text = minText + ":" + secText;
    }
}
