using System;

namespace Books.Lib.Entities
{
    /// <summary>
    /// A book written by an Author.
    /// </summary>
    public class Book
    {
        protected internal BookPublishState _publishingState = new();
        internal Book()
        {

        }

        protected Book(string authorsName, string booksTitle)
        {
            AuthorName = authorsName;
            Title = booksTitle;
        }

        protected Book(string authorName, string title, BookPublishState publishStatus)
            : this (authorName, title)
        {
            _publishingState = publishStatus;
        }

        public string AuthorName { get; protected internal set; }
        public string Title { get; protected internal set; }
        public DateTimeOffset PublishedOn => _publishingState.RevisionDate;
        public int Version => _publishingState.CurrentVersion;

        public void RenameTo(string newTitle)
        {
            Title = newTitle;
        }

        public virtual void Publish(DateTimeOffset publicationDate, bool bumpEdition = true)
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
        public struct BookPublishState
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
            internal BookPublishState Bump(DateTimeOffset publishDate)
            {
                return new BookPublishState(CurrentVersion + 1, publishDate);
            }

            /// <summary>
            /// Change the publication date without bumping the current version.
            /// </summary>
            /// <param name="revisionDate"></param>
            /// <returns></returns>
            internal BookPublishState Revise(DateTimeOffset revisionDate)
            {
                return new BookPublishState(CurrentVersion, revisionDate);
            }
        }
    }

    public class UnpublishedBook : Book
    {
        public UnpublishedBook(string authorsName, string title)
            : base(authorsName, title)
        {
            _publishingState = new BookPublishState();
        }
    }

    public class PublishedBook : Book
    {
        public PublishedBook(string authorsName, string title, BookPublishState publishState) 
            : base(authorsName, title, publishState)
        {

        }
    }

    /// <summary>
    /// A Factory responsible for creating books from raw data.
    /// </summary>
    public abstract class BookFactory : IDisposable
    {
        protected readonly string authorsName;
        protected readonly string title;

        protected BookFactory(string authorsName, string title)
        {
            this.authorsName = authorsName;
            this.title = title;
        }

        /// <summary>
        /// Creates a book.
        /// </summary>
        /// <returns></returns>
        public abstract Book Build();

        public abstract void Dispose();
    }

    /// <summary>
    /// A Factory for books that weren't published.
    /// </summary>
    public class UnpublishedBookFactory : BookFactory
    {
        public UnpublishedBookFactory(string authorsName, string title) 
            : base(authorsName, title)
        {
        }

        public override Book Build()
        {
            return new UnpublishedBook(authorsName, title);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// A Factory for books that have been previously published.
    /// </summary>
    public class PublishedBookFactory : BookFactory
    {
        protected Book.BookPublishState publishState;

        public PublishedBookFactory(string authorsName, string title, int edition, DateTimeOffset publishedOn) 
            : base(authorsName, title)
        {
            if (edition <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(edition), "A published book must have an edition greater than zero");
            }

            if (publishedOn.ToUniversalTime() > DateTimeOffset.UtcNow)
            {
                throw new ArgumentException("A book can only be published on the Past", nameof(publishedOn));
            }

            publishState = new Book.BookPublishState(edition, publishedOn);
        }

        public override Book Build()
        {
            return new PublishedBook(authorsName, title, publishState);
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Builder for creating Books.
    /// </summary>
    public abstract class BookBuilder : IDisposable
    {
        protected struct BookBuilderState
        {
            public BookBuilderState(BookBuilderState originalState)
            {
                Title = originalState.Title;
                AuthorName = originalState.AuthorName;
            }

            public string Title { get; init; }
            public string AuthorName { get; init; }
        }

        private bool _isDisposed = false;
        protected BookBuilderState _state = new();
        protected Book _subject = new();

        protected BookBuilder()
        {

        }

        public virtual BookBuilder Named(string title)
        {
            _state = new BookBuilderState(_state)
            {
                Title = title
            };
            return this;
        }

        public virtual BookBuilder WrittenBy(string author)
        {
            _state = new BookBuilderState(_state)
            {
                AuthorName = author
            };
            return this;
        }

        public abstract Book Apply();

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _isDisposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BookBuilder()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Builder for creating Unpublished Books.
    /// </summary>
    public class UnpublishedBookBuilder : BookBuilder
    {
        public override UnpublishedBookBuilder Named(string title)
        {
            base.Named(title);
            return this;
        }

        public override UnpublishedBookBuilder WrittenBy(string author)
        {
            base.WrittenBy(author);
            return this;
        }

        public override UnpublishedBook Apply()
        {
            return new UnpublishedBook(_state.AuthorName, _state.Title);
        }
    }

    /// <summary>
    /// Builder for creating published books.
    /// </summary>
    public class PublishedBookBuilder : BookBuilder
    {
        public override PublishedBook Apply()
        {
            throw new NotImplementedException();
        }
    }
}
