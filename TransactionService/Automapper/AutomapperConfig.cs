using AutoMapper;

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
