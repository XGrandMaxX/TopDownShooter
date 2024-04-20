using UnityEngine;

namespace Game.Scripts.Systems
{
    public static class TimeScaleController
    {
        private const byte BASE_TIME_SCALE = 1;
        
        public static void PauseGame() => Time.timeScale = 0;
        
        public static void UnpauseGame() => Time.timeScale = BASE_TIME_SCALE;
        
        public static void AcceleratedTime(byte timeMultiplier) 
            => Time.timeScale = BASE_TIME_SCALE * timeMultiplier;
    }
}
