using TMPro;
using UnityEngine;
namespace Game1
{
    
    public class ScoreController : MonoBehaviour
    {
        private TMP_Text text;
        private int currentScore;
        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }
        private void Start()
        {
            currentScore = 0;
            text.text = "Puntos: " + currentScore;
        }
        [SerializeField] private ScoreSO score;
        private void OnEnable()
        {
            Items.OnScore += UpdateScore;
        }
        private void OnDisable()
        {
            Items.OnScore -= UpdateScore;
        }
        private void UpdateScore(int score)
        {
            currentScore += score;
            text.text = "Puntos: " + currentScore;
            this.score.Score = currentScore;
        }
    }
}

