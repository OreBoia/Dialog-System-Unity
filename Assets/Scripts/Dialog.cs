using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DialogTest.asset", menuName = "Dialog")]

public class Dialog : ScriptableObject
{
    //[SerializeField] public string[] dialogLine;

    [System.Serializable]
    public class DialogString
    {
        //public int index;
        public Sprite image;
        public string line;
        public string characterName;
    }

    public List<DialogString> strings = new List<DialogString>();

   /* public string GetString(int index)
    {
        DialogString stringFound = strings.Find(s => s.index == index);
        return stringFound != null ? stringFound.line : $"{index} ??";
    }*/
}
