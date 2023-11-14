using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingLoader : MonoBehaviour
{
    [SerializeField] private Setting[] allSetting;


    private void Awake()
    {
        for (int i = 0; i < allSetting.Length; i++)
        {
            allSetting[i].Load();
            allSetting[i].Apply();
        }
    }


}
