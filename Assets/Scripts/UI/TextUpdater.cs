using UnityEngine;
using TMPro;
public class TextUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text _text;

    public void UpdateText(string newText)
    {
        _text.text = newText;
    }
}