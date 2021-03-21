using System;

namespace Books.Lib.Entities
{
    /// <summary>
    /// A book written by an Author.
    /// </summary>
    public class Book
    {
        protected BookPublishState _publishingState = new();
        public Book(string authorsName, string booksTitle)
        {
            AuthorName = authorsName;
            Title = booksTitle;
        }

        public string AuthorName { get; protected set; }
        public string Title { get; protected set; }
        public DateTimeOffset PublishedOn => _publishingState.RevisionDate;
        public int Version => _publishingState.CurrentVersion;

        public void RenameTo(string newTitle)
        {
            Title = newTitle;
        }

        public void Publish(DateTimeOffset publicationDate, bool bumpEdition = true)
        {
            if (publicationDate > DateTimeOffset.Now)
            {
                throw new ArgumentException("A book cannot be published on the Future", nameof(publicationDate));
            }

            _publishingState = bumpEdition ?
                _publishingState.Bump(publicationDate) :
                _publishingState.Revise(publicationDate);
        }

        /// <summary>
        /// Publication status of a Book.
        /// </summary>
        protected struct BookPublishState
        {
            public BookPublishState(int currentVersion = 0, DateTimeOffset? revisionDate = null)
            {
                CurrentVersion = currentVersion;
                RevisionDate = revisionDate ?? DateTimeOffset.MinValue;
            }

            public BookPublishState(BookPublishState memento)
                : this(memento.CurrentVersion, memento.RevisionDate)
            {
            }

            /// <summary>
            /// Current revision.
            /// </summary>
            public int CurrentVersion { get; }
            
            /// <summary>
            /// When it was last revised.
            /// </summary>
            public DateTimeOffset RevisionDate { get; }

            /// <summary>
            /// Change the publication date and bump the current version.
            /// </summary>
            /// <param name="publishDate"></param>
            /// <returns></returns>
            public BookPublishState Bump(DateTimeOffset publishDate)
            {
                return new BookPublishState(CurrentVersion + 1, publishDate);
            }

            /// <summary>
            /// Change the publication date without bumping the current version.
            /// </summary>
            /// <param name="revisionDate"></param>
            /// <returns></returns>
            public BookPublishState Revise(DateTimeOffset revisionDate)
            {
                return new BookPublishState(CurrentVersion, revisionDate);
            }
        }
    }

    /// <summary>
    /// A Factory responsible for creating books from raw data.
    /// </summary>
    public class BookFactory : IDisposable
    {
        private Book _inProgress = null;
        private string _title;
        private string _authorName;

        public BookFactory() { }

        public void Dispose()
        {
            _inProgress = null;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Creates the book with all the required elements so far.
        /// </summary>
        /// <returns></returns>
        public virtual Book Build()
        {
            Validate();
            _inProgress ??= new Book(_authorName, _title);
            return _inProgress;
        }

        protected virtual void Validate()
        {
            // TODO: Validate specific business requirements
        }

        /// <summary>
        /// Sets the book's title.
        /// </summary>
        /// <param name="bookTitle"></param>
        public void WithTitle(string bookTitle)
        {
            if (string.IsNullOrWhiteSpace(bookTitle))
            {
                throw new ArgumentException("A book must have a title", nameof(bookTitle));
            }

            _title = bookTitle;
        }

        /// <summary>
        /// Sets the book's author's name.
        /// </summary>
        /// <param name="authorName"></param>
        public void WithAuthorNamed(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                throw new ArgumentException("A book must have an author", nameof(authorName));
            }

            _authorName = authorName;
        }
    }
}
