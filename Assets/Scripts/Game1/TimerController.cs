using TMPro;
using UnityEngine;
namespace Game1
{
    public class TimerController : StartableEntity
    {
        private TMP_Text text;
        private float currentTime;
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
        }
        private void UpdateTime(float time)
        {
            currentTime += time;
        }
    }
}

