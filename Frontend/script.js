document.addEventListener('DOMContentLoaded', () => {

    const loginBtnHomePage = document.getElementById('loginBtnHomePage');
    const signUpBtnHomePage = document.getElementById('signUpBtnHomePage');

    if (loginBtnHomePage) {
        loginBtnHomePage.addEventListener('click', () => {
            window.location.href = 'Login.html';
        });
    }

    if (signUpBtnHomePage) {
        signUpBtnHomePage.addEventListener('click', () => {
            window.location.href = 'register.html';
        });
    }


    const loginForm = document.getElementById('loginForm');
    const usernameInput = document.getElementById('usernameInput');
    const passwordInput = document.getElementById('passwordInput');
    const errorContainer = document.getElementById('errorContainer');

    const clearErrorMessages = () => {
        if (errorContainer) {
            errorContainer.innerHTML = "";
        }
    };

    const showError = (message) => {
        clearErrorMessages();
        if (errorContainer) {
            const errorDiv = document.createElement('div');
            errorDiv.className = 'errorMessage';
            errorDiv.innerText = message;
            errorContainer.appendChild(errorDiv);
        }
    };

    if (loginForm) {
        loginForm.addEventListener('submit', async (event) => {
            event.preventDefault();
            clearErrorMessages();

            const username = usernameInput.value.trim();
            const password = passwordInput.value.trim();


            if (!username || username.length < 2 || username.length > 15) {
                showError("Invalid Username. Must be between 2 and 15 characters.");
                return;
            }
            if (!password || password.length < 2 || password.length > 16) {
                showError("Invalid Password. Must be between 2 and 16 characters.");
                return;
            }

            try {

                const response = await fetch('http://localhost:8080/api/auth/Login', {
                    method: 'POST',
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ username, password })
                });

                const data = await response.json();

                if (response.ok) {
                    window.location.href = `home.html?username=${username}`;
                } else {
                    showError(data.message || "Login failed. Please try again.");
                }
            } catch (error) {
                showError("Error: Unable to connect to the server.");
                console.error(error);
            }
        });
    }


    const registerForm = document.getElementById('registerForm');
    const regUsernameInput = document.getElementById('regUsernameInput');
    const regPasswordInput = document.getElementById('regPasswordInput');

    if (registerForm) {
        registerForm.addEventListener('submit', async (event) => {
            event.preventDefault();
            clearErrorMessages();

            const username = regUsernameInput.value.trim();
            const password = regPasswordInput.value.trim();

            if (!username || username.length < 2 || username.length > 15) {
                showError("Invalid Username. Must be between 2 and 15 characters.");
                return;
            }
            if (!password || password.length < 2 || password.length > 16) {
                showError("Invalid Password. Must be between 2 and 16 characters.");
                return;
            }

            try {

                const response = await fetch('http://localhost:8080/api/Auth/register', {
                    method: 'POST',
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ username, password })
                });

                const data = await response.json();

                if (response.ok) {

                    window.location.href = 'login.html';
                } else {
                    showError(data.message || "Registration failed. Please try again.");
                }
            } catch (error) {
                showError("Error: Unable to connect to the server.");
                console.error(error);
            }
        });
    }


    const params = new URLSearchParams(window.location.search);
    const username = params.get('username');
    const welcomeDiv =document.getElementById('welcomeMessage');
    welcomeDiv.textContent += `${username}`;

});


