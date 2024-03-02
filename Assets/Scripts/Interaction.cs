using UnityEngine;
using System;
using TMPro;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title = null;
    [SerializeField]
    private TextMeshProUGUI _yes = null;
    [SerializeField]
    private TextMeshProUGUI _no = null;

    private Action _yesCallback = null;
    private Action _noCallback = null;

    public void SetInteraction(string title, string yesText, string noText, Action yesCallback, Action noCallback)
    {
        _title.text = title;
        _yes.text = yesText;
        _no.text = noText;

        _yesCallback = yesCallback;
        _noCallback = noCallback;
    }

    public void OnYes()
    {
        _yesCallback.Invoke();
    }

    public void OnNo()
    {
        _noCallback.Invoke();
    }
}
