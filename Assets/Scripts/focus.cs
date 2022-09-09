using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class focus : MonoBehaviour
{
    InputField m_inputfield;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        m_inputfield = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (m_inputfield.isFocused)
        {
            Debug.Log("fff");
        }
    }
}
