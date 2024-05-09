using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Decode : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text decodedText;
    private string code = "itperuc";

    private void Start()
    {
        decodedText.gameObject.SetActive(false);
        inputField.onValueChanged.AddListener(delegate { CheckText(); });
    }

    public void CheckText()
    {
        string input = inputField.text.Trim();

        if (input.Equals(code, System.StringComparison.OrdinalIgnoreCase))
        {
           
            decodedText.gameObject.SetActive(true);
        }
        else
        {
            
            decodedText.gameObject.SetActive(false);
        }
    }
}
