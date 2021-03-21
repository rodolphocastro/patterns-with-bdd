using System;

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

    /// <summary>
    /// A Factory responsible for creating books from raw data.
    /// </summary>
    public class BookFactory : IDisposable
    {
        private Book _inProgress = null;
        public BookFactory() { }

        public void Dispose()
        {
            _inProgress = null;
        }

        /// <summary>
        /// Creates the book with all the required elements so far.
        /// </summary>
        /// <returns></returns>
        public Book Build()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the book's title.
        /// </summary>
        /// <param name="bookTitle"></param>
        public void WithTitle(string bookTitle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the book's author's name.
        /// </summary>
        /// <param name="authorName"></param>
        public void WithAuthorNamed(string authorName)
        {
            throw new NotImplementedException();
        }
    }
}
