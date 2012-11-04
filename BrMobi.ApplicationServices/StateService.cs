using System.Collections.Generic;
using BrMobi.ApplicationServices.ServiceInterfaces;
using BrMobi.Core;
using BrMobi.Core.RepositoryInterfaces;

namespace BrMobi.ApplicationServices
{
    public class StateService : IStateService
    {
        private readonly IStateRepository stateRepository;

        public StateService(IStateRepository stateRepository)
        {
            this.stateRepository = stateRepository;
        }

        public IList<State> ListAll()
        {
            return stateRepository.ListAll();
        }
    }
}