using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MonkeyFinder.Model;

namespace MonkeyFinder.Service
{
    public interface IDataService
    {
        Task<IEnumerable<Monkey>> GetMonkeysAsync();
    }
}
