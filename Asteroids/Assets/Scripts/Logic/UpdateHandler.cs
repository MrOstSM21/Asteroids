using System;

namespace Assets.Scripts.Logic
{
    public class UpdateHandler
    {
        public event Action Update;
        public event Action StartGameActions;

        public void Init() => Update?.Invoke();
        public void StartActions() => StartGameActions?.Invoke(); 

    }
}
