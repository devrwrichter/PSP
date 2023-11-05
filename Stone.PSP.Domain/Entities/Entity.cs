namespace Stone.PSP.Domain.Entities
{
    public abstract record Entity
    {
        public  Guid Id { get; set; }
        public DateTime CreateAt { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreateAt = DateTime.Now;
        }
    }
}
