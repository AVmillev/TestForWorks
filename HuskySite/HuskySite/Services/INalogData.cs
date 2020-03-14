using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuskySite.Services
{
    interface INalogData
    {
        Task<string> GetReceiptAsinch(string fd, string fn, string fp);
    }
}
