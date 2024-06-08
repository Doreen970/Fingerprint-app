import { useState } from 'react';
import axios from 'axios';

const FingerprintRegister = () => {
  const [message, setMessage] = useState('');
  const [isFingerprintCaptured, setIsFingerprintCaptured] = useState(false);
  const [fingerprintData, setFingerprintData] = useState(null);

  const handleFingerprintScan = async () => {
    setMessage('Please put your finger on the scanner...');
    setIsFingerprintCaptured(false);

    try {
      // Call the SDK method to start the fingerprint scan
      const result = await startFingerprintScan();

      if (result.success) {
        setFingerprintData(result.data);
        setMessage('Fingerprint captured successfully!');
        setIsFingerprintCaptured(true);
      } else {
        setMessage('Error capturing fingerprint, please try again.');
      }
    } catch (error) {
      console.error('Error during fingerprint scan:', error);
      setMessage('Error capturing fingerprint, please try again.');
    }
  };

  const handleSaveFingerprint = async () => {
    if (!isFingerprintCaptured) return;

    try {
      const response = await axios.post('http://localhost:5000/save-fingerprint', { fingerprintData });

      if (response.data.status === 'ok') {
        setMessage('Fingerprint registration complete!');
      } else {
        setMessage('Error saving fingerprint, please try again.');
      }
    } catch (error) {
      console.error('Error saving fingerprint:', error);
      setMessage('Error saving fingerprint, please try again.');
    }
  };

  // Dummy function to simulate fingerprint scanning process
  const startFingerprintScan = () => {
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve({ success: true, data: 'dummy-fingerprint-data' });
      }, 2000); // Simulate a 2-second fingerprint scan
    });
  };

  return (
    <div>
      <h2 className="text-3xl text-center mt-6 font-bold">Register Fingerprint</h2>
      <button className="w-[40%] bg-blue-600 text-white px-7 py-3 text-sm font-medium uppercase rounded shadow-md hover:bg-blue-700 transition duration-150 ease-in-out hover:shadow-lg active:bg-blue-800" onClick={handleFingerprintScan}>Scan Fingerprint</button>
      {message && <p>{message}</p>}
      <button className="w-[40%] bg-blue-600 text-white px-7 py-3 text-sm font-medium uppercase rounded shadow-md hover:bg-blue-700 transition duration-150 ease-in-out hover:shadow-lg active:bg-blue-800" onClick={handleSaveFingerprint} disabled={!isFingerprintCaptured}>
        Finish
      </button>
    </div>
  );
};

export default FingerprintRegister;
