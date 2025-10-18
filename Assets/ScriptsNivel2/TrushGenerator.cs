
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrushGenerator : MonoBehaviour
{
    [SerializeField] GameObject trushPrefab;
    [SerializeField] private TextAsset txtMap;
    string[] arrayMapRows;
    string[] arrayMapColums;
    [SerializeField] int MinNumberInitialTrush, MaxNumberInitialTrush;
    [SerializeField] Vector2 PositionIntial;
    [SerializeField] Vector2 separation;

    private int candiesCreated = 0;
   

    public SimpleLinkList<GameObject> listTrush = new SimpleLinkList<GameObject>();

    public static event Action<int> OnCreatedCandiesInitials;

    private void Start()
    {
        OnDrawMap();
    }

 

  
    void OnDrawMap()
    {

        int index;
        int aux = 0;
        int numberMagic;
        GameObject currentMapPart;
        int numberTrush = UnityEngine.Random.Range(MinNumberInitialTrush, MaxNumberInitialTrush + 1);

        while (aux != numberTrush)
        {
            arrayMapRows = txtMap.text.Split("\n");
            for (int i = 0; i < arrayMapRows.Length; i++)
            {
                arrayMapColums = arrayMapRows[i].Split(";");
                for (int j = 0; j < arrayMapColums.Length; j++)
                {
                    index = int.Parse(arrayMapColums[j]);
                    numberMagic = UnityEngine.Random.Range(0, 100);
                    if (index == 0 && numberMagic < 1 && numberTrush != aux)
                    {
                        currentMapPart = Instantiate(trushPrefab, new Vector2(PositionIntial.x + j * separation.x,
                        PositionIntial.y - i * separation.y), transform.rotation);
                        listTrush.AddAtEnd(currentMapPart);
                        candiesCreated++;
                        aux++;
                        currentMapPart.transform.SetParent(this.transform);
                    }



                }
            }
        }

        OnCreatedCandiesInitials?.Invoke(candiesCreated);


    }

}