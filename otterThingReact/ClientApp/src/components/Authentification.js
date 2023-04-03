import React, { Component } from 'react';
import cookies from './Login';

export class Login extends Component {
    static displayName = Counter.name;

    constructor(props) {
        super(props);
        this.state = { currentCount: 0 };
        this.incrementCounter = this.incrementCounter.bind(this);
    }

    saveCookieInSession() {
        var d = new Date();
        d.setTime(d.getTime() + (1 * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = "myCookie=123; " + expires;
    }


     requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + cookies.get('W')onderfulCookie
        },
        body: JSON.stringify({ username: 'user', password: 'password' })
    };

    fetch('https://localhost:44432/api/auth/login', requestOptions)
    .then(response => response.json())
    .then(data => console.log(data))
    .catch(error => console.error(error));

    render() {
        return (
            <div>
                <h1>Login</h1>

                <p>Log into you acc</p>

                <button className="btn btn-primary" onClick={ saveCookieInSession}>Login</button>

                <button className="btn btn-primary" onClick={this.incrementCounter}>Increment</button>
            </div>
        );
    }
}
