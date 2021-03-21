using Books.Lib.Entities;
using Books.Spec.Drivers;

using System;

using TechTalk.SpecFlow;

using Xunit;

namespace Books.Spec.Steps
{

    [Binding]
    public sealed class BookStepDefinitions
    {
        private readonly BookDataDriver _bookDataDriver;

        public BookStepDefinitions(BookDataDriver context)
        {
            _bookDataDriver = context;
        }

        [Given(@"the author's name is ""(.*)""")]
        public void GivenTheAuthorSNameIs(string authorName)
        {
            _bookDataDriver.AuthorName = authorName;
        }

        [Given(@"the book's title is ""(.*)""")]
        public void GivenTheBookSTitleIs(string bookTitle)
        {
            _bookDataDriver.BookTitle = bookTitle;
        }

        [Given(@"the publication date is ""(.*)""")]
        public void GivenThePublicationDateIs(string dateUtc)
        {
            _bookDataDriver.PublicationDate = DateTimeOffset.Parse(dateUtc);
        }

        [Given(@"the publication version is (.*)")]
        public void GivenThePublicationVersionIs(int version)
        {
            _bookDataDriver.PublicationVersion = version;
        }

        [Given(@"the book is created")]
        public void GivenTheBookIsCreated()
        {
            _bookDataDriver.BookSubject = _bookDataDriver.CreateUnpublished();
        }

        [When(@"the book is created")]
        public void WhenTheBookIsCreated()
        {
            _bookDataDriver.BookSubject = _bookDataDriver.CreateUnpublished();
        }

        [When(@"the book is renamed to ""(.*)""")]
        public void WhenTheBookIsRenamedTo(string newTitle)
        {
            _bookDataDriver.BookSubject.RenameTo(newTitle);
        }

        [When(@"the book is published")]
        public void WhenTheBookIsPublished()
        {
            _bookDataDriver.BookSubject.Publish(_bookDataDriver.PublicationDate, true);
        }

        [When(@"the published date is set")]
        public void WhenThePublishedDateIsSet()
        {
            _bookDataDriver.BookSubject.Publish(_bookDataDriver.PublicationDate, false);
        }


        [Then(@"the book should not be null")]
        public void ThenTheBookShouldNotBeNull()
        {
            Assert.NotNull(_bookDataDriver.BookSubject);
        }

        [Then(@"the book's title should match")]
        public void ThenTheBookSTitleShouldMatch()
        {
            Assert.Equal(_bookDataDriver.BookTitle, _bookDataDriver.BookSubject.Title);
        }

        [Then(@"the book's title should not match")]
        public void ThenTheBookSTitleShouldNotMatch()
        {
            Assert.NotEqual(_bookDataDriver.BookTitle, _bookDataDriver.BookSubject.Title);
        }

        [Then(@"the book author's name should match")]
        public void ThenTheBookSNameShouldMatch()
        {
            Assert.Equal(_bookDataDriver.AuthorName, _bookDataDriver.BookSubject.AuthorName);
        }

        [Then(@"the book's published date should match")]
        public void ThenTheBookSPublishedDateShouldMatch()
        {
            Assert.Equal(_bookDataDriver.PublicationDate, _bookDataDriver.BookSubject.PublishedOn);
        }

        [Then(@"the book's revision should be (.*)")]
        public void ThenTheBookSRevisionShouldBe(int p0)
        {
            Assert.Equal(p0, _bookDataDriver.BookSubject.Version);
        }

        [Then(@"the book's published date should be null")]
        public void ThenTheBookSPublishedDateShouldBeNull()
        {
            Assert.Equal(DateTimeOffset.MinValue, _bookDataDriver.BookSubject.PublishedOn);
        }

    }
}
