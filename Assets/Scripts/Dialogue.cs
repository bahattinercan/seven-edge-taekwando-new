using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private string[] lines;
    [SerializeField] float textDelay;
    private int index;
    public static Dialogue instantiate;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        text.text = string.Empty;
        if (instantiate==null)
        {
            instantiate = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (text.text == lines[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
    }

    public void StartDialogue(string[] linesParam)
    {

        lines = linesParam;
        index = 0;
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textDelay);
        }
    }

    void nextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else {
            gameObject.SetActive(false);
            text.text = string.Empty;
        }
    }
}
