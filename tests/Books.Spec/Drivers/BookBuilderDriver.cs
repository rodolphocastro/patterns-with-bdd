using Books.Lib.Entities;

using System;
using System.Collections.Generic;

using TechTalk.SpecFlow;

namespace Books.Spec.Drivers
{
    public sealed class BookBuilderDriver
    {
        private readonly ScenarioContext _context;
        private readonly BookDataDriver _bookDriver;

        public BookBuilderDriver(ScenarioContext context, BookDataDriver bookDriver)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _bookDriver = bookDriver ?? throw new ArgumentNullException(nameof(bookDriver));
        }

        public UnpublishedBookBuilder UnpublishedBuilder
        {
            get
            {
                if (!_context.ContainsKey(nameof(UnpublishedBuilder)))
                {
                    UnpublishedBuilder = new UnpublishedBookBuilder();
                }

                return _context.GetValueOrDefault(nameof(UnpublishedBuilder)) as UnpublishedBookBuilder;
            }
            set => _context[nameof(UnpublishedBuilder)] = value;
        }

        internal void ApplyTitle()
        {
            BuilderSubject.Named(_bookDriver.BookTitle);
        }

        internal void ApplyAuthor()
        {
            BuilderSubject.WrittenBy(_bookDriver.AuthorName);
        }

        public BookBuilder BuilderSubject
        {
            get => _context.GetValueOrDefault(nameof(BuilderSubject)) as BookBuilder;
            set => _context[nameof(BuilderSubject)] = value;
        }
        public Book BuilderResult
        {
            get => _bookDriver.BookSubject;
            set => _bookDriver.BookSubject = value;
        }

        internal void ApplyPublicationDate()
        {
            PublishedBuilder.PublishedOn(_bookDriver.PublicationDate);
        }

        internal void ApplyPublicationVersion()
        {
            PublishedBuilder.WithVersion(_bookDriver.PublicationVersion);
        }

        public PublishedBookBuilder PublishedBuilder
        {
            get
            {
                if (!_context.ContainsKey(nameof(PublishedBuilder)))
                {
                    PublishedBuilder = new PublishedBookBuilder();
                }

                return _context.GetValueOrDefault(nameof(PublishedBuilder)) as PublishedBookBuilder;
            }
            set => _context[nameof(PublishedBuilder)] = value;
        }

        internal Book ApplyBuild()
        {
            return BuilderSubject.Apply();
        }
    }
}
