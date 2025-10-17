using TMPro;
using UnityEngine;
namespace Game1
{
    public class TimerController : StartableEntity
    {
        private TMP_Text text;
        private float currentTime;
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
    }
}

