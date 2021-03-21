using Books.Lib.Entities;

using System;
using System.Collections.Generic;

using TechTalk.SpecFlow;

namespace Books.Spec.Drivers
{
    /// <summary>
    /// Wrapper for the Book and its related data.
    /// </summary>
    public sealed class BookFactoryDriver
    {
        private readonly ScenarioContext _context;
        private readonly BookDataDriver _bookDriver;

        public BookFactoryDriver(ScenarioContext context, BookDataDriver bookDriver)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _bookDriver = bookDriver ?? throw new ArgumentNullException(nameof(bookDriver));
        }

        public BookFactory FactorySubject
        {
            get => _context.GetValueOrDefault(nameof(FactorySubject)) as BookFactory;
            set => _context[nameof(FactorySubject)] = value;
        }

        public Book FactoryResult
        {
            get => _bookDriver.BookSubject;
            set => _bookDriver.BookSubject = value;
        }

        internal BookFactory CreatePublishedFactory()
        {
            return new PublishedBookFactory(
                _bookDriver.AuthorName,
                _bookDriver.BookTitle,
                _bookDriver.PublicationVersion,
                _bookDriver.PublicationDate);
        }

        internal BookFactory CreateUnpublishedFactory()
        {
            return new UnpublishedBookFactory(_bookDriver.AuthorName, _bookDriver.BookTitle);
        }
    }
}
