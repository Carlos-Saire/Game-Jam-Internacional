using System;
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
            text = GetComponent<TMP_Text>();
        }
        private void Start()
        {
            currentTime = initTime;
            text.text = "Tiempo: " + (int)currentTime;
        }
        private void Update()
        {
            if (!isStartGame || isFinish) return;

            currentTime -= Time.deltaTime;
            text.text = "Tiempo: " +((int)currentTime).ToString();

            if (!isGameFast && currentTime <= 10 && currentTime > 0)
            {
                isGameFast = true;
                OnGameSpeedIncreased?.Invoke(true);
            }
            else if(isGameFast && currentTime >= 10 && currentTime > 0)
            {
                isGameFast = false;
                OnGameSpeedIncreased?.Invoke(false);
            }
            else if (currentTime <= 0)
            {
                isFinish = true;
                currentTime = 0;
                text.text = "0";
                OnGameFinish?.Invoke();
            }
        }
        private void UpdateTime(float time)
        {
            currentTime += time;
        }
    }
}

