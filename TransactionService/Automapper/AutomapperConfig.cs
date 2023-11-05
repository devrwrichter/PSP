using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stone.PSP.TransactionService.Automapper
{
    public static class AutomapperConfig
    {
        public static MapperConfiguration GetConfiguration()
        {
            return new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TransactionProfile());
            });
        }
    }
}
