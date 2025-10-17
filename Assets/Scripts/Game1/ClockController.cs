using System;
using UnityEngine;
namespace Game1
{
    public class ClockController : Items
    {
        [SerializeField] private float time;
        public static event Action<float> OnClock;
        protected override void CollisionPlayer()
        {
            base.CollisionPlayer();
            OnClock?.Invoke(time);
        }
    }
}

