Feature: Books

	A Book written by an Author and that might have been published.

	Background: 
		Given the author's name is "H.P. Lovecraft"
		And the book's title is "The Call of Cthulhu"

	Scenario: Create a book
		When the book is created
		Then the book should not be null 
		And the book's title should match
		And the book author's name should match

	Scenario: Rename a book
		Given the book is created
		When the book is renamed to "At the Mountains of Madness"
		Then the book should not be null
		And the book's title should not match
		And the book author's name should match

	Scenario: Factorize a book
		When the factory creates a book
		Then the book should not be null
		And the book's title should match
		And the book author's name should match