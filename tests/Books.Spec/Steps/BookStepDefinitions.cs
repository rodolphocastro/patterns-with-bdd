using Books.Lib.Entities;

using System;
using System.Collections.Generic;

using TechTalk.SpecFlow;

using Xunit;

namespace Books.Spec.Steps
{
    /// <summary>
    /// Wrapper for the Book's feature context.
    /// </summary>
    public sealed class BookFeatureContext
    {
        private readonly FeatureContext _context;

        public BookFeatureContext(FeatureContext context)
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
    }

    [Binding]
    public sealed class BookStepDefinitions
    {
        private readonly BookFeatureContext _wrappedContext;
        private Book _subject = null;

        public BookStepDefinitions(FeatureContext context)
        {
            _wrappedContext = new BookFeatureContext(context);
        }

        [Given(@"the author's name is ""(.*)""")]
        public void GivenTheAuthorSNameIs(string authorName)
        {
            _wrappedContext.AuthorName = authorName;
        }

        [Given(@"the book's title is ""(.*)""")]
        public void GivenTheBookSTitleIs(string bookTitle)
        {
            _wrappedContext.BookTitle = bookTitle;
        }

        [Given(@"the book is created")]
        public void GivenTheBookIsCreated()
        {
            _subject = new Book(_wrappedContext.AuthorName, _wrappedContext.BookTitle);
        }


        [When(@"the book is created")]
        public void WhenTheBookIsCreated()
        {
            _subject = new Book(_wrappedContext.AuthorName, _wrappedContext.BookTitle);
        }

        [When(@"the book is renamed to ""(.*)""")]
        public void WhenTheBookIsRenamedTo(string newTitle)
        {
            _subject.RenameTo(newTitle);
        }

        [When(@"the factory creates a book")]
        public void WhenTheFactoryCreatesABook()
        {
            using BookFactory bookFactory = new BookFactory();
            bookFactory.WithTitle(_wrappedContext.BookTitle);
            bookFactory.WithAuthorNamed(_wrappedContext.AuthorName);
            _subject = bookFactory.Build();
        }

        [Then(@"the book should not be null")]
        public void ThenTheBookShouldNotBeNull()
        {
            Assert.NotNull(_subject);
        }

        [Then(@"the book's title should match")]
        public void ThenTheBookSTitleShouldMatch()
        {
            Assert.Equal(_wrappedContext.BookTitle, _subject.Title);
        }

        [Then(@"the book's title should not match")]
        public void ThenTheBookSTitleShouldNotMatch()
        {
            Assert.NotEqual(_wrappedContext.BookTitle, _subject.Title);
        }

        [Then(@"the book author's name should match")]
        public void ThenTheBookSNameShouldMatch()
        {
            Assert.Equal(_wrappedContext.AuthorName, _subject.AuthorName);
        }

    }
}
