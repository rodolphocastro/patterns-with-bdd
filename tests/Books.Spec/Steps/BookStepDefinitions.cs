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
        private BookFactory _factory;

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
            _bookDataDriver.Subject = _bookDataDriver.CreateUnpublished();
        }

        [Given(@"an unpublished book factory")]
        public void GivenAnUnpublishedBookFactory()
        {
            _factory = new UnpublishedBookFactory(_bookDataDriver.AuthorName, _bookDataDriver.BookTitle);
        }

        [Given(@"a published book factory")]
        public void GivenAPublishedBookFactory()
        {
            _factory = new PublishedBookFactory(
                _bookDataDriver.AuthorName,
                _bookDataDriver.BookTitle,
                _bookDataDriver.PublicationVersion,
                _bookDataDriver.PublicationDate);
        }

        [When(@"the book is created")]
        public void WhenTheBookIsCreated()
        {
            _bookDataDriver.Subject = _bookDataDriver.CreateUnpublished();
        }

        [When(@"the book is renamed to ""(.*)""")]
        public void WhenTheBookIsRenamedTo(string newTitle)
        {
            _bookDataDriver.Subject.RenameTo(newTitle);
        }

        [When(@"the factory creates a book")]
        public void WhenTheFactoryCreatesABook()
        {
            _bookDataDriver.Subject = _factory.Build();
        }

        [When(@"the book is published")]
        public void WhenTheBookIsPublished()
        {
            _bookDataDriver.Subject.Publish(_bookDataDriver.PublicationDate, true);
        }

        [When(@"the published date is set")]
        public void WhenThePublishedDateIsSet()
        {
            _bookDataDriver.Subject.Publish(_bookDataDriver.PublicationDate, false);
        }


        [Then(@"the book should not be null")]
        public void ThenTheBookShouldNotBeNull()
        {
            Assert.NotNull(_bookDataDriver.Subject);
        }

        [Then(@"the book's title should match")]
        public void ThenTheBookSTitleShouldMatch()
        {
            Assert.Equal(_bookDataDriver.BookTitle, _bookDataDriver.Subject.Title);
        }

        [Then(@"the book's title should not match")]
        public void ThenTheBookSTitleShouldNotMatch()
        {
            Assert.NotEqual(_bookDataDriver.BookTitle, _bookDataDriver.Subject.Title);
        }

        [Then(@"the book author's name should match")]
        public void ThenTheBookSNameShouldMatch()
        {
            Assert.Equal(_bookDataDriver.AuthorName, _bookDataDriver.Subject.AuthorName);
        }

        [Then(@"the book's published date should match")]
        public void ThenTheBookSPublishedDateShouldMatch()
        {
            Assert.Equal(_bookDataDriver.PublicationDate, _bookDataDriver.Subject.PublishedOn);
        }

        [Then(@"the book's revision should be (.*)")]
        public void ThenTheBookSRevisionShouldBe(int p0)
        {
            Assert.Equal(p0, _bookDataDriver.Subject.Version);
        }

        [Then(@"the book's published date should be null")]
        public void ThenTheBookSPublishedDateShouldBeNull()
        {
            Assert.Equal(DateTimeOffset.MinValue, _bookDataDriver.Subject.PublishedOn);
        }

    }
}
