using System.Collections.Generic;
using Entities;

namespace Functionality.Car
{
    public class CarLogic
    {
        private List<GameEntity> _entities;

        public CarLogic(List<GameEntity> entities)
        {
            _entities = entities;
        }

        public void FixedUpdate()
        {
            if (_entities.Count < 2)
            {
                return;
            }
            
            // DELETE ASA STATE MACHINE IS IMPLEMENTED
            DoSomething();
        }

        
        // DELETE ASA STATE MACHINE IS IMPLEMENTED
        private void DoSomething()
        {
            
        }
    }
}