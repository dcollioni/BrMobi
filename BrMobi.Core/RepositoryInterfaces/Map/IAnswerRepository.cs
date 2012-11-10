using BrMobi.Core.Map;
using System.Collections.Generic;

namespace BrMobi.Core.RepositoryInterfaces.Map
{
    public interface IAnswerRepository
    {
        Answer Get(int id);
        IList<Answer> List(int markerId);
    }
}