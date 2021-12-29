using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextGUI : MonoBehaviour
{
    public Dialog dialogAsset;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI characterNameText;
    public Image image;
    private int index;

    private void Start()
    {
        dialogText.text = dialogAsset.strings[0].line;
        characterNameText.text = dialogAsset.strings[0].characterName;
        image.sprite = dialogAsset.strings[0].image;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && index < dialogAsset.strings.Count - 1)
        {
            index++;
            UpdateDialogLine(index);
        }
    }

    private void UpdateDialogLine(int index)
    {
        dialogText.text = dialogAsset.strings[index].line;
        characterNameText.text = dialogAsset.strings[index].characterName;
        image.sprite = dialogAsset.strings[index].image;
    }
}
