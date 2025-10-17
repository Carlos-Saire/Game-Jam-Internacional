using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        arrayNodeRows = GraphMap.text.Split('\n');
        for (int i = 0; i < arrayNodeRows.Length; i++)
        {
            arrayNodeColumns = arrayNodeRows[i].Split(";");
            currentNode = Instantiate(NodePrefab, new Vector2(float.Parse(arrayNodeColumns[0]),
                float.Parse(arrayNodeColumns[1])), transform.rotation);
            currentNode.name = "NODE" + i.ToString();
            ListNodes.AddAtEnd(currentNode.GetComponent<NodeControll>());
            currentNode.transform.SetParent(transform);
        }

    }

    void ConnectNodes()
    {

        arrayNodeConnectionsRows = ConnectionsMap.text.Split("\n");
        for (int i = 0; i < ListNodes.GetCount(); i++)
        {
            arrayNodeConnectionsColums = arrayNodeConnectionsRows[i].Split(";");
            for (int j = 0; j < arrayNodeConnectionsColums.Length - 1; j++)
            {
                ListNodes.GetValueAtPosition(i).AddAdjacentNode(ListNodes.GetValueAtPosition(int.Parse(arrayNodeConnectionsColums[j])));
            }

        }
    }
    void SetInitialNode()
    {
        for (int i = 0; i < arrayEnemys.Length; i++)
        {
            if (arrayEnemys[i].gameObject.tag == "Enemy1")
            {
                //int position = Random.Range(0, ListNodes.GetCount());
                arrayEnemys[i].SetNewPosition(ListNodes.GetValueAtPosition(23).gameObject.transform.position);
            }
            else if (arrayEnemys[i].gameObject.tag == "Enemy2")
            {
                arrayEnemys[i].SetNewPosition(ListNodes.GetValueAtPosition(27).gameObject.transform.position);
            }
        }
    }


    private void OnEnable()
    {
        EnemyController.OnTimeisOver += SetInitialNode;
    }

    private void OnDisable()
    {
        EnemyController.OnTimeisOver -= SetInitialNode;
    }
}


