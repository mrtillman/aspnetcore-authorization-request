Feature: GetCounters
	In order to browse counters
	As an authenticated user
	I want to retrieve counter data using the CounterService

Scenario: GetCounters
	Given an access token
	When I request counters 
	Then I should receive counter data