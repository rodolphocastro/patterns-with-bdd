Feature: BookFactory
	A Factory is a "creational" design pattern that allows you to create an interface
	for creating objects in a superclass that can be tuned by subclasses in order
	to alter the type of objects created, insert additional logic, et cetera.
	You can read more about the Factory on [Refactoring Guru](https://refactoring.guru/design-patterns/factory-method).

Background:
	Given the author's name is "H.P. Lovecraft"
	And the book's title is "The Call of Cthulhu"
	And the publication date is "2020-04-10"
	And the publication version is 1

Scenario: Create an Unpublished Book
	Given an unpublished book factory
	When the factory creates a book
	Then the book should not be null
	And the book's title should match
	And the book author's name should match
	And the book's revision should be 0
	And the book's published date should be null

Scenario: Create a Published Book
	Given a published book factory
	When the factory creates a book
	Then the book should not be null
	And the book's title should match
	And the book author's name should match
	And the book's revision should be 1