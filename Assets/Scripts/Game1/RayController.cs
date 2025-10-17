using System;
using UnityEngine;
namespace Game1
{
    public class RayController : Items
    {
        [SerializeField] private float increasespeed;
        public static event Action<float> OnSpeed;
        protected override void CollisionPlayer()
        {
            base.CollisionPlayer();
            OnSpeed?.Invoke(increasespeed);
        }
    }
}

