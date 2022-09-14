using System;

namespace Assets.Scripts.Logic
{
    public class UpdateHandler
    {
        public event Action Update;
        public event Action StartGameActions;
        public event Action EndGame;

        public void Init() => Update?.Invoke();
        public void StartActions() => StartGameActions?.Invoke(); 
        public void FinishGame() => EndGame?.Invoke(); 

    }
}
