using _3._Scripts.UI.Manager;
using DG.Tweening;

namespace _3._Scripts.UI.Components
{
    public class HealthWidget: TextWidget
    {
        private void OnEnable()
        {
            OnChange(HealthManager.HealthCount, HealthManager.HealthCount);
            HealthManager.OnChanged += OnChange;
        }
        
        private void OnDisable()
        {
            HealthManager.OnChanged -= OnChange;
        }

        protected override void OnChange(int oldValue, int newValue)
        {
            Text.DOCounter(oldValue, newValue, 0.15f).OnUpdate(() =>
            {
                Text.text += " / 150";
            });
        }
    }
}