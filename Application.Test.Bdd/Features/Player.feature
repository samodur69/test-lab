Feature: Player Functionality
  Verify that a user can play a track after logging in

  @Smoke
  @Priority-P1
  Scenario: User logs in and plays a track from the library
    Given I open the home page
    And I click the Login button
    Then I should be on the login page
    And I should see the login options
    When I log in with valid credentials
    Then I should be successfully logged in
    And I closed the cookies
    And I have opened my library
    And I select the first track
    When I wait for the player to be ready
    And I play the track
    Then the player should start playing the track
    And the "Now Playing" control should display the selected track
    And the timeline should move forward after a few seconds