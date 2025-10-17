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
            text.text = "" + (int)currentTime;
        }
        private void Update()
        {
            if (!isStartGame) return;
            currentTime -= Time.deltaTime;
            text.text = "" + (int)currentTime;

            if (!isFinish)
            {
                if (currentTime <= 0)
                {
                    isFinish = true;
                    OnGameFinish?.Invoke();

                }
                else if (currentTime <= 10 && !isGameFast)
                {
                    isGameFast = true;
                    OnGameSpeedIncreased?.Invoke(isGameFast);
                }
                else
                {
                    isGameFast = false;
                    OnGameSpeedIncreased?.Invoke(isGameFast);
                }
            }
           
        }
        private void UpdateTime(float time)
        {
            currentTime += time;
        }
    }
}

