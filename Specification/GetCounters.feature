Feature: GetCounters
       In order to browse Counters
       As an authenticated user
       I want to retrieve counter data using the CountersService

Scenario: GetCounters
       Given an access token
       When I request counters
       Then I should receive counter data