using TMPro;
using UnityEngine;
namespace Game3
{
    public class GameManagerLevel3 : MonoBehaviour
    {
        public static GameManagerLevel3 Instance;

        [Header("UI")]
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI lifeText;
        public TextMeshProUGUI timerText;

        [Header("Game Settings")]
        public byte totalCandies = 10;
        public byte playerLives = 3;
        public float gameTime = 60f;

        private int score = 0;
        private float remainingTime;

        [Header("Game States")]
        [SerializeField] public bool gameStarted = false;
        [SerializeField] public bool gameEnded = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            remainingTime = gameTime;
            UpdateTexts();
            timerText.text = "Time: 0";
        }
        private void Update()
        {
            if (!gameStarted || gameEnded) return;

            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                remainingTime = 0; 
                EndGame(false);
            }

            timerText.text = "Tiempo: " + Mathf.Ceil(remainingTime);
        }
        private void UpdateTexts()
        {
            scoreText.text = "Caramelos: " + score + "/" + totalCandies;
            lifeText.text = "Salud: " + playerLives;
        }
        public void AddScore(int amount)
        {
            if (gameEnded || !gameStarted) return;

            score += amount;
            UpdateTexts();

            if (score >= totalCandies)
            {
                EndGame(true);
            }
        }
        public void ReduceLife(byte amount)
        {
            if (gameEnded || !gameStarted) return;

            playerLives -= amount;
            UpdateTexts();

            if (playerLives <= 0)
            {
                EndGame(false);
            }
        }
        public void StartGame()
        {
            // LLAMAS ACA AL TERMINAR LOS DIALOGOS DEL REY CALABAZA GameManagerLevel3.Instance.StartGame();
            gameStarted = true;
            remainingTime = gameTime;
            timerText.text = "Time: " + Mathf.Ceil(remainingTime);
            Debug.Log("JUEGO INICIA V:");
        }
        private void EndGame(bool win)
        {
            if (gameEnded) return;

            gameEnded = true;

            Debug.Log(win ? "GANASTE COMO YO EN LA VIDA" : "PERDISTE COMO HERNAN BARCOS PERDERA EN EL DOTA");
            // PONER CINEMATICA DE PERDER O Q WEBA
        }
    }
}
