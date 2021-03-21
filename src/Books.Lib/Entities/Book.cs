namespace Books.Lib.Entities
{
    /// <summary>
    /// A book written by an Author.
    /// </summary>
    public class Book
    {
        public Book(string authorsName, string booksTitle)
        {
            AuthorName = authorsName;
            Title = booksTitle;
        }

        public string AuthorName { get; }
        public string Title { get; protected set; }

        public void RenameTo(string newTitle)
        {
            Title = newTitle;
        }
    }
}
