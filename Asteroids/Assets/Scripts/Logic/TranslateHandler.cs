using Assets.Scripts.View;
using System.Collections.Generic;

namespace Assets.Scripts.Logic
{
    public class TranslateHandler
    {
        public int Lang { get; private set; }

        private readonly TranslateView _translateView;
        private readonly UpdateHandler _updateHandler;
        private Dictionary<Translate, string> _ru;
        private Dictionary<Translate, string> _en;



        public TranslateHandler(TranslateView translateView, UpdateHandler updateHandler)
        {
            _translateView = translateView;
            _updateHandler = updateHandler;
            CreateTranslate();
            Subscribe();
        }
        private void CreateTranslate()
        {
            _en = new Dictionary<Translate, string>()
            {
                {Translate.NewGame,"New game" },
                {Translate.Help,"Help" },
                {Translate.Position,"Position" },
                {Translate.Rotation,"Rotation" },
                {Translate.Speed,"Speed" },
                {Translate.LaserAmmo,"Laser ammo" },
                {Translate.Reloading,"Reloading" },
                {Translate.YourScore,"Your score:" },
                {Translate.BestScore,"Best score:" },
                {Translate.TryAgain,"Try again" },
                {Translate.RewardHealth,"Reward health" }
            };

            _ru = new Dictionary<Translate, string>()
                {
                {Translate.NewGame,"����� ����" },
                {Translate.Help,"���������" },
                {Translate.Position,"�������" },
                {Translate.Rotation,"��������" },
                {Translate.Speed,"��������" },
                {Translate.LaserAmmo,"������ ������" },
                {Translate.Reloading,"�����������" },
                {Translate.YourScore,"��� ����:" },
                {Translate.BestScore,"������ ����:" },
                {Translate.TryAgain,"������ ������" },
                {Translate.RewardHealth,"��������� �����" }
            };
        }
        public void SetTranslateRu()
        {
            Lang = 0;
            _translateView.SetTranslate(_ru);
        }
        public void SetTranslateEn()
        {
            Lang = 1;
            _translateView.SetTranslate(_en);
        }
        private void Subscribe()
        {
            _translateView.SetRU += SetTranslateRu;
            _translateView.SetEN += SetTranslateEn;
            _updateHandler.EndGame += EndGame;
        }

        private void EndGame() => Unsubscribe();

        private void Unsubscribe()
        {
            _translateView.SetRU -= SetTranslateRu;
            _translateView.SetEN -= SetTranslateEn;
            _updateHandler.EndGame -= EndGame;
        }
    }

}
