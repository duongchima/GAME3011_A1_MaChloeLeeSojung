using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject ResourcePanel;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => SetActive());
    }

    public void SetActive()
    {
        ResourcePanel.gameObject.SetActive(!ResourcePanel.activeSelf);
    }
}
