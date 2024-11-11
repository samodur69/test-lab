# Explanation of Client and Refresh Tokens

A brief description of Access Tokens and Credentials.

## How to get Credentials?

1. Sign up on Spotify API, and create application.
   You will be provided with Client ID and Client Secret.
 
2. Follow official tutorial (https://developer.spotify.com/documentation/web-api/tutorials/code-flow)
   Add the scopes from the table below. They are changed depending on the tests executed
   You will be provided with Access Token and Refresh Token. We will use Refresh Token.
   P.S. You can make API requests though Online Platforms (e.g. reqbin.com)

3. Put Client ID,Client Secret and Refresh Token into environment variables.

## Scopes
user-read-email
user-read-private
playlist-modify-public
playlist-modify-private
playlist-read-private
playlist-read-collaborative
ugc-image-upload

## Usage

Refresh Token and Client Credentials are essential for the work API tests.
They are used to gain access