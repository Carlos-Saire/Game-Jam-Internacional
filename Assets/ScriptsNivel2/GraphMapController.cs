using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;


public class GraphMapController : MonoBehaviour
{
    [SerializeField] TextAsset GraphMap;
    [SerializeField] TextAsset ConnectionsMap;
    string[] arrayNodeConnectionsRows;
    string[] arrayNodeConnectionsColums;
    string[] arrayNodeRows;
    string[] arrayNodeColumns;
    [SerializeField] GameObject NodePrefab;
    DoubleCircleList<NodeControll> ListNodes = new DoubleCircleList<NodeControll>();
    [SerializeField] EnemyController[] arrayEnemys;


    //soy bajito
    private void Start()
    {

        OnDrawGraph();
        ConnectNodes();
        //SetInitialNode();
    }
    void OnDrawGraph()
    {
        GameObject currentNode;
        arrayNodeRows = GraphMap.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < arrayNodeRows.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(arrayNodeRows[i])) continue;
            arrayNodeColumns = arrayNodeRows[i].Split(";");
            string xString = arrayNodeColumns[0].Replace(',', '.').Trim();
            string yString = arrayNodeColumns[1].Replace(',', '.').Trim();
            float xPos = float.Parse(xString, CultureInfo.InvariantCulture);
            float yPos = float.Parse(yString, CultureInfo.InvariantCulture);
            currentNode = Instantiate(NodePrefab, new Vector2(xPos, yPos), transform.rotation);
            currentNode.name = "NODE" + i.ToString();
            ListNodes.AddAtEnd(currentNode.GetComponent<NodeControll>());
            currentNode.transform.SetParent(transform);
        }

    }

    void ConnectNodes()
    {
        arrayNodeConnectionsRows = ConnectionsMap.text.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < ListNodes.GetCount(); i++)
        {
            if (i >= arrayNodeConnectionsRows.Length) break;

            arrayNodeConnectionsColums = arrayNodeConnectionsRows[i].Split(';');

            for (int j = 0; j < arrayNodeConnectionsColums.Length; j++)
            {
                string indexStr = arrayNodeConnectionsColums[j].Trim();
                if (!string.IsNullOrEmpty(indexStr))
                {
                    ListNodes.GetValueAtPosition(i).AddAdjacentNode(ListNodes.GetValueAtPosition(int.Parse(indexStr)));
                }
            }

        }
    }

}