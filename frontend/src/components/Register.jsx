import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const Register = () => {
  const [fullName, setFullName] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [accNo, setAccNo] = useState('');
  const [message, setMessage] = useState('');
  const navigate = useNavigate();

  const handleRegister = async () => {

    if (!username || !phone) {
      setMessage('Please fill in both fields');
      return;
    }

    try {
      await axios.post('http://localhost:5000/users/register', { username, phone });

      
      navigate('/register-fingerprint', { state: { username, phone } });
    } catch (error) {
      setMessage('Registration failed.');
      console.error(error);
    }
  };

  return (
    <div>
      <h2>Customer Registration</h2>
      <input
        type="text"
        placeholder="Username"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />
      <input
        type="text"
        placeholder="Phone"
        value={phone}
        onChange={(e) => setPhone(e.target.value)}
      />
      <button onClick={handleRegister}>Register</button>
      {message && <p>{message}</p>}
    </div>
  );
};

export default Register;