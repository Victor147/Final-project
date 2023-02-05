namespace final_project.Data.Entities
{
    public class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}
