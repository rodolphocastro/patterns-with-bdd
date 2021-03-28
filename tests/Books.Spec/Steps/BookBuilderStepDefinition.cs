
using Books.Spec.Drivers;

using System;

using TechTalk.SpecFlow;

namespace Books.Spec.Steps
{
    [Binding]
    public sealed class BookBuilderStepDefinition
    {
        private readonly BookBuilderDriver _builderDriver;

        public BookBuilderStepDefinition(BookBuilderDriver builderDriver)
        {
            _builderDriver = builderDriver ?? throw new ArgumentNullException(nameof(builderDriver));
        }

        [Given(@"an unpublished book builder")]
        public void GivenAnUnpublishedBookBuilder()
        {
            _builderDriver.BuilderSubject = _builderDriver.UnpublishedBuilder;
        }

        [Given(@"a published book builder")]
        public void GivenAPublishedBookBuilder()
        {
            _builderDriver.BuilderSubject = _builderDriver.PublishedBuilder;
        }

        [When(@"the builder receives a Title")]
        public void WhenTheBuilderReceivesATitle()
        {
            _builderDriver.ApplyTitle();
        }

        [When(@"the builder receives an Author's name")]
        public void WhenTheBuilderReceivesAnAuthorSName()
        {
            _builderDriver.ApplyAuthor();
        }

        [When(@"the builder applies the values")]
        public void WhenTheBuilderAppliesTheValues()
        {
            _builderDriver.BuilderResult = _builderDriver.ApplyBuild();
        }

        [When(@"the builder receives a publication date")]
        public void WhenTheBuilderReceivesAPublicationDate()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"the builder receives a publication version")]
        public void WhenTheBuilderReceivesAPublicationVersion()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
