using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour
{
    public string savingTag;
    public Saving saving;
    public Text text;

    public void Start()
    {
        saving = GameObject.FindWithTag(savingTag).GetComponent<Saving>();
    }

    public void Update()
    {
        text.text = saving.data.currency.ToString();
    }
}
