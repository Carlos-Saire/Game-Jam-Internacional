using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeControll : MonoBehaviour
{
    public DoubleCircleList<NodeControll> listAdjacentsNodes = new DoubleCircleList<NodeControll>();

    public void AddAdjacentNode(NodeControll node)
    {
        listAdjacentsNodes.AddAtEnd(node);
    }

    public NodeControll GetAdjacentNode()
    {
        return listAdjacentsNodes.GetValueAtPosition(Random.Range(0, listAdjacentsNodes.GetCount()));
    }
}
