Feature: EPAM Insights Carousel Validation
  As a user
  I want to verify that the carousel article title matches the article page title
  So that I can ensure content consistency

  Scenario: Validate carousel article title
    Given I am on the EPAM homepage
    When I navigate to the Insights page
    And I swipe the carousel twice
    And I retrieve the carousel article title
    And I click the Read More link
    Then the article page title should match the carousel titleFeature: EPAM File Download
                                                                 As a user
                                                                 I want to download a file from the About page
                                                                 So that I can access the EPAM company overview document

                                                                 Scenario: Validate file download from About page
                                                                   Given I am on the EPAM homepage
                                                                   When I navigate to the About page
                                                                   And I scroll to the EPAM at a Glance section
                                                                   And I click the download button
                                                                   Then the file "EPAM_Systems_Company_Overview.pdf" should be downloaded