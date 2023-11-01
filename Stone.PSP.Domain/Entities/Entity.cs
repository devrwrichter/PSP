namespace Stone.PSP.Domain.Entities
{
    public abstract record Entity
    {
        public  Guid Id { get; set; }

        public Entity()
        {
               Id = Guid.NewGuid();
        }
    }
}
