namespace E_Learning.Models
{
    public class SuperCategory
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;
        //public List<Category>? Categories { get; set; }

        public SuperCategory() 
        {
            Id= Guid.NewGuid().ToString();

        }
    }
}
