import React, { useState } from 'react';
import Login from './components/Login';
import Register from './components/Register';
import { getFingerprint } from './services/fingerprintService';

const App = () => {
  const [isLogin, setIsLogin] = useState(true); // true: show login, false: show register
  const [isAuthenticated, setIsAuthenticated] = useState(false); // track login status
  const [email, setEmail] = useState(''); // To store the logged-in user's email
  const [password, setPassword] = useState('logout'); // for logout purpose

  const toggleForm = () => {
    setIsLogin((prev) => !prev); // Toggle between login and register
  };

  const handleLogout = async () => {
    try {
      // Get the fingerprint and use the user's email
      const fingerprint = await getFingerprint();
      
      const response = await fetch('https://localhost:7093/api/auth/logout', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email,
          password,
          fingerprint, // Send fingerprint
        }),
      });

      if (!response.ok) {
        throw new Error('Logout failed');
      }

      // Reset states after successful logout
      setIsAuthenticated(false);
      setEmail(''); // Clear the email field
      setIsLogin(true); // Reset to show login form
    } catch (error) {
      console.error('Logout error:', error);
      alert('Logout failed. Please try again.');
    }
  };

  return (
    <div>
      {isAuthenticated ? (
        <div>
          <p>You are logged in as {email}</p>
          <button onClick={handleLogout}>Logout</button>
        </div>
      ) : (
        <>
          <h1>{isLogin ? 'Login' : 'Register'}</h1>
          {isLogin ? (
            <Login
              setIsAuthenticated={setIsAuthenticated}
              setEmail={setEmail} // Set the user's email when logged in
            />
          ) : (
            <Register />
          )}
          <div style={{ marginTop: '20px' }}>
            <button onClick={toggleForm}>
              {isLogin
                ? 'Need an account? Register'
                : 'Already have an account? Login'}
            </button>
          </div>
        </>
      )}
    </div>
  );
};

export default App;
