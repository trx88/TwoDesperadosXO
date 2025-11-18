using UnityEngine;

namespace Services
{
    public class CounterServiceRunner : MonoBehaviour
    {
        private CounterService _counterService;

        public void Initialize(CounterService service)
        {
            _counterService = service;
        }

        private void Update()
        {
            _counterService?.Tick(Time.deltaTime);
        }
    }
}
