import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const Register = () => {
    const [message, setMessage] = useState('');
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        fullName: "",
        phoneNumber: "",
        accNo: "",
        email: "",
        gender: "",
        idNo: "",
        dob: "",
    });

    const { fullName, phoneNumber, accNo, email, gender, idNo, dob } = formData;

    function onChange(e) {
        setFormData((prevState) => ({
            ...prevState,
            [e.target.id]: e.target.value,
        })
        );
    }

    const onSubmit = async () => {
        try {
            await axios.post('http://localhost:5000/users/register', formData);


            navigate('/fingerprint-register', { state: formData });
        } catch (error) {
            setMessage('Registration failed.');
            console.error(error);
        }
    };

    return (
        <div className="flex justify-center flex-wrap items-center px-6 py-12 max-w-6xl mx-auto">
            <div className="w-full md:w-[67%] lg:w-[40%] lg:ml-20">
                <form onSubmit={onSubmit}>
                    <h1 className="text-3xl text-center mt-6 font-bold">Customer Registration</h1>

                    <div className="flex">
                        <label htmlFor="fullName" className='mr-2 '>Full Name</label>
                        <input
                            type="text"
                            placeholder="Enter full name"
                            value={fullName}
                            onChange={onChange}
                            required
                            className="mb-6 w-full px-4 py-2 text-xl text-gray-700 bg-white border-gray-300 rounded transition ease-in-out"
                        />
                    </div>

                    <div className="flex">
                        <label htmlFor="phoneNumber" className='mr-2'>Phone Number</label>
                        <input
                            type="text"
                            placeholder="Enter phone number"
                            value={phoneNumber}
                            onChange={onChange}
                            required
                            className="mb-6 w-full px-4 py-2 text-xl text-gray-700 bg-white border-gray-300 rounded transition ease-in-out"
                        />
                    </div>

                    <div className="flex">
                        <label htmlFor="accNo" className='mr-2'>Account Number</label>
                        <input
                            type="text"
                            placeholder="Account Number"
                            value={accNo}
                            onChange={onChange}
                            required
                            className="mb-6 w-full px-4 py-2 text-xl text-gray-700 bg-white border-gray-300 rounded transition ease-in-out"
                        />
                    </div>

                    <div className="flex">
                        <label htmlFor="idNo" className='mr-2'>Id Number</label>
                        <input
                            type="text"
                            placeholder="ID Number"
                            value={idNo}
                            onChange={onChange}
                            required
                            className="mb-6 w-full px-4 py-2 text-xl text-gray-700 bg-white border-gray-300 rounded transition ease-in-out"
                        />
                    </div>

                    <div className="flex">
                        <label htmlFor="dob" className='mr-2'>Date of Birth</label>
                        <input
                            type="text"
                            placeholder="Date of Birth"
                            value={dob}
                            onChange={onChange}
                            required
                            className="mb-6 w-full px-4 py-2 text-xl text-gray-700 bg-white border-gray-300 rounded transition ease-in-out"
                        />
                    </div>

                    <div className="flex">
                        <label htmlFor="email" className='mr-2'>Email Address</label>
                        <input
                            type="email"
                            placeholder="Email Address"
                            value={email}
                            onChange={onChange}
                            required
                            className="mb-6 w-full px-4 py-2 text-xl text-gray-700 bg-white border-gray-300 rounded transition ease-in-out"
                        />
                    </div>

                    <div className="flex">
                        <label htmlFor="gender" className='mr-2'>Gender</label>
                        <input
                            type="text"
                            placeholder="Gender"
                            value={gender}
                            onChange={onChange}
                            required
                            className="mb-6 w-full px-4 py-2 text-xl text-gray-700 bg-white border-gray-300 rounded transition ease-in-out"
                        />
                    </div>

                    <button className="w-full bg-blue-600 text-white px-7 py-3 text-sm font-medium uppercase rounded shadow-md hover:bg-blue-700 transition duration-150 ease-in-out hover:shadow-lg active:bg-blue-800">Register</button>
                    {message && <p>{message}</p>}
                </form>
            </div>
        </div>
    );
};

export default Register;