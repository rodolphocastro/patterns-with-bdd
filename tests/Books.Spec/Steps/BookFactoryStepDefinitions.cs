
using Books.Spec.Drivers;

using System;

using TechTalk.SpecFlow;

namespace Books.Spec.Steps
{
    [Binding]
    public sealed class BookFactoryStepDefinitions
    {
        private readonly BookFactoryDriver _factoryDriver;

        public BookFactoryStepDefinitions(BookFactoryDriver factoryDriver)
        {
            _factoryDriver = factoryDriver ?? throw new ArgumentNullException(nameof(factoryDriver));
        }

        [Given(@"an unpublished book factory")]
        public void GivenAnUnpublishedBookFactory()
        {
            _factoryDriver.FactorySubject = _factoryDriver.CreateUnpublishedFactory();
        }

        [Given(@"a published book factory")]
        public void GivenAPublishedBookFactory()
        {
            _factoryDriver.FactorySubject = _factoryDriver.CreatePublishedFactory();
        }

        [When(@"the factory creates a book")]
        public void WhenTheFactoryCreatesABook()
        {
            _factoryDriver.FactoryResult = _factoryDriver.FactorySubject.Build();
        }
    }
}
