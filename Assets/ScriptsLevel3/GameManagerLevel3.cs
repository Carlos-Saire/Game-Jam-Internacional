using TMPro;
using UnityEngine;
namespace Game3
{
    public class GameManagerLevel3 : StartableEntity
    {
        public static GameManagerLevel3 Instance;

        public AudioSource backgroundMusic;

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

            backgroundMusic = GetComponent<AudioSource>();
        }
        private void Start()
        {
            remainingTime = gameTime;
            UpdateTexts();
            timerText.text = "Time: " + Mathf.Ceil(remainingTime);
        }
        private void Update()
        {
            if (!isStartGame) return;


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
            gameStarted = true;
            remainingTime = gameTime;

            backgroundMusic.Play();
            timerText.text = "Time: " + Mathf.Ceil(remainingTime);
        }
        private void EndGame(bool win)
        {
            if (gameEnded) return;

            gameEnded = true;

            backgroundMusic.Stop();

            if(win)
            {
                GameManager.instance.Win();
            }
            else
            {
                GameManager.instance.Fail();
            }
        }
    }
}
