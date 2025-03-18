namespace ModelBindingAndValidations
{
    public class Book
    {
        public int? BookId { get; set; }
        public string? Author { get; set; }

        public override string ToString()
        {
            return $"Book Object - Book id: {BookId}, Book Author: {Author}";
        }
    }
}
