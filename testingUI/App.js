import { useState } from 'react';
import './App.css';

function App() {
    const [message, setMessage] = useState('');

    const handleJwtLogin = () => {
        console.log('Token set in local storage:', localStorage.getItem('token'));
        const token = localStorage.getItem('token');
        console.log('Token set:', token);
        if (!token) {
            setMessage('Please log in first.');
            return;
        }

        fetch('https://localhost:7166/api/Auth/loginJwt', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Username: 'admin',
                Password: '1234'
            })
        })
            .then(response => {
                if (response.ok) {
                    response.json().then(data => {
                        const jwtToken = data.Token;
                        localStorage.setItem('token', jwtToken); // set the token in local storage
                        fetch('https://localhost:7166/api/Auth/protected', { // make request to protected endpoint
                            method: 'GET',
                            headers: {
                                'Authorization': `Bearer ${jwtToken}`
                            }
                        })
                            .then(response => {
                                if (response.ok) {
                                    response.json().then(data => {
                                        setMessage(data.Message);
                                        console.log('worked');
                                    });
                                } else {
                                    setMessage('Access denied');
                                    console.log('Access denied');
                                }
                            })
                            .catch(error => {
                                setMessage('Network error');
                                console.log('Network error');
                            });
                    });
                } else {
                    setMessage('Login failed');
                    console.log('Login failed');
                }
            })
            .catch(error => {
                setMessage('Network error');
                console.log('Network error');
            });
    }

    const handleLogout = () => {
        localStorage.removeItem('token');
        setMessage('Logged out successfully.');
    }

    return (
        <div className="App">
            <header className="App-header">
                {message ? <p>{message}</p> : <p>Click the login button to try logging in.</p>}
                <button onClick={handleJwtLogin}>JWT Login</button>
                <button onClick={handleLogout}>Logout</button>
            </header>
        </div>
    );
}

export default App;