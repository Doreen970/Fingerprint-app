import { useState } from 'react';
import PropTypes from 'prop-types';
import { useNavigate } from 'react-router-dom';

const ResetPassword = ({ token }) => {
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Validate the password inputs
    if (!password) {
      setError('Please enter your new password');
      return;
    }

    if (password !== confirmPassword) {
      setError('Passwords do not match');
      return;
    }

    try {
      // Send the password reset request to the server
      const response = await fetch('#', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ token, password }), // Include the token in the request payload
      });

      if (response.ok) {
        setSuccess(true);
        setError(''); // Clear any previous errors
      } else {
        const data = await response.json();
        setError(data.message || 'An error occurred');
      }
    } catch (error) {
      console.error('Error resetting password:', error);
      setError('An error occurred while resetting your password');
    }
  };

  if (success) {
    // Redirect to the login page after a successful password reset
    navigate('/login');
    return null;
  }

  return (
    <div>
      <h2>Reset Password</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="password">New Password:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        <div>
          <label htmlFor="confirmPassword">Confirm Password:</label>
          <input
            type="password"
            id="confirmPassword"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />
        </div>
        <button type="submit">Reset Password</button>
        {error && <p className="error">{error}</p>}
      </form>
    </div>
  );
};

ResetPassword.propTypes = {
  token: PropTypes.string.isRequired, 
};

export default ResetPassword;
