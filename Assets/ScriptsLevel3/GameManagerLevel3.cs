using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
namespace Game3
{
    public class GameManagerLevel3 : StartableEntity
    {
        public static GameManagerLevel3 Instance;

        public AudioSource backgroundMusic;

 

        [Header("Game Settings")]
        public byte totalCandies = 10;
        public byte playerLives = 3;
        public float gameTime = 60f;

        private int score = 0;
        private float remainingTime;

        [Header("Game States")]
        [SerializeField] public bool gameStarted = false;
        [SerializeField] public bool gameEnded = false;


        [DllImport("__Internal")]
        private static extern void SetTime(string text);

        [DllImport("__Internal")]
        private static extern void SetCandys(string text);

        [DllImport("__Internal")]
        private static extern void SetLife(string text);

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
            string tiempo = "Time: " + Mathf.Ceil(remainingTime);
            #if UNITY_WEBGL && !UNITY_EDITOR
                        SetTime(tiempo);
            #endif
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

            string tiempo = "Tiempo: " + Mathf.Ceil(remainingTime);
            #if UNITY_WEBGL && !UNITY_EDITOR
                                    SetTime(tiempo);
            #endif
        }
        private void UpdateTexts()
        {
            string caramelo = "Caramelos: " + score + "/" + totalCandies;
            #if UNITY_WEBGL && !UNITY_EDITOR
                        
                        SetCandys(caramelo);
            #endif
            string vida = "Salud: " + playerLives;
            #if UNITY_WEBGL && !UNITY_EDITOR
                        SetLife(vida);
            #endif
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
            string tiempo = "Time: " + Mathf.Ceil(remainingTime);
            #if UNITY_WEBGL && !UNITY_EDITOR
                                    SetTime(tiempo);
            #endif
        }
        private void EndGame(bool win)
        {
            if (gameEnded) return;

            gameEnded = true;

            backgroundMusic.Stop();

            if(win)
            {
                GameManager.instance.Win();
#if UNITY_WEBGL && !UNITY_EDITOR
                                                    SetTime("");
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
                        SetLife("");
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
                        
                        SetCandys("");
#endif
            }
            else
            {
                GameManager.instance.Fail();
#if UNITY_WEBGL && !UNITY_EDITOR
                                                    SetTime("");
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
                        SetLife("");
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
                        
                        SetCandys("");
#endif
            }
        }
    }
}
