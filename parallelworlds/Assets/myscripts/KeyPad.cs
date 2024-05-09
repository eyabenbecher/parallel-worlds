using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyPad : MonoBehaviour
{
    [SerializeField] private Text num;
    [SerializeField] private Animator animator;
    private string ans = "281233";

  public void Number(int number)
    {
        num.text += number.ToString();
    }
    public void Excute()
    {
        if (num.text == ans)
        {
            num.text = "CORRECT";
            num.color = Color.green;
            num.fontSize = 20;
            num.rectTransform.localPosition = Vector3.zero;
            animator.SetBool("open", true);
        }
        else
        {
            num.text = "INCORRECT";
            num.color = Color.red; 
            num.fontSize = 20;
            num.rectTransform.localPosition = Vector3.zero;
            StartCoroutine(ResetText());
        }
    }
    IEnumerator ResetText()
    {

        yield return new WaitForSeconds(1f);

        num.text = "";
        num.color = Color.white;
        num.fontSize = 36;
    }
}
