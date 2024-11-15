
# Spotify Login Module Test Cases

## Test Case 1: Login with Valid Email and Password
**Priority**: P1  
### Input Data:
   - **Email**: 
      - Should be already registered;
      - Latin-only;
      - Ending with @somedomain.name;
      - No special characters;
      - Min 5 characters;
      - Max 128 characters;
   - **Password**:
      - Contains special characters, such as ($!@#*?);
      - Contains no spaces ( );
      - Min 8 characters;
      - Max 128 characters;
### Steps:
1. Navigate to [Spotify's login page](https://www.spotify.com/login).
   - "Log in to Spotify" label should be displayed.
   - "Email or username", "Password" fields and "Log in" button should be present and enabled.
   - "Continue with Google/Facebook/Apple" login buttons should be present and enabled.
2. Enter a valid email address in the "Email or Username" field.
   - The entered text is displayed in the field, and the placeholder disappears.
3. Enter a valid password in the "Password" field.
   - The entered text is displayed in the field, and the placeholder disappears.
4. Click the "Log in" button.
   - The "Log in" button becomes disabled.
   - An authentication request is sent with the provided email and password.
   - A loading indicator may be shown while the request is processed.

### Expected Final Results:
- The user is successfully authenticated.
- The user is redirected to the Spotify homepage, where the user’s name is displayed in the account menu.

---

## Test Case 2: Login with Valid Username and Password
**Priority**: P1  
### Input Data:
   - **Username**: 
      - Latin-only;
      - No special characters;
      - No spaces;
      - Min 5 characters;
      - Max 128 characters;
   - **Password**:
      - Contains special characters, such as ($!@#*?);
      - Contains no spaces ( );
      - Min 8 characters;
      - Max 128 characters;
### Steps:
1. Navigate to [Spotify's login page](https://www.spotify.com/login).
   - "Log in to Spotify" label should be displayed.
   - "Email or username", "Password" fields and "Log in" button should be present and enabled.
   - "Continue with Google/Facebook/Apple" login buttons should be present and enabled.
2. Enter a valid username in the "Email or Username" field.
   - The entered text is displayed in the field, and the placeholder disappears.
3. Enter a valid password in the "Password" field.
   - The entered text is displayed in the field, and the placeholder disappears.
4. Click the "Log in" button.
   - The "Log in" button becomes disabled.
   - Authentication request is sent with the provided username and password.
   - A loading indicator may be shown while the request is processed.

### Expected Final Results:
- The user is successfully authenticated.
- The user is redirected to the Spotify homepage, where the user’s name is displayed in the account menu.

---

## Test Case 3: Login with Valid Email or Username but Wrong Password
**Priority**: P2  
### Input Data:
   - **Username**: 
      - Latin-only;
      - No special characters;
      - No spaces;
      - Min 5 characters;
      - Max 128 characters;
   - **Email**: 
      - Latin-only;
      - Ending with @somedomain.name;
      - No special characters;
      - Min 5 characters;
      - Max 128 characters;
   - **Password**:
      - Contains special characters, such as ($!@#*?);
      - Contains no spaces ( );
      - Min 8 characters;
      - Max 128 characters;
### Steps:
1. Navigate to [Spotify's login page](https://www.spotify.com/login).
   - "Log in to Spotify" label should be displayed.
   - "Email or username", "Password" fields and "Log in" button should be present and enabled.
   - "Continue with Google/Facebook/Apple" login buttons should be present and enabled.
2. Enter a valid email address or username in the "Email or Username" field.
   - The entered text is displayed in the field, and the placeholder disappears.
3. Enter an invalid password in the "Password" field.
   - The entered text is displayed in the field, and the placeholder disappears.
4. Click the "Log in" button.
   - The "Log in" button becomes disabled.
   - An authentication request is sent with the provided email or username and invalid password.
   - A loading indicator may be shown while the request is processed.

### Expected Final Results:
- An error message is displayed: "Incorrect username or password."
- The "Password" and "Username" fields are not cleared.
- The "Log in" button becomes enabled again.

---

## Test Case 4: Login with Wrong Email or Username
**Priority**: P2  
### Input Data:
   - **Username**: 
      - Should be **wrong**;
      - Latin-only;
      - No special characters;
      - No spaces;
      - Min 5 characters;
      - Max 128 characters;
   - **Email**: 
      - Should be **wrong**;
      - Latin-only;
      - Ending with @somedomain.name;
      - No special characters;
      - Min 5 characters;
      - Max 128 characters;
   - **Password**:
      - Contains special characters, such as ($!@#*?);
      - Contains no spaces ( );
      - Min 8 characters;
      - Max 128 characters;
### Steps:
1. Navigate to [Spotify's login page](https://www.spotify.com/login).
   - "Log in to Spotify" label should be displayed.
   - "Email or username", "Password" fields and "Log in" button should be present and enabled.
   - "Continue with Google/Facebook/Apple" login buttons should be present and enabled.
2. Enter an invalid email address or username in the "Email or Username" field.
   - The entered text is displayed in the field, and the placeholder disappears.
3. Enter any password in the "Password" field.
   - The entered text is displayed in the field, and the placeholder disappears.
4. Click the "Log in" button.
   - The "Log in" button becomes disabled.
   - An authentication request is sent with the provided invalid email or username and password.
   - A loading indicator may be shown while the request is processed.

### Expected Final Results:
- An error message is displayed: "Incorrect username or password."
- The "Password" and "Username" fields are not cleared.
- The "Log in" button becomes enabled again.

---

## Test Case 5: Error Message under Empty Email Field
**Priority**: P3  
### Input Data:
   - **Username**: 
      - Latin-only;
      - No special characters;
      - No spaces;
      - Min 5 characters;
      - Max 128 characters;
   - **Email**: 
      - Latin-only;
      - Ending with @somedomain.name;
      - No special characters;
      - Min 5 characters;
      - Max 128 characters;

### Steps:
1. Navigate to [Spotify's login page](https://www.spotify.com/login).
   - "Log in to Spotify" label should be displayed.
   - "Email or username", "Password" fields and "Log in" button should be present and enabled.
   - "Continue with Google/Facebook/Apple" login buttons should be present and enabled.
2. Enter any valid email address or username in the "Email or Username" field.
   - The entered text is displayed in the field, and the placeholder disappears.
3. Delete the entered email address or username, leaving the field empty.
   - The field is now empty, and the placeholder text "Enter your email or username" reappears.

### Expected Final Results:
- An error message is displayed under the email field: "Please enter your Spotify username or email address."

---

## Test Case 6: Error Message under Empty Password Field
**Priority**: P3  
### Input Data:
   - **Password**:
      - Contains special characters, such as ($!@#*?);
      - Contains no spaces ( );
      - Min 8 characters;
      - Max 128 characters;
### Steps:
1. Navigate to [Spotify's login page](https://www.spotify.com/login).
   - "Log in to Spotify" label should be displayed.
   - "Email or username", "Password" fields and "Log in" button should be present and enabled.
   - "Continue with Google/Facebook/Apple" login buttons should be present and enabled.
2. Enter any password in the "Password" field.
   - The entered text is displayed in the field, and the placeholder disappears.
3. Delete the entered password, leaving the field empty.
   - The field is now empty, and the placeholder text "Enter your password" reappears.

### Expected Final Results:
- An error message is displayed under the password field: "Please enter your password."