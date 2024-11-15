Feature: User Login
  Verify that a user can log in with valid credentials

  @Smoke
  @Priority-P1
  Scenario: User logs in with valid email and password
    Given I open the home page
    And I click the Login button
    Then I should be on the login page
    And I should see the login options
    When I log in with valid credentials
    Then I should be successfully logged in