Feature: EPAM Website Services Navigation
  As a user
  I want to navigate to a specific service category from the Services menu
  So that I can view details about that service

  Scenario Outline: Validate navigation to specific service category
    Given I am on the EPAM homepage
    When I navigate to the Services menu
    And I select the "<serviceCategory>" category
    Then the page should contain the title "<expectedTitle>"
    And the "Our Related Expertise" section should be displayed

    Examples:
      | serviceCategory  | expectedTitle                     |
      | Generative AI    | Generative AI Services at EPAM    |
      | Responsible AI   | Responsible AI Services at EPAM   |