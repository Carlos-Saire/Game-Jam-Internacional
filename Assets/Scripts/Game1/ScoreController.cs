using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
namespace Game1
{
    
    public class ScoreController : MonoBehaviour
    {
        private TMP_Text text;
        private int currentScore;

        [DllImport("__Internal")]
        private static extern void SetCandys(string text);
        private void Awake()
        {
        }
        private void Start()
        {
            currentScore = 0;
            string gaaa = "Puntos: " + currentScore + "/70";
                #if UNITY_WEBGL && !UNITY_EDITOR
                                       
                   SetCandys(gaaa);
                #endif

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
            string gaaa = "Puntos: " + currentScore + "/70";
            #if UNITY_WEBGL && !UNITY_EDITOR
                                       
                       SetCandys(gaaa);
            #endif
            this.score.Score = currentScore;
        }
    }
}

