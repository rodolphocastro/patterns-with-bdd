using Bogus;

using Books.Lib.Entities;

using System;
using System.Collections.Generic;
using System.Linq;

using TechTalk.SpecFlow;

namespace Books.Spec.Drivers
{
    /// <summary>
    /// Wrapper for the Book and its related data.
    /// </summary>
    public sealed class BookDataDriver : Faker
    {
        private readonly ScenarioContext _context;

        public BookDataDriver(ScenarioContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string AuthorName
        {
            get => _context.GetValueOrDefault(nameof(AuthorName)) as string;
            set => _context[nameof(AuthorName)] = value;
        }

        public string BookTitle
        {
            get => _context.GetValueOrDefault(nameof(BookTitle)) as string;
            set => _context[nameof(BookTitle)] = value;
        }
        public DateTimeOffset PublicationDate
        {
            get => (DateTimeOffset)_context.GetValueOrDefault(nameof(PublicationDate));
            set => _context[nameof(PublicationDate)] = value;
        }
        public int PublicationVersion
        {
            get => (int)_context.GetValueOrDefault(nameof(PublicationVersion));
            set => _context[nameof(PublicationVersion)] = value;
        }

        public Book BookSubject
        {
            get => _context.GetValueOrDefault(nameof(BookSubject)) as Book;
            set => _context[nameof(BookSubject)] = value;
        }

        public Book CreateUnpublished()
        {
            return new UnpublishedBook(AuthorName, BookTitle);
        }

        public Book CreatePublished()
        {
            return new PublishedBook(AuthorName, BookTitle, 
                new Book.BookPublishState(PublicationVersion, PublicationDate));
        }

        public string CreateRandomAuthor()
        {
            return Name.FullName();
        }

        public string CreateRandomTitle()
        {
            return string.Join(" ", Lorem.Words());
        }
    }
}
