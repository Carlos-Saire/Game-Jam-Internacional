using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
namespace Game1
{
    public class TimerController : StartableEntity
    {
        public static event Action<bool> OnGameSpeedIncreased;
        public static event Action OnGameFinish;

        [SerializeField] private float initTime;
        private TMP_Text text;
        private float currentTime;
        private bool isGameFast;
        private bool isFinish;
        [DllImport("__Internal")]
        private static extern void SetTime(string text);
        protected override void OnEnable()
        {
            base.OnEnable();
            ClockController.OnClock += UpdateTime;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            ClockController.OnClock -= UpdateTime;
        }
        private void Awake()
        {
        }
        private void Start()
        {
            currentTime = initTime;
            string gaaa = "Tiempo: " + (int)currentTime;
            #if UNITY_WEBGL && !UNITY_EDITOR
                        SetTime(gaaa);
            #endif
        }
        private void Update()
        {
            if (!isStartGame || isFinish) return;

            currentTime -= Time.deltaTime;

            string gaaa = "Tiempo: " +((int)currentTime).ToString();
            #if UNITY_WEBGL && !UNITY_EDITOR
                                    SetTime(gaaa);
            #endif

            if (!isGameFast && currentTime <= 30 && currentTime > 0)
            {
                isGameFast = true;
                OnGameSpeedIncreased?.Invoke(true);
            }
            else if(isGameFast && currentTime >= 30 && currentTime > 0)
            {
                isGameFast = false;
                OnGameSpeedIncreased?.Invoke(false);
            }
            else if (currentTime <= 0)
            {
                isFinish = true;
                currentTime = 0;
                string gaaaa = "Tiempo: 0";
                #if UNITY_WEBGL && !UNITY_EDITOR
                          SetTime(gaaaa);
                #endif
                OnGameFinish?.Invoke();
            }
        }
        private void UpdateTime(float time)
        {
            currentTime += time;
        }
    }
}

