using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnObjectByPropertiesList : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject prefab;
    [SerializeField] private ScriptableObject[] properties;


    [ContextMenu(nameof(SpawnEditMode))]
    public void SpawnEditMode()
    {
        if (Application.isPlaying == true) return;

        GameObject[] allObgect = new GameObject[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            allObgect[i] = parent.GetChild(i).gameObject;
        }

        for (int i = 0; i < allObgect.Length; i++)
        {
            DestroyImmediate(allObgect[i]);
        }

        for (int i = 0; i < properties.Length; i++)
        {
            GameObject go = Instantiate(prefab, parent);
            IScriptableObjectProperty scriptableObjectProperty = go.GetComponent<IScriptableObjectProperty>();
            scriptableObjectProperty.ApplyProperty(properties[i]);
        }
    }


}
