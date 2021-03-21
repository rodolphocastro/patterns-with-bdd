Feature: Books
	A Book written by an Author and that might have been published.

Background:
	Given the author's name is "H.P. Lovecraft"
	And the book's title is "The Call of Cthulhu"

Scenario: Create an unpublished book
	When the book is created
	Then the book should not be null
	And the book's title should match
	And the book author's name should match
	And the book's revision should be 0

Scenario: Create a published book
	Given the publication date is "2020-04-10"
	When the book is created
	And the published date is set
	Then the book should not be null
	And the book's title should match
	And the book author's name should match
	And the book's published date should match
	And the book's revision should be 0

Scenario: Rename a book
	Given the book is created
	When the book is renamed to "At the Mountains of Madness"
	Then the book should not be null
	And the book's title should not match
	And the book author's name should match

Scenario: Publish a book
	Given the book is created
	And the publication date is "2020-04-10"
	When the book is published
	Then the book should not be null
	And the book's published date should match
	And the book's title should match
	And the book author's name should match
	And the book's revision should be 1

Scenario: Republish a book
	Given the book is created
	And the publication date is "2020-04-10"
	When the book is published
	And the book is published
	Then the book should not be null
	And the book's title should match
	And the book author's name should match
	And the book's revision should be 2