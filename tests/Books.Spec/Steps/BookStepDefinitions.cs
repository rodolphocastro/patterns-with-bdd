using Books.Spec.Drivers;

using FluentAssertions;

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

        [Given(@"A random author's name")]
        public void GivenARandomAuthorsName()
        {
            _bookDataDriver.AuthorName = _bookDataDriver.CreateRandomAuthor();
        }

        [Given(@"A random book's title")]
        public void GivenARandomTitle()
        {
            _bookDataDriver.BookTitle = _bookDataDriver.CreateRandomTitle();
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
            _bookDataDriver.BookSubject.Title
                .Should()
                .NotBeNullOrEmpty("a book cannot have an empty title").And
                .BeEquivalentTo(_bookDataDriver.BookTitle);
        }

        [Then(@"the book's title should not match")]
        public void ThenTheBookSTitleShouldNotMatch()
        {
            _bookDataDriver.BookSubject.Title
                .Should()
                .NotBeNullOrEmpty("a book cannot have an empty title").And
                .NotBeEquivalentTo(_bookDataDriver.BookTitle);
        }

        [Then(@"the book author's name should match")]
        public void ThenTheBookSNameShouldMatch()
        {
            _bookDataDriver.BookSubject.AuthorName
                .Should()
                .NotBeNullOrEmpty("a book cannot have an empty author").And
                .BeEquivalentTo(_bookDataDriver.AuthorName);
        }

        [Then(@"the book's published date should match")]
        public void ThenTheBookSPublishedDateShouldMatch()
        {
            _bookDataDriver.BookSubject.PublishedOn
                .Should()
                .Be(_bookDataDriver.PublicationDate);
        }

        [Then(@"the book's revision should be (.*)")]
        public void ThenTheBookSRevisionShouldBe(int expectedVersion)
        {
            _bookDataDriver.BookSubject.Version
                .Should()
                .Be(expectedVersion);
        }

        [Then(@"the book's published date should be null")]
        public void ThenTheBookSPublishedDateShouldBeNull()
        {
            _bookDataDriver.BookSubject.PublishedOn
                .Should()
                .Be(DateTimeOffset.MinValue);
        }

    }
}
