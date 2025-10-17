using System;
using TMPro;
using UnityEngine;
namespace Game1
{
    public class TimerController : StartableEntity
    {
        public static event Action<bool> OnGameSpeedIncreased;

        private TMP_Text text;
        private float currentTime;
        private bool isGameFast;
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
        private void Update()
        {
            if (!isStartGame) return;
            currentTime += Time.deltaTime;
            text.text = "" + (int)currentTime;

            if (currentTime <= 10&&!isGameFast)
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
        private void UpdateTime(float time)
        {
            currentTime += time;
        }
    }
}

