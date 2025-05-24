Feature: EPAM Website Search
  As a user
  I want to search for specific terms on the EPAM website
  So that I can find relevant information

  Scenario Outline: Perform search with valid terms
    Given I am on the EPAM homepage
    When I open the search panel
    And I search for "<searchTerm>"
    Then I should see search results

    Examples:
      | searchTerm        |
      | AI                |
      | Machine Learning  |