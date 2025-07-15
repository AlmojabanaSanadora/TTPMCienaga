using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using TMPro;
public class LogicaCalidad : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int calidad;

    void Start()
    {
        calidad = PlayerPrefs.GetInt("NumeroDecalidad", 0);
        dropdown.value = calidad;
        AjustarCalidad();
    }

    public void AjustarCalidad()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("NumeroDecalidad", dropdown.value);
        calidad = dropdown.value;
    }
}
