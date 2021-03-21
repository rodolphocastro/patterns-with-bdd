﻿using Books.Lib.Entities;

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
        public DateTimeOffset PublicationDate 
        { 
            get => (DateTimeOffset) _context.GetValueOrDefault(nameof(PublicationDate)); 
            set => _context[nameof(PublicationDate)] = value; 
        }
        public int PublicationVersion 
        { 
            get => (int) _context.GetValueOrDefault(nameof(PublicationVersion)); 
            set => _context[nameof(PublicationVersion)] = value; 
        }
    }

    [Binding]
    public sealed class BookStepDefinitions
    {
        private readonly BookFeatureContext _wrappedContext;
        private BookFactory _factory;
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

        [Given(@"the publication date is ""(.*)""")]
        public void GivenThePublicationDateIs(string dateUtc)
        {
            _wrappedContext.PublicationDate = DateTimeOffset.Parse(dateUtc);
        }

        [Given(@"the publication version is (.*)")]
        public void GivenThePublicationVersionIs(int version)
        {
            _wrappedContext.PublicationVersion = version;
        }

        [Given(@"the book is created")]
        public void GivenTheBookIsCreated()
        {
            _subject = new UnpublishedBookFactory(_wrappedContext.AuthorName, _wrappedContext.BookTitle).Build();
        }

        [Given(@"an unpublished book factory")]
        public void GivenAnUnpublishedBookFactory()
        {
            _factory = new UnpublishedBookFactory(_wrappedContext.AuthorName, _wrappedContext.BookTitle);
        }

        [Given(@"a published book factory")]
        public void GivenAPublishedBookFactory()
        {
            _factory = new PublishedBookFactory(
                _wrappedContext.AuthorName,
                _wrappedContext.BookTitle,
                _wrappedContext.PublicationVersion,
                _wrappedContext.PublicationDate);
        }

        [When(@"the book is created")]
        public void WhenTheBookIsCreated()
        {
            GivenTheBookIsCreated();
        }

        [When(@"the book is renamed to ""(.*)""")]
        public void WhenTheBookIsRenamedTo(string newTitle)
        {
            _subject.RenameTo(newTitle);
        }

        [When(@"the factory creates a book")]
        public void WhenTheFactoryCreatesABook()
        {
            _subject = _factory.Build();
        }

        [When(@"the book is published")]
        public void WhenTheBookIsPublished()
        {
            _subject.Publish(_wrappedContext.PublicationDate, true);
        }

        [When(@"the published date is set")]
        public void WhenThePublishedDateIsSet()
        {
            _subject.Publish(_wrappedContext.PublicationDate, false);
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

        [Then(@"the book's published date should match")]
        public void ThenTheBookSPublishedDateShouldMatch()
        {
            Assert.Equal(_wrappedContext.PublicationDate, _subject.PublishedOn);
        }

        [Then(@"the book's revision should be (.*)")]
        public void ThenTheBookSRevisionShouldBe(int p0)
        {
            Assert.Equal(p0, _subject.Version);
        }

        [Then(@"the book's published date should be null")]
        public void ThenTheBookSPublishedDateShouldBeNull()
        {
            Assert.Equal(DateTimeOffset.MinValue, _subject.PublishedOn);
        }

    }
}
