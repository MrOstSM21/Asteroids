using System;

namespace Assets.Scripts.Logic
{
    public class UpdateHandler
    {
        public event Action Update;

        public void Init() => Update?.Invoke();

    }
}
