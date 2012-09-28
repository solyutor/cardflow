Feature: Board Management
	In order to manage boards 
	I want to able to create new board. 

@Management
Scenario: Create new board
	
	Given I created new board named CardFlow
	And I set following parameters
		| stepname      | order | capacity |
		| Pending       | 1     | 3        |
		| In Proggress  | 2     | 5        |
		| Ready To Ship | 3     | 6        |
	When I save changes	
	Then I should receive board created event
