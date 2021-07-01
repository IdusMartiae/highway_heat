using System;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Game Configuration", fileName = "GameConfiguration")]
    public class GameConfiguration : ScriptableObject
    {
        [SerializeField] private bool paused;

        public bool Paused
        { get; set; }

        private void OnEnable()
        {
            Paused = paused;
        }
    }
}