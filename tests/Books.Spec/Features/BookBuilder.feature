Feature: BookBuilder
	A builder is a "creational" design pattern that allows you to slowly create
	an object step-by-step. On C# this is usually implemented with a "Fluent"
	interface, such as the one seem for LINQ's methods.
	You can read more about the Builder on [Refactoring Guru](https://refactoring.guru/design-patterns/builder)

Background: 
	Given A random author's name
	And A random book's title

Scenario: Build an unpublished book
	Given an unpublished book builder
	When the builder receives a Title
	And the builder receives an Author's name
	And the builder applies the values
	Then the book should not be null
	And the book's title should match
	And the book author's name should match

Scenario: Build a published book
	Given a published book builder
	And the publication date is "2020-04-10"
	And the publication version is 1
	When the builder receives a Title
	And the builder receives an Author's name
	And the builder receives a publication date
	And the builder receives a publication version
	And the builder applies the values
	Then the book should not be null
	And the book's title should match
	And the book author's name should match
	And the book's revision should be 1