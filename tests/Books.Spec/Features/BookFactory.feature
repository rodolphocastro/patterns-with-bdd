Feature: BookFactory
	A Factory is a "creational" design pattern that allows you to create an interface
	for creating objects in a superclass that can be tuned by subclasses in order
	to alter the type of objects created, insert additional logic, et cetera.
	You can read more about the Factory on [Refactoring Guru](https://refactoring.guru/design-patterns/factory-method).

Background:
	Given the author's name is "H.P. Lovecraft"
	And the book's title is "The Call of Cthulhu"

Scenario: Factorize a book with the default Factory
	When the vanilla factory creates a book
	Then the book should not be null
	And the book's title should match
	And the book author's name should match