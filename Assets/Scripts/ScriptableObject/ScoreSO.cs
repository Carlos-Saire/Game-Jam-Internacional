using UnityEngine;
[CreateAssetMenu(fileName = "ScoreSO", menuName = "Scriptable Objects/Score/ScoreSO", order = 1)]
public class ScoreSO : ScriptableObject
{
    [SerializeField] private int currentScore;
    public int Score { get { return currentScore; } set { currentScore = value; } }
}
