import { useState } from 'react';

const ResetRequest = () => {
  const [email, setEmail] = useState('');
  const [error, setError] = useState('');
  const [successMessage, setSuccessMessage] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    // Validate email format
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      setError('Invalid email format');
      return;
    }

    // Logic to send reset password
    try {
      // Code to send reset password email (API call, etc.)
      // POST request to endpoint
      
      // timeout
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      
      setEmail('');
      setSuccessMessage('Reset password instructions sent to your email');
    } catch (error) {
      // Handle error
      setError('Failed to reset password. Please try again later.');
    }
  };

  return (
    <div>
      <h2>Reset Password</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="email">Email address:</label>
          <input
            type="email"
            id="email"
            value={email}
            placeholder='wrire your email'
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <button type="submit">Reset Password</button>
        {error && <p style={{ color: 'red' }}>{error}</p>}
        {successMessage && <p style={{ color: 'green' }}>{successMessage}</p>}
      </form>
    </div>
  );
};

export default ResetRequest;